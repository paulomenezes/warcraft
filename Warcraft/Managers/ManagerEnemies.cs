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

        List<EA.ActionType> actionsTypes = new List<EA.ActionType>();
        List<int> actions = new List<int>();

		public ManagerEnemies(ManagerMouse managerMouse, ManagerMap managerMap, int index)
        {
			ManagerResources.BOT_GOLD.Add(5000);
			ManagerResources.BOT_WOOD.Add(99999);
			ManagerResources.BOT_FOOD.Add(5);
			ManagerResources.BOT_OIL.Add(99999);

			this.index = index;
            this.managerMap = managerMap;
            this.managerMouse = managerMouse;
			this.managerBuildings = new ManagerBotsBuildings(managerMouse, managerMap, index);
            this.managerUnits = new ManagerBotsUnits(managerMouse, managerMap, managerBuildings, index);

            actionsTypes.Add(EA.ActionType.BUILDING);
            actions.Add(0);

            actionsTypes.Add(EA.ActionType.BUILDING);
			actions.Add(1);

            actionsTypes.Add(EA.ActionType.BUILDING);
			actions.Add(2);

            actionsTypes.Add(EA.ActionType.MINING);
			actions.Add(4);

            actionsTypes.Add(EA.ActionType.TOWN_HALL);
			actions.Add(0);

            actionsTypes.Add(EA.ActionType.BARRACKS);
			actions.Add(0);

            actionsTypes.Add(EA.ActionType.BARRACKS);
			actions.Add(1);
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

            Builder builder = managerUnits.units.Find(u => u is Builder && u.workState == WorkigState.NOTHING) as Builder;
            Building greatHall = managerBuildings.buildings.Find(b => (b.information as InformationBuilding).Type == Util.Buildings.GREAT_HALL && b.isWorking);
			Barracks barrack = managerBuildings.buildings.Find(b => (b.information as InformationBuilding).Type == Util.Buildings.ORC_BARRACKS && b.isWorking) as Barracks;

            if (actionsTypes.Count > 0) 
            {
                switch (actionsTypes[0]) 
                {
                    case EA.ActionType.BUILDING:
                        if (builder != null)
                        {
                            Building building = (builder.commands[actions[0]] as BuilderBuildings).building;
							building.isPlaceSelected = true;

                            if (actionsTypes[0] == 0)
                                building.Position = Functions.CleanHalfPosition(managerMap, ManagerBuildings.goldMines[1].position);
                            else
                                building.Position = Functions.CleanHalfPosition(managerMap, greatHall.position);
                            
                            builder.commands[actions[0]].execute();
							actionsTypes.RemoveAt(0);
                            actions.RemoveAt(0);
                        }
                        break;
                    case EA.ActionType.MINING:
                        if (builder != null)
                        {
							builder.commands[actions[0]].execute();
							actionsTypes.RemoveAt(0);
							actions.RemoveAt(0);
                        }
						break;
                    case EA.ActionType.BARRACKS:
                        if (barrack != null)
						{
                            barrack.commands[actions[0]].execute();
							actionsTypes.RemoveAt(0);
							actions.RemoveAt(0);
						}
						break;
                    case EA.ActionType.TOWN_HALL:
                        if (greatHall != null)
						{
                            greatHall.commands[actions[0]].execute();
							actionsTypes.RemoveAt(0);
							actions.RemoveAt(0);
						}
						break;
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
