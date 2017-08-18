using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Managers;
using FootmanUnit = Warcraft.Units.Humans.Footman;

namespace Warcraft.UI.Units
{
    class Footman : UI
    {
        FootmanUnit footman;

        public Footman(ManagerMouse managerMouse, FootmanUnit footman)
        {
            buttonPortrait = new Button(2, 0);

            this.footman = footman;

            managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
        }

        private void ManagerMouse_MouseEventHandler(object sender, Events.MouseEventArgs e)
        {
            if (footman.selected && e.SelectRectangle.Width == 0 && e.SelectRectangle.Height == 0)
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
                
                spriteBatch.DrawString(font, footman.information.Name, new Vector2(minX + 50, 100), Color.Black);
                spriteBatch.DrawString(font, "Armor: " + footman.information.Armor, new Vector2(minX, 150), Color.Black);
                spriteBatch.DrawString(font, "Damage: " + footman.information.Damage, new Vector2(minX, 170), Color.Black);
                spriteBatch.DrawString(font, "Range: " + footman.information.Range + "sq", new Vector2(minX, 190), Color.Black);
                spriteBatch.DrawString(font, "Sight: " + footman.information.Sight + "º", new Vector2(minX, 210), Color.Black);
                spriteBatch.DrawString(font, "Speed: " + footman.information.MovementSpeed, new Vector2(minX, 230), Color.Black);
                spriteBatch.DrawString(font, "Hit points: " + footman.information.HitPoints, new Vector2(minX, 250), Color.Black);
            }
        }
    }
}
