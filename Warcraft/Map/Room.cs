using System;
using Microsoft.Xna.Framework;

namespace Warcraft.Map
{
    public class Room
    {
        public Rectangle rectangle;
        public int area;

        public Room(Rectangle rectangle)
        {
            this.rectangle = rectangle;

            this.area = rectangle.Width * rectangle.Height;
        }

        public Boolean TooCloseTo(Room room) 
        {
            return rectangle.Intersects(room.rectangle);
        }

        public Vector2 GetPosition()
        {
            return new Vector2(rectangle.X, rectangle.Y);
        }

        public void SetPosition(Vector2 newPos)
        {
            this.rectangle.X = (int)newPos.X;
            this.rectangle.Y = (int)newPos.Y;
        }
    }
}
