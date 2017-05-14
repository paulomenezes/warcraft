using Warcraft.Units;

namespace Warcraft.Commands
{
    class BuilderUnits : ICommand
    {
        public bool go;
        public bool completed;
        public bool remove;

        public int elapsed;
        public InformationUnit informationUnit;

        public Util.Units type;

        public BuilderUnits(Util.Units type, InformationUnit informationUnit)
        {
            this.informationUnit = informationUnit;
            this.type = type;
        }

        public void execute()
        {
            if (Warcraft.GOLD - informationUnit.CostGold >= 0 && Warcraft.FOOD - informationUnit.CostFood >= 0)
            {
                Warcraft.GOLD -= informationUnit.CostGold;
                Warcraft.FOOD -= informationUnit.CostFood;

                go = true;
                completed = false;
                remove = false;
            }
        }

        public void Update()
        {
            if (go)
            {
                elapsed++;
                if (elapsed > informationUnit.BuildTime)
                {
                    completed = true;
                    go = false;

                    elapsed = 0;
                }
            }
        }
    }
}
