using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Microsoft.Xna.Framework;
using Warcraft.Managers;

namespace Warcraft.Util
{
    class Functions
    {
		public static ManagerMap managerMap;
		static RandomNumberGenerator _rand = RandomNumberGenerator.Create();

        private static List<int> tabuListX = new List<int>();
        private static List<int> tabuListY = new List<int>();

		public static Vector2 CleanPosition()
		{
			Random random = new Random(DateTime.Now.Millisecond);

			int x = 0;
			int y = 0;
			do
			{
				x = random.Next(3, Warcraft.MAP_SIZE - 5);
				y = random.Next(3, Warcraft.MAP_SIZE - 5);
			} while (managerMap.CheckWalls(new Vector2(x * 32, y * 32), 3, 3));

			return new Vector2(x * 32, y * 32);
		}

		public static Vector2 CleanPosition(ManagerMap managerMap, int width, int height)
        {
            Random random = new Random(DateTime.Now.Millisecond);

            int x = 0;
			int y = 0;
			do
			{
				x = random.Next(3, Warcraft.MAP_SIZE - 5);
                y = random.Next(3, Warcraft.MAP_SIZE - 5);
            } while (managerMap.CheckWalls(new Vector2(x * 32, y * 32), width / 32, height / 32));

            if (tabuListX.Contains(x) && tabuListY.Contains(y)) {
                var a = 2;
            }

            tabuListX.Add(x);
            tabuListY.Add(y);

            return new Vector2(x * 32, y * 32);
		}

		public static int Normalize(float value)
		{
			return (int)(value / 32) * 32;
		}

		static int RandomNext(int min, int max)
		{
			byte[] bytes = new byte[4];
			_rand.GetBytes(bytes);

			uint next = BitConverter.ToUInt32(bytes, 0);
			int range = max - min;

			return (int)((next % range) + min);
		}
	}
}
