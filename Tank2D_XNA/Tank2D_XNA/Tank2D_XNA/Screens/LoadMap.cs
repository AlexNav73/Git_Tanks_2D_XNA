using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Tank2D_XNA.GameField;
using Tank2D_XNA.Utills;

namespace Tank2D_XNA.Screens
{
    class LoadMap : Screen
    {
        private readonly List<Button> _buttons;
        private Menu _menu;

        public LoadMap(Menu menu)
        {
            _menu = menu;
            int x = 100, y = 100;
            foreach (string map in Helper.Maps)
            {
                _buttons.Add(new Button(new Rectangle(860, 440, Helper.BUTTON_WIDTH, Helper.BUTTON_HEIGHT), new Vector2(55, 5),
                    Path.GetFileName(map), delegate
                    {
                        BattleField.GetInstance().Initialize(Helper.Maps.Where(n => n == map).First());
                    }));
            }
        }

        public override void ButtonClicked(Vector2 loc)
        {
            foreach (Button button in _buttons)
                button.ButtonClicked(loc);
        }
    }
}
