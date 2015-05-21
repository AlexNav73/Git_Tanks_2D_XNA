using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Tank2D_XNA.GameField;

namespace Tank2D_XNA
{
    abstract class Entity
    {
        protected Vector2 Position;
        protected Texture2D Sprite;
        protected string SpriteName;
        protected Rectangle EntityCollisionRect;
        protected int RotationAngleDegrees;
        protected float Scale = 1.0f;
        protected Vector2 SpriteCenter;

        public Vector2 Location { get { return Position; } }

        public Rectangle MeshRect { get { return EntityCollisionRect; } }

        public virtual void LoadContent(ContentManager content)
        {
            Sprite = content.Load<Texture2D>(SpriteName);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Sprite == null) return;
            EntityCollisionRect.X = (int)Position.X - (int)(Sprite.Width * Scale) / 2;
            EntityCollisionRect.Y = (int)Position.Y - (int)(Sprite.Height * Scale) / 2;
        }

        public virtual void TakeDamage(int damage) { }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Sprite,
                Position,
                null, 
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
