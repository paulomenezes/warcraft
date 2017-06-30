using Warcraft.Map;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;
using System.IO;
using Warcraft.UI;

namespace Warcraft.Managers
{
    public class MyEqualityComparer : IEqualityComparer<int[]>
    {
        public bool Equals(int[] x, int[] y)
        {
            if (x.Length != y.Length)
            {
                return false;
            }
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i] != y[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int GetHashCode(int[] obj)
        {
            int result = 17;
            for (int i = 0; i < obj.Length; i++)
            {
                unchecked
                {
                    result = result * 23 + obj[i];
                }
            }
            return result;
        }
    }

    class ManagerMap
    {
        private Texture2D texture;
        public List<List<Tile>> map = new List<List<Tile>>();
		private List<List<int>> match = new List<List<int>>();
        public List<Tile> walls = new List<Tile>();
		public List<Tile> water = new List<Tile>();

        public static Dictionary<int[], int[]> Mapping = new Dictionary<int[], int[]>(new MyEqualityComparer());

        public static SpriteFont font;

        public ManagerMap()
        {
            float[,] noise = new float[Warcraft.MAP_SIZE, Warcraft.MAP_SIZE]; 

			String mapValues = "";

            using (var filestream = File.Open("../../Content/map.txt", FileMode.Open))
            using (var binaryStream = new BinaryReader(filestream))
            {
            	while (binaryStream.PeekChar() != -1)
            	{
                    mapValues += binaryStream.ReadChar();
            	}
            }

            String[] values = mapValues.Split(',');
            int vX = 0, vY = 0;
            for (int i = 0; i < values.Length; i++)
            {
                noise[vX, vY] = (float)Convert.ToDouble(values[i]);

                vX++;
                if (vX == 50) {
                    vY++;
                    vX = 0;
                }
            }

			//float[,] noise = Noise2D.GenerateNoiseMap(Warcraft.MAP_SIZE, Warcraft.MAP_SIZE, 20);

			for (int i = 0; i < Warcraft.MAP_SIZE; i++)
            {
                List<Tile> t = new List<Tile>();
                for (int j = 0; j < Warcraft.MAP_SIZE; j++)
                {
					if (noise[i, j] < 0.4f)
						t.Add(new Tile(i, j, TileType.GRASS));
					else if (noise[i, j] >= 0.4f)
						t.Add(new Tile(i, j, TileType.DESERT));
                    
                    //if (noise[i, j] < 0.1f)
                    //    t.Add(new Tile(i, j, TileType.FLOREST));
                    //else if (noise[i, j] >= 0.1f && noise[i, j] < 0.3f)
                    //    t.Add(new Tile(i, j, TileType.GRASS));
                    //else if (noise[i, j] >= 0.3f && noise[i, j] < 0.7f)
                    //    t.Add(new Tile(i, j, TileType.DESERT));
                    //else if (noise[i, j] >= 0.7f)
                        //t.Add(new Tile(i, j, TileType.WATER));
                }

                map.Add(t);
            }

            for (int i = 1; i < Warcraft.MAP_SIZE + 2; i++)
            {
                List<int> t = new List<int>();
                for (int j = 1; j < Warcraft.MAP_SIZE + 2; j++)
				{
					int x = Math.Min(i, Warcraft.MAP_SIZE);
					int y = Math.Min(j, Warcraft.MAP_SIZE);

					t.Add((int)map[x - 1][y - 1].tileType);
                }

                match.Add(t);
            }

            Dictionary<int[], int[]> matches = new Dictionary<int[], int[]>(new MyEqualityComparer());
			// Water to Desert
			matches.Add(new int[] { 0, 0, 0, 1 }, new int[] { 11, 11 });
			matches.Add(new int[] { 0, 0, 1, 0 }, new int[] { 0, 12 });
			matches.Add(new int[] { 0, 0, 1, 1 }, new int[] { 2, 11 });
			matches.Add(new int[] { 0, 1, 0, 0 }, new int[] { 5, 12 });
			matches.Add(new int[] { 0, 1, 0, 1 }, new int[] { 7, 11 });
			matches.Add(new int[] { 0, 1, 1, 0 }, new int[] { 14, 11 });
			matches.Add(new int[] { 0, 1, 1, 1 }, new int[] { 17, 10 });
			matches.Add(new int[] { 1, 0, 0, 0 }, new int[] { 6, 12 });
			matches.Add(new int[] { 1, 0, 0, 1 }, new int[] { 8, 12 });
			matches.Add(new int[] { 1, 0, 1, 0 }, new int[] { 16, 11 });
			matches.Add(new int[] { 1, 0, 1, 1 }, new int[] { 0, 11 });
			matches.Add(new int[] { 1, 1, 0, 0 }, new int[] { 2, 12 });
			matches.Add(new int[] { 1, 1, 0, 1 }, new int[] { 5, 11 });
			matches.Add(new int[] { 1, 1, 1, 0 }, new int[] { 12, 11 });
            // Desert to Grass
            matches.Add(new int[] { 1, 1, 1, 2 }, new int[] { 18, 14 });
			matches.Add(new int[] { 1, 1, 2, 1 }, new int[] { 7, 15 });
			matches.Add(new int[] { 1, 1, 2, 2 }, new int[] { 9, 14 });
			matches.Add(new int[] { 1, 2, 1, 1 }, new int[] { 11, 15 });
			matches.Add(new int[] { 1, 2, 1, 2 }, new int[] { 13, 14 });
			matches.Add(new int[] { 1, 2, 2, 1 }, new int[] { 3, 15 }); //
			matches.Add(new int[] { 1, 2, 2, 2 }, new int[] { 4, 14 });
			matches.Add(new int[] { 2, 1, 1, 1 }, new int[] { 13, 15 });
			matches.Add(new int[] { 2, 1, 1, 2 }, new int[] { 17, 14 });
			matches.Add(new int[] { 2, 1, 2, 1 }, new int[] { 5, 15 });
			matches.Add(new int[] { 2, 1, 2, 2 }, new int[] { 6, 14 });
			matches.Add(new int[] { 2, 2, 1, 1 }, new int[] { 8, 15 });
			matches.Add(new int[] { 2, 2, 1, 2 }, new int[] { 11, 14 });
			matches.Add(new int[] { 2, 2, 2, 1 }, new int[] { 0, 15 });
			// Grass to Florest
			matches.Add(new int[] { 2, 2, 2, 3 }, new int[] { 3, 7 });
			matches.Add(new int[] { 2, 2, 3, 2 }, new int[] { 18, 6 });
			matches.Add(new int[] { 2, 2, 3, 3 }, new int[] { 11, 5 }); //
			matches.Add(new int[] { 2, 3, 2, 2 }, new int[] { 16, 6 });
			matches.Add(new int[] { 2, 3, 2, 3 }, new int[] { 2, 7 });
			//matches.Add(new int[] { 2, 3, 3, 2 }, new int[] { 0, 0 }); //
			matches.Add(new int[] { 2, 3, 3, 3 }, new int[] { 10, 5 }); ////
			matches.Add(new int[] { 3, 2, 2, 2 }, new int[] { 15, 6 }); //
			//matches.Add(new int[] { 3, 2, 2, 3 }, new int[] { 0, 0 });
			matches.Add(new int[] { 3, 2, 3, 2 }, new int[] { 0, 7 });
			//matches.Add(new int[] { 3, 2, 3, 3 }, new int[] { 12, 5 });
			matches.Add(new int[] { 3, 3, 2, 2 }, new int[] { 10, 6 });
			matches.Add(new int[] { 3, 3, 2, 3 }, new int[] { 13, 5 });//
			matches.Add(new int[] { 3, 3, 3, 2 }, new int[] { 0, 6 });

			for (int i = 0; i < match.Count - 1; i++)
			{
                for (int j = 0; j < match[i].Count - 1; j++)
				{
					int[] key = { match[i][j], match[i + 1][j], match[i][j + 1], match[i + 1][j + 1] };
					for (int k = 0; k < matches.Count; k++)
                    {
                        if (matches.ContainsKey(key)) {
                            map[i][j].ChangeTexture(matches[key][0], matches[key][1]);
                            break;
                        }
                    }

					if (key[0] == 0 || key[1] == 0 || key[2] == 0 || key[3] == 0 || 
                        key[0] == 3 || key[1] == 3 || key[2] == 3 || key[3] == 3)
					{
					    water.Add(new Tile(i, j));
					}
				}
			}

            for (int i = 0; i < water.Count; i++)
            {
                water[i].isWater = true;
                walls.Add(water[i]);
            }
        }


        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Summer Tiles");
            font = content.Load<SpriteFont>("Font");

