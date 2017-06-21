using System;
using System.Collections.Generic;

namespace Warcraft.Managers
{
    class ManagerEA
    {
		const int MAX_START = 4;
		const int MAX_RESOURCE = 3;
		const int MAX_COMBAT = 7;
		const int MAX_BUILDING = 3;
		const int MAX_DEFENSE = 4;

        public ManagerEA()
        {
            Random random = new Random();

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

			// [ [2, {5, 5, 5, 5}, {5, 5, 5}, {5, 5}], [5, 5, 5], [{5, 5, 5}, {5, 5, 5}] ]

			// Combat controller


			List<int[]> population = new List<int[]>();
            for (int i = 0; i < 10; i++)
            {
				int[] gene = new int[5];
				gene[0] = random.Next(0, MAX_START);
				gene[1] = random.Next(0, MAX_RESOURCE);
				gene[2] = random.Next(0, MAX_COMBAT);
				gene[3] = random.Next(0, MAX_BUILDING);
				gene[4] = random.Next(0, MAX_DEFENSE);

                population.Add(gene);
			}
        }
    }
}
