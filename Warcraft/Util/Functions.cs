using System;
using Microsoft.Xna.Framework;
using Warcraft.Managers;

namespace Warcraft.Util
{
    class Functions
    {
        public static Vector2 CleanPosition(ManagerMap managerMap, int width, int height)
        {
			Random random = new Random();

            int x = 0;
			int y = 0;
			do
			{
				x = random.Next(3, Warcraft.MAP_SIZE - 3);
				y = random.Next(3, Warcraft.MAP_SIZE - 3);
			} while (managerMap.CheckWalls(new Vector2(x * 32, y * 32), width / 32, height / 32));

            return new Vector2(x * 32, y * 32);
		}
    }
}
