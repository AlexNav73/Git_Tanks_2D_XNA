using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Tank2D_XNA.GameField;

namespace Tank2D_XNA.Tanks
{
    class Player
    {
        private readonly Tank _tank;
        private readonly Cursor _mouse;
        private bool _autoLock;
        private Vector2 _lockPosition;
        public Vector2 TankPosition { get { return _tank.TankPosition; } }

        public Player(Tank tank)
        {
            _tank = tank;
            _mouse = Cursor.GetCursor();
            _mouse.AutoLock = SetTarget;
            _autoLock = false;
        }

        public void LoadContent(ContentManager content)
        {
            _tank.LoadContent(content);
        }

        public void SetTarget(Vector2 position)
        {
            _autoLock = !_autoLock;
            _lockPosition = position;
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                _tank.DriveForward(gameTime.ElapsedGameTime.TotalSeconds);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                _tank.DriveBackward(gameTime.ElapsedGameTime.TotalSeconds);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                _tank.Turn(true, gameTime.ElapsedGameTime.TotalSeconds);
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                _tank.Turn(false, gameTime.ElapsedGameTime.TotalSeconds);

            _tank.UpdatePosition(BattleField.GetInstance().Intersects(_tank.Mesh) == null);
            _tank.TankTurret.CursorPosition = !_autoLock ? _mouse.GetPosition : _lockPosition;

            _tank.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _tank.Draw(spriteBatch);
        }
    }
}
