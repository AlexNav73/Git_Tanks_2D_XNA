﻿using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Tank2D_XNA.Utills;

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
                X = (int)Position.X,
                Y = (int)Position.Y,
                Width = (int)(Sprite.Width * Scale), 
                Height = (int)(Sprite.Height * Scale)
            };

        }
    }
}
