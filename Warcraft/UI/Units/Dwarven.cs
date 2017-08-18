using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Managers;
using DwarvenUnit = Warcraft.Units.Humans.Dwarven;

namespace Warcraft.UI.Units
{
	class Dwarven : UI
	{
		DwarvenUnit dwarven;

		public Dwarven(ManagerMouse managerMouse, DwarvenUnit dwarven)
		{
			buttonPortrait = new Button(2, 1);

			this.dwarven = dwarven;

			managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
		}

		private void ManagerMouse_MouseEventHandler(object sender, Events.MouseEventArgs e)
		{
			if (dwarven.selected && e.SelectRectangle.Width == 0 && e.SelectRectangle.Height == 0)
			{

			}
		}

		public override void LoadContent(ContentManager content)
		{
			base.LoadContent(content);
			buttonPortrait.LoadContent(content);
		}

		public override void Update()
		{

		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			if (DrawIndividual)
			{
				buttonPortrait.Draw(spriteBatch);

				spriteBatch.DrawString(font, dwarven.information.Name, new Vector2(minX + 50, 100), Color.Black);
				spriteBatch.DrawString(font, "Armor: " + dwarven.information.Armor, new Vector2(minX, 150), Color.Black);
				spriteBatch.DrawString(font, "Damage: " + dwarven.information.Damage, new Vector2(minX, 170), Color.Black);
				spriteBatch.DrawString(font, "Range: " + dwarven.information.Range, new Vector2(minX, 190), Color.Black);
				spriteBatch.DrawString(font, "Sight: " + dwarven.information.Sight, new Vector2(minX, 210), Color.Black);
				spriteBatch.DrawString(font, "Speed: " + dwarven.information.MovementSpeed, new Vector2(minX, 230), Color.Black);
				spriteBatch.DrawString(font, "Hitpoints: " + dwarven.information.HitPoints + "/" + dwarven.information.HitPointsTotal, new Vector2(minX, 250), Color.Black);
			}
		}
	}
}
