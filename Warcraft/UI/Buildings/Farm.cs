using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Buildings.Humans;
using Warcraft.Managers;
using ChickenFarmBuilding = Warcraft.Buildings.Neutral.Farm;

namespace Warcraft.UI.Buildings
{
    class Farm : UI
    {
        ChickenFarmBuilding chickenFarm;

        public Farm(ManagerMouse managerMouse, ChickenFarmBuilding chickenFarm)
        {
            if (chickenFarm is ChickenFarm)
                buttonPortrait = new Button(8, 3);
            else
                buttonPortrait = new Button(9, 3);

            this.chickenFarm = chickenFarm;

            if (managerMouse != null)
                managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
        }

        private void ManagerMouse_MouseEventHandler(object sender, Events.MouseEventArgs e)
        {
            if (chickenFarm.selected && e.SelectRectangle.Width == 0 && e.SelectRectangle.Height == 0)
            {

            }
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            buttonPortrait.LoadContent(content);
        }

        public override void Update()
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (DrawIndividual)
            {
                buttonPortrait.Draw(spriteBatch);

                spriteBatch.DrawString(font, chickenFarm.information.Name, new Vector2(minX + 50, 100), Color.Black);
                spriteBatch.DrawString(font, "HP: " + chickenFarm.information.HitPoints, new Vector2(minX, 150), Color.Black);
            }
        }
    }
}
