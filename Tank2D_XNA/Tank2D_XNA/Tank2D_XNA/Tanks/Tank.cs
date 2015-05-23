using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Tank2D_XNA.AmmoType;
using Tank2D_XNA.GameField;
using Tank2D_XNA.Screens;
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

        private const float Resistance = 0.7f;
        public Vector2 Direct { set { Direction = value; } get { return Direction; } } // for debug pnnel uses
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
            //IsSpoted = true; // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        }

        [SuppressMessage("ReSharper", "PossibleLossOfFraction")]
        public override void LoadContent(ContentManager content)
        {
            _content = content;
            base.LoadContent(content);
            CollisionMesh = new Rectangle
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
            Position += Direction * Speed * (float)time + ResistanceForse(CollisionMesh);
            CollisionMesh.X = (int)Position.X - (int)(Sprite.Width * Scale) / 2;
            CollisionMesh.Y = (int)Position.Y - (int)(Sprite.Height * Scale) / 2;
        }

        public void DriveBackward(double time)
        {
            if (_isForward)
            {
                _isForward = false;
                Direction *= -1;
            }
            Position += Direction * Speed * (float)time + ResistanceForse(CollisionMesh);
            CollisionMesh.X = (int)Position.X - (int)(Sprite.Width * Scale) / 2;
            CollisionMesh.Y = (int)Position.Y - (int)(Sprite.Height * Scale) / 2;
        }

        public void TurnLeft(bool toLeft)
        {
            RotationAngleDegrees += ((toLeft ? -1 : 1) * RotationSpeed) % 360;
            Vector2 prevDir = new Vector2(Direction.X, Direction.Y); // GC crying
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

                Vector2 shellOffs = _tankTurret.CursorPosition - Position;
                shellOffs.Normalize();
                shellOffs *= 45;

                Ammo shell = new PiercingAmmo(
                    Position + shellOffs, 
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

        public override Vector2 GetResistenceForce(Vector2 target, Vector2 targetDirection)
        {
            Vector2 result = new Vector2(
                Position.X + Sprite.Width * Scale / 2,
                Position.Y + Sprite.Height * Scale / 2);
            result.X = target.X - result.X;
            result.Y = target.Y - result.Y;
            int dir = (int)(Vector2.Dot(result, targetDirection) * 100);
            if (dir < 5 && dir > -5)
                return -targetDirection;
            return result * Resistance;
        }

        public Vector2 ResistanceForse(Rectangle mesh)
        {
            Entity collision = BattleField.GetInstance().Intersects(mesh);
            if (!object.ReferenceEquals(collision, this) && collision != null)
                return collision.GetResistenceForce(Position, Direction);
            return Vector2.Zero;
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
