using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Tank2D_XNA.Screens;
using Tank2D_XNA.Tanks;

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

        public void Initialize(Tank playerTank)
        {
            _mouseCursor = Cursor.GetCursor();
            Cursor.GetCursor().Fire = playerTank.Fire;
            _blocks = new List<Block>();
            _tanks = new List<AI>();
            _player = new Player(playerTank);
            _gui = new Gui(playerTank);

            _grid = new FieldGrid(1920, 1080, 20); // A* pathfinding grid, sell size need to configurate

            _menu = new Menu(1920, 1080);
            _isMenu = false;
        }

        public void LoadGame()
        {
            AddTank(new MediumTank(new Vector2(200, 200)));
            AddTank(new MediumTank(new Vector2(1000, 100)));
            AddTank(new MediumTank(new Vector2(700, 700)));

            AddBlock(new Vector2(50, 50));
            AddBlock(new Vector2(50, 150));
            AddBlock(new Vector2(50, 250));
            AddBlock(new Vector2(50, 350));
            AddBlock(new Vector2(50, 450));
            AddBlock(new Vector2(50, 550));
            AddBlock(new Vector2(50, 650));
            AddBlock(new Vector2(50, 750));
        }

        public void AddTank(Tank tank)
        {
            _tanks.Add(new AI(tank, _player.TankPosition));
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

            return null;
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
                    _tanks[i].SetTargetPosition(_player.TankPosition);
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

            _mouseCursor.Draw(spriteBatch);
        }

        public void PrintMessage(string message)
        {
            _gui.Message = message;
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
