using System.Collections.Generic;
using Warcraft.Managers;
using Warcraft.Util;

namespace Warcraft.Buildings.Orcs
{
	class PigFarm : Building
	{
		public PigFarm(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) :
			base(tileX, tileY, 64, 64, managerMouse, managerMap, managerUnits)
		{
            information = new InformationBuilding("Pig Farm", 800, 700, 450, Util.Units.PEON, 300, Util.Buildings.PIG_FARM);

			Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
			List<Sprite> spriteBuilding = new List<Sprite>();
			// BUILDING
			spriteBuilding.Add(new Sprite(272, 603, 63, 64));
			spriteBuilding.Add(new Sprite(337, 603, 64, 64));

			sprites.Add(AnimationType.WALKING, spriteBuilding);

			Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
			animations.Add("building", new Frame(0, 2));

			this.animations = new Animation(sprites, animations, "building", width, height, false, information.BuildTime / sprites.Count);

			textureName = "Orc Buildings (Summer) ";

			unselected = true;
		}
	}
}
