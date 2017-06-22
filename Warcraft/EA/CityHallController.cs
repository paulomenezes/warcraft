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
		public ManagerUnits managerUnits;

		public CityHallController(int gold, int food, int mining, int index, ManagerMap managerMap)
        {
            GOLD = gold;
            FOOD = food;
            MINING = mining;

			this.index = index;
			this.managerMap = managerMap;
		}

		public String[][] GetGenes()
		{
			String[][] genes = new String[1][];

			genes[0] = new String[3];
			genes[0][0] = GeneticUtil.IntToBinary(GOLD, 12);
            genes[0][1] = GeneticUtil.IntToBinary(FOOD, 4);
            genes[0][2] = GeneticUtil.IntToBinary(MINING, 7);

			return genes;
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
