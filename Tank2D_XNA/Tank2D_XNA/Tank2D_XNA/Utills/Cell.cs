using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tank2D_XNA.Utills
{
    class Cell : Entity
    {

        public Cell()
        {
            Position = new Vector2(573, 502);
            SpriteName = Helper.Sprites["Rectangle"];
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            EntityCollisionRect = new Rectangle(573, 502, 112, 122);
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Sprite,
                Position,
                EntityCollisionRect,
                Color.White,
                MathHelper.ToRadians(RotationAngleDegrees),
                SpriteCenter,
                Scale,
                SpriteEffects.None,
                0
            );
        }
    }
}
