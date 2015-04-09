﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Tank2D_XNA.GameField;
using Tank2D_XNA.Tanks;

namespace Tank2D_XNA
{
    public class Tank2D : Microsoft.Xna.Framework.Game
    {
        readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private BattleField _field;

        public Tank2D()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 1920,
                PreferredBackBufferHeight = 1080,
                IsFullScreen = true
            };

            Content.RootDirectory = "Content";
            Window.Title = "Tanks 2D";
        }

        protected override void Initialize()
        {
            _field = BattleField.GetInstance();
            _field.Exit = Exit;
            _field.Initialize(new MediumTank(new Vector2(400, 250)));
            _field.LoadGame();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _field.LoadContent(Content);
        }

        protected override void UnloadContent() { }

        protected override void Update(GameTime gameTime)
        {
            _field.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();

            _field.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}