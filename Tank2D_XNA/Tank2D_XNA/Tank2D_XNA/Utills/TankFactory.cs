using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Tank2D_XNA.Tanks;

namespace Tank2D_XNA.Utills
{
    class TankFactory
    {
        private static TankFactory _instance;

        private TankFactory() { }

        public static TankFactory GetInstance()
        {
            return _instance ?? (_instance = new TankFactory());
        }

        public Tank CreateTank(string type, Vector2 position)
        {
            switch (type)
            {
                case "MT":
                    return new MediumTank(position);
            }

            return null;
        }

    }
}
