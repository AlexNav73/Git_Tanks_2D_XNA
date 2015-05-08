
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Tank2D_XNA
{

    public delegate void Click(Vector2 position);

    class Cursor : Entity
    {
        private MouseState _currentMouseState;
        private MouseState _prevMouseState;
        private static Cursor _instance;
        private bool _isLocked;

        public bool InMenu { get; set; }
        public Click Fire { set; private get; }
        public Click AutoLock { set; private get; }
        public Click MenuClick { set; private get; }
        public Vector2 GetPosition { get { return Position; } }

        public static Cursor GetCursor()
        {
            return _instance ?? (_instance = new Cursor());
        }

        private Cursor()
        {
            Position = new Vector2(0, 0);
            SpriteName = Helper.Sprites["Cursor"];
            _isLocked = false;
        }

        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            EntityCollisionRect = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
            SpriteCenter = new Vector2
            {
                X = Sprite.Width / 2, 
                Y = Sprite.Height / 2
            };
        }

        public override void Update(GameTime gameTime)
        {
            _currentMouseState = Mouse.GetState();
            if (InMenu)
            {
                AutoLock(Position);
                _isLocked = false;
            }

            if (!_isLocked)
            {
                Position.X = _currentMouseState.X;
                Position.Y = _currentMouseState.Y;
            }

            if (_currentMouseState.LeftButton == ButtonState.Pressed && 
                _prevMouseState.LeftButton == ButtonState.Released && !InMenu)
                Fire(Position);

            if (_currentMouseState.LeftButton == ButtonState.Pressed &&
                _prevMouseState.LeftButton == ButtonState.Released && InMenu)
                MenuClick(Position);

            if (_currentMouseState.RightButton == ButtonState.Pressed &&
                _prevMouseState.RightButton == ButtonState.Released)
            {
                AutoLock(Position);
                _isLocked = !_isLocked;
            }

            _prevMouseState = _currentMouseState;

            base.Update(gameTime);
        }
    }
}
