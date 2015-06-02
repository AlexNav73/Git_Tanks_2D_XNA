using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Microsoft.Xna.Framework;
using Tank2D_XNA.AmmoType;

namespace Tank2D_XNA.Utills
{
    [DataContract]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    struct GameSetting
    {
        [DataMember] public int ScreenWidth;
        [DataMember] public int ScreenHeight;
        [DataMember] public bool IsFullScreen;
        [DataMember] public int GridCellSize;
        [DataMember] public float BlockScale;
        [DataMember] public float ResistanceForce;
        [DataMember] public bool ShowAICheckPoints;
        [DataMember] public int AmmoMaxDistanse;
    }

    [DataContract]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    struct GUISetting
    {
        [DataMember] public int GUIOffsX;
        [DataMember] public int GUIOffsY;
        [DataMember] public int GUIReloadX;
        [DataMember] public int GUIReloadY;
        [DataMember] public int ButtonStartX;
        [DataMember] public int ButtonStartY;
        [DataMember] public int ButtonWidth;
        [DataMember] public int ButtonHeight;
    }

    [DataContract]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    struct HPBarSetting
    {
        [DataMember] public int OffsX;
        [DataMember] public int OffsY;
        [DataMember] public int Width;
        [DataMember] public int Height;
        [DataMember] public int FontOffsX;
        [DataMember] public int FontOffsY;
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    class Helper
    {
        public static Dictionary<string, string> Sprites = new Dictionary<string, string>
        {
            {"PiercingAmmo", @"Sprites\Piercing"},
            {"Block", @"Sprites\Block"},
            {"Rectangle", @"GUI\hp"},
            {"MenuFont", @"GUI\MenuFont"},
            {"GuiFont", @"GUI\GuiFont"},
            {"MainMenu", @"Sprites\MainWindow2"},
            {"MediumTank", @"Sprites\PlayerTank"},
            {"PannelFont", @"GUI\PannelFont"},
            {"Turret", @"Sprites\Turret"},
            {"Cursor", @"Sprites\Cursor"}
        };

        private static readonly Dictionary<string, AmmoTTX> _ammos = new Dictionary<string, AmmoTTX>();
        public static AmmoTTX GetAmmo(string name)
        {
            return _ammos.ContainsKey(name) ? _ammos[name] : _ammos["default"];
        }
        public static void LoadAmmo()
        {
            string[] Ammos = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Content\Settings\AmmosTTX");
            if (Ammos.Length == 0)
            {
                AmmoTTX t = new AmmoTTX()
                {
                    MaxDamage = 300,
                    MaxDistanse = 700,
                    MinDamage = 150,
                    Speed = 10
                };

                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(AmmoTTX));
                FileStream ms = new FileStream("Content/Settings/AmmosTTX/default.json", FileMode.Create);
                js.WriteObject(ms, t);
                ms.Close();

                FileStream file = new FileStream("Content/Settings/AmmosTTX/default.json", FileMode.Open);
                _ammos.Add("default", (AmmoTTX)js.ReadObject(file));
                file.Close();
                return;
            }

            foreach (string ammo in Ammos)
            {
                DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(AmmoTTX));
                FileStream file = new FileStream(ammo, FileMode.Open);
                string ammoName = Path.GetFileName(ammo);
                if (ammoName == null) continue;
                _ammos.Add(ammoName.Substring(0, ammoName.Length - 5), (AmmoTTX)js.ReadObject(file));
                file.Close();
            }
        }

        // Game settings

        public static int       SCREEN_WIDTH ;
        public static int       SCREEN_HEIGHT;
        public static bool      SCREEN_IS_FULL_SCREEN;
        public static int       GRID_CELL_SIZE;
        public static float     BLOCK_SCALE;
        public static float     RESISTANCE_FORCE;
        public static string[]  Maps;
        public static bool      SHOW_AI_CHECK_POINTS;
        public static int       AMMO_MAX_DISTANSE;
        public static void LoadGameSettings()
        {
            Maps = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Content\Maps\");
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(GameSetting)); ;
            FileStream file;

            try
            {
                file = new FileStream("Content/Settings/GameSettings.json", FileMode.Open);
            }
            catch (Exception)
            {
                GameSetting t = new GameSetting()
                {
                    AmmoMaxDistanse = 700,
                    BlockScale = 0.27f,
                    GridCellSize = 60,
                    IsFullScreen = false,
                    ResistanceForce = 0.07f,
                    ScreenHeight = 1080,
                    ScreenWidth = 1920,
                    ShowAICheckPoints = false
                };

                FileStream ms = new FileStream("Content/Settings/GameSettings.json", FileMode.Create);
                js.WriteObject(ms, t);
                ms.Close();

                file = new FileStream("Content/Settings/GameSettings.json", FileMode.Open);
            }
            GameSetting settings = (GameSetting)js.ReadObject(file);
            file.Close();

