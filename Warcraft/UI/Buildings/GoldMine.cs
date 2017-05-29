using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Events;
using Warcraft.Managers;
using Warcraft.Units;

namespace Warcraft.UI.Buildings
{
    class GoldMine : UI
    {
        private global::Warcraft.Buildings.Neutral.GoldMine goldMine;
        private ManagerMouse managerMouse;

        private Button buttonCancel;

        public GoldMine(ManagerMouse managerMouse, global::Warcraft.Buildings.Neutral.GoldMine goldMine)
        {
            buttonPortrait = new Button(4, 7);

            buttonCancel = new Button(0, 260, 1, 9);

            this.managerMouse = managerMouse;
            this.goldMine = goldMine;

            managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
        }

        private void ManagerMouse_MouseEventHandler(object sender, MouseEventArgs e)
        {
            if (goldMine.selected && e.SelectRectangle.Width == 0 && e.SelectRectangle.Height == 0)
            {
                if (buttonCancel.rectangle.Intersects(e.SelectRectangle))
                {
                    goldMine.Fire();
                }
            }
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            buttonPortrait.LoadContent(content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (DrawIndividual)
            {
                buttonPortrait.Draw(spriteBatch);
                buttonCancel.Draw(spriteBatch);

				spriteBatch.DrawString(font, goldMine.information.Name, new Vector2(minX + 50, 100), Color.Black);
                spriteBatch.DrawString(font, "Gold: " + goldMine.QUANITY, new Vector2(minX, 150), Color.Black);
            }
        }
    }
}