            map.ForEach((item) => item.ForEach((tile) => tile.LoadContent(texture)));
            walls.ForEach((item) => item.LoadContent(texture));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Warcraft.MAP_SIZE; i++)
            {
                for (int j = 0; j < Warcraft.MAP_SIZE; j++)
                {
                    map[i][j].Draw(spriteBatch);
                }
            }

            walls.ForEach((item) =>
            {
                item.Draw(spriteBatch);

          });
        }

        public void AddWalls(Vector2 position, int xQuantity, int yQuantity)
        {
            for (int i = 0; i < xQuantity; i++)
                for (int j = 0; j < yQuantity; j++)
                    walls.Add(new Tile(((int)position.X / 32) + i, ((int)position.Y / 32) + j));
        }

        public void ResetWalls()
        {
            walls.Clear();
        }

        public void AddWalls(Vector2 position, Rectangle rectangle)
        {
            int tileX = ((int)position.X / 32);
            int tileY = ((int)position.Y / 32);

            int textureX = rectangle.X / 32;
            int textureY = rectangle.Y / 32;

            if (!CheckWalls(tileX, tileY))
                walls.Add(new Tile(tileX, tileY, textureX, textureY));
        }

        private bool ArrayEquals(int[] arr1, int[] arr2)
        {
            if (arr1.Length != arr2.Length) return false;

            for (int i = 0; i < arr1.Length; i++)
                if (arr1[i] != arr2[i]) return false;

            return true;
        }

