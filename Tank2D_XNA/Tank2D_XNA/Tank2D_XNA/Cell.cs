using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tank2D_XNA
{
    class Cell : Entity
    {

        public Cell()
        {
            Position = new Vector2(0, 0);
            SpriteName = Helper.Sprites["Rectangle"];
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            EntityCollisionRect = new Rectangle(0, 0, Helper.GRID_CELL_SIZE, Helper.GRID_CELL_SIZE);
            SpriteCenter = new Vector2(0, 0);
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
