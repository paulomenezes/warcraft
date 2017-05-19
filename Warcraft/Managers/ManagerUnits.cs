using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Warcraft.Units;
using Warcraft.Units.Humans;

namespace Warcraft.Managers
{
    class ManagerUnits
    {
        public List<Unit> units = new List<Unit>();

        ManagerMouse managerMouse;
        public ManagerMap managerMap;
        ManagerBuildings managerBuildings;

        ContentManager content;

        public ManagerUnits(ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings)
        {
            this.managerMouse = managerMouse;
            this.managerMap = managerMap;
            this.managerBuildings = managerBuildings;

            managerMouse.MouseClickEventHandler += ManagerMouse_MouseClickEventHandler;

            units.Add(new Peasant(30, 23, managerMouse, managerMap, managerBuildings, this));
        }

        public void Factory(Util.Units type, int x, int y, int targetX, int targetY)
        {
            if (type == Util.Units.PEASANT)
                units.Add(new Peasant(x, y, managerMouse, managerMap, managerBuildings, this));
            else if (type == Util.Units.ELVEN_ARCHER)
                units.Add(new ElvenArcher(x, y, managerMouse, managerMap, managerBuildings, this));
            else if (type == Util.Units.FOOTMAN)
                units.Add(new Footman(x, y, managerMouse, managerMap, managerBuildings, this));

            units[units.Count - 1].Move(targetX, targetY);

            LoadContent();
        }

        private void ManagerMouse_MouseClickEventHandler(object sender, Events.MouseClickEventArgs e)
        {
            List<Unit> selecteds = GetSelected();
            
            int threshold = (int)Math.Sqrt(selecteds.Count) / 2;
            int x = -threshold, y = x;
            for (int i = 0; i < selecteds.Count; i++)
            {
                selecteds[i].Move(e.XTile + x, e.YTile + y);
                x++;
                if (x > threshold)
                {
                    x = -threshold;
                    y++;
                }
            }
        }

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

        public List<Unit> GetSelected()
        {
            List<Unit> selecteds = new List<Unit>(); ;
            for (int i = 0; i < units.Count; i++)
            {
                if (units[i].selected)
                    selecteds.Add(units[i]);
            }

            return selecteds;
        }
    }
}