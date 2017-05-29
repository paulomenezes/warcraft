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
        }
    }
}
