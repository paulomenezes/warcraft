using Warcraft.Util;

namespace Warcraft.Buildings
{
    class InformationBuilding : Information
    {
        public Util.Buildings Type;
        public Util.Units Creates;

        public InformationBuilding(string name, int hitPoints, int costGold, int costFood, Util.Units creates, int buildTime, Util.Buildings type)
        {
            Name = name;

            HitPoints = hitPoints;
            HitPointsTotal = hitPoints;

            CostGold = costGold;
            CostFood = costFood;
            Creates = creates;
            BuildTime = buildTime;

            Type = type;
        }
    }
}
