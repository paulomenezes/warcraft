using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Warcraft.Scenes
{
    abstract class Scene
    {
        public Scene()
        {
        }

        public abstract void Initializer();

        public abstract void LoadContent(ContentManager Content);

        public abstract void Update();

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
