using System;
using Microsoft.Xna.Framework;
using Warcraft.Commands;
using Warcraft.Managers;

namespace Warcraft.Buildings.Neutral
{
    class CityHall : Building
    {
		public CityHall(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) :
			base(tileX, tileY, 128, 128, managerMouse, managerMap, managerUnits)
        {
        }

		public override void Update()
		{
			base.Update();

			for (int i = 0; i < commands.Count; i++)
			{
				var c = (commands[i] as BuilderUnits);
				c.Update();

				if (c.completed)
				{
					var p = new Point(((int)Position.X / 32) + ((width / Warcraft.TILE_SIZE) / 2), ((int)Position.Y / 32) + ((height / Warcraft.TILE_SIZE)));
					managerUnits.Factory(c.type, p.X, p.Y, target.X, target.Y);
					c.completed = false;
					c.remove = true;
				}
			}
		}
    }
}
