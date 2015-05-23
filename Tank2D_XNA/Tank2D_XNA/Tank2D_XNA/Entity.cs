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
        protected Rectangle CollisionMesh;
        protected int RotationAngleDegrees;
        protected float Scale = 1.0f;
        protected Vector2 SpriteCenter;

        protected Vector2 EntityC;//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        public Vector2 Location { get { return Position; } }
        public Rectangle MeshRect { get { return CollisionMesh; } }
        public Vector2 EntityCentr { get { return EntityC; } } //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

        public virtual void LoadContent(ContentManager content)
        {
            Sprite = content.Load<Texture2D>(SpriteName);
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Sprite == null) return;
            CollisionMesh.X = (int)Position.X - (int)(Sprite.Width * Scale) / 2;
            CollisionMesh.Y = (int)Position.Y - (int)(Sprite.Height * Scale) / 2;
            EntityC.X = Position.X + Sprite.Width / 2;//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            EntityC.Y = Position.Y + Sprite.Height / 2;//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        public virtual void TakeDamage(int damage) { }

        public virtual Vector2 GetResistenceForce(Vector2 target, Vector2 targetDirection) { return new Vector2(); }

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
