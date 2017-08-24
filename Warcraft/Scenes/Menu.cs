using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Warcraft.Managers;

namespace Warcraft.Scenes
{
    class Menu : Scene
    {
		Texture2D logo, play, exit;
        public Rectangle rPlay, rExit;
        ManagerMap managerMap;

        public Menu()
        {
        }

        public override void Initializer()
        {
            managerMap = new ManagerMap(new Map.Room(0, 0, Warcraft.WINDOWS_WIDTH + 200, Warcraft.WINDOWS_HEIGHT));
		}

        public override void LoadContent(ContentManager Content)
		{
            managerMap.LoadContent(Content);

			logo = Content.Load<Texture2D>("Risland");
			play = Content.Load<Texture2D>("Play");
			exit = Content.Load<Texture2D>("Exit");

            rPlay = new Rectangle((Warcraft.WINDOWS_WIDTH + 200) / 2 - play.Width / 2, 400, play.Width, play.Height);
            rExit = new Rectangle((Warcraft.WINDOWS_WIDTH + 200) / 2 - exit.Width / 2, 500, exit.Width, exit.Height);
		}

		public override void Update()
		{
			
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
            spriteBatch.Begin();
            managerMap.Draw(spriteBatch);
            spriteBatch.Draw(logo, new Vector2((Warcraft.WINDOWS_WIDTH + 200) / 2 - logo.Width / 2, 200), Color.White);
            spriteBatch.Draw(play, rPlay, Color.White);
            spriteBatch.Draw(exit, rExit, Color.White);
            spriteBatch.End();
		}
    }
}
