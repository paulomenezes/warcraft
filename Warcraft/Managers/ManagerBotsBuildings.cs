using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Warcraft.Buildings;
using Warcraft.Buildings.Neutral;
using Warcraft.Util;

namespace Warcraft.Managers
{
    class ManagerBotsBuildings : ManagerBuildings
    {
        public ManagerBotsBuildings(ManagerMouse managerMouse, ManagerMap managerMap, int index)
            : base(managerMouse, managerMap)
        {
            this.index = index;

            Vector2 pos = Functions.CleanPosition(managerMap, 96, 96);
            buildings.Add(new GoldMine((int)pos.X / 32, (int)pos.Y / 32, managerMouse, managerMap, null));
        }

        public override List<Building> GetSelected()
        {
            return new List<Building>();
        }
    }
}
