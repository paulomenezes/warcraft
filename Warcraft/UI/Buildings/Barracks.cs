using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Warcraft.Commands;
using Warcraft.Managers;
using BarracksBuilding = Warcraft.Buildings.Neutral.Barracks;
using HumansBarracksBuilding = Warcraft.Buildings.Humans.Barracks;

namespace Warcraft.UI.Buildings
{
    class Barracks : UI
    {
        BarracksBuilding barracks;
        List<Button> builder = new List<Button>();

        List<int> commandsOrder = new List<int>();

        public Barracks(ManagerMouse managerMouse, BarracksBuilding barracks)
        {
            if (barracks is HumansBarracksBuilding)
            {
                buttonPortrait = new Button(2, 4);
                builder.Add(new Button(0, 260, 4, 0));
                builder.Add(new Button(50, 260, 2, 0));
            }
            else
			{
				buttonPortrait = new Button(3, 4);
            }

            this.barracks = barracks;

            if (managerMouse != null)
                managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
        }

        private void ManagerMouse_MouseEventHandler(object sender, Events.MouseEventArgs e)
        {
            if (barracks.selected && e.SelectRectangle.Width == 0 && e.SelectRectangle.Height == 0)
            {
                for (int i = 0; i < builder.Count; i++)
                {
                    if (e.SelectRectangle.Intersects(builder[i].rectangle))
                    {
                        if (commandsOrder.Count < 9)
                        {
                            if (commandsOrder.Count == 0)
                                barracks.commands[i].execute();

                            if ((barracks.commands[i] as BuilderUnits).go)
                                commandsOrder.Add(i);
                        }
                        break;
                    }
                }
            }
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
            buttonPortrait.LoadContent(content);
        }

        public override void Update()
        {
            for (int i = commandsOrder.Count - 1; i >= 0; i--)
            {
                if ((barracks.commands[commandsOrder[i]] as BuilderUnits).remove)
                {
                    commandsOrder.RemoveAt(i);
                    if (commandsOrder.Count > 0)
                        barracks.commands[commandsOrder[0]].execute();
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (DrawIndividual)
            {
                buttonPortrait.Draw(spriteBatch);

                builder.ForEach((b) => b.Draw(spriteBatch));

                int y = 0;
                for (int i = 0; i < commandsOrder.Count; i++)
                {
                    if (i > 0 && i % 3 == 0)
                        y++;

                    builder[commandsOrder[i]].Draw(spriteBatch, new Vector2(minX + (50 * (i % 3)), 300 + (38 * y)));
                }

                spriteBatch.DrawString(font, barracks.information.Name, new Vector2(minX + 50, 100), Color.Black);
                spriteBatch.DrawString(font, "HP: " + barracks.information.HitPoints, new Vector2(minX, 150), Color.Black);
            }
        }
    }
}
