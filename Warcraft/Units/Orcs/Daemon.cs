using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Managers;
using Warcraft.Util;

namespace Warcraft.Units.Orcs
{
    class Daemon : Unit
    {
		public Daemon(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) 
            : base(tileX, tileY, 64, 64, 1, managerMouse, managerMap, managerUnits)
        {
			Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
			List<Sprite> spriteWalking = new List<Sprite>();
			// UP
			spriteWalking.Add(new Sprite(16, 9, 44, 55));
			spriteWalking.Add(new Sprite(15, 85, 45, 56));
			spriteWalking.Add(new Sprite(8, 164, 57, 52));
			spriteWalking.Add(new Sprite(14, 243, 49, 51));
			spriteWalking.Add(new Sprite(16, 319, 45, 50));
			// DOWN
			spriteWalking.Add(new Sprite(321, 6, 43, 54));
			spriteWalking.Add(new Sprite(319, 82, 46, 55));
			spriteWalking.Add(new Sprite(319, 164, 46, 51));
			spriteWalking.Add(new Sprite(318, 243, 49, 50));
			spriteWalking.Add(new Sprite(319, 318, 49, 50));
			// LEFT
			spriteWalking.Add(new Sprite(160, 5, 51, 56));
			spriteWalking.Add(new Sprite(161, 85, 54, 52));
			spriteWalking.Add(new Sprite(160, 168, 58, 46));
			spriteWalking.Add(new Sprite(160, 251, 59, 41));
			spriteWalking.Add(new Sprite(160, 319, 57, 50));
			// UP-RIGHT
			spriteWalking.Add(new Sprite(85, 10, 51, 47));
			spriteWalking.Add(new Sprite(83, 84, 53, 56));
			spriteWalking.Add(new Sprite(83, 166, 56, 49));
			spriteWalking.Add(new Sprite(84, 248, 53, 46));
			spriteWalking.Add(new Sprite(83, 327, 55, 45));
			// DOWN-RIGHT
			spriteWalking.Add(new Sprite(241, 10, 49, 53));
			spriteWalking.Add(new Sprite(237, 86, 58, 54));
			spriteWalking.Add(new Sprite(237, 166, 58, 51));
			spriteWalking.Add(new Sprite(239, 245, 56, 50));
			spriteWalking.Add(new Sprite(241, 321, 49, 52));
			sprites.Add(AnimationType.WALKING, spriteWalking);

			List<Sprite> spriteAttacking = new List<Sprite>();
			// UP
			spriteAttacking.Add(new Sprite(15, 396, 46, 53));
			spriteAttacking.Add(new Sprite(13, 472, 46, 54));
			spriteAttacking.Add(new Sprite(17, 545, 44, 57));
			spriteAttacking.Add(new Sprite(9, 622, 52, 55));
			spriteAttacking.Add(new Sprite(19, 704, 37, 52));
			// DOWN
			spriteAttacking.Add(new Sprite(316, 390, 47, 56));
			spriteAttacking.Add(new Sprite(320, 468, 45, 54));
			spriteAttacking.Add(new Sprite(313, 549, 49, 53));
			spriteAttacking.Add(new Sprite(315, 630, 56, 50));
			spriteAttacking.Add(new Sprite(312, 703, 42, 44));
			// LEFT
			spriteAttacking.Add(new Sprite(160, 400, 53, 40));
			spriteAttacking.Add(new Sprite(160, 483, 59, 34));
			spriteAttacking.Add(new Sprite(160, 554, 57, 49));
			spriteAttacking.Add(new Sprite(160, 622, 59, 55));
			spriteAttacking.Add(new Sprite(160, 707, 57, 42));
			// UP-RIGHT
			spriteAttacking.Add(new Sprite(83, 393, 51, 55));
			spriteAttacking.Add(new Sprite(83, 472, 50, 54));
			spriteAttacking.Add(new Sprite(83, 549, 59, 53));
			spriteAttacking.Add(new Sprite(92, 622, 40, 58));
			spriteAttacking.Add(new Sprite(93, 706, 42, 51));
			// DOWN-RIGHT
			spriteAttacking.Add(new Sprite(237, 401, 59, 36));
			spriteAttacking.Add(new Sprite(237, 478, 59, 35));
			spriteAttacking.Add(new Sprite(237, 551, 56, 52));
			spriteAttacking.Add(new Sprite(240, 628, 56, 47));
			spriteAttacking.Add(new Sprite(241, 710, 49, 37));
			sprites.Add(AnimationType.ATTACKING, spriteAttacking);

			List<Sprite> spriteDiying = new List<Sprite>();
			spriteDiying.Add(new Sprite(163, 782, 41, 49));
			sprites.Add(AnimationType.DYING, spriteDiying);

			Dictionary<string, Frame> animations = new Dictionary<string, Frame>();

			Dictionary<AnimationType, int> framesCount = new Dictionary<AnimationType, int>();
			framesCount.Add(AnimationType.WALKING, 5);
			framesCount.Add(AnimationType.ATTACKING, 4);
			framesCount.Add(AnimationType.DYING, 1);

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

			textureName.Add(AnimationType.WALKING, "Daemon");

            information = new InformationUnit("Daemon", Race.ORC, Faction.HORDE, 200, 10, 360, 10, 500, 1, Util.Buildings.NONE, 70 * Warcraft.FPS, 10, 4, Util.Units.DAEMON, 10);
			Information = information;
		}

        public override void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            base.LoadContent(content);

			if (!texture.ContainsKey(AnimationType.DYING))
				texture.Add(AnimationType.DYING, texture[AnimationType.WALKING]);
			if (!texture.ContainsKey(AnimationType.ATTACKING))
				texture.Add(AnimationType.ATTACKING, content.Load<Texture2D>(textureName[AnimationType.WALKING]));
        }

        public override void Update()
        {
            base.Update();

            if (!transition && target == null && targetBuilding == null)
            {
                Vector2 pos = Functions.CleanPosition(managerMap, ManagerBuildings.goldMines[1].position, 32, 32, 10);
                Move((Functions.Normalize(pos.X) / 32), (Functions.Normalize(pos.Y) / 32));
            }
        }
    }
}
