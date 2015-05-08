using Microsoft.Xna.Framework;

namespace Tank2D_XNA.Tanks
{
    class MediumTank : Tank
    {
        public MediumTank(Vector2 spawnPosition) : base(spawnPosition)
        {
            SpriteName = Helper.Sprites["MediumTank"];
            Position = spawnPosition;
            Speed = Helper.MEDIUM_TANK_SPEED;
            RotationSpeed = Helper.MEDIUM_TANK_ROTATION_SPEED;
            Direction = new Vector2(-Speed, 0);
            Hp = Helper.MEDIUM_TANK_HP;
            Pannel = new TankInfoPanel(spawnPosition) { Hp = Hp, MaxHp = Hp };
            ReloadTime = Helper.MEDIUM_TANK_RELOAD_TIME;
        }
    }
}
