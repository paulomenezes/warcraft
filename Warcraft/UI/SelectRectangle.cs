using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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

        public static void DrawTarget(SpriteBatch spriteBatch, Rectangle rectangle)
        {
            spriteBatch.Draw(texture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height / 2, rectangle.Width, 1), Color.Blue);
            spriteBatch.Draw(texture, new Rectangle(rectangle.X + rectangle.Width / 2, rectangle.Y, 1, rectangle.Height), Color.Blue);
        }
    }
}
