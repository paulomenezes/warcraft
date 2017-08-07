using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Warcraft.Units;
using Warcraft.Buildings;
using Warcraft.Units.Humans;

namespace Warcraft.Managers
{
    class ManagerCombat
    {
        ManagerEnemies managerEnemies;
        ManagerUnits managerUnits;
        ManagerBuildings managerBuildings;

        public ManagerCombat(ManagerEnemies managerEnemies, ManagerUnits managerUnits, ManagerBuildings managerBuildings)
        {
            this.managerUnits = managerUnits;
            this.managerEnemies = managerEnemies;
            this.managerBuildings = managerBuildings;
        }

        public void Update()
        {
            Dictionary<int, List<Unit>> allUnits = new Dictionary<int, List<Unit>>();
            Dictionary<int, List<Building>> allBuildings = new Dictionary<int, List<Building>>();

            allUnits.Add(-1, managerUnits.units);
			allUnits.Add(managerEnemies.index, managerEnemies.managerUnits.units);
			
            allBuildings.Add(-1, managerBuildings.buildings);
			allBuildings.Add(managerEnemies.index, managerEnemies.managerBuildings.buildings);

            foreach (var item01 in allUnits)
            {
                foreach (var item02 in allUnits)
                {
                    if (item01.Key != item02.Key)
                    {
                        for (int i = 0; i < allUnits[item01.Key].Count; i++)
                        {
                            if (allUnits[item01.Key][i].target == null || allUnits[item01.Key][i].targetBuilding == null)
                            {
                                for (int j = 0; j < allUnits[item02.Key].Count; j++)
                                {
                                    //if (i != j)
                                    {
                                        Unit enemy1 = allUnits[item01.Key][i];
                                        Unit enemy2 = allUnits[item02.Key][j];

                                        float distance = Vector2.Distance(enemy1.position, enemy2.position);

                                        int range = Math.Max(enemy1.information.Range, enemy2.information.Range);

                                        if (distance < 32 * (range + 1) &&
											enemy1.information.HitPoints > 0 &&
											enemy2.information.HitPoints > 0 &&
                                            enemy2.workState == WorkigState.NOTHING &&
                                            enemy1.workState == WorkigState.NOTHING)
                                        {
                                            if (!(enemy1 is Builder) && enemy1.target == null)
                                            {
                                                enemy1.target = enemy2;
                                                enemy1.targetBuilding = null;
                                            }

                                            if (!(enemy2 is Builder) && enemy2.target == null)
                                            {
                                                enemy2.target = enemy1;
                                                enemy2.targetBuilding = null;
                                            }
                                        }
                                    }
                                }

                                for (int j = 0; j < allBuildings[item02.Key].Count; j++)
                                {
                                    Unit enemy = allUnits[item01.Key][i];
                                    Building building = allBuildings[item02.Key][j];

                                    if (enemy.target == null && enemy.targetBuilding == null)
                                    {
                                        float distance = Vector2.Distance(enemy.position, building.position);

                                        if (distance < 32 * (enemy.information.Range + 1) &&
                                            enemy.information.HitPoints > 0 &&
                                            building.information.HitPoints > 0 &&
                                            enemy.workState == WorkigState.NOTHING &&
                                            building.isWorking)
                                        {
                                            enemy.targetBuilding = building;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}