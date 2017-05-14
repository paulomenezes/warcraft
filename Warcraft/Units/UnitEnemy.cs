using System;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Managers;
using Warcraft.Util;
using Warcraft.UI;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;

namespace Warcraft.Units
{
    class UnitEnemy : Unit
    {
        int currentPatrol = 0;
        int[,] patrol;

        public UnitEnemy(int width, int height, int speed, ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings) 
            : base(0, 0, width, height, speed, managerMouse, managerMap, managerBuildings)
        {

        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            Console.WriteLine(information.ToString());

            Random rng = ManagerEnemies.random;

            if (information.Spawn == 0)
            {
                position = new Vector2(0, 25 * 32);

                patrol = new int[4, 2] { 
                    { rng.Next(0, 25), rng.Next(0, 25) }, 
                    { rng.Next(25, 50), rng.Next(0, 25) }, 
                    { rng.Next(25, 50), rng.Next(25, 50) }, 
                    { rng.Next(0, 25), rng.Next(25, 50) }
                };
            }
            else if (information.Spawn == 1)
            {
                position = new Vector2(25 * 32, 0);

                patrol = new int[4, 2] {
                    { rng.Next(25, 50), rng.Next(0, 25) },
                    { rng.Next(25, 50), rng.Next(25, 50) },
                    { rng.Next(0, 25), rng.Next(25, 50) },
                    { rng.Next(0, 25), rng.Next(0, 25) }
                };
            }
            else if (information.Spawn == 2)
            {
                position = new Vector2(50 * 32, 25 * 32);

                patrol = new int[4, 2] {
                    { rng.Next(25, 50), rng.Next(25, 50) },
                    { rng.Next(0, 25), rng.Next(25, 50) },
                    { rng.Next(0, 25), rng.Next(0, 25) },
                    { rng.Next(25, 50), rng.Next(0, 25) }
                };
            }
            else if (information.Spawn == 3)
            {
                position = new Vector2(25 * 32, 50 * 32);

                patrol = new int[4, 2] {
                    { rng.Next(0, 25), rng.Next(25, 50) },
                    { rng.Next(0, 25), rng.Next(0, 25) },
                    { rng.Next(25, 50), rng.Next(0, 25) },
                    { rng.Next(25, 50), rng.Next(25, 50) }
                };
            }
        }

        public override void Update()
        {
            base.Update();

            if (target == null && information.HitPoints > 0)
            {
                if (!transition)
                {
                    Move(patrol[currentPatrol, 0], patrol[currentPatrol, 1]);
                    currentPatrol++;

                    if (currentPatrol > 3)
                        currentPatrol = 0;
                }
            }
        }
    }
}
