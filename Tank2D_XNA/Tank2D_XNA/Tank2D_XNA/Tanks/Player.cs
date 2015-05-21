using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Tank2D_XNA.GameField;

namespace Tank2D_XNA.Tanks
{
    class Player
    {
        private readonly Cursor _mouse;
        private bool _autoLock;
        private Vector2 _lockPosition;
        public Tank Tank { private set; get; }

        public Player(Tank tank)
        {
            Tank = tank;
            _mouse = Cursor.GetCursor();
            _mouse.AutoLock = SetTarget;
            _autoLock = false;
        }

        public void LoadContent(ContentManager content)
        {
            Tank.LoadContent(content);
        }

        public void SetTarget(Vector2 position)
        {
            _autoLock = !_autoLock;
            _lockPosition = position;
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                Tank.DriveForward(gameTime.ElapsedGameTime.TotalSeconds);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                Tank.DriveBackward(gameTime.ElapsedGameTime.TotalSeconds);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                Tank.TurnLeft(true);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                Tank.TurnLeft(false);

            Tank.TankTurret.CursorPosition = !_autoLock ? _mouse.Location : _lockPosition;

            Tank.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Tank.Draw(spriteBatch);
        }
    }
}
