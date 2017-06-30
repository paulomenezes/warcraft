using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Warcraft.UI;

namespace Warcraft.Map
{
    enum TileType
    {
        WATER,
		DESERT,
		GRASS,
        FLOREST,
        ROCK,
        NONE
    }

    class Tile
    {
        public static Texture2D texture;
        private Rectangle rectangle;
        public Vector2 position;

        public int TileX;
        public int TileY;

		public bool isWall = false;
		public bool isWater = false;

        public TileType tileType = TileType.NONE;

        public Tile(int tileX, int tileY)
        {
            TileX = tileX;
            TileY = tileY;

            isWall = false;

            rectangle = new Rectangle(0, 0, -1, -1);
        }

        public Tile(int tileX, int tileY, TileType type)
        {
            TileX = tileX;
            TileY = tileY;

            isWall = true;

            tileType = type;

            int textureX = 0, textureY = 0;
            switch (type)
            {
                case TileType.WATER:
                    textureX = 5;
                    textureY = 17;
                    break;
                case TileType.GRASS:
                    textureX = 14;
                    textureY = 18;
                    break;
                case TileType.DESERT:
                    textureX = 11;
                    textureY = 17;
                    break;
                case TileType.FLOREST:
                    textureX = 13;
                    textureY = 5;
                    break;
                case TileType.ROCK:
                    textureX = 15;
                    textureY = 7;
                    break;
            }

            position = new Vector2(tileX * Warcraft.TILE_SIZE, tileY * Warcraft.TILE_SIZE);
            rectangle = new Rectangle(textureX * (Warcraft.TILE_SIZE + 1), textureY * (Warcraft.TILE_SIZE + 1), Warcraft.TILE_SIZE, Warcraft.TILE_SIZE);
        }
        
        public Tile(int tileX, int tileY, int textureX, int textureY)
        {
            TileX = tileX;
            TileY = tileY;

            isWall = true;

            position = new Vector2(tileX * Warcraft.TILE_SIZE, tileY * Warcraft.TILE_SIZE);
            rectangle = new Rectangle(textureX * (Warcraft.TILE_SIZE + 1), textureY * (Warcraft.TILE_SIZE + 1), Warcraft.TILE_SIZE, Warcraft.TILE_SIZE);
        }

        public void ChangeTexture(int textureX, int textureY)
        {
            rectangle = new Rectangle(textureX * (Warcraft.TILE_SIZE + 1), textureY * (Warcraft.TILE_SIZE + 1), Warcraft.TILE_SIZE, Warcraft.TILE_SIZE);
        }

        public void ChangeTexture(TileType[] n)
        {
            if (tileType != TileType.NONE)
            {
                int[] nn = { (int)n[0], (int)n[1], (int)n[2], (int)n[3] };
                if (Managers.ManagerMap.Mapping.ContainsKey(nn))
                {
                    int[] t = Managers.ManagerMap.Mapping[nn];
                    ChangeTexture(t[0], t[1]);
                }
            }
        }

        public void LoadContent(Texture2D _texture)
        {
            if (texture == null)
                texture = _texture;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (rectangle.Width >= 0)
                spriteBatch.Draw(texture, position, rectangle, Color.White);
            else
                SelectRectangle.Draw(spriteBatch, new Rectangle(TileX * 32, TileY * 32, 32, 32));
        }

        private bool ArrayEquals(TileType[] arr1, TileType[] arr2)
        {
            if (arr1.Length != arr2.Length) return false;

            for (int i = 0; i < arr1.Length; i++)
                if (arr1[i] != arr2[i]) return false;

            return true;
        }
    }
}
