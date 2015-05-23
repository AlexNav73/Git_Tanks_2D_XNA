using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tank2D_XNA.GameField;
using Tank2D_XNA.Screens;
using Tank2D_XNA.Utills;

namespace Tank2D_XNA.Tanks
{
    class AI
    {
        private ContentManager _content;
        private readonly FieldGrid _grid;
        private Vector2 _targetPosition;
        private Vector2 _toTargetDirection;
        private Vector2 _direction;
        private int _prevTargetX, _prevTargetY;
        private bool _stop;
        public Tank Tank { private set; get; }
        public Vector2 TankPosition { get { return Tank.Location; } }

        private List<Vector2> _pathToTarget = new List<Vector2>();
        private readonly List<Cell> _cells = new List<Cell>();

        public AI(Tank tank, Vector2 target)
        {
            _stop = true;
            Tank = tank;
            _targetPosition = target;
            _direction = Tank.Direct;
            _direction.Normalize();
            _grid = new FieldGrid(Helper.SCREEN_WIDTH, Helper.SCREEN_HEIGHT, Helper.GRID_CELL_SIZE);
        }

        public void LoadContent(ContentManager content)
        {
            Tank.LoadContent(content);
            _content = content;

            BattleField.GetInstance().AddWallsToGrid(_grid);
        }

        public void SetTargetPosition(Vector2 position)
        {
            _targetPosition = position;

            int currentTargetX = (int)(position.X / Helper.GRID_CELL_SIZE);
            int currentTargetY = (int)(position.Y / Helper.GRID_CELL_SIZE);
            if (_prevTargetX != currentTargetX || _prevTargetY != currentTargetY)
            {
                _stop = false;
                _prevTargetX = currentTargetX;
                _prevTargetY = currentTargetY;

                _grid.SetStart(Tank.Location);
                _grid.SetFinish(position);

                _pathToTarget = _grid.GetPath();
                _pathToTarget.Reverse();
                _pathToTarget.RemoveAt(_pathToTarget.Count - 1);

                _cells.Clear();
                foreach (Vector2 pos in _pathToTarget)
                {
                    Cell c = new Cell(pos);
                    c.LoadContent(_content);
                    _cells.Add(c);
                }

                if (_pathToTarget.Count != 0)
                {
                    _pathToTarget[0] = new Vector2(
                        _pathToTarget[0].X + Helper.GRID_CELL_SIZE/2,
                        _pathToTarget[0].Y + Helper.GRID_CELL_SIZE/2);
                    _toTargetDirection = _pathToTarget[0] - Tank.Location;
                    _toTargetDirection.Normalize();
                }
            }
        }

        private bool CanMoveNext(Vector2 destination)
        {
            int tankLocationX = (int)(Tank.Location.X / Helper.GRID_CELL_SIZE);
            int tankLocationY = (int)(Tank.Location.Y / Helper.GRID_CELL_SIZE);

            int nextPosX = (int)(destination.X / Helper.GRID_CELL_SIZE);
            int nextPosY = (int)(destination.Y / Helper.GRID_CELL_SIZE);

            return (tankLocationX == nextPosX && tankLocationY == nextPosY);
        }

        public void Update(GameTime gameTime)
        {
            Tank.TankTurret.CursorPosition = _targetPosition;

            _direction.X = -Tank.Direct.Y;
            _direction.Y = Tank.Direct.X;
            _direction.Normalize();

            if (_pathToTarget.Count > 0 && CanMoveNext(_pathToTarget[0]))
            {
                _pathToTarget.RemoveAt(0);
                if (_pathToTarget.Count != 0 && (Tank.Location - _targetPosition).Length() > Helper.GRID_CELL_SIZE * 1.5)
                {
                    _pathToTarget[0] = new Vector2(
                        _pathToTarget[0].X + Helper.GRID_CELL_SIZE / 2,
                        _pathToTarget[0].Y + Helper.GRID_CELL_SIZE / 2);
                    _toTargetDirection = _pathToTarget[0] - Tank.Location;
                    _toTargetDirection.Normalize();
                    _cells.RemoveAt(0);
                }
                else _stop = true;
            }

            if (!_stop)
            {
                if ((int)(Vector2.Dot(_direction, _toTargetDirection) * 100) < -2) Tank.TurnLeft(true);
                else if ((int)(Vector2.Dot(_direction, _toTargetDirection) * 100) > 2) Tank.TurnLeft(false);
                else Tank.DriveForward(gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (BattleField.GetInstance().CanSeeEnemy(TankPosition, _targetPosition, Helper.PIERCING_AMMO_MAX_DISTANSE))
                Tank.Fire(_targetPosition);

            Tank.Update(gameTime);
        }

        [SuppressMessage("ReSharper", "ConditionIsAlwaysTrueOrFalse")]
        public void Draw(SpriteBatch spriteBatch)
        {
            Tank.Draw(spriteBatch);
            if (Helper.SHOW_AI_CHECK_POINTS)
            #pragma warning disable 162
            {
                foreach (Cell cell in _cells)
                {
                    cell.Draw(spriteBatch);
                }
            }
            #pragma warning restore 162
        }
    }
}
