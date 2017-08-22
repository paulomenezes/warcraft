using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Warcraft.Managers;
using Warcraft.Util;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;

namespace Warcraft.Units.Orcs
{
    class Grunt : Unit
    {
        public Grunt(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) 
            : base(tileX, tileY, 52, 52, 1, managerMouse, managerMap, managerUnits)
        {
            Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
            List<Sprite> spriteWalking = new List<Sprite>();
            // UP
            spriteWalking.Add(new Sprite(22, 8, 38, 43));
            spriteWalking.Add(new Sprite(24, 57, 33, 49));
            spriteWalking.Add(new Sprite(23, 115, 35, 44));
            spriteWalking.Add(new Sprite(23, 171, 38, 44));
            spriteWalking.Add(new Sprite(23, 225, 37, 43));
            // DOWN
            spriteWalking.Add(new Sprite(319, 17, 39, 36));
            spriteWalking.Add(new Sprite(319, 71, 33, 36));
            spriteWalking.Add(new Sprite(321, 125, 34, 37));
            spriteWalking.Add(new Sprite(321, 178, 37, 40));
            spriteWalking.Add(new Sprite(321, 233, 37, 35));
            // LEFT
            spriteWalking.Add(new Sprite(179, 9, 28, 42));
            spriteWalking.Add(new Sprite(172, 63, 43, 40));
            spriteWalking.Add(new Sprite(173, 116, 42, 41));
            spriteWalking.Add(new Sprite(169, 173, 36, 41));
            spriteWalking.Add(new Sprite(172, 225, 28, 39));
            // UP-RIGHT
            spriteWalking.Add(new Sprite(96, 6, 35, 45));
            spriteWalking.Add(new Sprite(98, 60, 37, 41));
            spriteWalking.Add(new Sprite(97, 113, 37, 42));
            spriteWalking.Add(new Sprite(93, 168, 46, 47));
            spriteWalking.Add(new Sprite(94, 221, 39, 47));
            // DOWN-RIGHT
            spriteWalking.Add(new Sprite(249, 12, 32, 38));
            spriteWalking.Add(new Sprite(243, 69, 39, 40));
            spriteWalking.Add(new Sprite(245, 121, 38, 41));
            spriteWalking.Add(new Sprite(248, 171, 31, 41));
            spriteWalking.Add(new Sprite(246, 227, 34, 37));
            sprites.Add(AnimationType.WALKING, spriteWalking);

            List<Sprite> spriteAttacking = new List<Sprite>();
            // UP
            spriteAttacking.Add(new Sprite(5, 279, 53, 44));
            spriteAttacking.Add(new Sprite(16, 336, 41, 45));
            spriteAttacking.Add(new Sprite(8, 393, 49, 42));
            spriteAttacking.Add(new Sprite(28, 440, 29, 51));
            // DOWN
            spriteAttacking.Add(new Sprite(320, 284, 53, 36));
            spriteAttacking.Add(new Sprite(320, 327, 35, 48));
            spriteAttacking.Add(new Sprite(316, 385, 49, 49));
            spriteAttacking.Add(new Sprite(315, 454, 35, 51));
            // LEFT
            spriteAttacking.Add(new Sprite(172, 271, 36, 51));
            spriteAttacking.Add(new Sprite(153, 328, 53, 50));
            spriteAttacking.Add(new Sprite(177, 385, 30, 51));
            spriteAttacking.Add(new Sprite(173, 452, 52, 39));
            // UP-RIGHT
            spriteAttacking.Add(new Sprite(89, 278, 45, 44));
            spriteAttacking.Add(new Sprite(79, 335, 55, 43));
            spriteAttacking.Add(new Sprite(84, 392, 55, 44));
            spriteAttacking.Add(new Sprite(104, 445, 46, 46));
            // DOWN-RIGHT
            spriteAttacking.Add(new Sprite(251, 271, 28, 50));
            spriteAttacking.Add(new Sprite(251, 327, 27, 50));
            spriteAttacking.Add(new Sprite(250, 385, 32, 51));
            spriteAttacking.Add(new Sprite(242, 458, 52, 40));
            sprites.Add(AnimationType.ATTACKING, spriteAttacking);

            List<Sprite> spriteDiying = new List<Sprite>();
            spriteDiying.Add(new Sprite(17, 569, 45, 51));
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

            ui = new UI.Units.Grunt(managerMouse, this);
            textureName.Add(AnimationType.WALKING, "Grunt");
            
            information = new InformationUnit("GRUNT", Race.ORC, Faction.HORDE, 60, 6, 360, 10, 600, 1, Util.Buildings.ORC_BARRACKS, 60 * Warcraft.FPS, 6, 1, Util.Units.GRUNT, 3);
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

            //if (!transition && target == null && targetBuilding == null) 
            //{
            //    Vector2 pos = Util.Functions.CleanPosition(managerMap, width, height);
            //    Move(Functions.Normalize(pos.X) / 32, Functions.Normalize(pos.Y) / 32);
            //}
        }
    }
}
