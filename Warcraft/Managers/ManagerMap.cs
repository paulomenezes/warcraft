using Warcraft.Map;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System;

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
        private List<List<Tile>> map = new List<List<Tile>>();
        public List<Tile> walls = new List<Tile>();

        int[,,] metadata = new int[50, 50, 4];
        
        public static Dictionary<int[], int[]> Mapping = new Dictionary<int[], int[]>(new MyEqualityComparer());

        public ManagerMap()
        {
            float[,] noise = Noise2D.GenerateNoiseMap(Warcraft.MAP_SIZE, Warcraft.MAP_SIZE, 20);

            for (int i = 0; i < Warcraft.MAP_SIZE; i++)
            {
                List<Tile> t = new List<Tile>();
                for (int j = 0; j < Warcraft.MAP_SIZE; j++)
                {
                    //if (noise[i, j] < 0.2f)
                    //    t.Add(new Tile(i, j, TileType.WATER));
                    //else if (noise[i, j] >= 0.2f && noise[i, j] < 0.4f)
                    //    t.Add(new Tile(i, j, TileType.DESERT));
                    //else if (noise[i, j] >= 0.4f && noise[i, j] < 0.8f)
                    //    t.Add(new Tile(i, j, TileType.GLASS));
                    //else if (noise[i, j] >= 0.8f)
                    //    t.Add(new Tile(i, j, TileType.FLOREST));
                    if (noise[i, j] < 0.5f)
                        t.Add(new Tile(i, j, TileType.GLASS));
                    else if (noise[i, j] >= 0.5f)
                        t.Add(new Tile(i, j, TileType.DESERT));
                }

                map.Add(t);
            }


            //Mapping.Add(new int[4] { (int)TileType.WATER, (int)TileType.DESERT, (int)TileType.WATER, (int)TileType.DESERT }, new int[2] { 7, 11 });
            //Mapping.Add(new int[4] { (int)TileType.DESERT, (int)TileType.GLASS, (int)TileType.GLASS, (int)TileType.GLASS }, new int[2] { 4, 14 });
            //Mapping.Add(new int[4] { (int)TileType.GLASS, (int)TileType.GLASS, (int)TileType.DESERT, (int)TileType.DESERT }, new int[2] { 9, 16 });

            //Mapping.Add(new int[4] { (int)TileType.WATER, (int)TileType.DESERT, (int)TileType.DESERT, (int)TileType.DESERT }, new int[2] { 17, 10 });
            //Mapping.Add(new int[4] { (int)TileType.DESERT, (int)TileType.WATER, (int)TileType.DESERT, (int)TileType.DESERT }, new int[2] { 18, 10 });
            //Mapping.Add(new int[4] { (int)TileType.DESERT, (int)TileType.DESERT, (int)TileType.DESERT, (int)TileType.GLASS }, new int[2] { 18, 14 });

            //Mapping.Add(new int[4] { (int)TileType.GLASS, (int)TileType.DESERT, (int)TileType.DESERT, (int)TileType.DESERT }, new int[2] { 14, 14 });

            //Mapping.Add(new int[4] { (int)TileType.GLASS, (int)TileType.FLOREST, (int)TileType.GLASS, (int)TileType.FLOREST }, new int[2] { 3, 7 });
            //Mapping.Add(new int[4] { (int)TileType.GLASS, (int)TileType.FLOREST, (int)TileType.FLOREST, (int)TileType.FLOREST }, new int[2] { 15, 14 });

            // OrganizeMap();
        }

        public void OrganizeMap()
        {
            for (int i = 0; i < map.Count; i++)
            {
                for (int j = 0; j < map[i].Count; j++)
                {
                    TileType[] n = MapNeighbourhood(i, j, map[i][j].tileType);

                    map[i][j].ChangeTexture(n);
                }
            }
        }

        private TileType[] MapNeighbourhood(int i, int j, TileType tileType)
        {
            TileType[] neighbourhood = new TileType[4];

            neighbourhood[0] = map[Math.Max(i - 1, 0)][Math.Max(j - 1, 0)].tileType;
            neighbourhood[1] = map[Math.Max(i - 1, 0)][Math.Min(j + 1, 49)].tileType;
            neighbourhood[2] = map[Math.Min(i + 1, 49)][Math.Max(j - 1, 0)].tileType;
            neighbourhood[3] = map[Math.Min(i + 1, 49)][Math.Min(j + 1, 49)].tileType;

            metadata[i, j, 0] = (int)neighbourhood[0];
            metadata[i, j, 1] = (int)neighbourhood[1];
            metadata[i, j, 2] = (int)neighbourhood[2];
            metadata[i, j, 3] = (int)neighbourhood[3];

            return neighbourhood;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Summer Tiles");

            map.ForEach((item) => item.ForEach((tile) => tile.LoadContent(texture)));
            walls.ForEach((item) => item.LoadContent(texture));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // map.ForEach((item) => item.ForEach((tile) => tile.Draw(spriteBatch)));
            for (int i = 0; i < 50; i++)
            {
                for (int j = 0; j < 50; j++)
                {
                    map[i][j].Draw(spriteBatch);
                }
            }
            walls.ForEach((item) => item.Draw(spriteBatch));
        }

        public void AddWalls(Vector2 position, int xQuantity, int yQuantity)
        {
            for (int i = 0; i < xQuantity; i++)
                for (int j = 0; j < yQuantity; j++)
                    walls.Add(new Tile(((int)position.X / 32) + i, ((int)position.Y / 32) + j));
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
