using System;
using Warcraft.Managers;

namespace Warcraft.EA
{
    class BarracksController
    {
		public int ARCHER_GOLD;
		public int ARCHER_FOOD;
		public int ARCHER_ARMY;
	
		public int WARRIOR_GOLD;
		public int WARRIOR_FOOD;
		public int WARRIOR_ARMY;

		private int index;
		private ManagerMap managerMap;
		private ManagerBuildings managerBuildings;
		private ManagerUnits managerUnits;

		public BarracksController(int index, ManagerMap managerMap, ManagerBuildings managerBuildings, ManagerUnits managerUnits)
		{
			this.index = index;
			this.managerMap = managerMap;
			this.managerBuildings = managerBuildings;
			this.managerUnits = managerUnits;
		}

		public void SetArcher(int gold, int food, int army)
		{
            ARCHER_GOLD = gold;
            ARCHER_FOOD = food;
            ARCHER_ARMY = army;
		}

		public void SetWarrior(int gold, int food, int army)
		{
            WARRIOR_GOLD = gold;
            WARRIOR_FOOD = food;
            WARRIOR_ARMY = army;
		}

		public bool BuildArcher()
		{
            return (ManagerResources.BOT_GOLD[index] >= ARCHER_GOLD && ManagerResources.BOT_FOOD[index] >= ARCHER_FOOD && Army() >= ARCHER_ARMY);
		}

		public bool BuildWarrior()
		{
            return (ManagerResources.BOT_GOLD[index] >= WARRIOR_GOLD && ManagerResources.BOT_FOOD[index] >= WARRIOR_FOOD && Army() >= WARRIOR_ARMY);
		}

		private int Army()
		{
			return managerUnits.units.FindAll(p => p.information.Type == Util.Units.GRUNT || p.information.Type == Util.Units.TROLL_AXETHROWER).Count;
		}
    }
}
