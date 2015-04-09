using Microsoft.Xna.Framework;

namespace Tank2D_XNA.Tanks
{
    class MediumTank : Tank
    {
        public MediumTank(Vector2 spawnPosition) : base(spawnPosition)
        {
            SpriteName = @"Sprites\PlayerTank";
            Position = spawnPosition;
            Speed = 15;
            RotationSpeed = 4;
            Direction = new Vector2(-Speed, 0);
            Hp = 1200;
            Pannel = new TankInfoPanel(spawnPosition) { Hp = Hp, MaxHp = Hp };
            ReloadTime = 0.0; //4.32;
        }
    }
}
