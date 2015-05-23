using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tank2D_XNA.Utills;

namespace Tank2D_XNA.Screens
{

    public delegate void ClickedEvent();

    class Button : Screen
    {
        private readonly string _text;
        private SpriteFont _font;
        private readonly Vector2 _fontOffs;
        private readonly ClickedEvent _click;
        private Point _mouseClicked;

        public Button(Rectangle area, Vector2 fontOffs, string text, ClickedEvent onClick)
        {
            SpriteName = Helper.Sprites["Rectangle"];
            Position = new Vector2(area.X, area.Y);
            CollisionMesh = area;
            _fontOffs = fontOffs;
            _text = text;
            _click = onClick;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            SpriteCenter = new Vector2(Position.X, Position.Y);
            _font = content.Load<SpriteFont>(Helper.Sprites["MenuFont"]);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, CollisionMesh, Color.White);
            spriteBatch.DrawString(_font, _text, Position + _fontOffs, Color.White);
        }

        public override void ButtonClicked(Vector2 position)
        {
            _mouseClicked.X = Convert.ToInt32(position.X);
            _mouseClicked.Y = Convert.ToInt32(position.Y);
            if (CollisionMesh.Contains(_mouseClicked))
                _click();
        }
    }
}
