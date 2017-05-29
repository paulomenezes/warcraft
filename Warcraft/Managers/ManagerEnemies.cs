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
using Warcraft.Commands;
using Microsoft.Xna.Framework;

namespace Warcraft.Managers
{
    class ManagerEnemies
    {
        ManagerMouse managerMouse;
        ManagerMap managerMap;
		public ManagerBuildings managerBuildings;
		public ManagerUnits managerUnits;

        Random random = new Random();

        public int index = 0;

		List<EA.Gene> actionsUnits = new List<EA.Gene>();
		Dictionary<int, List<EA.Gene>> GENE_DATA = new Dictionary<int, List<EA.Gene>>();

		public ManagerEnemies(ManagerMouse managerMouse, ManagerMap managerMap, int index)
        {
            this.index = index;
            this.managerMap = managerMap;
            this.managerMouse = managerMouse;
			this.managerBuildings = new ManagerBotsBuildings(managerMouse, managerMap, index);
            this.managerUnits = new ManagerBotsUnits(managerMouse, managerMap, managerBuildings, index);

            actionsUnits.Add(new EA.GeneBuilding(0));
            actionsUnits.Add(new EA.GeneBuilding(1));
            actionsUnits.Add(new EA.GeneBuilding(4));

			actionsUnits.Add(new EA.GeneUnit(Util.Buildings.GREAT_HALL, 0));
            actionsUnits.Add(new EA.GeneUnit(Util.Buildings.ORC_BARRACKS, 0));
            actionsUnits.Add(new EA.GeneUnit(Util.Buildings.ORC_BARRACKS, 1));

            GENE_DATA.Add(index, new List<EA.Gene>());
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
            if (actionsUnits.Count > 0) 
            {
                if (actionsUnits[0] is EA.GeneUnit)
                {
                    Building building = managerBuildings.buildings.Find(b => (b.information as InformationBuilding).Type == (actionsUnits[0] as EA.GeneUnit).building && b.isWorking);
					if (building != null)
					{
                        building.commands[actionsUnits[0].action].execute();
                        actionsUnits.RemoveAt(0);
					}
                }
                else
                {
                    Builder builder = managerUnits.units.Find(u => u is Builder && u.workState == WorkigState.NOTHING) as Builder;

                    if (builder != null)
                    {
                        builder.commands[actionsUnits[0].action].execute();

                        if (builder.commands[actionsUnits[0].action] is BuilderBuildings)
                        {
                            Building building = (builder.commands[actionsUnits[0].action] as BuilderBuildings).building;

                            building.isPlaceSelected = true;
                            building.Position = Functions.CleanPosition(managerMap, building.width, building.height);

                            (actionsUnits[0] as EA.GeneBuilding).position = building.Position;
                        }
                        else if (builder.commands[actionsUnits[0].action] is BuilderWalls)
                        {
                            BuilderWalls walls = builder.commands[actionsUnits[0].action] as BuilderWalls;
                            walls.started = false;

                            int x1 = random.Next(3, Warcraft.MAP_SIZE - 3);
                            int y1 = random.Next(3, Warcraft.MAP_SIZE - 3);

                            int x2 = random.Next(3, Warcraft.MAP_SIZE - 3);
                            int y2 = random.Next(3, Warcraft.MAP_SIZE - 3);

                            walls.startPoint = new Vector2(x1 * 32, y1 * 32);
                            walls.endPoint = new Vector2(x2 * 32, y2 * 32);

                            walls.FinishWall();

                            (actionsUnits[0] as EA.GeneBuildWall).start = walls.startPoint;
                            (actionsUnits[0] as EA.GeneBuildWall).end = walls.endPoint;
                        }

                        // Wait for a builder?
                        GENE_DATA[index].Add(actionsUnits[0]);
                        actionsUnits.RemoveAt(0);
                    }
                }
			}
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            managerBuildings.Draw(spriteBatch);
            managerUnits.Draw(spriteBatch);
		}

		public void DrawUI(SpriteBatch spriteBatch)
		{
			managerBuildings.DrawUI(spriteBatch);
			managerUnits.DrawUI(spriteBatch);
		}
    }
}
