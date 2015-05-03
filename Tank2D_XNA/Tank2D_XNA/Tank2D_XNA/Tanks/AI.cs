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

        public AI(Tank tank, Vector2 target)
        {
            Tank = tank;
            _targetPosition = target;
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
            Tank.TankTurret.CursorPosition = _targetPosition;
            Tank.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Tank.Draw(spriteBatch);
        }
    }
}
