using System;
using System.Collections.Generic;
using Warcraft.Units;
using Warcraft.Units.Humans;
using Warcraft.Util;

namespace Warcraft.Managers
{
    class ManagerPlayerUnits : ManagerUnits
    {
        public ManagerPlayerUnits(ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings)
            : base(managerMouse, managerMap, managerBuildings)
		{
			managerMouse.MouseClickEventHandler += ManagerMouse_MouseClickEventHandler;

            units.Add(new Peasant(30, 23, managerMouse, managerMap, this, managerBuildings));
		}

		private void ManagerMouse_MouseClickEventHandler(object sender, Events.MouseClickEventArgs e)
		{
			List<Unit> selecteds = GetSelected();

			int threshold = (int)Math.Sqrt(selecteds.Count) / 2;
			int x = -threshold, y = x;
			for (int i = 0; i < selecteds.Count; i++)
			{
				selecteds[i].Move(e.XTile + x, e.YTile + y);
				x++;
				if (x > threshold)
				{
					x = -threshold;
					y++;
				}
			}
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
    }
}