        public void OrganizeWalls()
        {
            for (int i = 0; i < walls.Count; i++)
            {
                if (walls[i].isWall)
                {
                    int[] n = GetNeighbourhood(walls[i].TileX, walls[i].TileY);

                         if (ArrayEquals(n, new int[] { 0, 1, 1, 0 })) walls[i].ChangeTexture(0, 1);
                    else if (ArrayEquals(n, new int[] { 0, 0, 0, 1 })) walls[i].ChangeTexture(1, 1);
                    else if (ArrayEquals(n, new int[] { 0, 1, 0, 1 })) walls[i].ChangeTexture(2, 1);
                    else if (ArrayEquals(n, new int[] { 0, 0, 1, 1 })) walls[i].ChangeTexture(4, 1);
                    else if (ArrayEquals(n, new int[] { 0, 1, 1, 1 })) walls[i].ChangeTexture(5, 1);
                    else if (ArrayEquals(n, new int[] { 1, 0, 0, 0 })) walls[i].ChangeTexture(6, 1);
                    else if (ArrayEquals(n, new int[] { 1, 1, 0, 0 })) walls[i].ChangeTexture(7, 1);
                    else if (ArrayEquals(n, new int[] { 1, 0, 1, 0 })) walls[i].ChangeTexture(8, 1);
                    else if (ArrayEquals(n, new int[] { 1, 1, 1, 0 })) walls[i].ChangeTexture(10, 1);
                    else if (ArrayEquals(n, new int[] { 1, 0, 0, 1 })) walls[i].ChangeTexture(11, 1);
                    else if (ArrayEquals(n, new int[] { 1, 1, 0, 1 })) walls[i].ChangeTexture(12, 1);
                    else if (ArrayEquals(n, new int[] { 1, 0, 1, 1 })) walls[i].ChangeTexture(13, 1);
                    else if (ArrayEquals(n, new int[] { 1, 1, 1, 1 })) walls[i].ChangeTexture(14, 1);
                    else if (ArrayEquals(n, new int[] { 0, 0, 0, 0 })) walls[i].ChangeTexture(16, 0);
                    else if (ArrayEquals(n, new int[] { 0, 1, 0, 0 })) walls[i].ChangeTexture(17, 0);
                    else if (ArrayEquals(n, new int[] { 0, 0, 1, 0 })) walls[i].ChangeTexture(18, 0);
                }
            }
        }

        private int[] GetNeighbourhood(int tileX, int tileY)
        {
            int[] neighbourhood = new int[4];
            
            for (int i = 0; i < walls.Count; i++)
            {
                if (walls[i].isWall)
                {
                    if (walls[i].TileX == tileX && walls[i].TileY + 1 == tileY) neighbourhood[3] = 1;
                    if (walls[i].TileX == tileX && walls[i].TileY - 1 == tileY) neighbourhood[1] = 1;
                    if (walls[i].TileX - 1 == tileX && walls[i].TileY == tileY) neighbourhood[2] = 1;
                    if (walls[i].TileX + 1 == tileX && walls[i].TileY == tileY) neighbourhood[0] = 1;
                }
            }

            return neighbourhood;
        }

        public bool CheckWalls(Vector2 position, int xQuantity, int yQuantity)
        {
            int pointX = (int)position.X / 32;
            int pointY = (int)position.Y / 32;

            if (pointX < 0 || pointY < 0 ||
                pointX + 1 > Warcraft.MAP_SIZE||
                pointY + 1 > Warcraft.MAP_SIZE)
                return true;

            for (int k = 0; k < walls.Count; k++)
            {
                for (int i = 0; i < xQuantity; i++)
                    for (int j = 1; j < yQuantity; j++)
                        if (walls[k].TileX == pointX + i && walls[k].TileY == pointY + j)
                            return true;
            }

            return false;
        }

        public bool CheckWalls(int pointX, int pointY)
        {
            if (pointX < 0 || pointY < 0 ||
                pointX + 1 > Warcraft.MAP_SIZE ||
                pointY + 1 > Warcraft.MAP_SIZE)
                return true;

            for (int k = 0; k < walls.Count; k++)
            {
                if (walls[k].TileX == pointX && walls[k].TileY == pointY)
                    return true;
            }

            return false;
        }
    }
}
