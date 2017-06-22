using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Warcraft.Managers
{
    class ManagerEA
    {
        public List<ManagerEnemies> managerEnemies = new List<ManagerEnemies>();

        float elapsed = 0;
        int index = 0;

        public ManagerEA(int quantity, ManagerMouse managerMouse, ManagerMap managerMap)
        {
            Random random = new Random();
            for (int i = 0; i < 10; i++)
			{
				ManagerResources.BOT_GOLD.Add(5000);
				ManagerResources.BOT_WOOD.Add(99999);
				ManagerResources.BOT_FOOD.Add(5);
				ManagerResources.BOT_OIL.Add(99999);

				EA.PeasantController peasantController = new EA.PeasantController(i, managerMap);
				peasantController.SetTownHall(random.Next(0, 4));
				peasantController.SetBaracks(random.Next(0, 2000), random.Next(0, 10), random.Next(0, 100), random.Next(0, 100));
				peasantController.SetFarms(random.Next(0, 2000), random.Next(0, 10), random.Next(0, 100));
				peasantController.SetMiner(random.Next(0, 2000), random.Next(0, 100));

				EA.CityHallController cityHallController = new EA.CityHallController(random.Next(0, 2000), random.Next(0, 10), random.Next(0, 10), i, managerMap);

				EA.BarracksController barracksController = new EA.BarracksController(i, managerMap);
				barracksController.SetArcher(random.Next(0, 2000), random.Next(0, 10), random.Next(0, 100));
				barracksController.SetWarrior(random.Next(0, 2000), random.Next(0, 10), random.Next(0, 100));

                managerEnemies.Add(new ManagerEnemies(managerMouse, managerMap, i, peasantController, cityHallController, barracksController));
			}

			// Peasants controller
			// 1 - Build Town Hall [Randomly or Near or Middle 1 or Middle 2]
			// 2 - Build Barracks [Gold > X and Food > Y and Army.length < Z and Peasant.idle > W]
			// 3 - Build Farms [Gold > X and Food > Y and Peasant.idle > Z]
			// 4 - Go Miner [Gold > X and Peasant.idle > Z]
			// [TownHall, Barracks, Farms, Miner]
			// [{TownHall}, {X, Y, Z, W}, {X, Y, Z}, {X, Z}]
			// bits = [2, {5, 5, 5, 5}, {5, 5, 5}, {5, 5}]

			// TownHall controller
			// Build Worker [Gold > X and Food > Y and Peasant.mining < W]
			// bits = [5, 5, 5]

			// Barracks controller
			// Build Warrior [Gold > X and Food > Y and Army.length < Z]
			// Build Archer [Gold > X and Food > Y and Army.length < Z]
			// bits = [{5, 5, 5}, {5, 5, 5}]

            // [ [{2}, {5, 5, 5, 5}, {5, 5, 5}, {5, 5}], [5, 5, 5], [{5, 5, 5}, {5, 5, 5}] ]

			// Combat controller
		}

		public void LoadContent(ContentManager content)
		{
            managerEnemies.ForEach(e => e.LoadContent(content));
		}

        public void Update(GameTime gameTime) 
        {
            elapsed += gameTime.ElapsedGameTime.Milliseconds;

            if (elapsed >= 10000f)
            {
                if (index == 8)
                {
                    Reproduce();
                }
                else
                {
                    index += 2;
                    elapsed = 0;
                }
            }
            else
            {
                for (int i = index; i < index + 2; i++)
                {
                    managerEnemies[i].Update();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
			for (int i = index; i < index + 2; i++)
			{
                managerEnemies[i].Draw(spriteBatch);
			}
		}

		public void DrawUI(SpriteBatch spriteBatch)
		{
			for (int i = index; i < index + 2; i++)
			{
                managerEnemies[i].DrawUI(spriteBatch);
			}
		}

        private void Reproduce()
        {
            List<String[]>[] genes = new List<string[]>[managerEnemies.Count];
            List<KeyValue> allFitness = new List<KeyValue>();

			for (int i = 0; i < managerEnemies.Count; i++)
			{
				String[][] genes01 = managerEnemies[i].peasantController.GetGenes();
				String[][] genes02 = managerEnemies[i].cityHallController.GetGenes();
				String[][] genes03 = managerEnemies[i].barracksController.GetGenes();

                genes[i] = new List<string[]>();
				genes[i].AddRange(genes01);
				genes[i].AddRange(genes02);
				genes[i].AddRange(genes03);

                float fitness = 0;

				managerEnemies[i].managerUnits.units.ForEach(u => fitness += u.information.Fitness);
                fitness += managerEnemies[i].managerUnits.units.Count;
                fitness += managerEnemies[i].managerBuildings.buildings.Count;

                allFitness.Add(new KeyValue(i, fitness));
			}

            allFitness.Sort((x, y) => x.value.CompareTo(y.value));
		}

        class KeyValue {
            public int key; 
            public float value;
            public KeyValue(int k, float v)
            {
                key = k;
                value = v;
            }
        }
    }
}
