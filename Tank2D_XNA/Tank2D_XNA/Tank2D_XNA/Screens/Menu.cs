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

        private readonly List<Screen> _menus;
        private Screen _currentScreen;

        public Menu(int width, int height)
        {
            _menus = new List<Screen> { new MainMenu(width, height) };
            Cursor.GetCursor().MenuClick = ButtonClicked;
        }

        public void LoadContent(ContentManager content)
        {
            foreach (Screen screen in _menus)
                screen.LoadContent(content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //_currentScreen.Draw(spriteBatch);
            _menus[0].Draw(spriteBatch);
        }

        private void ButtonClicked(Vector2 location)
        {
            foreach (Screen screen in _menus)
                screen.ButtonClicked(location);
        }

    }
}
