using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Tank2D_XNA.Screens;
using Tank2D_XNA.Tanks;
using Tank2D_XNA.Utills;

namespace Tank2D_XNA.GameField
{

    public delegate void ExitGame();

    [SuppressMessage("ReSharper", "LoopCanBeConvertedToQuery")]
    [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]
    [SuppressMessage("ReSharper", "LoopCanBePartlyConvertedToQuery")]
    [SuppressMessage("ReSharper", "SuggestVarOrType_BuiltInTypes")]
    class BattleField
    {
        private Cursor _mouseCursor;
        private static BattleField _instance;
        private List<Block> _blocks;
        private List<AI> _tanks;
        private Player _player;
        private Gui _gui;
        private bool _isMenu;
        private KeyboardState _prevKeyboard;
        private Menu _menu;
        private FieldGrid _grid;

        public ExitGame Exit { set; private get; }

        public static BattleField GetInstance()
        {
            return _instance ?? (_instance = new BattleField());
        }

        private BattleField() { }

        public void Initialize()
        {
            _mouseCursor = Cursor.GetCursor();
            if (_blocks == null) _blocks = new List<Block>();
            else _blocks.Clear();
            if (_tanks == null ) _tanks = new List<AI>();
            else _tanks.Clear();
            _grid = new FieldGrid(Helper.SCREEN_WIDTH, Helper.SCREEN_HEIGHT, Helper.GRID_CELL_SIZE);

            _menu = new Menu(Helper.SCREEN_WIDTH, Helper.SCREEN_HEIGHT);
            _isMenu = false;

            LoadGame();
        }

        public void LoadGame()
        {
            MapReader reader = new MapReader(Helper.MAP_PATH);
            reader.Map(info =>
            {
                switch (info.Type)
                {
                    case "Block":
                        AddBlock(info.Position);
                        break;
                    case "AI":
                        AddBot(TankFactory.GetInstance().CreateTank(info.TankType, info.Position), true);
                        break;
                    case "Player":
                        Tank playerTank = TankFactory.GetInstance().CreateTank(info.TankType, info.Position);
                        if (playerTank != null)
                        {
                            Cursor.GetCursor().Fire = playerTank.Fire;
                            _player = new Player(playerTank);
                            _gui = new Gui(playerTank);
                        }
                        else SafeExit();
                        break;
                }
            });
        }

        public void AddBot(Tank tank, bool t)
        {
            if (tank != null)
                _tanks.Add(new AI(tank, _player.Tank.TankPosition, t));
        }

        public void AddBlock(Vector2 pos)
        {
            _blocks.Add(new Block(pos));
        }

        public Entity Intersects(Rectangle rect)
        {
            foreach (Block block in _blocks)
                if (rect.Intersects(block.GetMeshRect))
                    return block;
                    

            foreach (AI ai in _tanks)
                if (rect.Intersects(ai.Tank.GetMeshRect))
                    return ai.Tank;

            return rect.Intersects(_player.Tank.GetMeshRect) ? _player.Tank : null;
        }

        private Vector2 _startPoint;
        private Vector2 _endPoint;

        public bool CanFire(Vector2 tankPos, Vector2 enemyPos)
        {
            foreach (Block block in _blocks)
            {
                _startPoint.X = block.Pos.X; _startPoint.Y = block.Pos.Y;
                _endPoint.X = block.Pos.X + block.Windth; _endPoint.Y = block.Pos.Y;
                if (Helper.Intersects(tankPos, enemyPos, _startPoint, _endPoint))
                    return false;

                _startPoint.X = block.Pos.X; _startPoint.Y = block.Pos.Y;
                _endPoint.X = block.Pos.X; _endPoint.Y = block.Pos.Y + block.Height;
                if (Helper.Intersects(tankPos, enemyPos, _startPoint, _endPoint))
                    return false;

                _startPoint.X = block.Pos.X + block.Windth; _startPoint.Y = block.Pos.Y;
                _endPoint.X = block.Pos.X + block.Windth; _endPoint.Y = block.Pos.Y + block.Height;
                if (Helper.Intersects(tankPos, enemyPos, _startPoint, _endPoint))
                    return false;

                _startPoint.X = block.Pos.X; _startPoint.Y = block.Pos.Y + block.Height;
                _endPoint.X = block.Pos.X + block.Windth; _endPoint.Y = block.Pos.Y + block.Height;
                if (Helper.Intersects(tankPos, enemyPos, _startPoint, _endPoint))
                    return false;
            }

            return true;
        }

        public void LoadContent(ContentManager content)
        {
            _mouseCursor.LoadContent(content);

            foreach (var tank in _tanks)
                tank.LoadContent(content);

            foreach (var block in _blocks)
                block.LoadContent(content);

            _player.LoadContent(content);
            _gui.LoadContent(content);

            _menu.LoadContent(content);
        }

        public void Update(GameTime gameTime)
        {
            _mouseCursor.InMenu = _isMenu;
            _mouseCursor.Update(gameTime);
            if (_isMenu) return;

            for (int i = 0; i < _tanks.Count; i++)
                if (_tanks[i].Tank.IsAlive)
                {
                    _tanks[i].Update(gameTime);
                    _tanks[i].SetTargetPosition(_player.Tank.TankPosition);
                }
                else
                    _tanks.Remove(_tanks[i--]);

            _player.Update(gameTime);
            _gui.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !_prevKeyboard.IsKeyDown(Keys.Escape))
                _isMenu = !_isMenu;
            _prevKeyboard = Keyboard.GetState();

            if (!_isMenu)
            {
                foreach (var tank in _tanks)
                    if (tank.Tank.IsAlive)
                        tank.Draw(spriteBatch);

                foreach (var block in _blocks)
                    block.Draw(spriteBatch);

                _player.Draw(spriteBatch);
                _gui.Draw(spriteBatch);
            }
            else
                _menu.Draw(spriteBatch);

            _mouseCursor.Draw(spriteBatch);;
        }

        public void SafeExit()
        {
            Exit();
        }

        public void ReturnToGame()
        {
            _isMenu = !_isMenu;
        }

    }
}
