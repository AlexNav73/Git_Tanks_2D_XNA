using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Tank2D_XNA.AmmoType;
using Tank2D_XNA.GameField;

namespace Tank2D_XNA.Tanks
{
    [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]
    [SuppressMessage("ReSharper", "SuggestVarOrType_BuiltInTypes")]
    abstract class Tank : Entity
    {
        private ContentManager _content;

        private readonly List<AmmoType.Ammo> _shells;
        private readonly Turret _tankTurret;
        protected TankInfoPanel Pannel;

        protected Vector2 Direction;
        protected int RotationSpeed;
        private int _rotateTo;
        protected int Speed;
        private bool _isForward;
        protected int Hp;

        private Vector2 _beforeCollisionPos;
        private Rectangle _beforeCollisionRect;

        private bool _isReloaded;
        protected double ReloadTime;
        private double _currentReloadTime;

        public Rectangle Mesh { get { return _beforeCollisionRect; } }
        public Turret TankTurret { get { return _tankTurret; } }
        public Vector2 TankPosition { set { Position = value; } get { return Position; } }
        public bool IsAlive { private set; get; }

        protected Tank(Vector2 spawnPosition)
        {
            _tankTurret = new Turret(spawnPosition);
            _isForward = true;
            _rotateTo = 0;
            _shells = new List<AmmoType.Ammo>();
            IsAlive = true;
            _isReloaded = true;
            _beforeCollisionPos = spawnPosition;

            _currentReloadTime = 0.0;
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

            _beforeCollisionRect.X = EntityCollisionRect.X;
            _beforeCollisionRect.Y = EntityCollisionRect.Y;
            _beforeCollisionRect.Width = EntityCollisionRect.Width;
            _beforeCollisionRect.Height = EntityCollisionRect.Height;
        }

        public void DriveForward(double time)
        {
            if (!_isForward)
            {
                _isForward = true;
                Direction *= -1;
            }
            _beforeCollisionPos += Direction * Speed * (float)time;
            _beforeCollisionRect.X = (int)_beforeCollisionPos.X - (int)(Sprite.Width * Scale) / 2;
            _beforeCollisionRect.Y = (int)_beforeCollisionPos.Y - (int)(Sprite.Height * Scale) / 2;
        }

        public void DriveBackward(double time)
        {
            if (_isForward)
            {
                _isForward = false;
                Direction *= -1;
            }
            _beforeCollisionPos += Direction * Speed * (float)time;
            _beforeCollisionRect.X = (int)_beforeCollisionPos.X - (int)(Sprite.Width * Scale) / 2;
            _beforeCollisionRect.Y = (int)_beforeCollisionPos.Y - (int)(Sprite.Height * Scale) / 2;
        }

        public void Turn(bool toLeft, double time)
        {
            _rotateTo += (toLeft ? -1 : 1) * RotationSpeed;
            Direction.Normalize();
            Direction = RotateVector(Direction, _rotateTo - RotationAngleDegrees);
            Direction *= Speed;
            RotationAngleDegrees = ((_rotateTo %= 360) + RotationSpeed * (int)(time));
        }

        public void Fire(Vector2 direction)
        {
            if (_isReloaded)
            {
                float deltaY = direction.Y - Position.Y;
                float deltaX = direction.X - Position.X;
                int angle = (int)MathHelper.ToDegrees((float)Math.Atan2(deltaY, deltaX));

                AmmoType.Ammo shell = new PiercingAmmo(
                    Position, 
                    direction - Position, 
                    angle, 
                    Helper.PIERCING_AMMO_MIN_DAMAGE, 
                    Helper.PIERCING_AMMO_MAX_DAMAGE
                );
                shell.LoadContent(_content);
                _shells.Add(shell);

                _isReloaded = false;
                ThreadPool.QueueUserWorkItem(OnReload);
            }
        }

        public void OnReload(object sender)
        {
            while (_currentReloadTime < ReloadTime)
            {
                _currentReloadTime += 0.01;
                Thread.Sleep(10);
            }
            _isReloaded = true;
            _currentReloadTime = 0.0;
        }

        public override void TakeDamage(int damage)
        {
            Hp -= damage;
            IsAlive = Hp > 0;
        }

        public void UpdatePosition(bool needUpdate)
        {
            if (needUpdate)
                Position = _beforeCollisionPos;
            else
                _beforeCollisionPos = Position;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            BattleField.GetInstance()
                .PrintMessage(
                    String.Format("Reload: {0} s", _currentReloadTime == 0.0 ? ReloadTime : Math.Round(_currentReloadTime, 2))
                );

            for (int i = 0; i < _shells.Count; i++)
            {
                if (!_shells[i].IsAlive)
                {
                    _shells.Remove(_shells[i]);
                    --i;
                    continue;
                }
                Entity obj = BattleField.GetInstance().Intersects(_shells[i].GetMeshRect);
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
            base.Draw(spriteBatch);

            foreach (AmmoType.Ammo shell in _shells)
                shell.Draw(spriteBatch);

            _tankTurret.Draw(spriteBatch);
            Pannel.Draw(spriteBatch);
        }
    }
}
