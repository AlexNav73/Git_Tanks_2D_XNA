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
        private DebugPannel _gui;
        private bool _isMenu;
        private KeyboardState _prevKeyboard;
        private Menu _menu;
        private int _currentMap;

        public ExitGame Exit { set; private get; }

        public static BattleField GetInstance()
        {
            return _instance ?? (_instance = new BattleField());
        }

        private BattleField() { }

        public void Initialize(int mapIndex = -1)
        {
            _mouseCursor = Cursor.GetCursor();
            if (_blocks == null) _blocks = new List<Block>();
            else _blocks.Clear();
            if (_tanks == null) _tanks = new List<AI>();
            else _tanks.Clear();

            _menu = new Menu();
            _isMenu = false;

            if (mapIndex != -1) 
                _currentMap = mapIndex;
            LoadGame(_currentMap);
        }

        public void LoadGame(int mapIndex)
        {
            MapReader reader = new MapReader(Helper.Maps[mapIndex]);
            reader.Map(info =>
            {
                switch (info.Type)
                {
                    case "Block":
                        AddBlock(info.Position);
                        break;
                    case "AI":
                        AddBot(TankFactory.GetInstance().CreateTank(info.TankType, info.Position));
                        break;
                    case "Player":
                        Tank playerTank = TankFactory.GetInstance().CreateTank(info.TankType, info.Position);
                        if (playerTank != null)
                        {
                            Cursor.GetCursor().Fire = playerTank.Fire;
                            _player = new Player(playerTank);
                            _gui = new DebugPannel(playerTank);
                        }
                        else SafeExit();
                        break;
                }
            });
        }

        public void AddBot(Tank tank)
        {
            if (tank != null)
                _tanks.Add(new AI(tank, _player.Tank.Location));
        }

        public void AddBlock(Vector2 pos)
        {
            _blocks.Add(new Block(pos));
        }

        public void AddWallsToGrid(FieldGrid grid)
        {
            foreach (Block block in _blocks)
            {
                grid.PlaceWall(block.Location);
            }
        }

        public Entity Intersects(Rectangle rect)
        {
            foreach (Block block in _blocks)
                if (rect.Intersects(block.MeshRect))
                    return block;
                    

            foreach (AI ai in _tanks)
                if (rect.Intersects(ai.Tank.MeshRect))
                    return ai.Tank;

            return rect.Intersects(_player.Tank.MeshRect) ? _player.Tank : null;
        }

        // Need to determinate start and ending point of box side
        private Vector2 _startPoint;
        private Vector2 _endPoint;
        // -----------------------------------------------------

        private bool CheckIntersectsWithEntitySide(Entity entity, Vector2 tankPos, Vector2 enemyPos, int bitMask)
        {
            _startPoint.X = entity.Location.X + ((bitMask & 8) != 0 ? 1 : 0) * entity.MeshRect.Width;
            _startPoint.Y = entity.Location.Y + ((bitMask & 4) != 0 ? 1 : 0) * entity.MeshRect.Height;
            _endPoint.X = entity.Location.X + ((bitMask & 2) != 0 ? 1 : 0) * entity.MeshRect.Width;
            _endPoint.Y = entity.Location.Y + ((bitMask & 1) != 0 ? 1 : 0) * entity.MeshRect.Height;
            return Helper.Intersects(tankPos, enemyPos, _startPoint, _endPoint);
        }

        private bool CheckIntersectsWithEntity(Entity entity, Vector2 tankPos, Vector2 enemyPos)
        {
            if (CheckIntersectsWithEntitySide(entity, tankPos, enemyPos, 2)) return true;
            if (CheckIntersectsWithEntitySide(entity, tankPos, enemyPos, 1)) return true;
            if (CheckIntersectsWithEntitySide(entity, tankPos, enemyPos, 11)) return true;
            if (CheckIntersectsWithEntitySide(entity, tankPos, enemyPos, 7)) return true;
            return false;
        }

        public bool CanSeeEnemy(Vector2 tankPos, Vector2 enemyPos, int distance)
        {
            foreach (Block block in _blocks)
                if (CheckIntersectsWithEntity(block, tankPos, enemyPos))
                    return false;

            return !((enemyPos - tankPos).Length() > distance);
        }

        public void LightArea(Tank player)
        {
            foreach (AI ai in _tanks)
                if (CanSeeEnemy(player.Location, ai.Tank.Location, Helper.MEDIUM_TANK_OVERLOOK))
                    ai.Tank.IsSpoted = true;
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
                    _tanks[i].SetTargetPosition(_player.Tank.Location);
                }
                else
                    _tanks.Remove(_tanks[i--]);

            _player.Update(gameTime);
            _gui.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !_prevKeyboard.IsKeyDown(Keys.Escape))
            {
                if (_isMenu) _menu.SetDefaultWindow();
                _isMenu = !_isMenu;
            }
            _prevKeyboard = Keyboard.GetState();

            if (!_isMenu)
            {                
                foreach (var block in _blocks)
                    block.Draw(spriteBatch);

                foreach (var tank in _tanks)
                    if (tank.Tank.IsAlive)
                        tank.Draw(spriteBatch);

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
