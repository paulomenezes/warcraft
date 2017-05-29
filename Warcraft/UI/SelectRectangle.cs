using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Util;

namespace Warcraft.UI
{
    class SelectRectangle
    {
        private static Texture2D texture;

        public static void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Cursor");
        }

        public static void Draw(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, 1), Color.Black);
            spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y, 1, rectangle.Height), Color.Black);
            spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width, 1), Color.Black);
            spriteBatch.Draw(texture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, 1, rectangle.Height), Color.Black);
		}

		public static void Draw(SpriteBatch spriteBatch, Rectangle rectangle, Color color)
		{
			spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, 1), color);
			spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y, 1, rectangle.Height), color);
			spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width, 1), color);
			spriteBatch.Draw(texture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, 1, rectangle.Height), color);
		}

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end)
		{
			Vector2 edge = end - start;
			float angle = (float)Math.Atan2(edge.Y, edge.X);

            spriteBatch.Draw(texture,
				new Rectangle((int)start.X, (int)start.Y, (int)edge.Length(), 1),
				null, Color.Black,
				angle,
				new Vector2(0, 0),
				SpriteEffects.None,
				0);

		}

        public static void DrawTarget(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height / 2, rectangle.Width, 1), Color.Blue);
            spriteBatch.Draw(texture, new Rectangle(rectangle.X + rectangle.Width / 2, rectangle.Y, 1, rectangle.Height), Color.Blue);
        }

        public static void DrawLine(SpriteBatch spriteBatch, Edge line)
		{
            Vector2 start = new Vector2(line.p1.x, line.p1.y);
            Vector2 end = new Vector2(line.p2.x, line.p2.y);

            Vector2 edge = start - end;
			float angle = (float)Math.Atan2(edge.Y, edge.X);

			spriteBatch.Draw(texture,
				new Rectangle((int)start.X, (int)start.Y, (int)edge.Length(), 1),
                             null, Color.Black,
				angle,
				new Vector2(0, 0),
				SpriteEffects.None,
				0);
        }
    }
}
