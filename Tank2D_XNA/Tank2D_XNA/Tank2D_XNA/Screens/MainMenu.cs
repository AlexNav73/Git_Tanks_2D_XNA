using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Tank2D_XNA.GameField;
using Tank2D_XNA.Tanks;

namespace Tank2D_XNA.Screens
{
    [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]
    class MainMenu : Screen
    {
        private readonly List<Button> _buttons;
        private ContentManager _contentManager;

        public MainMenu(int width, int height)
        {
            SpriteName = @"Sprites\MainWindow2";
            Position = new Vector2(0, 0);
            _buttons = new List<Button>
            {
                new Button(new Rectangle(860, 390, 200, 50), new Vector2(18, 5), "New Game", delegate
                {
                    BattleField.GetInstance().Initialize(new MediumTank(new Vector2(400, 250)));
                    BattleField.GetInstance().LoadGame();
                    BattleField.GetInstance().LoadContent(_contentManager);
                }), 
                new Button(new Rectangle(860, 440, 200, 50), new Vector2(55, 5), "Return", delegate { BattleField.GetInstance().ReturnToGame(); }), 
                new Button(new Rectangle(860, 490, 200, 50), new Vector2(55, 5), "Option", delegate { }), 
                new Button(new Rectangle(860, 530, 200, 50), new Vector2(60, 5), "Exit", delegate { BattleField.GetInstance().SaveExit(); })
            };
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
            foreach(Button button in _buttons)
                button.ButtonClicked(loc);
        }

    }
}
