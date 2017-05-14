using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Managers;
using TrollAxethrowerUnit = Warcraft.Units.Orcs.TrollAxethrower;

namespace Warcraft.UI.Units
{
    class TrollAxethrower : UI
    {
        TrollAxethrowerUnit trollAxethrower;

        public TrollAxethrower(ManagerMouse managerMouse, TrollAxethrowerUnit trollAxethrower)
        {
            buttonPortrait = new Button(3, 0);

            this.trollAxethrower = trollAxethrower;

            managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
        }

        private void ManagerMouse_MouseEventHandler(object sender, Events.MouseEventArgs e)
        {
            if (trollAxethrower.selected && e.SelectRectangle.Width == 0 && e.SelectRectangle.Height == 0)
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

                spriteBatch.DrawString(font, trollAxethrower.information.Name, new Vector2(minX + 50, 100), Color.Black);
                spriteBatch.DrawString(font, "Armor: " + trollAxethrower.information.Armor, new Vector2(minX, 150), Color.Black);
                spriteBatch.DrawString(font, "Damage: " + trollAxethrower.information.Damage + " (" + trollAxethrower.information.Precision + "%)", new Vector2(minX, 170), Color.Black);
                spriteBatch.DrawString(font, "Range: " + trollAxethrower.information.Range, new Vector2(minX, 190), Color.Black);
                spriteBatch.DrawString(font, "Sight: " + trollAxethrower.information.Sight, new Vector2(minX, 210), Color.Black);
                spriteBatch.DrawString(font, "Speed: " + trollAxethrower.information.MovementSpeed, new Vector2(minX, 230), Color.Black);
                spriteBatch.DrawString(font, "Hitpoints: " + trollAxethrower.information.HitPoints + "/" + trollAxethrower.information.HitPointsTotal, new Vector2(minX, 250), Color.Black);
            }
        }
    }
}
