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
        public static int index = 0;

        ManagerMap managerMap;
        ManagerMouse managerMouse;

        ContentManager content;
		Random random = new Random();

		public ManagerEA(int quantity, ManagerMouse managerMouse, ManagerMap managerMap)
        {
            this.managerMap = managerMap;
            this.managerMouse = managerMouse;

            for (int i = 0; i < 10; i++)
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
            this.content = content;
            managerEnemies.ForEach(e => e.LoadContent(content));
		}

        public void Update(GameTime gameTime) 
        {
            elapsed += gameTime.ElapsedGameTime.Milliseconds;

            if (elapsed >= 50000f)
            {
                if (index > 0 && index % 8 == 0)
                {
                    Reproduce();
                }

                index += 2;
                elapsed = 0;
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
            ManagerBuildings.goldMines.ForEach(g => g.QUANITY = 10000);

            List<String[]>[] genes = new List<string[]>[managerEnemies.Count];
            List<KeyValue> allFitness = new List<KeyValue>();

            for (int i = managerEnemies.Count - 10; i < managerEnemies.Count; i++)
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
                fitness += ManagerResources.BOT_FOOD[managerEnemies[i].index];

                allFitness.Add(new KeyValue(i, fitness));
			}

            allFitness.Sort((x, y) => x.value.CompareTo(y.value));
            allFitness.Reverse();

            for (int i = 0; i < allFitness.Count; i += 2)
            {
                List<String[]> parent01 = genes[allFitness[i].key];
                List<String[]> parent02 = genes[allFitness[i + 1].key];

                List<String[]> children01 = parent01.GetRange(0, 4);
                children01.AddRange(parent02.GetRange(4, 3));

                List<String[]> children02 = parent02.GetRange(0, 4);
                children02.AddRange(parent01.GetRange(4, 3));

				String p1 = "", p2 = "", c1 = "", c2 = "";
                parent01.ForEach(c => p1 += string.Join(",", c) + " - ");
                parent02.ForEach(c => p2 += string.Join(",", c) + " - ");
                children01.ForEach(c => c1 += string.Join(",", c) + " - ");
				children02.ForEach(c => c2 += string.Join(",", c) + " - ");

                managerEnemies.Add(NewEnemy(children01));
                managerEnemies.Add(NewEnemy(children02));
			}
        }

        private ManagerEnemies NewEnemy(List<String[]> children01)
        {
            for (int i = 0; i < children01.Count; i++)
            {
                for (int j = 0; j < children01[i].Length; j++)
                {
                    if (random.NextDouble() <= 0.2)
                    {
                        if (children01[i][j].Equals("1"))
                        {
                            children01[i][j] = "0";
                        }
                        else
                        {
                            children01[i][j] = "1";   
                        }
                    }
                }
            }

            ManagerResources.BOT_GOLD.Add(5000);
			ManagerResources.BOT_WOOD.Add(99999);
			ManagerResources.BOT_FOOD.Add(5);
			ManagerResources.BOT_OIL.Add(99999);

            int index = ManagerResources.BOT_GOLD.Count - 1;

            EA.PeasantController peasantController = new EA.PeasantController(index, managerMap);
            peasantController.SetTownHall(EA.GeneticUtil.BinaryToInt(children01[0][0]));

            peasantController.SetBaracks(EA.GeneticUtil.BinaryToInt(children01[1][0]),
                                         EA.GeneticUtil.BinaryToInt(children01[1][1]),
                                         EA.GeneticUtil.BinaryToInt(children01[1][2]),
                                         EA.GeneticUtil.BinaryToInt(children01[1][3]));

            peasantController.SetFarms(EA.GeneticUtil.BinaryToInt(children01[2][0]),
                                       EA.GeneticUtil.BinaryToInt(children01[2][1]),
                                       EA.GeneticUtil.BinaryToInt(children01[2][2]));

            peasantController.SetMiner(EA.GeneticUtil.BinaryToInt(children01[3][0]), EA.GeneticUtil.BinaryToInt(children01[3][1]));

            EA.CityHallController cityHallController = new EA.CityHallController(EA.GeneticUtil.BinaryToInt(children01[4][0]),
                                                                                 EA.GeneticUtil.BinaryToInt(children01[4][1]),
                                                                                 EA.GeneticUtil.BinaryToInt(children01[4][2]), index, managerMap);

            EA.BarracksController barracksController = new EA.BarracksController(index, managerMap);
            barracksController.SetArcher(EA.GeneticUtil.BinaryToInt(children01[5][0]),
                                         EA.GeneticUtil.BinaryToInt(children01[5][1]),
                                         EA.GeneticUtil.BinaryToInt(children01[5][2]));

            barracksController.SetWarrior(EA.GeneticUtil.BinaryToInt(children01[6][0]),
                                          EA.GeneticUtil.BinaryToInt(children01[6][1]),
                                          EA.GeneticUtil.BinaryToInt(children01[6][2]));

            ManagerEnemies newEnemy = new ManagerEnemies(managerMouse, managerMap, index, peasantController, cityHallController, barracksController);
            newEnemy.LoadContent(content);

            return newEnemy;
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
