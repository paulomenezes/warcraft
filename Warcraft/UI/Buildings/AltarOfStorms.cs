using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Warcraft.UI.Buildings
{
	class AltarOfStorms : UI
	{
        private global::Warcraft.Buildings.Orcs.AltarOfStorms church;

        public AltarOfStorms(global::Warcraft.Buildings.Orcs.AltarOfStorms church)
		{
			buttonPortrait = new Button(2, 7);

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
