using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Warcraft.Util
{
    class Progress
    {
        Vector2 position;
        int max;
        int width;
        int height;

        Texture2D texture;

        float current = 0;
        float progress = 0;

        public bool start;
        public bool finish;

        public Progress(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public void Start(Vector2 position, int max)
        {
            this.position = position;
            this.start = true;
			this.max = max;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Cursor");
        }

        public void Update() 
        {
            if (current < width - 2 && start)
            {
                current += width / ((float)max);
                progress = current / width;
            } 
        }

        public void HP(float current, float max)
        {
            // 1000/1200
            // max - width
            // current - x 
            // max x = width * current
            // x = (width * current) / max
            this.current = (width * current) / max;
            this.progress = this.current / width;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, width, height), Color.Black);
            spriteBatch.Draw(texture, new Rectangle((int)position.X + 1, (int)position.Y + 1, (int)current, height - 2), new Color(1 - progress, progress, 0));
        }
    }
}
