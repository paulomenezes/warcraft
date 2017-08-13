using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Util;
using Warcraft.Managers;
using Warcraft.Commands;

namespace Warcraft.Units.Humans
{
    class Knight : Unit
	{
		public Knight(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits, ManagerBuildings managerBuildings)
			: base(tileX, tileY, 64, 64, 1, managerMouse, managerMap, managerUnits)
		{
			Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
			List<Sprite> spriteWalking = new List<Sprite>();
			// UP
			spriteWalking.Add(new Sprite(24, 9, 34, 64));
			spriteWalking.Add(new Sprite(21, 83, 37, 64));
			spriteWalking.Add(new Sprite(18, 157, 40, 62));
			spriteWalking.Add(new Sprite(21, 233, 37, 62));
			spriteWalking.Add(new Sprite(24, 305, 34, 64));
			// DOWN
			spriteWalking.Add(new Sprite(318, 18, 34, 49));
			spriteWalking.Add(new Sprite(318, 92, 34, 48));
			spriteWalking.Add(new Sprite(318, 164, 34, 49));
			spriteWalking.Add(new Sprite(317, 238, 35, 57));
			spriteWalking.Add(new Sprite(317, 312, 35, 59));
			// LEFT
			spriteWalking.Add(new Sprite(160, 19, 61, 42));
			spriteWalking.Add(new Sprite(160, 93, 59, 40));
			spriteWalking.Add(new Sprite(156, 168, 63, 39));
			spriteWalking.Add(new Sprite(156, 242, 64, 41));
			spriteWalking.Add(new Sprite(158, 315, 62, 41));
			// UP-RIGHT
			spriteWalking.Add(new Sprite(94, 9, 47, 58));
			spriteWalking.Add(new Sprite(91, 83, 49, 54));
			spriteWalking.Add(new Sprite(89, 159, 50, 50));
			spriteWalking.Add(new Sprite(90, 233, 49, 56));
			spriteWalking.Add(new Sprite(91, 305, 50, 60));
			// DOWN-RIGHT
			spriteWalking.Add(new Sprite(241, 21, 47, 40));
			spriteWalking.Add(new Sprite(240, 93, 48, 41));
			spriteWalking.Add(new Sprite(240, 167, 46, 43));
			spriteWalking.Add(new Sprite(236, 240, 48, 53));
			spriteWalking.Add(new Sprite(239, 315, 47, 47));

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
            sprites.Add(AnimationType.ATTACKING, spriteAttacking);

			List<Sprite> spriteDie = new List<Sprite>();
			spriteDie.Add(new Sprite(11, 752, 57, 39));
			sprites.Add(AnimationType.DYING, spriteDie);

			Dictionary<AnimationType, int> framesCount = new Dictionary<AnimationType, int>();
			framesCount.Add(AnimationType.WALKING, 5);
			framesCount.Add(AnimationType.ATTACKING, 4);
			framesCount.Add(AnimationType.DYING, 1);

			Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
			animations.Add("up", new Frame(framesCount));
			animations.Add("down", new Frame(framesCount));
			animations.Add("right", new Frame(framesCount));
			animations.Add("left", new Frame(framesCount, true));
			animations.Add("upRight", new Frame(framesCount));
			animations.Add("downRight", new Frame(framesCount));
			animations.Add("upLeft", new Frame(framesCount, true));
			animations.Add("downLeft", new Frame(framesCount, true));
			animations.Add("dying", new Frame(0, 1));

			this.animations = new Animation(sprites, animations, "down", width, height);

            ui = new UI.Units.Knight(managerMouse, this);
			textureName.Add(AnimationType.WALKING, "Knight");

            information = new InformationUnit("Knight", Race.HUMAN, Faction.ALLIANCE, 90, 8, 360, 13, 800, 1, Util.Buildings.BARRACKS, 90 * Warcraft.FPS, 8, 100, 1, 0, Util.Units.KNIGHT);
			Information = information;
		}

		public override void LoadContent(ContentManager content)
		{
			base.LoadContent(content);

			if (!texture.ContainsKey(AnimationType.DYING))
				texture.Add(AnimationType.DYING, texture[AnimationType.WALKING]);
			if (!texture.ContainsKey(AnimationType.ATTACKING))
				texture.Add(AnimationType.ATTACKING, content.Load<Texture2D>(textureName[AnimationType.WALKING]));
		}
	}
}
