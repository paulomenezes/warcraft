using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Warcraft.Buildings;
using Warcraft.Buildings.Neutral;
using Warcraft.Buildings.Orcs;
using Warcraft.Map;
using Warcraft.Util;

namespace Warcraft.Managers
{
    class ManagerBotsBuildings : ManagerBuildings
    {
        public ManagerBotsBuildings(ManagerMouse managerMouse, ManagerMap managerMap, int index)
            : base(managerMouse, managerMap)
        {
            this.index = index;

			int minX = 999, minY = 999;
			for (int i = 0; i < managerMap.FULL_MAP.Count; i++)
			{
				for (int j = 0; j < managerMap.FULL_MAP[i].Count; j++)
				{
					if (managerMap.FULL_MAP[i][j].tileType != TileType.WATER && !managerMap.FULL_MAP[i][j].isWall && !managerMap.FULL_MAP[i][j].isWater && j < minY)
					{
						minX = i;
						minY = j;
					}
				}
			}

            this.buildings.Add(new DarkPortal(minX, minY, managerMouse, managerMap, null));
        }
    }
}
