using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Warcraft.Units;
using Warcraft.Units.Humans;
using Warcraft.Util;

namespace Warcraft.Managers
{
    class ManagerBotsUnits : ManagerUnits
    {
        public ManagerBotsUnits(ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings, int index)
            : base(managerMouse, managerMap, managerBuildings)
        {
            this.index = index;

            Vector2 pos = Functions.CleanPosition(managerMap, 96, 96);
            units.Add(new Peon((int)pos.X / 32, (int)pos.Y / 32, managerMouse, managerMap, this, managerBuildings));
        }

        public override void Factory(Util.Units type, int x, int y, int targetX, int targetY)
        {
            if (type == Util.Units.PEASANT)
                units.Add(new Peasant(x, y, managerMouse, managerMap, this, managerBuildings));
            else if (type == Util.Units.ELVEN_ARCHER)
                units.Add(new ElvenArcher(x, y, managerMouse, managerMap, this));
            else if (type == Util.Units.FOOTMAN)
                units.Add(new Footman(x, y, managerMouse, managerMap, this));

            units[units.Count - 1].Move(targetX, targetY);

            LoadContent();
        }

        public override List<Unit> GetSelected()
        {
            List<Unit> selecteds = new List<Unit>(); ;
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i].selected)
                    selecteds.Add(units[i]);
            }

            return selecteds;
        }
    }
}