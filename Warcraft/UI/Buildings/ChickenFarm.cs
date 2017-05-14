using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Managers;
using ChickenFarmBuilding = Warcraft.Buildings.Humans.ChickenFarm;

namespace Warcraft.UI.Buildings
{
    class ChickenFarm : UI
    {
        ChickenFarmBuilding barracks;

        public ChickenFarm(ManagerMouse managerMouse, ChickenFarmBuilding chickenFarm)
        {
            buttonPortrait = new Button(8, 3);

            this.barracks = chickenFarm;

            if (managerMouse != null)
                managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
        }

        private void ManagerMouse_MouseEventHandler(object sender, Events.MouseEventArgs e)
        {
            if (barracks.selected && e.SelectRectangle.Width == 0 && e.SelectRectangle.Height == 0)
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

                spriteBatch.DrawString(font, barracks.information.Name, new Vector2(minX + 50, 100), Color.Black);
            }
        }
    }
}
