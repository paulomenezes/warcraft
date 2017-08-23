using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Warcraft.UI.Buildings
{
	class Church : UI
	{
        private global::Warcraft.Buildings.Humans.Church church;
		
        public Church(global::Warcraft.Buildings.Humans.Church church)
		{
			buttonPortrait = new Button(2, 6);

            this.church = church;
		}

		public override void LoadContent(ContentManager content)
		{
			base.LoadContent(content);
			buttonPortrait.LoadContent(content);
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (DrawIndividual)
			{
				buttonPortrait.Draw(spriteBatch);

				spriteBatch.DrawString(font, church.information.Name, new Vector2(minX + 50, 100), Color.Black);
			}
		}
	}
}
