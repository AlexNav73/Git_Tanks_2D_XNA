using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Tank2D_XNA.GameField
{
    class Block : Entity
    {
        public Block(Vector2 pos)
        {
            Position = pos;
            SpriteName = Helper.Sprites["Block"];
            RotationAngleDegrees = 0;
            Scale = Helper.BLOCK_SCALE;
        }

        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            EntityCollisionRect = new Rectangle
            {
                X = (int)Position.X - (int)(Sprite.Width * Scale) / 2,
                Y = (int)Position.Y - (int)(Sprite.Height * Scale) / 2,
                Width = (int)(Sprite.Width * Scale), 
                Height = (int)(Sprite.Height * Scale)
            };
            SpriteCenter = new Vector2
            {
                X = Sprite.Width / 2, 
                Y = Sprite.Height / 2
            };
        }

        public override void TakeDamage(int damage)
        {
            BattleField.GetInstance().PrintMessage("Block taking damage ...");
        }
    }
}
