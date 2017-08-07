using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Map;
using Warcraft.Util;

namespace Warcraft.Managers
{
    class ManagerIsland
    {
        List<ManagerMap> managerMap = new List<ManagerMap>();
        public static List<Room> rooms = new List<Room>();

        public ManagerIsland(ManagerMouse managerMouse)
        {
            GenerateRooms generateRooms = new GenerateRooms();
            rooms = generateRooms.Rooms;

            for (int i = 0; i < rooms.Count; i++)
            {
                managerMap.Add(new ManagerMap(rooms[i]));
            }


            Functions.managerMap = CurrentMap();

            for (int i = 0; i < managerMap.Count; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Vector2 pos = Functions.CleanPosition(managerMap[i], rooms[i].rectangle);
                    pos = pos + new Vector2(rooms[i].rectangle.X, rooms[i].rectangle.Y);

					ManagerBuildings.goldMines.Add(new Buildings.Neutral.GoldMine((int)(pos.X / Warcraft.TILE_SIZE), (int)(pos.Y / Warcraft.TILE_SIZE), managerMouse, managerMap[i], null));
				}
			}

		}

        public void LoadContent(ContentManager content)
        {
            managerMap.ForEach(mp => mp.LoadContent(content));
        }

        public ManagerMap CurrentMap()
        {
            return managerMap[Warcraft.PLAYER_ISLAND];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            CurrentMap().Draw(spriteBatch);
        }
    }
}
