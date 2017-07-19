using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.UI;
using Warcraft.Util;

namespace Warcraft.Map
{
    public class GenerateRooms
	{
		List<Room> rooms = new List<Room>();

        public GenerateRooms()
        {
            Random rng = new Random();

            for (int i = 0; i < 10; i++)
            {
                int w = rng.Next(64, 100);
                int h = rng.Next(64, 100);

				int x = 500 + rng.Next(0, 200);
				int y = 250 + rng.Next(0, 200);

                Room room = new Room(new Vector2(x, y), new Vector2(w, h));
                rooms.Add(room);
			}

            separateRooms();
            ///SeparateRooms();
        }

        public void Update() {
            //SeparateRooms();
        }

		public void Draw(SpriteBatch spriteBatch)
        {
            rooms.ForEach(r => r.Draw(spriteBatch));
        }

		void SeparateRooms()
		{
			foreach (Room room in rooms)
			{
				Vector2 oldPos = room.GetPosition();
				Vector2 separation = computeSeparation(room);
				Vector2 newPos = new Vector2(oldPos.X + separation.X, oldPos.Y + separation.Y);
                newPos = Vector2.Min(newPos, Vector2.Zero);
				room.SetPosition(newPos);
			}
		}

		Vector2 computeSeparation(Room agent)
		{

			int neighbours = 0;
			Vector2 v = new Vector2();

			foreach (Room room in rooms)
			{
				if (room != agent)
				{
					if (agent.TooCloseTo(room))
					{
						v.X += Difference(room, agent, "x");
						v.Y += Difference(room, agent, "y");
						neighbours++;
					}
				}
			}

			if (neighbours == 0)
				return v;

            v.X /= neighbours;
			v.X *= -1;
			
            v.Y /= neighbours;
            v.Y *= -1;
			//v.Normalize();
			return v;
		}

		void separateRooms()
		{
			Room a, b; // to hold any two rooms that are over lapping
            float dx, dxa, dxb, dy, dya, dyb; // holds delta values of the overlap
			bool touching; // a boolean flag to keep track of touching rooms
            float padding = 10;
			do
			{
				touching = false;
                for (int i = 0; i < rooms.Count; i++)
				{
					a = rooms[i];
                    for (int j = i + 1; j < rooms.Count; j++)
					{ // for each pair of rooms (notice i+1)
						b = rooms[j];
                        if (a.TooCloseTo(b)) //touches(b, padding))
						{ // if the two rooms touch (allowed to overlap by 1)
							touching = true; // update the touching flag so the loop iterates again
											 // find the two smallest deltas required to stop the overlap
                            dx = Math.Min(a.GetPosition().X + a.GetSize().X - b.GetPosition().X + padding, a.GetPosition().X - b.GetPosition().X + b.GetSize().X - padding);
                            dy = Math.Min(a.GetPosition().Y + a.GetSize().Y - b.GetPosition().Y + padding, a.GetPosition().Y - b.GetPosition().Y + b.GetSize().Y - padding);
							// only keep the smalled delta
							if (Math.Abs(dx) < Math.Abs(dy)) dy = 0;
							else dx = 0;
							// create a delta for each rectangle as half the whole delta.
							dxa = -dx / 2;
							dxb = dx + dxa;
							// same for y
							dya = -dy / 2;
							dyb = dy + dya;
                            // shift both rectangles
                            rooms[i].SetPosition(new Vector2(a.GetPosition().X + dxa, a.GetPosition().Y + dya));
                            rooms[j].SetPosition(new Vector2(b.GetPosition().X + dxb, b.GetPosition().Y + dyb));
							//a.shift(dxa, dya);
							//b.shift(dxb, dyb);
						}
					}
				}
			} while (touching); // loop until no rectangles are touching
		}

		float Difference(Room room, Room agent, string type)
		{
			switch (type)
			{
				case "x":
                    float xBottom = (room.GetPosition().X + room.GetSize().X / 2) - (agent.GetPosition().X - agent.GetSize().X / 2);
                    float xTop = (agent.GetPosition().X + agent.GetSize().X / 2) - (room.GetPosition().X - room.GetSize().X / 2);
					return xBottom > 0 ? xBottom : xTop;
				case "y":
                    float xRight = (room.GetPosition().Y + room.GetSize().Y / 2) - (agent.GetPosition().Y - agent.GetSize().Y / 2);
                    float xLeft = (agent.GetPosition().Y + agent.GetSize().Y / 2) - (room.GetPosition().Y - room.GetSize().Y / 2);
					return xRight > 0 ? xRight : xLeft;
				default:
					return 0;
			}
		}
    }
}
