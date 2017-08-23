using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Warcraft.Buildings;
using Warcraft.Buildings.Orcs;
using Warcraft.UI;
using Warcraft.Units;
using Warcraft.Units.Humans;
using Warcraft.Units.Orcs;
using Warcraft.Util;

namespace Warcraft.Managers
{
    class ManagerBotsUnits : ManagerUnits
    {
        List<int> defense = new List<int>();
        List<int> attack = new List<int>();

        int[] attackQuantity = { 6, 4, 2 };
        int[] defenseQuantity = { 3, 3, 1 };

        public ManagerBotsUnits(ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings, int index)
            : base(managerMouse, managerMap, managerBuildings)
        {
            this.index = index;

            Vector2 goldMinePos = Functions.CleanHalfPosition(managerMap, ManagerBuildings.goldMines[1].position);
			units.Add(new Peon(Functions.TilePos(goldMinePos.X), Functions.TilePos(goldMinePos.Y), managerMouse, managerMap, this, managerBuildings));

            AddSkeleton();
        }

        private void AddSkeleton()
        {
            Building darkPortal = managerBuildings.buildings.Find(b => b is DarkPortal);

            units.Add(new Units.Neutral.Skeleton(Functions.TilePos(darkPortal.position.X) + 4, Functions.TilePos(darkPortal.position.Y) + 4, managerMouse, managerMap, this));

            if (content != null)
                LoadContent();
        }

        public override void Factory(Util.Units type, int x, int y, int targetX, int targetY)
        {
            if (type == Util.Units.PEON)
                units.Add(new Peon(x, y, managerMouse, managerMap, this, managerBuildings));
            else if (type == Util.Units.TROLL_AXETHROWER)
                units.Add(new TrollAxethrower(x, y, managerMouse, managerMap, this));
            else if (type == Util.Units.GRUNT)
                units.Add(new Grunt(x, y, managerMouse, managerMap, this));

            if (type != Util.Units.PEON)
            {
                Random rng = new Random();
                if (rng.NextDouble() > 0.5)
                {
                    defense.Add(units.Count - 1);
                }
                else
                {
                    attack.Add(units.Count - 1);
                }
            }

            units[units.Count - 1].Move(targetX, targetY);

            LoadContent();
        }

        public override void Update()
        {
            base.Update();

            for (int i = units.Count - 1; i >= 0; i--)
            {
                if (units[i].information.HitPoints <= 0 && units[i] is Units.Neutral.Skeleton)
                {
                    Summary.SKELETONS++;
                    units.RemoveAt(i);
                    AddSkeleton();

                    if (i % 2 == 0) 
                    {
                        AddSkeleton();
                    }
                }
            }

            if (attack.Count >= attackQuantity[Warcraft.ISLAND]) 
            {
                Vector2 pos = Vector2.Zero;
                for (int i = 0; i < attack.Count; i++)
                {
                    if (!units[attack[i]].transition && units[attack[i]].target == null && units[attack[i]].targetBuilding == null)
					{
                        if (pos == Vector2.Zero)
						    pos = Functions.CleanPosition(managerMap, 32, 32);

                        units[attack[i]].Move((Functions.Normalize(pos.X) / 32) + i, (Functions.Normalize(pos.Y) / 32) + i);
                    }
                }
			}
            else
            {
				for (int i = 0; i < attack.Count; i++)
				{
					if (!units[attack[i]].transition && units[attack[i]].target == null && units[attack[i]].targetBuilding == null)
					{
						Vector2 pos = Functions.CleanPosition(managerMap, ManagerBuildings.goldMines[1].position, 32, 32, 10);
						units[attack[i]].Move((Functions.Normalize(pos.X) / 32) + i, (Functions.Normalize(pos.Y) / 32) + i);
					}
				}
            }

            // if (defense.Count > defenseQuantity[Warcraft.ISLAND]) 
            {
                for (int i = 0; i < defense.Count; i++)
				{
                    if (!units[defense[i]].transition && units[defense[i]].target == null && units[defense[i]].targetBuilding == null)
					{
						Vector2 pos = Functions.CleanPosition(managerMap, ManagerBuildings.goldMines[1].position, 32, 32, 10);

						units[defense[i]].Move((Functions.Normalize(pos.X) / 32) + i, (Functions.Normalize(pos.Y) / 32) + i);
					}
				}
            }
        }
    }
}