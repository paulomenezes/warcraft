using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Managers;
using Warcraft.UI;

namespace Warcraft.Buildings.Neutral
{
    class Church : Building
    {
        Rectangle healthArea;
        int index = -1;

        int elapsed = 0;

        public Church(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) :
            base(tileX, tileY, 96, 96, managerMouse, managerMap, managerUnits)
        {
			unselected = true;
		}

        public override void Update()
        {
            base.Update();

            if (isPlaceSelected && isBuilding)
            {
                healthArea = new Rectangle((int)position.X - (10 * Warcraft.TILE_SIZE / 2) + width / 2, (int)position.Y - (10 * Warcraft.TILE_SIZE / 2) + height / 2, 10 * Warcraft.TILE_SIZE, 10 * Warcraft.TILE_SIZE);
            }

            if (isWorking)
            {
                managerUnits.units.ForEach(unit =>
                {
                    if (unit.rectangle.Intersects(healthArea) && unit.information.HitPoints > 0 && unit.information.HitPoints < unit.information.HitPointsTotal)
                    {
                        index = managerUnits.units.IndexOf(unit);
                    }
                });

                if (index > -1)
                {
                    elapsed++;

                    if (!managerUnits.units[index].rectangle.Intersects(healthArea))
                    {
                        index = -1;
                        elapsed = 0;
                    }
                    else if (elapsed >= 10 * Warcraft.FPS)
                    {
                        elapsed = 0;
                        managerUnits.units[index].information.HitPoints += 0.1f;

                        if (managerUnits.units[index].information.HitPoints >= managerUnits.units[index].information.HitPointsTotal)
                        {
							managerUnits.units[index].information.HitPoints = managerUnits.units[index].information.HitPointsTotal;
							index = -1;
                        }
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (selected)
            {
                SelectRectangle.Draw(spriteBatch, healthArea, Color.Yellow);
            }

            if (index > -1) 
            {
                SelectRectangle.Draw(spriteBatch, managerUnits.units[index].rectangle, Color.YellowGreen);
            }
        }
    }
}
