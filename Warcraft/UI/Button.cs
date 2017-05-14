using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Warcraft.UI
{
    class Button : UI
    {
        private Vector2 position;
        public int TextureX;
        public int TextureY;

        public Rectangle rectangle;

        public Button(int textureX, int textureY)
            : this(0, 100, textureX, textureY)
        {

        }

        public Button(int posX, int posY, int textureX, int textureY)
            : this(posX, posY, textureX * 49, textureY * 41, false)
        {

        }

        public Button(int posX, int posY, int textureX, int textureY, bool absolute)
        {
            position = new Vector2(minX + posX, posY);
            TextureX = textureX;
            TextureY = textureY;

            rectangle = new Rectangle((int)position.X, (int)position.Y, 48, 36);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(portraits, position, new Rectangle(TextureX, TextureY, 48, 36), Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 newPosition)
        {
            spriteBatch.Draw(portraits, newPosition, new Rectangle(TextureX, TextureY, 48, 36), Color.White);
        }
    }
}
