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

namespace Warcraft.Managers
{
    class ManagerEnemies
    {
        public List<Unit> enemies = new List<Unit>();
        public List<Building> buildings = new List<Building>();

        public static Random random = new Random(Guid.NewGuid().GetHashCode());

        ManagerMouse managerMouse;
        ManagerMap managerMap;
		ManagerBuildings managerBuildings;
		ManagerUnits managerUnits;

        PeonBuilding peonBuilding;

        public ManagerEnemies(ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings, ManagerUnits managerUnits)
        {
            this.managerMap = managerMap;
            this.managerMouse = managerMouse;
            this.managerBuildings = managerBuildings;
            this.managerUnits = managerUnits;

            this.buildings.Add(new GoldMine(10, 10, managerMouse, managerMap, managerUnits));
            this.enemies.Add(new Peon(13, 13, managerMouse, managerMap, managerBuildings, managerUnits));

            peonBuilding = new PeonBuilding(managerMouse, managerMap, managerUnits);
            this.buildings.Add(peonBuilding.builder(Util.Buildings.TOWN_HALL));
        }

        public void LoadContent(ContentManager content)
        {
			buildings.ForEach((u) => u.LoadContent(content));
			enemies.ForEach((u) => u.LoadContent(content));
        }

        public void Update()
		{
			buildings.ForEach((u) => u.Update());
            enemies.ForEach((u) => u.Update());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            buildings.ForEach((u) => u.Draw(spriteBatch));
			enemies.ForEach((u) => u.Draw(spriteBatch));
        }

        public void DrawUI(SpriteBatch spriteBatch)
        {
            buildings.ForEach((u) => u.DrawUI(spriteBatch));
			enemies.ForEach((u) => u.DrawUI(spriteBatch));
        }

        public List<Unit> GetSelected()
        {
            List<Unit> selecteds = new List<Unit>(); ;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].selected)
                    selecteds.Add(enemies[i]);
            }

            return selecteds;
        }
    }
}
