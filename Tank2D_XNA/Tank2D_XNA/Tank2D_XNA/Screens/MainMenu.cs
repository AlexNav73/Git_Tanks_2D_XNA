using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Tank2D_XNA.GameField;
using Tank2D_XNA.Tanks;
using Tank2D_XNA.Utills;

namespace Tank2D_XNA.Screens
{
    [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]
    class MainMenu : Screen
    {
        private readonly List<Button> _buttons;
        private ContentManager _contentManager;
        private readonly Menu _menu;

        public MainMenu(Menu menu)
        {
            _menu = menu;
            SpriteName = Helper.Sprites["MainMenu"];
            Position = new Vector2(0, 0);
            _buttons = new List<Button>
            {
                new Button(
                    new Rectangle(
                        Helper.BUTTON_START_X, Helper.BUTTON_START_Y, Helper.BUTTON_WIDTH, Helper.BUTTON_HEIGHT), 
                        new Vector2(18, 5), 
                        "New Game", 
                        delegate
                        {
                            BattleField.GetInstance().Initialize();
                            BattleField.GetInstance().LoadContent(_contentManager);
                            BattleField.GetInstance().IsMenu = false;
                        }), 
                new Button(
                    new Rectangle(
                        Helper.BUTTON_START_X, Helper.BUTTON_START_Y + 50, Helper.BUTTON_WIDTH, Helper.BUTTON_HEIGHT), 
                        new Vector2(55, 5), 
                        "Return", delegate { BattleField.GetInstance().ReturnToGame(); }), 
                new Button(
                    new Rectangle(
                        Helper.BUTTON_START_X, Helper.BUTTON_START_Y + 100, Helper.BUTTON_WIDTH, Helper.BUTTON_HEIGHT), 
                        new Vector2(30, 5), 
                        "Load Map", delegate { _menu.SetWindow("LoadMapMenu"); }), 
                new Button(
                    new Rectangle(
                        Helper.BUTTON_START_X, Helper.BUTTON_START_Y + 150, Helper.BUTTON_WIDTH, Helper.BUTTON_HEIGHT), 
                        new Vector2(60, 5), 
                        "Exit", delegate { BattleField.GetInstance().SafeExit(); })
            };
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            _contentManager = content;
            CollisionMesh = new Rectangle
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
            foreach(Button button in _buttons)
                button.ButtonClicked(loc);
        }

    }
}
