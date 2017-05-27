using Microsoft.Xna.Framework;
using System;

namespace Warcraft.Managers
{
    class ManagerCombat
    {
        ManagerUnits managerUnits;
        ManagerEnemies managerEnemies;
        ManagerBuildings managerBuildings;

        public ManagerCombat(ManagerUnits managerUnits, ManagerEnemies managerEnemies, ManagerBuildings managerBuildings)
        {
            this.managerUnits = managerUnits;
            this.managerEnemies = managerEnemies;
            this.managerBuildings = managerBuildings;
        }

        public void Update()
        {
            //for (int u = 0; u < managerUnits.units.Count; u++)
            //{
            //    for (int e = 0; e < managerEnemies.enemies.Count; e++)
            //    {
            //        float angle = MathHelper.ToDegrees((float)(Math.Atan2(managerUnits.units[u].position.Y - managerEnemies.enemies[e].position.Y,
            //                                                              managerUnits.units[u].position.X - managerEnemies.enemies[e].position.X)));
                    
            //        float distance = Vector2.Distance(managerUnits.units[u].position, managerEnemies.enemies[e].position);

            //        if (distance < 32 * (managerEnemies.enemies[e].information.Range + 1) &&
            //            angle >= 0 && angle <= managerEnemies.enemies[e].information.Sight &&
            //            managerEnemies.enemies[e].information.HitPoints > 0 &&
            //            managerUnits.units[u].information.HitPoints > 0 &&
            //            managerUnits.units[u].workState == Units.WorkigState.NOTHING)
            //        {
            //            if (managerEnemies.enemies[e].target == null)
            //                managerEnemies.enemies[e].target = managerUnits.units[u];

            //            if (managerUnits.units[u].information.Type != Util.Units.PEASANT)
            //                if (managerUnits.units[u].target == null)
            //                    managerUnits.units[u].target = managerEnemies.enemies[e];
            //        }
            //    }
            //}
        }
    }
}
