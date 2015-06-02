using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
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

        public Tank CreateTank(string type, string name, Vector2 position)
        {
            FileStream file;
            DataContractJsonSerializer js;

            switch (type)
            {
                case "MT":
                    try
                    {
                        file = new FileStream("Content/Settings/TanksTTX/" + name + ".json", FileMode.Open);
                    }
                    catch(Exception e)
                    {
                        TankTTX t = new TankTTX()
                        {
                            HP = 1200,
                            Overlook = 700,
                            PiercingAmmoName = "Pzgr_40_K",
                            ReloadTime = 4.32,
                            RotationSpeed = 4,
                            Speed = 15,
                            TurretCentrX = 19
                        };

                        js = new DataContractJsonSerializer(typeof(TankTTX));
                        FileStream tmpFile = new FileStream("Content/Settings/TanksTTX/" + name + ".json", FileMode.Create);
                        js.WriteObject(tmpFile, t);
                        tmpFile.Close();

                        file = new FileStream("Content/Settings/TanksTTX/" + name + ".json", FileMode.Open);
                    }
                    js = new DataContractJsonSerializer(typeof(TankTTX));
                    TankTTX ttx = (TankTTX)js.ReadObject(file);
                    file.Close();
                    return new MediumTank(position, ttx);

            }

            return null;
        }

    }
}
