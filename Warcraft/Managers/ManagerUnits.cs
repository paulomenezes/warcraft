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

        protected ContentManager content;

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

        public virtual void Update()
		{
			for (int i = units.Count - 1; i >= 0; i--)
			{
				units[i].Update();
     //           if (units[i].information.HitPoints <= 0)
					//units.RemoveAt(i);
			}

		}

        public void Draw(SpriteBatch spriteBatch)
        {
            units.ForEach((u) => u.Draw(spriteBatch));
        }

        public void DrawUI(SpriteBatch spriteBatch)
        {
            units.ForEach((u) => u.DrawUI(spriteBatch));
        }

        public List<Unit> GetSelected()
        {
			List<Unit> selecteds = new List<Unit>();
			for (int i = 0; i < units.Count; i++)
			{
				if (units[i].selected)
					selecteds.Add(units[i]);
			}

			return selecteds;
        }
    }
}