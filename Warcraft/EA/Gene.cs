using System;
namespace Warcraft.EA
{
    class Gene
    {
        public String[] peasantTownhall, peasantBarracks, peasantFarms, peasantMiner;
        public String[] townHall;
        public String[] barracksWarriors, barracksArchers;

        public float fitness;

        public Gene(String[][] genes)
        {
            peasantTownhall = genes[0];
            peasantBarracks = genes[1];
            peasantFarms = genes[2];
            peasantMiner = genes[3];

            townHall = genes[4];
            barracksWarriors = genes[5];
            barracksArchers = genes[6];
        }
    }
}
