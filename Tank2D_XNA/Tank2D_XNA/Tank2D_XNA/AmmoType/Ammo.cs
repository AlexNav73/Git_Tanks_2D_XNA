using Microsoft.Xna.Framework;

namespace Tank2D_XNA.AmmoType
{
    abstract class Ammo : Entity
    {
        protected Vector2 Direction;
        protected int Speed;
        protected int Damage;
        protected int MaxDistance;

        public int GetDamage { get { return Damage; } }
        public bool IsAlive { protected set; get; }

        public void UpdatePosition(GameTime gameTime, Vector2 tankPosition)
        {
            if ((tankPosition - Position).Length() > MaxDistance)
                IsAlive = false;
            Position += Direction * Speed;
            Update(gameTime);
        }

    }
}
