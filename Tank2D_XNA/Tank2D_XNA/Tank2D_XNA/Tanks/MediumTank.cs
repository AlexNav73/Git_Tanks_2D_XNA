using Microsoft.Xna.Framework;
using Tank2D_XNA.Utills;

namespace Tank2D_XNA.Tanks
{
    class MediumTank : Tank
    {
        public MediumTank(Vector2 spawnPosition, TankTTX ttx) : base(spawnPosition)
        {
            SpriteName = Helper.Sprites["MediumTank"];
            Position = spawnPosition;
            Speed = ttx.Speed;
            RotationSpeed = ttx.RotationSpeed;
            Direction = new Vector2(Speed, 0);
            Hp = ttx.HP;
            Pannel = new TankInfoPanel(spawnPosition) { Hp = Hp, MaxHp = Hp };
            ReloadTime = ttx.ReloadTime;
            Overlook = ttx.Overlook;
            TurretCentrX = ttx.TurretCentrX;
            PiercingAmmoName = ttx.PiercingAmmoName;
        }
    }
}