            SCREEN_WIDTH = settings.ScreenWidth;
            SCREEN_HEIGHT = settings.ScreenHeight;
            SCREEN_IS_FULL_SCREEN = settings.IsFullScreen;
            GRID_CELL_SIZE = settings.GridCellSize;
            BLOCK_SCALE = settings.BlockScale;
            RESISTANCE_FORCE = settings.ResistanceForce;
            SHOW_AI_CHECK_POINTS = settings.ShowAICheckPoints;
            AMMO_MAX_DISTANSE = settings.AmmoMaxDistanse;
        }

        // GUI

        public static int GUI_OFFS_X;
        public static int GUI_OFFS_Y;
        public static int GUI_RELOAD_X;
        public static int GUI_RELOAD_Y;
        public static int BUTTON_START_X;
        public static int BUTTON_START_Y;
        public static int BUTTON_WIDTH;
        public static int BUTTON_HEIGHT;
        public static void LoadGUISettings()
        {
            var js = new DataContractJsonSerializer(typeof(GUISetting)); ;
            FileStream file;

            try
            {
                file = new FileStream("Content/Settings/GUISettings.json", FileMode.Open);
            }
            catch (Exception)
            {
                GUISetting t = new GUISetting()
                {
                    ButtonHeight = 50,
                    ButtonStartX = 860,
                    ButtonStartY = 390,
                    ButtonWidth = 200,
                    GUIOffsX = 10,
                    GUIOffsY = 10,
                    GUIReloadX = 25,
                    GUIReloadY = -10
                };

                var ms = new FileStream("Content/Settings/GUISettings.json", FileMode.Create);
                js.WriteObject(ms, t);
                ms.Close();

                file = new FileStream("Content/Settings/GUISettings.json", FileMode.Open);
            }
            var settings = (GUISetting)js.ReadObject(file);
            file.Close();

            GUI_OFFS_X = settings.GUIOffsX;
            GUI_OFFS_Y = settings.GUIOffsY;
            GUI_RELOAD_X = settings.GUIReloadX;
            GUI_RELOAD_Y = settings.GUIReloadY;
            BUTTON_START_X = settings.ButtonStartX;
            BUTTON_START_Y = settings.ButtonStartY;
            BUTTON_WIDTH = settings.ButtonWidth;
            BUTTON_HEIGHT = settings.ButtonHeight;
        }

        // Tank info pannel params

        public static int HP_BAR_OFFS_X = -30;
        public static int HP_BAR_OFFS_Y = 40;
        public static int HP_BAR_WIDTH = 60;
        public static int HP_BAR_HEIGHT = 7;
        public static int HP_FONT_OFFS_X = 12;
        public static int HP_FONT_OFFS_Y = 5;
        public static void LoadHPBarSettings()
        {
            var js = new DataContractJsonSerializer(typeof(HPBarSetting)); ;
            FileStream file;

            try
            {
                file = new FileStream("Content/Settings/HPBarSettings.json", FileMode.Open);
            }
            catch (Exception)
            {
                HPBarSetting t = new HPBarSetting()
                {
                    FontOffsX = 12,
                    FontOffsY = 5,
                    Height = 7,
                    OffsX = -30,
                    OffsY = 40,
                    Width = 60
                };

                var ms = new FileStream("Content/Settings/HPBarSettings.json", FileMode.Create);
                js.WriteObject(ms, t);
                ms.Close();

                file = new FileStream("Content/Settings/HPBarSettings.json", FileMode.Open);
            }
            var settings = (HPBarSetting)js.ReadObject(file);
            file.Close();

            HP_BAR_OFFS_X = settings.OffsX;
            HP_BAR_OFFS_Y = settings.OffsY;
            HP_BAR_WIDTH = settings.Width;
            HP_BAR_HEIGHT = settings.Height;
            HP_FONT_OFFS_X = settings.FontOffsX;
            HP_FONT_OFFS_Y = settings.FontOffsY;
        }

        // Turret TTX

        public const int TURRET_CENTR_X = 19;

        public static void RotateVector(ref Vector2 vect, float angle)
        {
            // 100 need to minimize error when cast double to int
            angle = MathHelper.ToRadians(angle + 90);
            if (angle >= 0 && angle <= 180)
            {
                vect.X += (int)(100 * Math.Sin(angle));
                vect.Y -= (int)(100 * Math.Cos(angle));
            }
            else
            {
                vect.X -= (int)(100 * -Math.Sin(angle));
                vect.Y -= (int)(100 * Math.Cos(angle));
            }
        }

        public static bool Intersects(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
        {
            Vector2 b = a2 - a1;
            Vector2 d = b2 - b1;
            float bDotDPerp = b.X * d.Y - b.Y * d.X;

            if (bDotDPerp == 0)
                return false;

            Vector2 c = b1 - a1;
            float t = (c.X * d.Y - c.Y * d.X) / bDotDPerp;
            if (t < 0 || t > 1)
                return false;

            float u = (c.X * b.Y - c.Y * b.X) / bDotDPerp;
            if (u < 0 || u > 1)
                return false;

            return true;
        }

    }
}
