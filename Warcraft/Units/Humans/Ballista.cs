using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Util;
using Warcraft.Managers;
using Warcraft.Commands;

namespace Warcraft.Units.Humans
{
	class Ballista : Unit
	{
		public Ballista(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits, ManagerBuildings managerBuildings)
			: base(tileX, tileY, 64, 64, 1, managerMouse, managerMap, managerUnits)
		{
			Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
			List<Sprite> spriteWalking = new List<Sprite>();
			spriteWalking.Add(new Sprite(3, 0, 58, 59));
			spriteWalking.Add(new Sprite(269, 0, 55, 60));
			spriteWalking.Add(new Sprite(133, 1, 62, 54));
			spriteWalking.Add(new Sprite(68, 1, 61, 60));
			spriteWalking.Add(new Sprite(200, 0, 62, 62));

			sprites.Add(AnimationType.WALKING, spriteWalking);

			List<Sprite> spriteAttacking = new List<Sprite>();
			// MINER UP
			spriteAttacking.Add(new Sprite(3, 0, 58, 59));
			spriteAttacking.Add(new Sprite(3, 66, 58, 58));
			spriteAttacking.Add(new Sprite(6, 133, 52, 57));
			spriteAttacking.Add(new Sprite(2, 199, 60, 57));
			// MINER DOWN
			spriteAttacking.Add(new Sprite(269, 0, 55, 60));
			spriteAttacking.Add(new Sprite(269, 66, 55, 60));
			spriteAttacking.Add(new Sprite(269, 132, 55, 59));
			spriteAttacking.Add(new Sprite(268, 198, 57, 59));
			// MINER RIGHT
			spriteAttacking.Add(new Sprite(133, 1, 62, 54));
			spriteAttacking.Add(new Sprite(133, 67, 62, 54));
			spriteAttacking.Add(new Sprite(133, 135, 62, 52));
			spriteAttacking.Add(new Sprite(133, 198, 62, 55));
			// MINER UP RIGHT
			spriteAttacking.Add(new Sprite(68, 1, 61, 60));
			spriteAttacking.Add(new Sprite(68, 67, 61, 59));
			spriteAttacking.Add(new Sprite(68, 133, 61, 59));
			spriteAttacking.Add(new Sprite(68, 198, 62, 60));
			// MINER DOWN RIGHT
			spriteAttacking.Add(new Sprite(200, 0, 62, 62));
			spriteAttacking.Add(new Sprite(200, 66, 62, 62));
			spriteAttacking.Add(new Sprite(200, 132, 62, 62));
			spriteAttacking.Add(new Sprite(200, 198, 62, 62));
			sprites.Add(AnimationType.GOLD, spriteAttacking);

			Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
			animations.Add("up", new Frame(0, 1));
			animations.Add("down", new Frame(1, 1));
			animations.Add("right", new Frame(2, 1));
			animations.Add("left", new Frame(2, 1, true));
			animations.Add("upRight", new Frame(3, 1));
			animations.Add("downRight", new Frame(4, 1));
			animations.Add("upLeft", new Frame(3, 1, true));
			animations.Add("downLeft", new Frame(4, 5, true));

			this.animations = new Animation(sprites, animations, "down", width, height);

			ui = new UI.Units.Ballista(managerMouse, this);
			textureName.Add(AnimationType.WALKING, "Ballista");
			textureName.Add(AnimationType.ATTACKING, "Ballista");
			textureName.Add(AnimationType.DYING, "Ballista");

			information = new InformationUnit("Ballista", Race.HUMAN, Faction.ALLIANCE, 30, 0, 4, 10, 400, 1, Util.Buildings.TOWN_HALL, 200, 3, 5, 1, 0, Util.Units.PEASANT);
			Information = information;
		}
	}
}
