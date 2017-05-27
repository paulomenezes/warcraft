using System;
using Microsoft.Xna.Framework;
using Warcraft.Managers;

namespace Warcraft.Units.Orcs.Actions
{
    class PeonBuilding
    {
		ManagerMouse managerMouse;
		ManagerMap managerMap;
		ManagerUnits managerUnits;

		public PeonBuilding(ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits)
        {
			this.managerMap = managerMap;
			this.managerMouse = managerMouse;
            this.managerUnits = managerUnits;
        }

        public Buildings.Building builder(Util.Buildings type) {
            Buildings.Building building = Buildings.Building.Factory(type, managerMouse, managerMap, managerUnits);
            building.Position = new Vector2(32, 32);
            building.StartBuilding();

            return building;
        }
    }
}
