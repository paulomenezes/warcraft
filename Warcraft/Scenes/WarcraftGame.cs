using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Managers;
using Warcraft.UI;
using Warcraft.Units.Humans;

namespace Warcraft.Scenes
{
    class WarcraftGame : Scene
	{
		ManagerIsland managerIsland;
		// ManagerMap managerMap;
		ManagerMouse managerMouse = new ManagerMouse();

		ManagerUI managerUI;
		ManagerEnemies managerEnemies;

		public ManagerUnits managerPlayerUnits;
		ManagerBuildings managerPlayerBuildings;

		ManagerCombat managerCombat;

		// GenerateRooms rooms;
		float increment = 0.1f;
		float fadeOut = 0;

		Texture2D cursor;

		bool showSummary = false;
		Summary summary = new Summary();

		bool start = false;

        public WarcraftGame()
        {
            
		}

		public override void Initializer()
		{
			ManagerResources.PLAYER_FOOD = 5;

			managerIsland = new ManagerIsland(managerMouse);

			managerEnemies = new ManagerEnemies(managerMouse, managerIsland.CurrentMap(), 0);

			managerPlayerBuildings = new ManagerPlayerBuildings(managerMouse, managerIsland.CurrentMap());
			managerPlayerUnits = new ManagerPlayerUnits(managerMouse, managerIsland.CurrentMap(), managerPlayerBuildings, managerEnemies);

			managerUI = new ManagerUI(managerMouse, managerPlayerBuildings, managerPlayerUnits, null);
			managerCombat = new ManagerCombat(managerEnemies, managerPlayerUnits, managerPlayerBuildings);

			increment = 0.1f;
			fadeOut = 0;
			showSummary = false;
			Battleship.move = false;

            Warcraft.camera.Start();
		}

		public override void LoadContent(ContentManager Content)
		{
			managerIsland.LoadContent(Content);
			managerPlayerUnits.LoadContent(Content);
			managerPlayerBuildings.LoadContent(Content);
			managerUI.LoadContent(Content);
			managerEnemies.LoadContent(Content);

			SelectRectangle.LoadContent(Content);

			cursor = Content.Load<Texture2D>("Cursor");

			managerMouse.MouseEventHandler += (sender, e) =>
			{
				if (!start)
				{
					start = true;
					increment = 0.1f;
					fadeOut = 0;
				}

				if (showSummary)
				{
					showSummary = false;
					Battleship.move = false;
					ManagerBuildings.goldMines.Clear();

                    Initializer();
                    LoadContent(Content);
				}
			};
		}

        public override void Update()
		{
            //if (IsActive)
                Warcraft.camera.Update();
            
			if (start)
			{
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
			}
			else
			{
				managerMouse.Update();
			}

		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, Warcraft.camera.transform);
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


			if (Battleship.move || !start)
			{
				spriteBatch.Draw(cursor, new Rectangle(0, 0, Warcraft.WINDOWS_WIDTH, Warcraft.WINDOWS_HEIGHT), new Color(0, 0, 0, fadeOut));

				if (!start)
				{
                    spriteBatch.Draw(cursor, new Rectangle(0, 0, Warcraft.WINDOWS_WIDTH, Warcraft.WINDOWS_HEIGHT), Color.Black);
					summary.DrawStart(spriteBatch);
				}

				if (showSummary)
				{
					summary.Draw(spriteBatch, managerPlayerUnits, managerEnemies.managerUnits);
				}
			}

			spriteBatch.End();
		}
    }
}
