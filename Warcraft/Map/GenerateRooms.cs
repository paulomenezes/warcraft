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
        public List<Room> Rooms = new List<Room>();

        public GenerateRooms()
        {
			Random rng = new Random();

            int w = rng.Next(32 * 30, 32 * 50);
            Rooms.Add(new Room(0, 0, w, w));

            while (Rooms.Count < 14)
            {
                w = rng.Next(32 * 30, 32 * 50);

                int x = rng.Next(0, (Warcraft.WINDOWS_WIDTH  * 8) - w);
                int y = rng.Next(0, (Warcraft.WINDOWS_HEIGHT * 8) - w);

                x = Functions.Normalize(x);
                y = Functions.Normalize(y);

                Room room = new Room(x, y, w, w);

                bool intersect = false;
                for (int j = 0; j < Rooms.Count; j++)
                {
                    if (Rooms[j].rectangle.Intersects(room.rectangle)) {
                        intersect = true;
                        break;
                    }
                }

                if (!intersect)
                {
                    Rooms.Add(room);
                }
			}
        }
    }
}
