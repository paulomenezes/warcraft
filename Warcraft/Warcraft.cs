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

        ManagerIsland managerIsland;
		// ManagerMap managerMap;
		ManagerMouse managerMouse = new ManagerMouse();

		ManagerUI managerUI;
        ManagerEA managerEA;

        ManagerUnits managerPlayerUnits;
        ManagerBuildings managerPlayerBuildings;

        // ManagerCombat managerCombat;

		public static int WINDOWS_WIDTH = 1280;
		public static int WINDOWS_HEIGHT = 800;

		public static int TILE_SIZE = 32;
		public static int MAP_SIZE = 50;

		public static int PLAYER_ISLAND = 0;

		public static Camera camera;

        // GenerateRooms rooms;

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
			Data.Write("##############");
			Data.Write("Começando jogo cada: " + DateTime.Now);
            managerIsland = new ManagerIsland(managerMouse);

            managerPlayerBuildings = new ManagerPlayerBuildings(managerMouse, managerIsland.CurrentMap());
            managerPlayerUnits = new ManagerPlayerUnits(managerMouse, managerIsland.CurrentMap(), managerPlayerBuildings);

            managerEA = new ManagerEA(1, managerMouse, managerIsland.CurrentMap());
            managerUI = new ManagerUI(managerMouse, managerPlayerBuildings, managerPlayerUnits, null);
            //managerCombat = new ManagerCombat(managerEA.managerEnemies, managerPlayerUnits, managerPlayerBuildings);

            camera = new Camera(GraphicsDevice.Viewport);

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

            managerIsland.LoadContent(Content);
            managerPlayerUnits.LoadContent(Content);
            managerPlayerBuildings.LoadContent(Content);
			managerUI.LoadContent(Content);
            managerEA.LoadContent(Content);

			SelectRectangle.LoadContent(Content);
		}

		protected override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			managerMouse.Update();
			managerPlayerUnits.Update();
            managerPlayerBuildings.Update();
			managerUI.Update();
            managerEA.Update(gameTime);

			// managerCombat.Update();

			if (IsActive)
				camera.Update(gameTime);
            
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
            managerEA.Draw(spriteBatch);
			managerMouse.Draw(spriteBatch);

			spriteBatch.End();

			spriteBatch.Begin();
			managerUI.DrawBack(spriteBatch);
			managerUI.Draw(spriteBatch);
            managerPlayerUnits.DrawUI(spriteBatch);
            managerPlayerBuildings.DrawUI(spriteBatch);
            managerEA.DrawUI(spriteBatch);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
