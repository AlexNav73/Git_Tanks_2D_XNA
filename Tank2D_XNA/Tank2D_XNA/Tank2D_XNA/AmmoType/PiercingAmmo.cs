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

        public PiercingAmmo(Vector2 pos, Vector2 direction, int angle, int minDamage, int maxDamage)
        {
            direction.Normalize();
            Direction = direction;
            Speed = Helper.PIERCING_AMMO_SPEED;
            Position = pos;
            SpriteName = Helper.Sprites["PiercingAmmo"];
            RotationAngleDegrees = angle;
            Damage = Rand.Next(minDamage, maxDamage);
            MaxDistance = Helper.PIERCING_AMMO_MAX_DISTANSE;
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
            EntityCollisionRect = new Rectangle((int)Position.X, (int)Position.Y, Sprite.Width, Sprite.Height);
        }
    }
}
