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

        float elapsed = 0;
        public static int index = 0;
        int generation = 1;

        ManagerMap managerMap;
        ManagerMouse managerMouse;

        ContentManager content;
		Random random = new Random();

		public ManagerEA(int quantity, ManagerMouse managerMouse, ManagerMap managerMap)
        {
            this.managerMap = managerMap;
            this.managerMouse = managerMouse;

            int k = 0;
            for (int i = 0; i < 10; i++)
			{
				EA.PeasantController peasantController = new EA.PeasantController(i, managerMap);
				peasantController.SetTownHall(random.Next(0, 4));
				peasantController.SetBaracks(random.Next(0, 500), random.Next(0, 4), random.Next(0, 10), random.Next(0, 10));
				peasantController.SetFarms(random.Next(0, 500), random.Next(0, 4), random.Next(0, 10));
				peasantController.SetMiner(random.Next(0, 500), random.Next(0, 10));

				EA.CityHallController cityHallController = new EA.CityHallController(random.Next(0, 500), random.Next(0, 4), random.Next(0, 4), i, managerMap);

				EA.BarracksController barracksController = new EA.BarracksController(i, managerMap);
				barracksController.SetArcher(random.Next(0, 500), random.Next(0, 4), random.Next(0, 10));
				barracksController.SetWarrior(random.Next(0, 500), random.Next(0, 4), random.Next(0, 10));

                for (int j = 0; j < 2; j++)
                {
					ManagerResources.BOT_GOLD.Add(5000);
					ManagerResources.BOT_WOOD.Add(99999);
					ManagerResources.BOT_FOOD.Add(5);
					ManagerResources.BOT_OIL.Add(99999);

					managerEnemies.Add(new ManagerEnemies(managerMouse, managerMap, k, peasantController, cityHallController, barracksController));
                    k++;
				}
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

            if (elapsed >= 60000f)
            {
                managerMap.ResetWalls();
                ManagerBuildings.goldMines.ForEach(g => g.QUANITY = 10000);

                if (index > 0 && generation * 20 - 2 == index)
                {
					index += 2;
					Reproduce();
                }
                else 
                {
					index += 2;
				}

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
            List<String[]>[] genes = new List<string[]>[managerEnemies.Count];
            List<KeyValue> allFitness = new List<KeyValue>();

            float total = 0;

            for (int i = managerEnemies.Count - 20; i < managerEnemies.Count; i++)
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
                fitness *= 0.05f;
                fitness += managerEnemies[i].managerUnits.units.Count;
                fitness += managerEnemies[i].managerBuildings.buildings.Count;

                allFitness.Add(new KeyValue(i, fitness));

                total += fitness;
			}

            List<KeyValue> newAllFitness = new List<KeyValue>();
            for (int i = 0; i < allFitness.Count; i += 2)
            {
                allFitness[i].value += allFitness[i + 1].value;
                allFitness[i].value /= 2;

                newAllFitness.Add(allFitness[i]);
            }

            newAllFitness.Sort((x, y) => x.value.CompareTo(y.value));
            newAllFitness.Reverse();

            Data.Write("#######-----#######");
			Data.Write("Geração " + generation);
			Data.Write("Média " + (total / 10));
            String fPrint = "";
            newAllFitness.ForEach(f => fPrint += f.value.ToString() + ", ");
            Data.Write("Values " + fPrint);
			generation++;

            //List<String[]> parent01 = genes[allFitness[0].key];
            //List<String[]> parent02 = genes[allFitness[1].key];
            //         managerEnemies.Add(NewEnemy(parent01));
            //         managerEnemies.Add(NewEnemy(parent02));
            //String p1 = "", p2 = "";
            //parent01.ForEach(c => p1 += string.Join(",", c) + " - ");
            //parent02.ForEach(c => p2 += string.Join(",", c) + " - ");
            //Data.Write("B1: " + p1);
            //Data.Write("B2: " + p2);

            List<ManagerEnemies> newEnemies = new List<ManagerEnemies>();

            String el = "";
            genes[newAllFitness[0].key].ForEach(c => el += string.Join(",", c) + " - ");
            Data.Write("E: " + el);

			for (int i = 0; i < newAllFitness.Count / 2; i++)
            {
				String c1 = "", c2 = "";

                KeyValue[] parents = RouletteWheelSelection(newAllFitness);

                List<String[]> parent01 = genes[parents[0].key]; // genes[allFitness[i].key];
                List<String[]> parent02 = genes[parents[1].key]; // genes[allFitness[i + 1].key];

                //ManagerEnemies e1, e2;

                if (random.NextDouble() >= 0.2)
                {
                    int cut = random.Next(2, 5);

                    List<String[]> children01 = parent01.GetRange(0, cut);
                    children01.AddRange(parent02.GetRange(cut, 7 - cut));

                    List<String[]> children02 = parent02.GetRange(0, cut);
                    children02.AddRange(parent01.GetRange(cut, 7 - cut));

                    children01.ForEach(c => c1 += string.Join(",", c) + " - ");
                    children02.ForEach(c => c2 += string.Join(",", c) + " - ");
                    Data.Write(c1);
                    Data.Write(c2);

                    children01 = Mutate(children01);
                    children02 = Mutate(children02);

					newEnemies.Add(NewEnemy(children01));
					newEnemies.Add(NewEnemy(children01));

					newEnemies.Add(NewEnemy(children02));
					newEnemies.Add(NewEnemy(children02));

                    //e1 = NewEnemy(children01, true); //newEnemies.Add(NewEnemy(children01, true));
                    //e2 = NewEnemy(children02, true); //newEnemies.Add(NewEnemy(children02, true));
                }
                else
                {
                    parent01.ForEach(c => c1 += string.Join(",", c) + " - ");
                    parent02.ForEach(c => c2 += string.Join(",", c) + " - ");

                    Data.Write(c1);
					Data.Write(c2);

					parent01 = Mutate(parent01);
					parent02 = Mutate(parent02);

					newEnemies.Add(NewEnemy(parent01));
					newEnemies.Add(NewEnemy(parent01));

					newEnemies.Add(NewEnemy(parent02));
					newEnemies.Add(NewEnemy(parent02));

                    //e1 = NewEnemy(parent01, true); //newEnemies.Add(NewEnemy(parent01, true));
                    //e2 = NewEnemy(parent02, true); //newEnemies.Add(NewEnemy(parent02, true));
				}

				//newEnemies.Add(e1);
				//newEnemies.Add(e1);
				//newEnemies.Add(e2);
				//newEnemies.Add(e2);
			}

            newEnemies.Insert(0, NewEnemy(genes[allFitness[0].key]));
            newEnemies.Insert(1, NewEnemy(genes[allFitness[0].key]));

			newEnemies.RemoveAt(newEnemies.Count - 1);
			newEnemies.RemoveAt(newEnemies.Count - 1);

            managerEnemies.AddRange(newEnemies);
        }

		private KeyValue[] RouletteWheelSelection(List<KeyValue> enemies)
		{
			float[] fitness = new float[enemies.Count];
			for (int i = 0; i < fitness.Length; i++)
			{
				if (i == 0)
                    fitness[i] = enemies[i].value;
				else
                    fitness[i] = fitness[i - 1] + enemies[i].value;
			}

			Random random = new Random();
			double value01 = random.NextDouble() * fitness[fitness.Length - 1];
			double value02 = random.NextDouble() * fitness[fitness.Length - 1];

			int[] index = new int[2];
			index[0] = -1;
			index[1] = -1;

			for (int i = 0; i < fitness.Length; i++)
			{
				if (index[0] == -1 && fitness[i] > value01)
					index[0] = i;

				if (index[1] == -1 && fitness[i] > value02)
					index[1] = i;

				if (index[0] != -1 && index[1] != -1)
					break;
			}

			if (index[0] == index[1])
				index = noRepeat(index, fitness);

			return new KeyValue[2] { enemies[index[0]], enemies[index[1]] };
		}

		private static int[] noRepeat(int[] indexes, float[] fitness)
		{
			Random random = new Random();

			while (indexes[0] == indexes[1])
			{
				indexes[1] = -1;

				double value01 = random.NextDouble() * fitness[fitness.Length - 1];

				for (int i = 0; i < fitness.Length; i++)
				{
					if (indexes[1] == -1 && fitness[i] > value01)
						indexes[1] = i;

					if (indexes[1] != -1)
						break;
				}
			}

			return indexes;
		}

        public List<ManagerEnemies> Shuffle(List<ManagerEnemies> list)
		{
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = random.Next(n + 1);
                ManagerEnemies value = list[k];
				list[k] = list[n];
				list[n] = value;
			}

            return list;
		}

        private List<String[]> Mutate(List<String[]> children01)
        {
            for (int i = 0; i < children01.Count; i++)
            {
                for (int j = 0; j < children01[i].Length; j++)
                {
                    char[] chars = children01[i][j].ToCharArray();

                    for (int k = 0; k < chars.Length; k++)
                    {
                        if (random.NextDouble() <= 0.2)
                        {
                            if (chars[k].Equals("1"))
                            {
                                chars[k] = '0';
                            }
                            else
                            {
                                chars[k] = '1';
                            }
                        }
                    }

                    children01[i][j] = new string(chars);
                }
            }

            return children01;
        }

        private ManagerEnemies NewEnemy(List<String[]> children01)
        {
            ManagerResources.BOT_GOLD.Add(5000);
			ManagerResources.BOT_WOOD.Add(99999);
			ManagerResources.BOT_FOOD.Add(5);
			ManagerResources.BOT_OIL.Add(99999);

            int newIndex = ManagerResources.BOT_GOLD.Count - 1;

            EA.PeasantController peasantController = new EA.PeasantController(newIndex, managerMap);
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
                                                                                 EA.GeneticUtil.BinaryToInt(children01[4][2]), newIndex, managerMap);

            EA.BarracksController barracksController = new EA.BarracksController(newIndex, managerMap);
            barracksController.SetArcher(EA.GeneticUtil.BinaryToInt(children01[5][0]),
                                         EA.GeneticUtil.BinaryToInt(children01[5][1]),
                                         EA.GeneticUtil.BinaryToInt(children01[5][2]));

            barracksController.SetWarrior(EA.GeneticUtil.BinaryToInt(children01[6][0]),
                                          EA.GeneticUtil.BinaryToInt(children01[6][1]),
                                          EA.GeneticUtil.BinaryToInt(children01[6][2]));

            ManagerEnemies newEnemy = new ManagerEnemies(managerMouse, managerMap, newIndex, peasantController, cityHallController, barracksController);
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
