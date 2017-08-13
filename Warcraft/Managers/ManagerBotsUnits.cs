using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Warcraft.Units;
using Warcraft.Units.Humans;
using Warcraft.Units.Orcs;
using Warcraft.Util;

namespace Warcraft.Managers
{
    class ManagerBotsUnits : ManagerUnits
    {
        public ManagerBotsUnits(ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings, int index)
            : base(managerMouse, managerMap, managerBuildings)
        {
            this.index = index;

            Vector2 goldMinePos = Functions.CleanHalfPosition(managerMap, ManagerBuildings.goldMines[1].position);
			units.Add(new Peon(Functions.TilePos(goldMinePos.X), Functions.TilePos(goldMinePos.Y), managerMouse, managerMap, this, managerBuildings));

            units.Add(new Units.Neutral.Skeleton(Functions.TilePos(goldMinePos.X), Functions.TilePos(goldMinePos.Y), managerMouse, managerMap, this));
        }

        public override void Factory(Util.Units type, int x, int y, int targetX, int targetY)
        {
            if (type == Util.Units.PEON)
                units.Add(new Peon(x, y, managerMouse, managerMap, this, managerBuildings));
            else if (type == Util.Units.TROLL_AXETHROWER)
                units.Add(new TrollAxethrower(x, y, managerMouse, managerMap, this));
            else if (type == Util.Units.GRUNT)
                units.Add(new Grunt(x, y, managerMouse, managerMap, this));

            units[units.Count - 1].Move(targetX, targetY);

            LoadContent();
        }
    }
}