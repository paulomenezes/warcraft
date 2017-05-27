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

		ManagerMap managerMap = new ManagerMap();
		ManagerMouse managerMouse = new ManagerMouse();

		ManagerUI managerUI;
        List<ManagerEnemies> managerEnemies = new List<ManagerEnemies>();

        ManagerUnits managerPlayerUnits;
        ManagerBuildings managerPlayerBuildings;

		public static int WINDOWS_WIDTH = 1280;
		public static int WINDOWS_HEIGHT = 800;

		public static int TILE_SIZE = 32;
		public static int MAP_SIZE = 50;

		public static Camera camera;

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
			Data.Write("Começando jogo: " + DateTime.Now);

			managerPlayerBuildings = new ManagerPlayerBuildings(managerMouse, managerMap);
			managerPlayerUnits = new ManagerPlayerUnits(managerMouse, managerMap, managerPlayerBuildings);

            managerEnemies.Add(new ManagerEnemies(managerMouse, managerMap, 0));
            managerEnemies.Add(new ManagerEnemies(managerMouse, managerMap, 1));
            new ManagerResources(managerEnemies.Count);

			managerUI = new ManagerUI(managerMouse, managerPlayerBuildings, managerPlayerUnits);

            //managerCombat = new ManagerCombat(managerUnits, managerEnemies, managerBuildings);

            camera = new Camera(GraphicsDevice.Viewport);

			base.Initialize();
		}

		protected override void LoadContent()
		{
			spriteBatch = new SpriteBatch(GraphicsDevice);

			managerMap.LoadContent(Content);
			managerPlayerUnits.LoadContent(Content);
			managerPlayerBuildings.LoadContent(Content);
            managerEnemies.ForEach(e => e.LoadContent(Content));
			managerUI.LoadContent(Content);

			SelectRectangle.LoadContent(Content);
		}

		protected override void Update(GameTime gameTime)
		{
			if (Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			managerMouse.Update();
			managerPlayerUnits.Update();
			managerPlayerBuildings.Update();
            managerEnemies.ForEach(e => e.Update());
			managerUI.Update();

			//managerCombat.Update();

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
			managerPlayerUnits.Draw(spriteBatch);
			managerPlayerBuildings.Draw(spriteBatch);
			managerMouse.Draw(spriteBatch);
			spriteBatch.End();

			spriteBatch.Begin();
			managerUI.DrawBack(spriteBatch);
			managerUI.Draw(spriteBatch);
			managerPlayerUnits.DrawUI(spriteBatch);
			managerPlayerBuildings.DrawUI(spriteBatch);
			spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
