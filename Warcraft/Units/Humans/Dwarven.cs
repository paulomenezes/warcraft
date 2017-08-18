using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Util;
using Warcraft.Managers;
using Warcraft.Commands;

namespace Warcraft.Units.Humans
{
	class Dwarven : Unit
	{
		public Dwarven(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits, ManagerBuildings managerBuildings)
			: base(tileX, tileY, 64, 64, 1, managerMouse, managerMap, managerUnits)
		{
			Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
			List<Sprite> spriteWalking = new List<Sprite>();
			// UP
			spriteWalking.Add(new Sprite(12, 8, 34, 37));
			spriteWalking.Add(new Sprite(11, 58, 38, 40));
			// DOWN
			spriteWalking.Add(new Sprite(244, 6, 35, 42));
			spriteWalking.Add(new Sprite(245, 57, 32, 43));
			// LEFT
			spriteWalking.Add(new Sprite(126, 3, 41, 39));
			spriteWalking.Add(new Sprite(120, 54, 51, 43));
			// UP-RIGHT
			spriteWalking.Add(new Sprite(67, 9, 38, 39));
			spriteWalking.Add(new Sprite(64, 61, 44, 37));
			// DOWN-RIGHT
			spriteWalking.Add(new Sprite(182, 5, 45, 41));
			spriteWalking.Add(new Sprite(179, 56, 45, 41));

			sprites.Add(AnimationType.WALKING, spriteWalking);

			List<Sprite> spriteAttacking = new List<Sprite>();
			// MINER UP
			spriteAttacking.Add(new Sprite(14, 383, 43, 60));
			spriteAttacking.Add(new Sprite(20, 4456, 39, 61));
			spriteAttacking.Add(new Sprite(12, 530, 45, 61));
			spriteAttacking.Add(new Sprite(25, 601, 29, 64));
			// MINER DOWN
			spriteAttacking.Add(new Sprite(319, 381, 32, 53));
			spriteAttacking.Add(new Sprite(320, 455, 29, 53));
			spriteAttacking.Add(new Sprite(320, 529, 37, 54));
			spriteAttacking.Add(new Sprite(320, 608, 33, 57));
			// MINER RIGHT
			spriteAttacking.Add(new Sprite(158, 379, 61, 52));
			spriteAttacking.Add(new Sprite(157, 453, 60, 52));
			spriteAttacking.Add(new Sprite(155, 527, 61, 52));
			spriteAttacking.Add(new Sprite(154, 608, 66, 45));
			// MINER UP RIGHT
			spriteAttacking.Add(new Sprite(91, 379, 46, 59));
			spriteAttacking.Add(new Sprite(90, 460, 44, 52));
			spriteAttacking.Add(new Sprite(94, 527, 43, 59));
			spriteAttacking.Add(new Sprite(95, 601, 49, 59));
			// MINER DOWN RIGHT
			spriteAttacking.Add(new Sprite(232, 379, 48, 48));
			spriteAttacking.Add(new Sprite(226, 453, 52, 48));
			spriteAttacking.Add(new Sprite(233, 527, 48, 48));
			spriteAttacking.Add(new Sprite(236, 608, 56, 47));
			sprites.Add(AnimationType.GOLD, spriteAttacking);

			List<Sprite> spriteDie = new List<Sprite>();
			spriteDie.Add(new Sprite(11, 752, 57, 39));
			sprites.Add(AnimationType.DYING, spriteDie);

			Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
			animations.Add("up", new Frame(0, 2));
			animations.Add("down", new Frame(2, 2));
			animations.Add("right", new Frame(4, 2));
			animations.Add("left", new Frame(4, 2, true));
			animations.Add("upRight", new Frame(6, 2));
			animations.Add("downRight", new Frame(8, 2));
			animations.Add("upLeft", new Frame(6, 2, true));
			animations.Add("downLeft", new Frame(8, 2, true));
			animations.Add("dying", new Frame(0, 1));

			this.animations = new Animation(sprites, animations, "down", width, height);

			ui = new UI.Units.Dwarven(managerMouse, this);
			textureName.Add(AnimationType.WALKING, "Dwarven");
			textureName.Add(AnimationType.ATTACKING, "Dwarven");
			textureName.Add(AnimationType.DYING, "Dwarven");

            information = new InformationUnit("Dwarven", Race.HUMAN, Faction.ALLIANCE, 30, 0, 4, 10, 400, 1, Util.Buildings.TOWN_HALL, 200 * Warcraft.FPS, 3, 1, Util.Units.PEASANT);
			Information = information;
		}
	}
}
