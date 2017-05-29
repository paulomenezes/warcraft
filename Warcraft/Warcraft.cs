using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Warcraft.Managers;
using Warcraft.Map;
using Warcraft.UI;
using Warcraft.Util;

namespace Warcraft
{
	public class Warcraft : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		ManagerMap managerMap;
		ManagerMouse managerMouse = new ManagerMouse();

		ManagerUI managerUI;
        List<ManagerEnemies> managerEnemies = new List<ManagerEnemies>();

        ManagerUnits managerPlayerUnits;
        ManagerBuildings managerPlayerBuildings;

        ManagerCombat managerCombat;

		public static int WINDOWS_WIDTH = 1280;
		public static int WINDOWS_HEIGHT = 800;

		public static int TILE_SIZE = 32;
		public static int MAP_SIZE = 50;

		public static Camera camera;

        public static bool PLAYER = false;

		public Warcraft()
		{
			graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = WINDOWS_WIDTH + 200;
			graphics.PreferredBackBufferHeight = WINDOWS_HEIGHT;

			Content.RootDirectory = "Content";

			IsMouseVisible = true;

            if (!PLAYER)
            {
                TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 100.0f);
                IsFixedTimeStep = false;
                graphics.SynchronizeWithVerticalRetrace = false;
            }
		}

		protected override void Initialize()
		{
			Data.Write("##############");
			Data.Write("Começando jogo: " + DateTime.Now);

            managerMap = new ManagerMap();
            Functions.managerMap = managerMap;

            if (PLAYER)
            {
                managerPlayerBuildings = new ManagerPlayerBuildings(managerMouse, managerMap);
                managerPlayerUnits = new ManagerPlayerUnits(managerMouse, managerMap, managerPlayerBuildings);
            }

			ManagerBuildings.goldMines.Add(new Buildings.Neutral.GoldMine(6, 33, managerMouse, managerMap, null));
			ManagerBuildings.goldMines.Add(new Buildings.Neutral.GoldMine(45, 5, managerMouse, managerMap, null));
			ManagerBuildings.goldMines.Add(new Buildings.Neutral.GoldMine(38, 22, managerMouse, managerMap, null));

			for (int i = 0; i < 4; i++)
			{
                ManagerResources.BOT_GOLD.Add(5000);
				ManagerResources.BOT_WOOD.Add(99999);
				ManagerResources.BOT_FOOD.Add(5);
				ManagerResources.BOT_OIL.Add(99999);

				managerEnemies.Add(new ManagerEnemies(managerMouse, managerMap, i));
			}

            managerUI = new ManagerUI(managerMouse, managerPlayerBuildings, managerPlayerUnits, managerEnemies);
            managerCombat = new ManagerCombat(managerEnemies, managerPlayerUnits, managerPlayerBuildings);

            camera = new Camera(GraphicsDevice.Viewport);

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			managerMap.LoadContent(Content);
            if (PLAYER)
            {
                managerPlayerUnits.LoadContent(Content);
                managerPlayerBuildings.LoadContent(Content);
            }
            managerEnemies.ForEach(e => e.LoadContent(Content));
			managerUI.LoadContent(Content);

			SelectRectangle.LoadContent(Content);
		}

		protected override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

            if (PLAYER)
            {
				managerMouse.Update();
				managerPlayerUnits.Update();
                managerPlayerBuildings.Update();
            }
            managerEnemies.ForEach(e => e.Update());
			managerUI.Update();

			managerCombat.Update();

			if (IsActive)
				camera.Update(gameTime);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.CornflowerBlue);

			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.transform);
			managerMap.Draw(spriteBatch);

            managerEnemies.ForEach(e => e.Draw(spriteBatch));
            if (PLAYER)
            {
                managerPlayerUnits.Draw(spriteBatch);
                managerPlayerBuildings.Draw(spriteBatch);
            }
			managerMouse.Draw(spriteBatch);
			spriteBatch.End();

			spriteBatch.Begin();
			managerUI.DrawBack(spriteBatch);
			managerUI.Draw(spriteBatch);
			managerEnemies.ForEach(e => e.DrawUI(spriteBatch));
            if (PLAYER)
            {
                managerPlayerUnits.DrawUI(spriteBatch);
                managerPlayerBuildings.DrawUI(spriteBatch);
            }
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
