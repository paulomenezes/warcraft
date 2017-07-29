using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.UI;

namespace Warcraft.Map
{
    public class Room
    {
        public Rectangle rectangle;

        public Room(int x, int y, int w, int h)
		{
            this.rectangle = new Rectangle(x, y, w, h);
		}

        public void Draw(SpriteBatch spriteBatch) 
        {
            SelectRectangle.DrawFilled(spriteBatch, rectangle);
        }
    }
}
