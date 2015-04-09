using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tank2D_XNA.Tanks
{
    class TankInfoPanel : Entity
    {
        private Rectangle _barRect;
        private readonly Vector2 _barOffs;
        private readonly Vector2 _fontOffs;
        private readonly int _width;
        private readonly int _height;
        public int Hp;
        public int MaxHp;

        private SpriteFont _font;
        private Vector2 _fontPosition;

        public Vector2 BarPosition
        {
            set
            {
                Position = value + _barOffs;
                _fontPosition = Position + _fontOffs;
            }
            get { return Position; }
        }

        public TankInfoPanel(Vector2 position)
        {
            _barOffs = new Vector2(-30, 40);
            _fontOffs = new Vector2(5, 5);
            Position = position + _barOffs;
            _fontPosition = Position + _fontOffs;
            SpriteName = @"GUI\hp";
            _height = 7;
            _width = 60;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            _barRect = new Rectangle((int)Position.X, (int)Position.Y, _width, _height);
            _font = content.Load<SpriteFont>(@"GUI\PannelFont");
        }

        public override void Update(GameTime gameTime)
        {
            _barRect.Width = (int)(((float)Hp / MaxHp) * _width);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, _barRect, Color.White);
            spriteBatch.DrawString(_font, String.Format("{0}xp", Hp), _fontPosition, Color.Red);
        }
    }
}
