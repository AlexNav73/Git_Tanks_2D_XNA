using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using Microsoft.Xna.Framework;
using Tank2D_XNA.AmmoType;
using Tank2D_XNA.Tanks;

namespace Tank2D_XNA.Utills
{
    class AmmoFactory
    {
        private static AmmoFactory _instance;

        private AmmoFactory() { }

        public static AmmoFactory GetInstance()
        {
            return _instance ?? (_instance = new AmmoFactory());
        }

        public Ammo CreateShell(string type, string name, Vector2 position, Vector2 direction, int angle)
        {
            switch (type)
            {
                case "Piercing":
                    return new PiercingAmmo(position, direction, Helper.GetAmmo(name), angle);
            }

            return null;
        }
    }
}
