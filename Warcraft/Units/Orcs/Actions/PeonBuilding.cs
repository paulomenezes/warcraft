using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Commands;
using Warcraft.Managers;
using Warcraft.Units.Humans;

namespace Warcraft.Units.Orcs.Actions
{
    class PeonBuilding
    {
		ManagerMouse managerMouse;
		ManagerMap managerMap;
		ManagerUnits managerUnits;
        ManagerBuildings managerBuildings;

        List<int> actions = new List<int>();

		public PeonBuilding(ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits, ManagerBuildings managerBuildings)
        {
			this.managerMap = managerMap;
			this.managerMouse = managerMouse;
            this.managerUnits = managerUnits;
            this.managerBuildings = managerBuildings;

            actions.Add(0);
            actions.Add(1);
			actions.Add(2);
			actions.Add(4);
        }

        public void builder(Builder builder) {
            builder.commands[0].execute();
            (builder.commands[0] as BuilderBuildings).building.isPlaceSelected = true;
            (builder.commands[0] as BuilderBuildings).building.Position = new Vector2(10 * 32, 32);
        }

        public void Update() {
            
        }

        public void Draw(SpriteBatch spriteBatch) {
            
        }
    }
}
