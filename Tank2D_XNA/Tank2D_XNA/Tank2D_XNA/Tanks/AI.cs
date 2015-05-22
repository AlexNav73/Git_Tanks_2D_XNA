using System;
using System.Collections.Generic;
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
        //private readonly FieldGrid _grid;
        private Vector2 _targetPosition;
        private Vector2 _direction;
        //private int _prevTargetX, _prevTargetY;
        public Tank Tank { private set; get; }
        public Vector2 TankPosition { get { return Tank.Location; } }

        //private List<Vector2> _pathToTarget = new List<Vector2>(); 

        //private readonly List<Cell> _cells = new List<Cell>();

        public AI(Tank tank, Vector2 target)
        {
            Tank = tank;
            _targetPosition = target;
            _direction = target - Tank.Location;
            //_grid = new FieldGrid(Helper.SCREEN_WIDTH, Helper.SCREEN_HEIGHT, Helper.GRID_CELL_SIZE);
        }

        public void LoadContent(ContentManager content)
        {
            Tank.LoadContent(content);
            _content = content;

            //BattleField.GetInstance().AddWallsToGrid(_grid);
        }

        public void SetTargetPosition(Vector2 position)
        {
            _targetPosition = position;
            _direction = position - Tank.Location;

            //int currentTargetX = (int)(position.X / Helper.GRID_CELL_SIZE);
            //int currentTargetY = (int)(position.Y / Helper.GRID_CELL_SIZE);
            //if (_prevTargetX != currentTargetX || _prevTargetY != currentTargetY)
            //{
            //    _prevTargetX = currentTargetX;
            //    _prevTargetY = currentTargetY;

            //    _grid.SetStart(Tank.Location);
            //    _grid.SetFinish(position);
            //    _cells.Clear();

            //    _pathToTarget = _grid.GetPath();

            //    foreach (Vector2 pos in _pathToTarget)
            //    {
            //        Cell c = new Cell(pos);
            //        c.LoadContent(_content);
            //        _cells.Add(c);
            //    }

            //    _pathToTarget.RemoveRange(0, 1);
            //    _pathToTarget.RemoveAt(_pathToTarget.Count - 1);
            //    //Tank.Direct = _pathToTarget.First();
            //}
        }

        public void Update(GameTime gameTime)
        {
            Tank.TankTurret.CursorPosition = _targetPosition;

            //do
            //{
            _direction.Normalize();
            float deltaY = _direction.Y - Tank.Direct.Y;
            float deltaX = _direction.X - Tank.Direct.X;
            var rotationAngleDegrees = (int)MathHelper.ToDegrees((float)Math.Atan2(deltaY, deltaX));
            if (rotationAngleDegrees != 0) Tank.TurnLeft(true);
            DebugPannel.Message = String.Format("\ndirection == x = {0} y = {1}\ntank direct == x = {2} y = {3}\nangle = {4}", 
                                                _direction.X, _direction.Y, Tank.Direct.X, Tank.Direct.Y, rotationAngleDegrees);
                //Helper.RotateVector(ref _direction, rotationAngleDegrees);
            //} while (rotationAngleDegrees != 0);

            if (BattleField.GetInstance().CanSeeEnemy(TankPosition, _targetPosition, Helper.PIERCING_AMMO_MAX_DISTANSE)) 
                Tank.Fire(_targetPosition);

            Tank.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Tank.Draw(spriteBatch);
            //foreach (Cell cell in _cells)
            //{
            //    cell.Draw(spriteBatch);
            //}
        }
    }
}
