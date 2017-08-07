﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Util;

namespace Warcraft.Managers
{
    class ManagerEA
    {
        public List<ManagerEnemies> managerEnemies = new List<ManagerEnemies>();

        public static int index = 0;

        ManagerMap managerMap;
        ManagerMouse managerMouse;

        ContentManager content;
		Random random = new Random();

		public ManagerEA(int quantity, ManagerMouse managerMouse, ManagerMap managerMap)
        {
            this.managerMap = managerMap;
            this.managerMouse = managerMouse;

            for (int i = 0; i < quantity; i++)
			{
				ManagerResources.BOT_GOLD.Add(5000);
				ManagerResources.BOT_WOOD.Add(99999);
				ManagerResources.BOT_FOOD.Add(5);
				ManagerResources.BOT_OIL.Add(99999);

				EA.PeasantController peasantController = new EA.PeasantController(i, managerMap);
				peasantController.SetTownHall(random.Next(0, 4));
				peasantController.SetBaracks(random.Next(0, 500), random.Next(0, 4), random.Next(0, 10), random.Next(0, 10));
				peasantController.SetFarms(random.Next(0, 500), random.Next(0, 4), random.Next(0, 10));
				peasantController.SetMiner(random.Next(0, 500), random.Next(0, 10));

				EA.CityHallController cityHallController = new EA.CityHallController(random.Next(0, 500), random.Next(0, 4), random.Next(0, 4), i, managerMap);

				EA.BarracksController barracksController = new EA.BarracksController(i, managerMap);
				barracksController.SetArcher(random.Next(0, 500), random.Next(0, 4), random.Next(0, 10));
				barracksController.SetWarrior(random.Next(0, 500), random.Next(0, 4), random.Next(0, 10));

                managerEnemies.Add(new ManagerEnemies(managerMouse, managerMap, i));
			}
		}

		public void LoadContent(ContentManager content)
		{
            this.content = content;
            managerEnemies.ForEach(e => e.LoadContent(content));
		}

        public void Update(GameTime gameTime) 
        {
            for (int i = 0; i < managerEnemies.Count; i++)
            {
                managerEnemies[i].Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
			for (int i = 0; i < managerEnemies.Count; i++)
			{
                managerEnemies[i].Draw(spriteBatch);
			}
		}

		public void DrawUI(SpriteBatch spriteBatch)
		{
			for (int i = 0; i < managerEnemies.Count; i++)
			{
                managerEnemies[i].DrawUI(spriteBatch);
			}
		}
    }
}
