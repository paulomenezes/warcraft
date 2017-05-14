using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Warcraft.Commands;
using Warcraft.Managers;
using TownHallBuilding = Warcraft.Buildings.Humans.TownHall;

namespace Warcraft.UI.Buildings
{
    class TownHall : UI
    {
        TownHallBuilding townHall;

        List<Button> builder = new List<Button>();
        List<ICommand> commandsOrder = new List<ICommand>();

        public TownHall(ManagerMouse managerMouse, TownHallBuilding townHall)
        {
            buttonPortrait = new Button(0, 4);

            builder.Add(new Button(0, 260, 0, 0));

            this.townHall = townHall;

            if (managerMouse != null)
                managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
        }

        private void ManagerMouse_MouseEventHandler(object sender, Events.MouseEventArgs e)
        {
            if (townHall.selected && e.SelectRectangle.Width == 0 && e.SelectRectangle.Height == 0)
            {
                for (int i = 0; i < builder.Count; i++)
                {
                    if (e.SelectRectangle.Intersects(builder[i].rectangle))
                    {
                        if (commandsOrder.Count < 10)
                        {
                            if (commandsOrder.Count == 0)
                                townHall.commands[i].execute();

                            commandsOrder.Add(townHall.commands[i]);
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
                if((commandsOrder[i] as BuilderUnits).remove)
                {
                    commandsOrder.RemoveAt(i);
                    if (commandsOrder.Count > 0)
                        commandsOrder[0].execute();
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

                    builder[0].Draw(spriteBatch, new Vector2(minX + (50 * (i % 3)), 300 + (38 * y)));
                }

                spriteBatch.DrawString(font, townHall.information.Name, new Vector2(minX + 50, 100), Color.Black);
                spriteBatch.DrawString(font, "Gold: " + 0, new Vector2(minX, 150), Color.Black);
                spriteBatch.DrawString(font, "Wood: " + 0, new Vector2(minX, 170), Color.Black);
                spriteBatch.DrawString(font, "Oil: " + 0, new Vector2(minX, 190), Color.Black);
            }
        }
    }
}
