﻿using System;
using System.Globalization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using Tank2D_XNA.Tanks;
using Tank2D_XNA.Utills;

namespace Tank2D_XNA.Screens
{
    class Gui : Entity
    {
        private readonly Tank _tank;
        private readonly Vector2 _reloadTimeOffs;
        public string Message = "null";
        private SpriteFont _font;

        public Gui(Tank tank)
        {
            _tank = tank;
            Position.X = Helper.GUI_OFFS_X;
            Position.Y = Helper.GUI_OFFS_Y;
            _reloadTimeOffs = new Vector2(Helper.GUI_RELOAD_X, Helper.GUI_RELOAD_Y);
        }

        public override void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>(Helper.Sprites["GuiFont"]);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font,
                String.Format("X = {0}\nY = {1}",
                    _tank.GetMeshRect.X,
                    _tank.GetMeshRect.Y
                ),
                Position, Color.Black);

            if (_tank.CurrentReloadTime != 0)
            {
                spriteBatch.DrawString(_font,
                    String.Format("{0} s", Math.Round(_tank.CurrentReloadTime, 2)),
                    Cursor.GetCursor().GetPosition - _reloadTimeOffs,
                    Color.Red);    
            }
            else
            {
                spriteBatch.DrawString(_font,
                    String.Format("{0} s", Math.Round(_tank.ReloadTime, 2)),
                    Cursor.GetCursor().GetPosition - _reloadTimeOffs,
                    Color.Green);    
            }
            
        }
    }
}
