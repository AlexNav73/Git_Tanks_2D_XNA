using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tank2D_XNA.Utills
{
    class Cell : Entity
    {
        private Rectangle _mash;
        public Cell(Vector2 position)
        {
            Position = position;
            SpriteName = Helper.Sprites["Rectangle"];
        }

        public void SetPosition(Vector2 newPos)
        {
            Position = newPos;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            _mash = new Rectangle((int)(Position.X), (int)(Position.Y), 55, 50);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, Position, _mash, Color.White);
        }
    }
}
