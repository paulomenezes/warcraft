using System;
using Warcraft.Managers;

namespace Warcraft.EA
{
    class CityHallController
    {
        public int GOLD;
        public int FOOD;
        public int MINING;
		
        private int index;
		private ManagerMap managerMap;
		private ManagerBuildings managerBuildings;
		private ManagerUnits managerUnits;

		public CityHallController(int gold, int food, int mining, int index, ManagerMap managerMap, ManagerBuildings managerBuildings, ManagerUnits managerUnits)
        {
            GOLD = gold;
            FOOD = food;
            MINING = mining;

			this.index = index;
			this.managerMap = managerMap;
			this.managerBuildings = managerBuildings;
			this.managerUnits = managerUnits;
		}
		
        public bool BuildPeon()
		{
            return (ManagerResources.BOT_GOLD[index] >= GOLD && ManagerResources.BOT_FOOD[index] >= FOOD && PeonMining() >= MINING);
		}

		private int PeonMining()
		{
			return managerUnits.units.FindAll(p => p.information.Type == Util.Units.PEON && p.workState == Units.WorkigState.NOTHING).Count;
		}

	}
}
