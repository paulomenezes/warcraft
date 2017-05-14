using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Managers;
using GruntUnit = Warcraft.Units.Orcs.Grunt;

namespace Warcraft.UI.Units
{
    class Grunt : UI
    {
        GruntUnit grunt;

        public Grunt(ManagerMouse managerMouse, GruntUnit grunt)
        {
            buttonPortrait = new Button(3, 0);

            this.grunt = grunt;

            managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
        }

        private void ManagerMouse_MouseEventHandler(object sender, Events.MouseEventArgs e)
        {
            if (grunt.selected && e.SelectRectangle.Width == 0 && e.SelectRectangle.Height == 0)
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

                spriteBatch.DrawString(font, grunt.information.Name, new Vector2(minX + 50, 100), Color.Black);
                spriteBatch.DrawString(font, "Armor: " + grunt.information.Armor, new Vector2(minX, 150), Color.Black);
                spriteBatch.DrawString(font, "Damage: " + grunt.information.Damage + " (" + grunt.information.Precision + "%)", new Vector2(minX, 170), Color.Black);
                spriteBatch.DrawString(font, "Range: " + grunt.information.Range, new Vector2(minX, 190), Color.Black);
                spriteBatch.DrawString(font, "Sight: " + grunt.information.Sight, new Vector2(minX, 210), Color.Black);
                spriteBatch.DrawString(font, "Speed: " + grunt.information.MovementSpeed, new Vector2(minX, 230), Color.Black);
                spriteBatch.DrawString(font, "Hitpoints: " + grunt.information.HitPoints + "/" + grunt.information.HitPointsTotal, new Vector2(minX, 250), Color.Black);
            }
        }
    }
}
