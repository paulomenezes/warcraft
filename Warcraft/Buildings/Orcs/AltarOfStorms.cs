using System;
using System.Collections.Generic;
using Warcraft.Managers;
using Warcraft.Util;

namespace Warcraft.Buildings.Orcs
{
    class AltarOfStorms : Neutral.Church
    {
		public AltarOfStorms(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) : 
            base(tileX, tileY, managerMouse, managerMap, managerUnits)
        {
            information = new InformationBuilding("Altar of Storms", 700, 900, 500, Util.Units.PEON, 175 * Warcraft.FPS, Util.Buildings.ALTAR_OF_STORMS);

			Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
			List<Sprite> spriteBuilding = new List<Sprite>();
			// BUILDING
			spriteBuilding.Add(new Sprite(576, 708, 48, 39));
			spriteBuilding.Add(new Sprite(572, 836, 61, 52));
			spriteBuilding.Add(new Sprite(217, 252, 91, 86));
			spriteBuilding.Add(new Sprite(308, 242, 96, 96));

			sprites.Add(AnimationType.WALKING, spriteBuilding);

			Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
			animations.Add("building", new Frame(0, 4));

			this.animations = new Animation(sprites, animations, "building", width, height, false, information.BuildTime / spriteBuilding.Count);

			textureName = "Orc Buildings (Summer) ";

            ui = new UI.Buildings.AltarOfStorms(this);
		}
    }
}
