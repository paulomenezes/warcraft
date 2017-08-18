using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Managers;
using ElvenArcherUnit = Warcraft.Units.Humans.ElvenArcher;

namespace Warcraft.UI.Units
{
    class ElvenArcher : UI
    {
        ElvenArcherUnit elvenArcher;

        public ElvenArcher(ManagerMouse managerMouse, ElvenArcherUnit peasant)
        {
            buttonPortrait = new Button(4, 0);

            this.elvenArcher = peasant;

            managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
        }

        private void ManagerMouse_MouseEventHandler(object sender, Events.MouseEventArgs e)
        {
            if (elvenArcher.selected && e.SelectRectangle.Width == 0 && e.SelectRectangle.Height == 0)
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
                
                spriteBatch.DrawString(font, elvenArcher.information.Name, new Vector2(minX + 50, 100), Color.Black);
                spriteBatch.DrawString(font, "Armor: " + elvenArcher.information.Armor, new Vector2(minX, 150), Color.Black);
                spriteBatch.DrawString(font, "Damage: " + elvenArcher.information.Damage, new Vector2(minX, 170), Color.Black);
                spriteBatch.DrawString(font, "Range: " + elvenArcher.information.Range + "sq", new Vector2(minX, 190), Color.Black);
                spriteBatch.DrawString(font, "Sight: " + elvenArcher.information.Sight + "º", new Vector2(minX, 210), Color.Black);
                spriteBatch.DrawString(font, "Speed: " + elvenArcher.information.MovementSpeed, new Vector2(minX, 230), Color.Black);
                spriteBatch.DrawString(font, "Hit points: " + elvenArcher.information.HitPoints, new Vector2(minX, 250), Color.Black);
            }
        }
    }
}
