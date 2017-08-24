﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Warcraft.Managers;
using Warcraft.Map;
using Warcraft.Scenes;
using Warcraft.UI;
using Warcraft.Units.Humans;
using Warcraft.Util;

namespace Warcraft
{
    public class Warcraft : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int WINDOWS_WIDTH = 1280;
        public static int WINDOWS_HEIGHT = 800;

        public static int FPS = 1;
        public static int TILE_SIZE = 32;
        public static int MAP_SIZE = 50;

        public static int PLAYER_ISLAND = 0;
        public static int ISLAND = 0;

        public static Camera camera;

        Scene scene;
		MouseState oldState;

		public Warcraft()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = WINDOWS_WIDTH + 200;
            graphics.PreferredBackBufferHeight = WINDOWS_HEIGHT;

            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            ManagerResources.PLAYER_FOOD = 5;

            scene = new Menu();
            scene.Initializer();

			camera = new Camera(GraphicsDevice.Viewport);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            scene.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

			if (scene is Menu)
			{
				if (Mouse.GetState().LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
                {
                    if ((scene as Menu).rPlay.Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
                    {
                        scene = new WarcraftGame();
                        scene.Initializer();
                        scene.LoadContent(Content);
                    }
                    else if ((scene as Menu).rExit.Intersects(new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 1, 1)))
					{
                        Exit();
					}
                }

				oldState = Mouse.GetState();
            }
            else if (scene is WarcraftGame)
            {
                WarcraftGame game = scene as WarcraftGame;
                Knight olivaw = game.managerPlayerUnits.units.Find(unit => unit is Knight) as Knight;
                if (olivaw.information.HitPoints <= 0 || ISLAND > 2)
                {
                    scene = new Menu();
                    scene.Initializer();
                    scene.LoadContent(Content);
                }
            }
			
            scene.Update();

			base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            scene.Draw(spriteBatch);

            base.Draw(gameTime);
        }
    }
}
