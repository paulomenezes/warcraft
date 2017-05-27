using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Warcraft.Units;
using Warcraft.Units.Humans;

namespace Warcraft.Managers
{
    abstract class ManagerUnits
    {
        public List<Unit> units = new List<Unit>();

        public ManagerMouse managerMouse;
        public ManagerMap managerMap;
        public ManagerBuildings managerBuildings;

        public int index = -1;

        ContentManager content;

        public ManagerUnits(ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings)
        {
            this.managerMouse = managerMouse;
            this.managerMap = managerMap;
            this.managerBuildings = managerBuildings;
        }

        public abstract void Factory(Util.Units type, int x, int y, int targetX, int targetY);

        public void LoadContent()
        {
            units.ForEach((u) => u.LoadContent(content));
        }

        public void LoadContent(ContentManager content)
        {
            if (this.content == null)
                this.content = content;

            units.ForEach((u) => u.LoadContent(content));
        }

        public void Update()
        {
            units.ForEach((u) => u.Update());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            units.ForEach((u) => u.Draw(spriteBatch));
        }

        public void DrawUI(SpriteBatch spriteBatch)
        {
            units.ForEach((u) => u.DrawUI(spriteBatch));
        }

        public abstract List<Unit> GetSelected();
    }
}