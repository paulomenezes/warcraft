using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Warcraft.Managers;
using Warcraft.Units;
using BuildingsHumans = Warcraft.Buildings.Building;
using Microsoft.Xna.Framework.Content;

namespace Warcraft.UI
{
    class Main : UI
    {
        private ManagerUnits managerUnits;
        private ManagerMouse managerMouse;
        private ManagerBuildings managerBuildings;

        private List<Unit> unitsSelecteds = new List<Unit>();
        private List<BuildingsHumans> buildingsSelecteds = new List<BuildingsHumans>();

        List<Button> buttons = new List<Button>();

        Texture2D background;

        public Main(ManagerUnits managerUnits, ManagerBuildings managerBuildings, ManagerMouse managerMouse)
        {
            this.managerUnits = managerUnits;
            this.managerMouse = managerMouse;
            this.managerBuildings = managerBuildings;

            managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            background = content.Load<Texture2D>("Cursor");
        }

        private void ManagerMouse_MouseEventHandler(object sender, Events.MouseEventArgs e)
        {
            buttons.Clear();
            unitsSelecteds = managerUnits.GetSelected();
            buildingsSelecteds = managerBuildings.GetSelected();

            if (unitsSelecteds.Count + buildingsSelecteds.Count > 1)
            {
                DrawIndividual = false;
                int y = 0;
                var b = 0;
                for (int i = 0; i < unitsSelecteds.Count + buildingsSelecteds.Count; i++)
                {
                    if (i > 0 && i % 3 == 0)
                        y++;

                    if (i >= unitsSelecteds.Count)
                    {
                        buttons.Add(new Button(50 * (i % 3), 100 + (38 * y), buildingsSelecteds[b].ui.buttonPortrait.TextureX, buildingsSelecteds[b].ui.buttonPortrait.TextureY, true));
                        b++;
                    }
                    else
                        buttons.Add(new Button(50 * (i % 3), 100 + (38 * y), unitsSelecteds[i].ui.buttonPortrait.TextureX, unitsSelecteds[i].ui.buttonPortrait.TextureY, true));
                }
            }
            else
                DrawIndividual = true;

            managerMouse.MouseEventHandler -= ManagerMouse_MouseEventHandler;
            managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
        }

        public override void Update()
        {
            
        }

        public void DrawBack(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Rectangle(Warcraft.WINDOWS_WIDTH, 0, 200, Warcraft.WINDOWS_HEIGHT), Color.CornflowerBlue);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, "Gold: " + Warcraft.GOLD, new Vector2(minX, 10), Color.Black);
            spriteBatch.DrawString(font, "Food: " + Warcraft.FOOD, new Vector2(minX, 30), Color.Black);
            //spriteBatch.DrawString(font, "Wood: " + Warcraft.WOOD, new Vector2(minX, 50), Color.Black);
            //spriteBatch.DrawString(font, "Oil: " + Warcraft.OIL, new Vector2(minX, 70), Color.Black);

            if (!DrawIndividual)
                buttons.ForEach((b) => b.Draw(spriteBatch));
        }
    }
}
