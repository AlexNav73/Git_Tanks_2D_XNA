using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tank2D_XNA.Screens
{
    [SuppressMessage("ReSharper", "SuggestVarOrType_SimpleTypes")]
    class Menu
    {

        private readonly Dictionary<string, Screen> _menus;
        private Screen _currentScreen;

        public Menu(int width, int height)
        {
            _menus = new Dictionary<string, Screen>
            {
                { "MainMenu", new MainMenu(this) }
            };
            Cursor.GetCursor().MenuClick = ButtonClicked;
            _currentScreen = _menus["MainMenu"];
        }

        public void SetWindow(string windowName)
        {
            _currentScreen = _menus[windowName];
        }

        public void LoadContent(ContentManager content)
        {
            foreach (KeyValuePair<string, Screen> pair in _menus)
                pair.Value.LoadContent(content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            _currentScreen.Draw(spriteBatch);
        }

        private void ButtonClicked(Vector2 location)
        {
            foreach (KeyValuePair<string, Screen> pair in _menus)
                pair.Value.ButtonClicked(location);
        }

    }
}
