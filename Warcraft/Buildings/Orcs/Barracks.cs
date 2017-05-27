using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Warcraft.Commands;
using Warcraft.Managers;
using Warcraft.Units.Humans;
using Warcraft.Util;

namespace Warcraft.Buildings.Orcs
{
	class Barracks : Neutral.Barracks
	{
		public Barracks(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) :
			base(tileX, tileY, 96, 96, managerMouse, managerMap, managerUnits)
		{
			information = new InformationBuilding("Barracks", 800, 700, 450, Util.Units.PEON, 500, Util.Buildings.ORC_BARRACKS);

			Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
			List<Sprite> spriteBuilding = new List<Sprite>();
			// BUILDING
			spriteBuilding.Add(new Sprite(18, 260, 88, 77));
			spriteBuilding.Add(new Sprite(109, 242, 95, 96));

			sprites.Add(AnimationType.WALKING, spriteBuilding);

			Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
			animations.Add("building", new Frame(0, 2));

			this.animations = new Animation(sprites, animations, "building", width, height, false, information.BuildTime / sprites.Count);

			textureName = "Orc Buildings (Summer) ";

            commands.Add(new BuilderUnits(Util.Units.ELVEN_ARCHER, managerUnits, ElvenArcher.Information));
            commands.Add(new BuilderUnits(Util.Units.FOOTMAN, managerUnits, Footman.Information));
		}
	}
}
