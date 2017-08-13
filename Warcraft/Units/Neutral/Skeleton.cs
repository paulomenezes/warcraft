using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Managers;
using Warcraft.Util;

namespace Warcraft.Units.Neutral
{
    class Skeleton : Unit
    {
        public Skeleton(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits)
            : base(tileX, tileY, 52, 52, 1, managerMouse, managerMap, managerUnits)
        {
            Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
            List<Sprite> spriteWalking = new List<Sprite>();
            // UP
            spriteWalking.Add(new Sprite(14, 10, 28, 30));
            spriteWalking.Add(new Sprite(13, 52, 30, 42));
            spriteWalking.Add(new Sprite(11, 112, 33, 27));
            spriteWalking.Add(new Sprite(14, 152, 32, 57));
            // DOWN
            spriteWalking.Add(new Sprite(257, 11, 29, 29));
            spriteWalking.Add(new Sprite(257, 52, 28, 40));
            spriteWalking.Add(new Sprite(256, 106, 34, 36));
            spriteWalking.Add(new Sprite(259, 152, 32, 36));
            // LEFT
            spriteWalking.Add(new Sprite(143, 10, 14, 33));
            spriteWalking.Add(new Sprite(134, 54, 33, 30));
            spriteWalking.Add(new Sprite(138, 99, 30, 46));
            spriteWalking.Add(new Sprite(137, 152, 28, 31));
            // UP-RIGHT
            spriteWalking.Add(new Sprite(80, 10, 18, 32));
            spriteWalking.Add(new Sprite(74, 48, 25, 39));
            spriteWalking.Add(new Sprite(68, 99, 34, 39));
            spriteWalking.Add(new Sprite(76, 150, 24, 34));
            // DOWN-RIGHT
            spriteWalking.Add(new Sprite(198, 11, 28, 31));
            spriteWalking.Add(new Sprite(196, 55, 36, 35));
            spriteWalking.Add(new Sprite(205, 104, 20, 40));
            spriteWalking.Add(new Sprite(198, 155, 35, 33));
            sprites.Add(AnimationType.WALKING, spriteWalking);

            List<Sprite> spriteAttacking = new List<Sprite>();
            // UP
            spriteAttacking.Add(new Sprite(13, 197, 32, 37));
            spriteAttacking.Add(new Sprite(14, 248, 28, 40));
            spriteAttacking.Add(new Sprite(10, 293, 28, 40));
			spriteAttacking.Add(new Sprite(14, 349, 29, 38));
			spriteAttacking.Add(new Sprite(5, 391, 32, 32));
            // DOWN
            spriteAttacking.Add(new Sprite(260, 212, 35, 29));
            spriteAttacking.Add(new Sprite(263, 250, 27, 37));
            spriteAttacking.Add(new Sprite(269, 306, 26, 35));
			spriteAttacking.Add(new Sprite(260, 350, 30, 32));
			spriteAttacking.Add(new Sprite(269, 394, 29, 33));
            // LEFT
            spriteAttacking.Add(new Sprite(143, 194, 21, 47));
            spriteAttacking.Add(new Sprite(134, 247, 34, 30));
            spriteAttacking.Add(new Sprite(139, 306, 39, 35));
			spriteAttacking.Add(new Sprite(136, 348, 31, 31));
			spriteAttacking.Add(new Sprite(133, 397, 28, 32));
            // UP-RIGHT
            spriteAttacking.Add(new Sprite(78, 194, 30, 40));
            spriteAttacking.Add(new Sprite(75, 247, 33, 39));
            spriteAttacking.Add(new Sprite(78, 300, 37, 36));
			spriteAttacking.Add(new Sprite(75, 347, 34, 39));
			spriteAttacking.Add(new Sprite(78, 397, 33, 31));
            // DOWN-RIGHT
            spriteAttacking.Add(new Sprite(200, 198, 28, 44));
            spriteAttacking.Add(new Sprite(200, 248, 27, 34));
            spriteAttacking.Add(new Sprite(194, 310, 34, 31));
			spriteAttacking.Add(new Sprite(200, 348, 23, 34));
			spriteAttacking.Add(new Sprite(190, 397, 30, 32));
            sprites.Add(AnimationType.ATTACKING, spriteAttacking);

            List<Sprite> spriteDiying = new List<Sprite>();
            spriteDiying.Add(new Sprite(187, 447, 39, 34));
            sprites.Add(AnimationType.DYING, spriteDiying);

            Dictionary<string, Frame> animations = new Dictionary<string, Frame>();

            Dictionary<AnimationType, int> framesCount = new Dictionary<AnimationType, int>();
            framesCount.Add(AnimationType.WALKING, 4);
            framesCount.Add(AnimationType.ATTACKING, 5);
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

            textureName.Add(AnimationType.WALKING, "Skeleton");

            information = new InformationUnit("Skeleton", Race.NEUTRAL, Faction.NEUTRAL, 40, 0, 360, 10, 0, 0, Util.Buildings.NONE, 300, 6, 100, 1, 0, Util.Units.SKELETON);
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

        public override void Update()
        {
            base.Update();

            if (!transition && target == null && targetBuilding == null)
            {
                Vector2 pos = Util.Functions.CleanPosition(managerMap, width, height);
                Move(Functions.Normalize(pos.X) / 32, Functions.Normalize(pos.Y) / 32);
            }
        }
    }
}