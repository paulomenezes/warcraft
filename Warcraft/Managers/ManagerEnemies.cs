using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Warcraft.Units;
using Warcraft.Units.Orcs;
using Warcraft.Util;
using Warcraft.Buildings;
using Warcraft.Buildings.Neutral;
using Warcraft.Units.Humans;
using Warcraft.Units.Orcs.Actions;
using Warcraft.Commands;
using Microsoft.Xna.Framework;

namespace Warcraft.Managers
{
    class ManagerEnemies
    {
        ManagerMouse managerMouse;
        ManagerMap managerMap;
		ManagerBuildings managerBuildings;
		ManagerUnits managerUnits;

		//PeonBuilding peonBuilding;

		List<int> actionsUnits = new List<int>();
		List<Util.Buildings> typesBuilding = new List<Util.Buildings>();
		List<int> actionsBuildings = new List<int>();

        Random random = new Random();

        public int index = 0;

		public ManagerEnemies(ManagerMouse managerMouse, ManagerMap managerMap, int index)
        {
            this.index = index;
            this.managerMap = managerMap;
            this.managerMouse = managerMouse;
			this.managerBuildings = new ManagerBotsBuildings(managerMouse, managerMap, index);
            this.managerUnits = new ManagerBotsUnits(managerMouse, managerMap, managerBuildings, index);

			actionsUnits.Add(0);
			actionsUnits.Add(1);
			actionsUnits.Add(2);
			actionsUnits.Add(3);
			actionsUnits.Add(4);

			typesBuilding.Add(Util.Buildings.GREAT_HALL);
            typesBuilding.Add(Util.Buildings.ORC_BARRACKS);
            typesBuilding.Add(Util.Buildings.ORC_BARRACKS);
			actionsBuildings.Add(0);
			actionsBuildings.Add(0);
			actionsBuildings.Add(1);
		}

        public void LoadContent(ContentManager content)
        {
            managerBuildings.LoadContent(content);
            managerUnits.LoadContent(content);
        }

        public void Update()
		{
            managerBuildings.Update();
            managerUnits.Update();

            // Actions for units (Peon, create new buildings and mining)
            Builder builder = managerUnits.units.Find(u => u is Builder && u.workState == WorkigState.NOTHING) as Builder;
            if (builder != null && actionsUnits.Count > 0) 
            {
                builder.commands[actionsUnits[0]].execute();

                if (builder.commands[actionsUnits[0]] is BuilderBuildings)
                {
                    Building building = (builder.commands[actionsUnits[0]] as BuilderBuildings).building;

					building.isPlaceSelected = true;
                    building.Position = Functions.CleanPosition(managerMap, building.width, building.height);
				}
                else if (builder.commands[actionsUnits[0]] is BuilderWalls)
				{
                    BuilderWalls walls = builder.commands[actionsUnits[0]] as BuilderWalls;
                    walls.started = false;

					int x1 = random.Next(3, Warcraft.MAP_SIZE - 3);
					int y1 = random.Next(3, Warcraft.MAP_SIZE - 3);

                    int x2 = random.Next(3, Warcraft.MAP_SIZE - 3);
                    int y2 = random.Next(3, Warcraft.MAP_SIZE - 3);

					walls.startPoint = new Vector2(x1 * 32, y1 * 32);
                    walls.endPoint = new Vector2(x2 * 32, y2 * 32);

                    walls.FinishWall();
				}

                actionsUnits.RemoveAt(0);
			}

            // Actions for buildings (build units)
            if (actionsBuildings.Count > 0)
			{
                Building building = managerBuildings.buildings.Find(b => (b.information as InformationBuilding).Type == typesBuilding[0] && b.isWorking);
                if (building != null) 
                {
                    building.commands[actionsBuildings[0]].execute();
                    actionsBuildings.RemoveAt(0);
                    typesBuilding.RemoveAt(0);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            managerBuildings.Draw(spriteBatch);
            managerUnits.Draw(spriteBatch);
        }
    }
}
