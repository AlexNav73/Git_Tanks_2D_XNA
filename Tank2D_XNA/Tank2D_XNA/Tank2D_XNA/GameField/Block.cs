using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Tank2D_XNA.Utills;

namespace Tank2D_XNA.GameField
{
    class Block : Entity
    {

        private readonly float _resistance;

        public Block(Vector2 pos)
        {
            Position = pos;
            SpriteName = Helper.Sprites["Block"];
            RotationAngleDegrees = 0;
            Scale = Helper.BLOCK_SCALE;

            _resistance = Helper.RESISTANCE_FORCE;
        }

        public override Vector2 GetResistenceForce(Vector2 target, Vector2 targetDirection)
        {
            Vector2 result = new Vector2(
                Position.X + Sprite.Width * Scale / 2,
                Position.Y + Sprite.Height * Scale / 2);
            result.X = target.X - result.X;
            result.Y = target.Y - result.Y;
            int dir = (int) (Vector2.Dot(result, targetDirection)*100);
            if (dir < 5 && dir > -5)
                return -targetDirection;
            return result * _resistance;
        }

        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            CollisionMesh = new Rectangle
            {
                X = (int)Position.X,
                Y = (int)Position.Y,
                Width = (int)(Sprite.Width * Scale), 
                Height = (int)(Sprite.Height * Scale)
            };

        }
    }
}
