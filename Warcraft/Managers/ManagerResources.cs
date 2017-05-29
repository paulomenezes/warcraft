using System;
using System.Collections.Generic;
using Warcraft.Units;
using Warcraft.Units.Humans;

namespace Warcraft.Managers
{
    class ManagerResources
    {
		public static int PLAYER_GOLD = 5000;
		public static int PLAYER_WOOD = 99999;
		public static int PLAYER_FOOD = 5;
		public static int PLAYER_OIL = 99999;

        public static List<int> BOT_GOLD = new List<int>();
        public static List<int> BOT_WOOD = new List<int>();
        public static List<int> BOT_FOOD = new List<int>();
        public static List<int> BOT_OIL = new List<int>();

		public static bool CompareGold(int index, int quantity)
		{
			return ((index == -1 && PLAYER_GOLD >= quantity) || (index > -1 && BOT_GOLD[index] >= quantity));
		}

		public static bool CompareFood(int index, int quantity)
		{
			return ((index == -1 && PLAYER_FOOD >= quantity) || (index > -1 && BOT_FOOD[index] >= quantity));
		}

		public static void ReduceGold(int index, int quantity)
		{
            if (index == -1)
                PLAYER_GOLD -= quantity;

            if (index > -1)
                BOT_GOLD[index] -= quantity;
		}

		public static void ReduceFood(int index, int quantity)
		{
			if (index == -1)
                PLAYER_FOOD -= quantity;

            if (index > -1)
                BOT_FOOD[index] -= quantity;
		}
	}
}
