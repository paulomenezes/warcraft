using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Managers;
using BallistaUnit = Warcraft.Units.Humans.Ballista;

namespace Warcraft.UI.Units
{
	class Ballista : UI
	{
		BallistaUnit knight;

		public Ballista(ManagerMouse managerMouse, BallistaUnit knight)
		{
			buttonPortrait = new Button(6, 1);

			this.knight = knight;

			managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
		}

		private void ManagerMouse_MouseEventHandler(object sender, Events.MouseEventArgs e)
		{
			if (knight.selected && e.SelectRectangle.Width == 0 && e.SelectRectangle.Height == 0)
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

				spriteBatch.DrawString(font, knight.information.Name, new Vector2(minX + 50, 100), Color.Black);
				spriteBatch.DrawString(font, "Armor: " + knight.information.Armor, new Vector2(minX, 150), Color.Black);
				spriteBatch.DrawString(font, "Damage: " + knight.information.Damage + " (" + knight.information.Precision + "%)", new Vector2(minX, 170), Color.Black);
				spriteBatch.DrawString(font, "Range: " + knight.information.Range, new Vector2(minX, 190), Color.Black);
				spriteBatch.DrawString(font, "Sight: " + knight.information.Sight, new Vector2(minX, 210), Color.Black);
				spriteBatch.DrawString(font, "Speed: " + knight.information.MovementSpeed, new Vector2(minX, 230), Color.Black);
				spriteBatch.DrawString(font, "Hitpoints: " + knight.information.HitPoints + "/" + knight.information.HitPointsTotal, new Vector2(minX, 250), Color.Black);
			}
		}
	}
}
