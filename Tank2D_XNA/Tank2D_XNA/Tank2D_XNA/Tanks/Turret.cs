﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Tank2D_XNA.Utills;

namespace Tank2D_XNA.Tanks
{
    class Turret : Entity
    {
        public Vector2 CursorPosition { set; get; }
        public Vector2 TurretPosition { set { Position = value; } }

        public Turret(Vector2 spawnPosition)
        {
            Position = spawnPosition;
            SpriteName = Helper.Sprites["Turret"];
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            CollisionMesh = new Rectangle(0, 0, Sprite.Width, Sprite.Height);
            SpriteCenter = new Vector2(Helper.TURRET_CENTR_X, Sprite.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            float deltaY = CursorPosition.Y - Position.Y;
            float deltaX = CursorPosition.X - Position.X;
            RotationAngleDegrees = (int)MathHelper.ToDegrees((float)Math.Atan2(deltaY, deltaX));

            base.Update(gameTime);
        }

    }
}
