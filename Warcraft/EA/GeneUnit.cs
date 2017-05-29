using System;
namespace Warcraft.EA
{
    class GeneUnit : Gene
    {
        public Util.Buildings building;

        public GeneUnit(Util.Buildings building, int action) 
            : base(action)
        {
            this.building = building;
        }
    }
}
