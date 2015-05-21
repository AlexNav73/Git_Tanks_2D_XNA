﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Tank2D_XNA.AmmoType;
using Tank2D_XNA.GameField;
using Tank2D_XNA.Utills;

namespace Tank2D_XNA.Tanks
{
    [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]
    [SuppressMessage("ReSharper", "SuggestVarOrType_BuiltInTypes")]
    [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
    abstract class Tank : Entity
    {
        private ContentManager _content;

        private readonly List<AmmoType.Ammo> _shells;
        private readonly Turret _tankTurret;
        protected TankInfoPanel Pannel;

        protected Vector2 Direction;
        protected int RotationSpeed;
        protected int Speed;
        private bool _isForward;
        protected int Hp;

        private Vector2 _bounseVector;
        public Vector2 GetDirection { get { return Direction; } } // for debug pnnel uses
        public int IsCollision { get; set; }

        public double ReloadTime { get; protected set; }
        public double CurrentReloadTime { get; private set; }
        public Turret TankTurret { get { return _tankTurret; } }
        public bool IsAlive { private set; get; }
        public bool IsSpoted { get; set; }

        protected Tank(Vector2 spawnPosition)
        {
            _tankTurret = new Turret(spawnPosition);
            _isForward = true;
            _shells = new List<AmmoType.Ammo>();
            IsAlive = true;

            CurrentReloadTime = 0.0;
        }

        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        public override void LoadContent(ContentManager content)
        {
            _content = content;
            base.LoadContent(content);
            EntityCollisionRect = new Rectangle
            {
                X = (int)Position.X - Sprite.Width, 
                Y = (int)Position.Y - Sprite.Height, 
                Width = Sprite.Width, 
                Height = Sprite.Height
            };
            SpriteCenter = new Vector2
            {
                X = Sprite.Width / 2, 
                Y = Sprite.Height / 2
            };
            _tankTurret.LoadContent(content);
            Pannel.LoadContent(content);
        }

        public void DriveForward(double time)
        {
            if (!_isForward)
            {
                _isForward = true;
                Direction *= -1;
            }
            if (!TryCollide(Position, EntityCollisionRect, time)) return;
            Position += Direction * Speed * (float)time;
            EntityCollisionRect.X = (int)Position.X - (int)(Sprite.Width * Scale) / 2;
            EntityCollisionRect.Y = (int)Position.Y - (int)(Sprite.Height * Scale) / 2;
        }

        public void DriveBackward(double time)
        {
            if (_isForward)
            {
                _isForward = false;
                Direction *= -1;
            }
            if (!TryCollide(Position, EntityCollisionRect, time)) return;
            Position += Direction * Speed * (float)time;
            EntityCollisionRect.X = (int)Position.X - (int)(Sprite.Width * Scale) / 2;
            EntityCollisionRect.Y = (int)Position.Y - (int)(Sprite.Height * Scale) / 2;
        }

        public void TurnLeft(bool toLeft)
        {
            RotationAngleDegrees += ((toLeft ? -1 : 1) * RotationSpeed) % 360;
            Vector2 prevDir = new Vector2(Direction.X, Direction.Y);
            Helper.RotateVector(ref Direction, RotationAngleDegrees);
            if (Vector2.Dot(prevDir, Direction) < 0) Direction *= -1;
            Direction.Normalize();
            Direction *= Speed;
        }

        public void Fire(Vector2 direction)
        {
            if (CurrentReloadTime == 0.0)
            {
                float deltaY = direction.Y - Position.Y;
                float deltaX = direction.X - Position.X;
                int angle = (int)MathHelper.ToDegrees((float)Math.Atan2(deltaY, deltaX));

                Ammo shell = new PiercingAmmo(
                    Position, 
                    direction - Position, 
                    angle, 
                    Helper.PIERCING_AMMO_MIN_DAMAGE, 
                    Helper.PIERCING_AMMO_MAX_DAMAGE
                );
                shell.LoadContent(_content);
                _shells.Add(shell);

                ThreadPool.QueueUserWorkItem(OnReload);
            }
        }

        private void OnReload(object sender)
        {
            while (CurrentReloadTime < ReloadTime)
            {
                CurrentReloadTime += 0.01;
                Thread.Sleep(10);
            }

            CurrentReloadTime = 0.0;
        }

        public override void TakeDamage(int damage)
        {
            Hp -= damage;
            IsAlive = Hp > 0;
        }

        public bool TryCollide(Vector2 pos, Rectangle mesh, double time)
        {
            pos += Direction * Speed * (float)time;
            mesh.X = (int)pos.X - (int)(Sprite.Width * Scale) / 2;
            mesh.Y = (int)pos.Y - (int)(Sprite.Height * Scale) / 2;

            Entity collision = BattleField.GetInstance().Intersects(mesh);
            IsCollision = BattleField.GetInstance().CheckIntersectsWithEntity(collision, Position, collision.EntityCentr); //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            return (object.ReferenceEquals(collision, this) || collision == null);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            for (int i = 0; i < _shells.Count; i++)
            {
                if (!_shells[i].IsAlive)
                {
                    _shells.Remove(_shells[i]);
                    --i;
                    continue;
                }
                Entity obj = BattleField.GetInstance().Intersects(_shells[i].MeshRect);
                if (obj == null || obj == this)
                {
                    _shells[i].UpdatePosition(gameTime, Position);
                    continue;
                }
                obj.TakeDamage(_shells[i].GetDamage);
                _shells.Remove(_shells[i]);
                --i;
            }

            _tankTurret.TurretPosition = Position;
            Pannel.BarPosition = Position;
            Pannel.Hp = Hp;
            _tankTurret.Update(gameTime);
            Pannel.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (AmmoType.Ammo shell in _shells)
                shell.Draw(spriteBatch);

            if (IsSpoted)
            {
                base.Draw(spriteBatch);

                _tankTurret.Draw(spriteBatch);
                Pannel.Draw(spriteBatch);
            }
        }
    }
}
