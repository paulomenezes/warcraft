using System;
using System.Collections.Generic;
using Warcraft.Managers;
using Warcraft.Util;

namespace Warcraft.Buildings.Humans
{
    class Church : Neutral.Church
    {
        public Church(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) : 
            base(tileX, tileY, managerMouse, managerMap, managerUnits)
        {
			information = new InformationBuilding("Church", 700, 900, 500, Util.Units.PEASANT, 175 * Warcraft.FPS, Util.Buildings.CHURCH);

			Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
			List<Sprite> spriteBuilding = new List<Sprite>();
			// BUILDING
			spriteBuilding.Add(new Sprite(576, 708, 48, 39));
			spriteBuilding.Add(new Sprite(572, 836, 61, 52));
			spriteBuilding.Add(new Sprite(308, 365, 87, 91));
			spriteBuilding.Add(new Sprite(308, 264, 94, 96));

			sprites.Add(AnimationType.WALKING, spriteBuilding);

			Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
			animations.Add("building", new Frame(0, 4));

			this.animations = new Animation(sprites, animations, "building", width, height, false, information.BuildTime / spriteBuilding.Count);

			textureName = "Human Buildings (Summer)";

            ui = new UI.Buildings.Church(this);
		}
    }
}
