using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Tank2D_XNA.Utills;

namespace Tank2D_XNA.AmmoType
{
    class PiercingAmmo : Ammo
    {
        private static readonly Random Rand = new Random();

        public PiercingAmmo(Vector2 pos, Vector2 direction, AmmoTTX ttx, int angle)
        {
            direction.Normalize();
            Direction = direction;
            Speed = ttx.Speed;
            Position = pos;
            SpriteName = Helper.Sprites["PiercingAmmo"];
            RotationAngleDegrees = angle;
            Damage = Rand.Next(ttx.MinDamage, ttx.MaxDamage);
            MaxDistance = ttx.MaxDistanse;
            IsAlive = true;
        }

        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            SpriteCenter = new Vector2
            {
                X = Sprite.Width / 2,
                Y = Sprite.Height / 2
            };
            CollisionMesh = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Width, Sprite.Height);
        }
    }
}
