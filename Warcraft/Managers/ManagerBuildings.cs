using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Warcraft.Buildings;
using Warcraft.Buildings.Neutral;

namespace Warcraft.Managers
{
    abstract class ManagerBuildings
    {
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

        public abstract List<Building> GetSelected();
    }
}
