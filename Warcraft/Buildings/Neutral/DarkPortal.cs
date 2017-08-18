using System;
using System.Collections.Generic;
using Warcraft.Managers;
using Warcraft.Util;

namespace Warcraft.Buildings.Neutral
{
    class DarkPortal : Building
    {
        public DarkPortal(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits)
            : base(tileX, tileY, 128, 128, managerMouse, managerMap, managerUnits)
        {
			information = new InformationBuilding("Dark Portal", 25500, 0, 0, Util.Units.NONE, 0, Util.Buildings.DARK_PORTAL);

			Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
			List<Sprite> spriteBuilding = new List<Sprite>();
			// NORMAL
			spriteBuilding.Add(new Sprite(325, 658, 128, 128));
			
			sprites.Add(AnimationType.WALKING, spriteBuilding);

			Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
			animations.Add("normal", new Frame(0, 1));

			this.animations = new Animation(sprites, animations, "normal", width, height, false, 0);

			textureName = "Human Buildings (Summer)";

			unselected = true;
			isWorking = true;

			managerMap.AddWalls(Position, width / 32, height / 32);
		}
    }
}
