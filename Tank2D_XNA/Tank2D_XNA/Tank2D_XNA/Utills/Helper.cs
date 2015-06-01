﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Microsoft.Xna.Framework;

namespace Tank2D_XNA.Utills
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

        public const int    SCREEN_WIDTH = 1920; // 1200
        public const int    SCREEN_HEIGHT = 1080; // 700
        public const bool   SCREEN_IS_FULL_SCREEN = true;

        // Game field
        
        public const int        GRID_CELL_SIZE = 60;
        public const float      BLOCK_SCALE = 0.27f;
        public const float      RESISTANCE_FORCE = 0.07f;
        public static string[]  Maps = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Content\Maps\");
        public const bool       SHOW_AI_CHECK_POINTS = false;

        // GUI

        public const int GUI_OFFS_X = 10;
        public const int GUI_OFFS_Y = 10;
        public const int GUI_RELOAD_X = 25;
        public const int GUI_RELOAD_Y = -10;
        public const int BUTTON_START_X = 860;
        public const int BUTTON_START_Y = 390;
        public const int BUTTON_WIDTH = 200;
        public const int BUTTON_HEIGHT = 50;

        // Medium tank TTX

        public const int    MEDIUM_TANK_SPEED = 15;
        public const int    MEDIUM_TANK_ROTATION_SPEED = 4;
        public const int    MEDIUM_TANK_HP = 1200;
        public const double MEDIUM_TANK_RELOAD_TIME = 4.32;
        public const int    MEDIUM_TANK_OVERLOOK = 700;

        // Piercing ammo TTX

        public const int PIERCING_AMMO_MIN_DAMAGE = 100;
        public const int PIERCING_AMMO_MAX_DAMAGE = 200;
        public const int PIERCING_AMMO_MAX_DISTANSE = 700;
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
