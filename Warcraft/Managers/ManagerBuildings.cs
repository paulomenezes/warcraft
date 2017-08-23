using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Warcraft.Buildings;
using Warcraft.Buildings.Neutral;
using Warcraft.Buildings.Orcs;
using Microsoft.Xna.Framework;
using Warcraft.Util;
using Warcraft.Map;

namespace Warcraft.Managers
{
    abstract class ManagerBuildings
    {
        public static List<GoldMine> goldMines = new List<GoldMine>();

        public List<Building> buildings = new List<Building>();
        public ManagerMap managerMap;

        public int index = -1;

        public ManagerBuildings(ManagerMouse managerMouse, ManagerMap managerMap)
        {
            this.managerMap = managerMap;
		}

        public void AddBuilding(Building building)
        {
            building.isWorking = true;
            buildings.Add(building);
        }
        
        public void LoadContent(ContentManager content)
        {
			buildings.ForEach((u) => u.LoadContent(content));
            goldMines.ForEach((u) => u.LoadContent(content));
        }

        public void Update()
        {
            for (int i = buildings.Count - 1; i >= 0; i--)
            {
                buildings[i].Update();
                if (buildings[i].information.HitPoints <= 0)
                {
                    managerMap.RemoveWalls(buildings[i].position, buildings[i].width / 32, buildings[i].height / 32);
                    buildings.RemoveAt(i);
                }
            }

            goldMines.ForEach((u) => u.Update());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
			buildings.ForEach((u) => u.Draw(spriteBatch));
            goldMines.ForEach((u) => u.Draw(spriteBatch));
        }

        public void DrawUI(SpriteBatch spriteBatch)
        {
			buildings.ForEach((u) => u.DrawUI(spriteBatch));
            goldMines.ForEach((u) => u.DrawUI(spriteBatch));
        }

        public List<Building> GetSelected()
        {
			List<Building> selecteds = new List<Building>();
            for (int i = 0; i < goldMines.Count; i++)
			{
                if (goldMines[i].selected)
                    selecteds.Add(goldMines[i]);
			}

			return selecteds;
        }
    }
}
