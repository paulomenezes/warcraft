using System;
using System.Collections.Generic;
using Warcraft.Buildings;
using Warcraft.Buildings.Neutral;

namespace Warcraft.Managers
{
    class ManagerPlayerBuildings : ManagerBuildings
    {
		public ManagerPlayerBuildings(ManagerMouse managerMouse, ManagerMap managerMap)
			: base(managerMouse, managerMap)
		{
            buildings.Add(new GoldMine(30, 18, managerMouse, managerMap, null));
		}

        public override List<Building> GetSelected()
        {
			List<Building> selecteds = new List<Building>(); ;
			for (int i = 0; i < buildings.Count; i++)
			{
				if (buildings[i].selected)
					selecteds.Add(buildings[i]);
			}

			return selecteds;
		}
    }
}
