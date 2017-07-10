using Warcraft.Managers;
using Warcraft.Units;
using Warcraft.Util;

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

        ManagerUnits managerUnits;

        public BuilderUnits(Util.Units type, ManagerUnits managerUnits, InformationUnit informationUnit)
        {
            this.informationUnit = informationUnit;
            this.type = type;

            this.managerUnits = managerUnits;
        }

        public void execute()
        {
            if (!go && ManagerResources.CompareGold(managerUnits.index, informationUnit.CostGold) && ManagerResources.CompareFood(managerUnits.index, informationUnit.CostFood))
            {
                ManagerResources.ReduceGold(managerUnits.index, informationUnit.CostGold);
                ManagerResources.ReduceFood(managerUnits.index, informationUnit.CostFood);

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
