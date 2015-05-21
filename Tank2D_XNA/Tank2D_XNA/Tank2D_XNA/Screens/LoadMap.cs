using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Tank2D_XNA.GameField;
using Tank2D_XNA.Utills;

namespace Tank2D_XNA.Screens
{
    class LoadMap : Screen
    {
        private readonly List<Button> _buttons;
        private Menu _menu;
        private ContentManager _contentManager;

        public LoadMap(Menu menu)
        {
            _buttons = new List<Button>();
            SpriteName = Helper.Sprites["MainMenu"];
            Position = new Vector2(0, 0);
            _menu = menu;
            int y = 50, index = 0;
            foreach (string map in Helper.Maps)
            {
                int mapIndexToClosure = index;
                string mapName = "";
                if (map != null) mapName = Path.GetFileName(map);

                _buttons.Add(
                    new Button(
                        new Rectangle(0, y += 55, mapName.Length * 15, Helper.BUTTON_HEIGHT), 
                        new Vector2(5, 2),
                        mapName.Substring(0, mapName.Length - 4),
                        delegate
                        {
                            BattleField.GetInstance().Initialize(mapIndexToClosure);
                            BattleField.GetInstance().LoadContent(_contentManager);
                        })
                );

                ++index;
            }
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            _contentManager = content;
            EntityCollisionRect = new Rectangle
            {
                X = 0,
                Y = 0,
                Width = Sprite.Width,
                Height = Sprite.Height
            };
            SpriteCenter = new Vector2(0, 0);

            foreach (Button button in _buttons)
                button.LoadContent(content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            foreach (Button button in _buttons)
                button.Draw(spriteBatch);
        }

        public override void ButtonClicked(Vector2 loc)
        {
            foreach (Button button in _buttons)
                button.ButtonClicked(loc);
        }
    }
}
