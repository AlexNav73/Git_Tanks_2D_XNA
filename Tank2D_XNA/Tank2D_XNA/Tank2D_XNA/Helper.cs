﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

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

        // Screen resolution

        public const int SCREEN_WIDTH = 1920;
        public const int SCREEN_HEIGHT = 1080;

        // Game field
        
        public const int GRID_CELL_SIZE = 12;
        public const float BLOCK_SCALE = 0.5f;

        // GUI

        public const int GUI_OFFS_X = 10;
        public const int GUI_OFFS_Y = 10;
        public const int BUTTON_WIDTH = 200;
        public const int BUTTON_HEIGHT = 50;

        // Medium tank TTX

        public const int MEDIUM_TANK_SPEED = 15;
        public const int MEDIUM_TANK_ROTATION_SPEED = 4;
        public const int MEDIUM_TANK_HP = 1200;
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

    }
}
