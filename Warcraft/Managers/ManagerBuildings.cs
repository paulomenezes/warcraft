using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Warcraft.Buildings;
using Warcraft.Buildings.Neutral;

namespace Warcraft.Managers
{
    class ManagerBuildings
    {
        public List<Building> buildings = new List<Building>();
        public ManagerMap managerMap;

        public ManagerBuildings(ManagerMouse managerMouse, ManagerMap managerMap)
        {
            this.managerMap = managerMap;

            buildings.Add(new GoldMine(25, 25, managerMouse, managerMap, null));
        }

        public void AddBuilding(Building building)
        {
            building.isWorking = true;
            buildings.Add(building);
        }
        
        public void LoadContent(ContentManager content)
        {
            buildings.ForEach((u) => u.LoadContent(content));
        }

        public void Update()
        {
            buildings.ForEach((u) => u.Update());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            buildings.ForEach((u) => u.Draw(spriteBatch));
        }

        public void DrawUI(SpriteBatch spriteBatch)
        {
            buildings.ForEach((u) => u.DrawUI(spriteBatch));
        }

        public List<Building> GetSelected()
        {
            List<Building> selecteds = new List<Building>(); ;
            for (int i = 0; i < buildings.Count; i++)
            {
                if (buildings[i].selected)
                    selecteds.Add(buildings[i]);
            }

            return selecteds;
        }
    }
}
