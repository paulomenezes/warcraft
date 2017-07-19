using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.UI;

namespace Warcraft.Map
{
    public class Room
    {
        Vector2 position;
        Vector2 size;
        int area;

        public Room(Vector2 position, Vector2 size, int area) : this(position, size)
		{
			this.area = area;
		}
       
		public Room(Vector2 position, Vector2 size)
		{
			this.position = position;
			this.size = size;
            this.area = (int)(size.X * size.Y);
		}

		public Vector2 GetPosition()
		{
			return position;
		}

		public Vector2 GetSize()
		{
			return size;
		}

		public void SetPosition(Vector2 position)
		{
			this.position = position;
		}

		public Boolean TooCloseTo(Room room)
		{
			return 
                position.X + size.X / 2 > room.GetPosition().X - room.GetSize().X / 2 || 
                position.Y + size.Y / 2 > room.GetPosition().Y - room.GetSize().Y / 2 ||
				position.X - size.X / 2 < room.GetPosition().X + room.GetSize().X / 2 || 
                position.Y - size.Y / 2 < room.GetPosition().Y + room.GetSize().Y / 2;
		}

        public void Draw(SpriteBatch spriteBatch) 
        {
            SelectRectangle.Draw(spriteBatch, new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y));
        }
    }
}
