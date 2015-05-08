using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Tank2D_XNA.Tanks;

namespace Tank2D_XNA.Screens
{
    class Gui : Entity
    {
        private SpriteFont _font;
        private readonly Tank _tank;
        public string Message = "null";

        public Gui(Tank tank)
        {
            _tank = tank;
        }

        public override void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>(Helper.Sprites["GuiFont"]);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font,
                String.Format("X = {0}\nY = {1}\nLog = {2}",
                    _tank.GetMeshRect.X,
                    _tank.GetMeshRect.Y,
                    Message
                ),
                new Vector2(Helper.GUI_OFFS_X, Helper.GUI_OFFS_Y),
                Color.Black);
        }
    }
}
