namespace Warcraft.Util
{
    enum Race
    {
        NEUTRAL,
        HUMAN,
        ORC,
        HIGH_ELF,
        FOREST_TROLL
    }

    enum Faction
    {
        NEUTRAL,
        ALLIANCE,
        HORDE
    }

    class Information
    {
        public string Name;

        public float HitPoints;
        public float HitPointsTotal;

        public int CostGold;
		public int CostFood;
		public int CostWood;
        public int BuildTime;
    }
}
