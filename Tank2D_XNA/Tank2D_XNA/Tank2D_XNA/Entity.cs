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
        protected int RotationAngleDegrees = 0;
        protected float Scale = 1.0f;
        protected Vector2 SpriteCenter;

        //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        protected Texture2D DebugTexture2D;

        public Rectangle GetMeshRect { get { return EntityCollisionRect; } }

        public static Vector2 RotateVector(Vector2 vect, float angle)
        {
            angle = MathHelper.ToRadians(angle);
            vect.X = (float)(vect.X * Math.Cos(angle) - vect.Y * Math.Sin(angle));
            vect.Y = (float)(vect.X * Math.Sin(angle) + vect.Y * Math.Cos(angle));
            return vect;
        }

        public virtual void LoadContent(ContentManager content)
        {
            Sprite = content.Load<Texture2D>(SpriteName);

            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            DebugTexture2D = content.Load<Texture2D>(@"GUI\hp");
        }

        public virtual void Update(GameTime gameTime)
        {
            if (Sprite == null) return;
            EntityCollisionRect.X = (int)Position.X - (int)(Sprite.Width * Scale) / 2;
            EntityCollisionRect.Y = (int)Position.Y - (int)(Sprite.Height * Scale) / 2;
        }

        public virtual void TakeDamage(int damage)
        {
            BattleField.GetInstance().PrintMessage("TakingDamage");
        }

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

            //spriteBatch.Draw(DebugTexture2D, new Vector2(EntityCollisionRect.X, EntityCollisionRect.Y), EntityCollisionRect, Color.White);
        }
    }
}
