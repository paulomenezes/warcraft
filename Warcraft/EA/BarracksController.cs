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
		public ManagerUnits managerUnits;

		public BarracksController(int index, ManagerMap managerMap)
		{
			this.index = index;
			this.managerMap = managerMap;
		}

		public String[][] GetGenes()
		{
			String[][] genes = new String[2][];

			genes[0] = new String[3];
			genes[0][0] = GeneticUtil.IntToBinary(WARRIOR_GOLD, 12);
            genes[0][1] = GeneticUtil.IntToBinary(WARRIOR_FOOD, 4);
            genes[0][2] = GeneticUtil.IntToBinary(WARRIOR_ARMY, 7);

			genes[1] = new String[3];
            genes[1][0] = GeneticUtil.IntToBinary(ARCHER_GOLD, 12);
            genes[1][1] = GeneticUtil.IntToBinary(ARCHER_FOOD, 4);
            genes[1][2] = GeneticUtil.IntToBinary(ARCHER_ARMY, 7);

			return genes;
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
