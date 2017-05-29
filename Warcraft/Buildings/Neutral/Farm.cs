using System;
using Warcraft.Managers;

namespace Warcraft.Buildings.Neutral
{
    class Farm : Building
    {
        public Farm(int tileX, int tileY, int width, int height, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) 
            : base(tileX, tileY, width, height, managerMouse, managerMap, managerUnits)
        {
			ui = new UI.Buildings.Farm(managerMouse, this);
		}
    }
}
