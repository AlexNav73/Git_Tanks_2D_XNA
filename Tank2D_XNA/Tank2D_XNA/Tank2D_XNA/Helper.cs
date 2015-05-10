using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Tank2D_XNA
{
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

        // Screen properties

        public const int  SCREEN_WIDTH = 1200;
        public const int  SCREEN_HEIGHT = 700;
        public const bool SCREEN_IS_FULL_SCREEN = false;

        // Game field
        
        public const int   GRID_CELL_SIZE = 50;
        public const float BLOCK_SCALE = 0.5f;

        // GUI

        public const int GUI_OFFS_X = 10;
        public const int GUI_OFFS_Y = 10;
        public const int BUTTON_WIDTH = 200;
        public const int BUTTON_HEIGHT = 50;

        // Medium tank TTX

        public const int    MEDIUM_TANK_SPEED = 15;
        public const int    MEDIUM_TANK_ROTATION_SPEED = 4;
        public const int    MEDIUM_TANK_HP = 1200;
        public const double MEDIUM_TANK_RELOAD_TIME = 4.32;

        // Piercing ammo TTX

        public const int PIERCING_AMMO_MIN_DAMAGE = 100;
        public const int PIERCING_AMMO_MAX_DAMAGE = 200;
        public const int PIERCING_AMMO_MAX_DISTANSE = 600;
        public const int PIERCING_AMMO_SPEED = 10;

        // Tank info pannel params

        public const int HP_BAR_OFFS_X = -30;
        public const int HP_BAR_OFFS_Y = 40;
        public const int HP_BAR_WIDTH = 60;
        public const int HP_BAR_HEIGHT = 7;
        public const int HP_FONT_OFFS_X = 12;
        public const int HP_FONT_OFFS_Y = 5;

        // Turret TTX

        public const int TURRET_CENTR_X = 19;

        public static Vector2 RotateVector(Vector2 vect, float angle)
        {
            angle = MathHelper.ToRadians(angle);
            vect.X = (float)(vect.X * Math.Cos(angle) - vect.Y * Math.Sin(angle));
            vect.Y = (float)(vect.X * Math.Sin(angle) + vect.Y * Math.Cos(angle));
            return vect;
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
