﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Warcraft.Managers;
using Warcraft.Map;
using Warcraft.UI;
using Warcraft.Units.Humans;
using Warcraft.Util;

namespace Warcraft
{
	public class Warcraft : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

        ManagerIsland managerIsland;
		// ManagerMap managerMap;
		ManagerMouse managerMouse = new ManagerMouse();

		ManagerUI managerUI;
        ManagerEnemies managerEnemies;

        ManagerUnits managerPlayerUnits;
        ManagerBuildings managerPlayerBuildings;

        ManagerCombat managerCombat;

		public static int WINDOWS_WIDTH = 1280;
		public static int WINDOWS_HEIGHT = 800;

        public static int FPS = 10;
		public static int TILE_SIZE = 32;
		public static int MAP_SIZE = 50;

		public static int PLAYER_ISLAND = 0;
		public static int ISLAND = 0;

		public static Camera camera;

        // GenerateRooms rooms;
        float increment = 0.1f;
        float fadeOut = 0;

        Texture2D cursor;

        bool showSummary = false;
        Summary summary = new Summary();

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
            managerIsland = new ManagerIsland(managerMouse);

			managerEnemies = new ManagerEnemies(managerMouse, managerIsland.CurrentMap(), 0);
			
            managerPlayerBuildings = new ManagerPlayerBuildings(managerMouse, managerIsland.CurrentMap());
            managerPlayerUnits = new ManagerPlayerUnits(managerMouse, managerIsland.CurrentMap(), managerPlayerBuildings, managerEnemies);

            managerUI = new ManagerUI(managerMouse, managerPlayerBuildings, managerPlayerUnits, null);
            managerCombat = new ManagerCombat(managerEnemies, managerPlayerUnits, managerPlayerBuildings);

            camera = new Camera(GraphicsDevice.Viewport);

			increment = 0.1f;
			fadeOut = 0;
			showSummary = false;
            Battleship.move = false;

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

            managerIsland.LoadContent(Content);
            managerPlayerUnits.LoadContent(Content);
            managerPlayerBuildings.LoadContent(Content);
			managerUI.LoadContent(Content);
            managerEnemies.LoadContent(Content);

			SelectRectangle.LoadContent(Content);

            cursor = Content.Load<Texture2D>("Cursor");

            managerMouse.MouseEventHandler += (sender, e) => {
                if (showSummary)
                {
                    ManagerBuildings.goldMines.Clear();
                    this.Initialize();
                }
            };
		}

		protected override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			if (IsActive)
				camera.Update(gameTime);

            managerPlayerUnits.Update();
			if (Battleship.move)
            {
                increment += 1;

                if (fadeOut < 0.8f)
                {
					fadeOut += (float)Math.Pow(1.05, increment) / 100;
				}

                if (fadeOut > 0.6)
                {
                    managerMouse.Update();
                    showSummary = true;
                }
            }
            else
            {
                managerMouse.Update();
                managerPlayerBuildings.Update();
                managerUI.Update();
                managerEnemies.Update();

                managerCombat.Update();
            }
            
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);

            managerIsland.Draw(spriteBatch);
            //managerEA.Draw(spriteBatch);
            managerPlayerUnits.Draw(spriteBatch);
            managerPlayerBuildings.Draw(spriteBatch);
            managerEnemies.Draw(spriteBatch);
			managerMouse.Draw(spriteBatch);

			spriteBatch.End();

			spriteBatch.Begin();
			managerUI.DrawBack(spriteBatch);
			managerUI.Draw(spriteBatch);
            managerPlayerUnits.DrawUI(spriteBatch);
            managerPlayerBuildings.DrawUI(spriteBatch);
            managerEnemies.DrawUI(spriteBatch);


            if (Battleship.move)
            {
                spriteBatch.Draw(cursor, new Rectangle(0, 0, WINDOWS_WIDTH, WINDOWS_WIDTH), new Color(0, 0, 0, fadeOut));

                if (showSummary)
                {
                    summary.Draw(spriteBatch, managerPlayerUnits, managerEnemies.managerUnits);
                }
            }

			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
