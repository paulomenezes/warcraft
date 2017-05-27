using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Warcraft.Managers;
using Warcraft.Units.Humans;

namespace Warcraft.UI.Units
{
    class Peasant : UI
    {
        Button buttonBuilder;
        Button buttonCancel;

        Button buttonMiner;

        Builder builderUnit;

        bool showBuilder = false;
        List<Button> builder = new List<Button>();

        public Peasant(ManagerMouse managerMouse, Builder builderUnit)
        {
            buttonPortrait = new Button(0, 0);
            buttonBuilder = new Button(0, 280, 7, 8);
            buttonCancel = new Button(0, 400, 1, 9);

            builder.Add(new Button(0, 280, 0, 4));
            builder.Add(new Button(50, 280, 2, 4));
            builder.Add(new Button(100, 280, 8, 3));
            builder.Add(new Button(0, 320, 2, 9));

            buttonMiner = new Button(50, 280, 9, 8);

            this.builderUnit = builderUnit;

            managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
        }

        private void ManagerMouse_MouseEventHandler(object sender, Events.MouseEventArgs e)
        {
            if (builderUnit.selected && e.SelectRectangle.Width == 0 && e.SelectRectangle.Height == 0)
            {
                for (int i = 0; i < builder.Count; i++)
                {
                    if (showBuilder && e.SelectRectangle.Intersects(builder[i].rectangle))
                    {
                        builderUnit.commands[i].execute();
                        break;
                    }
                }

                if (!showBuilder && e.SelectRectangle.Intersects(buttonMiner.rectangle))
                    builderUnit.commands[builderUnit.commands.Count - 1].execute();

                if (e.SelectRectangle.Intersects(buttonBuilder.rectangle))
                    showBuilder = true;

                if (e.SelectRectangle.Intersects(buttonCancel.rectangle))
                    showBuilder = false;
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
                buttonMiner.Draw(spriteBatch);

                if (!showBuilder)
                    buttonBuilder.Draw(spriteBatch);
                else
                {
                    builder.ForEach((b) => b.Draw(spriteBatch));
                    buttonCancel.Draw(spriteBatch);
                }

                spriteBatch.DrawString(font, builderUnit.information.Name, new Vector2(minX + 50, 100), Color.Black);
                spriteBatch.DrawString(font, "Armor: " + builderUnit.information.Armor, new Vector2(minX, 150), Color.Black);
                spriteBatch.DrawString(font, "Damage: " + builderUnit.information.Damage + " (" + builderUnit.information.Precision + "%)", new Vector2(minX, 170), Color.Black);
                spriteBatch.DrawString(font, "Range: " + builderUnit.information.Range + "sq", new Vector2(minX, 190), Color.Black);
                spriteBatch.DrawString(font, "Sight: " + builderUnit.information.Sight + "º", new Vector2(minX, 210), Color.Black);
                spriteBatch.DrawString(font, "Speed: " + builderUnit.information.MovementSpeed, new Vector2(minX, 230), Color.Black);
                spriteBatch.DrawString(font, "Hit points: " + builderUnit.information.HitPoints, new Vector2(minX, 250), Color.Black);
            }
        }
    }
}
