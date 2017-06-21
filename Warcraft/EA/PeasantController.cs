using System;
using Microsoft.Xna.Framework;
using Warcraft.Buildings;
using Warcraft.Buildings.Neutral;
using Warcraft.Managers;
using Warcraft.Units.Humans;
using Warcraft.Util;

namespace Warcraft.EA
{
    class PeasantController
    {
        public int TOWN_HALL;

        public int BARRACKS_GOLD;
        public int BARRACKS_FOOD;
        public int BARRACKS_ARMY;
		public int BARRACKS_IDLE;

		public int FARMS_GOLD;
		public int FARMS_FOOD;
		public int FARMS_IDLE;

		public int MINER_GOLD;
		public int MINER_IDLE;

        private int index;
        private ManagerMap managerMap;
        private ManagerBuildings managerBuildings;
        private ManagerUnits managerUnits;

        public PeasantController(int index, ManagerMap managerMap, ManagerBuildings managerBuildings, ManagerUnits managerUnits)
        {
            this.index = index;
            this.managerMap = managerMap;
            this.managerBuildings = managerBuildings;
            this.managerUnits = managerUnits;
        }

        public void SetTownHall(int townHall) 
        {
            TOWN_HALL = townHall;
        }

        public void SetBaracks(int gold, int food, int army, int idle) 
        {
            BARRACKS_GOLD = gold;
            BARRACKS_FOOD = food;
            BARRACKS_ARMY = army;
            BARRACKS_IDLE = idle;
        }

        public void MultiplyBarracks(int times)
        {
			BARRACKS_GOLD *= times;
			BARRACKS_FOOD *= times;
			BARRACKS_ARMY *= times;
			BARRACKS_IDLE *= times;
		}

        public void SetFarms(int gold, int food, int idle) 
        {
            FARMS_GOLD = gold;
            FARMS_FOOD = food;
            FARMS_IDLE = idle;
        }

        public void MultiplyFarms(int times)
        {
			FARMS_GOLD *= times;
			FARMS_FOOD *= times;
			FARMS_IDLE *= times;
		}

        public void SetMiner(int gold, int idle) 
        {
            MINER_GOLD = gold;
            MINER_IDLE = idle;
        }

        public Vector2 BuildTownHall(Builder builder)
		{
            switch (TOWN_HALL) {
                case 1:
					return NearGoldMine(builder).position + new Vector2(0, 4 * 32);
				case 2:
                    return (ManagerBuildings.goldMines[0].position + ManagerBuildings.goldMines[1].position) / 2;
				case 3:
                    return (ManagerBuildings.goldMines[0].position + ManagerBuildings.goldMines[1].position + ManagerBuildings.goldMines[2].position) / 3;
                default:
                    return Functions.CleanPosition(managerMap, 128, 128);
            }
        }

        public bool BuildBarracks()
        {
            return (ManagerResources.BOT_GOLD[index] >= BARRACKS_GOLD && ManagerResources.BOT_FOOD[index] <= BARRACKS_FOOD && PeonIdle() >= BARRACKS_IDLE && Army() >= BARRACKS_ARMY);
		}

		public bool BuildFarms()
		{
            return (ManagerResources.BOT_GOLD[index] >= FARMS_GOLD && ManagerResources.BOT_FOOD[index] <= FARMS_FOOD && PeonIdle() >= FARMS_IDLE);
		}

		public bool Miner()
		{
            return (ManagerResources.BOT_GOLD[index] >= MINER_GOLD && PeonIdle() >= MINER_IDLE);
		}

        private GoldMine NearGoldMine(Builder builder)
        {
            GoldMine goldMine = null;

			float maxDistance = float.MaxValue;
			for (int i = 0; i < ManagerBuildings.goldMines.Count; i++)
			{
				float distance = Vector2.Distance(ManagerBuildings.goldMines[i].Position, builder.Position);
				if (distance < maxDistance && ManagerBuildings.goldMines[i].QUANITY > 0)
				{
					maxDistance = distance;
					goldMine = ManagerBuildings.goldMines[i];
				}
			}

            return goldMine;
		}

        private int PeonIdle() 
        {
            return managerUnits.units.FindAll(p => p.information.Type == Util.Units.PEON && p.workState == Units.WorkigState.NOTHING).Count;
        }

        private int Army() 
        {
            return managerUnits.units.FindAll(p => p.information.Type == Util.Units.GRUNT || p.information.Type == Util.Units.TROLL_AXETHROWER).Count;
        }
    }
}
