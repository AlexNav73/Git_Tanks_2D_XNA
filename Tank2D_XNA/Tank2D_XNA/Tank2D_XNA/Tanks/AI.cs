using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Tank2D_XNA.GameField;

namespace Tank2D_XNA.Tanks
{
    class AI
    {
        private Vector2 _targetPosition;
        public Tank Tank { private set; get; }
        public Vector2 TankPosition { get { return Tank.TankPosition; } }
        private bool _forw = false;
        public AI(Tank tank, Vector2 target, bool t)
        {
            Tank = tank;
            _targetPosition = target;
            _forw = t;
        }

        public void LoadContent(ContentManager content)
        {
            Tank.LoadContent(content);
        }

        public void SetTargetPosition(Vector2 position)
        {
            _targetPosition = position;
        }

        public void Update(GameTime gameTime)
        {
            if (_forw)
                Tank.DriveForward(gameTime.ElapsedGameTime.TotalSeconds);
            else
                Tank.DriveBackward(gameTime.ElapsedGameTime.TotalSeconds);
            Tank.TankTurret.CursorPosition = _targetPosition;

            Tank.Fire(_targetPosition);

            Tank.UpdatePosition();
            Tank.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Tank.Draw(spriteBatch);
        }
    }
}
