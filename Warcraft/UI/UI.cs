using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Warcraft.UI
{
    class UI
    {
        public static SpriteFont font;
        public static Texture2D portraits;
        protected int minX = Warcraft.WINDOWS_WIDTH + 10;

        public Button buttonPortrait;

        public static bool DrawIndividual = true;

        public UI()
        {

        }

        public virtual void LoadContent(ContentManager content)
        {
            if (portraits == null || font == null)
            {
                font = content.Load<SpriteFont>("Font");
                portraits = content.Load<Texture2D>("Icons");
            }
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}
