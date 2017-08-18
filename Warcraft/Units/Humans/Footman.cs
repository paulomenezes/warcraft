using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using Warcraft.Managers;
using Warcraft.Util;

namespace Warcraft.Units.Humans
{
    class Footman : Unit
    {
        public Footman(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) 
            : base(tileX, tileY, 52, 52, 2, managerMouse, managerMap, managerUnits)
        {
            Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
            List<Sprite> spriteWalking = new List<Sprite>();
            // UP
            spriteWalking.Add(new Sprite(22, 10, 31, 40));
            spriteWalking.Add(new Sprite(22, 62, 30, 52));
            spriteWalking.Add(new Sprite(22, 119, 30, 49));
            spriteWalking.Add(new Sprite(23, 182, 29, 37));
            spriteWalking.Add(new Sprite(22, 230, 31, 37));
            // DOWN
            spriteWalking.Add(new Sprite(316, 12, 32, 46));
            spriteWalking.Add(new Sprite(315, 72, 29, 42));
            spriteWalking.Add(new Sprite(316, 128, 30, 44));
            spriteWalking.Add(new Sprite(318, 178, 31, 42));
            spriteWalking.Add(new Sprite(316, 226, 32, 45));
            // LEFT
            spriteWalking.Add(new Sprite(176, 12, 32, 37));
            spriteWalking.Add(new Sprite(168, 73, 45, 37));
            spriteWalking.Add(new Sprite(170, 127, 39, 38));
            spriteWalking.Add(new Sprite(170, 179, 32, 33));
            spriteWalking.Add(new Sprite(169, 226, 35, 34));
            // UP-RIGHT
            spriteWalking.Add(new Sprite(99, 10, 25, 39));
            spriteWalking.Add(new Sprite(94, 65, 39, 41));
            spriteWalking.Add(new Sprite(95, 123, 32, 40));
            spriteWalking.Add(new Sprite(97, 179, 33, 38));
            spriteWalking.Add(new Sprite(99, 225, 31, 39));
            // DOWN-RIGHT
            spriteWalking.Add(new Sprite(244, 13, 40, 35));
            spriteWalking.Add(new Sprite(240, 74, 43, 40));
            spriteWalking.Add(new Sprite(242, 129, 41, 37));
            spriteWalking.Add(new Sprite(244, 177, 36, 35));
            spriteWalking.Add(new Sprite(244, 227, 38, 31));
            sprites.Add(AnimationType.WALKING, spriteWalking);

            List<Sprite> spriteAttacking = new List<Sprite>();
            // UP
            spriteAttacking.Add(new Sprite(2, 287, 48, 37));
            spriteAttacking.Add(new Sprite(15, 335, 32, 58));
            spriteAttacking.Add(new Sprite(2, 409, 56, 37));
            spriteAttacking.Add(new Sprite(27, 452, 28, 50));
            // DOWN
            spriteAttacking.Add(new Sprite(319, 285, 46, 38));
            spriteAttacking.Add(new Sprite(321, 329, 31, 47));
            spriteAttacking.Add(new Sprite(313, 398, 51, 47));
            spriteAttacking.Add(new Sprite(312, 466, 32, 54));
            // LEFT
            spriteAttacking.Add(new Sprite(172, 275, 33, 49));
            spriteAttacking.Add(new Sprite(151, 336, 58, 42));
            spriteAttacking.Add(new Sprite(164, 396, 40, 49));
            spriteAttacking.Add(new Sprite(173, 466, 47, 35));
            // UP-RIGHT
            spriteAttacking.Add(new Sprite(77, 286, 52, 38));
            spriteAttacking.Add(new Sprite(77, 338, 50, 40));
            spriteAttacking.Add(new Sprite(78, 396, 55, 49));
            spriteAttacking.Add(new Sprite(101, 453, 45, 48));
            // DOWN-RIGHT
            spriteAttacking.Add(new Sprite(248, 275, 22, 48));
            spriteAttacking.Add(new Sprite(241, 329, 35, 48));
            spriteAttacking.Add(new Sprite(246, 396, 35, 52));
            spriteAttacking.Add(new Sprite(233, 467, 61, 33));
            sprites.Add(AnimationType.ATTACKING, spriteAttacking);

            List<Sprite> spriteDie = new List<Sprite>();
            spriteDie.Add(new Sprite(236, 533, 47, 40));
            sprites.Add(AnimationType.DYING, spriteDie);

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

            ui = new UI.Units.Footman(managerMouse, this);
            textureName.Add(AnimationType.WALKING, "Footman");

            information = new InformationUnit("Footman", Race.HUMAN, Faction.ALLIANCE, 60, 6, 360, 10, 600, 1, Util.Buildings.BARRACKS, 60 * Warcraft.FPS, 6, 1, Util.Units.FOOTMAN);
            Information = information;
        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);

            if (!texture.ContainsKey(AnimationType.DYING))
                texture.Add(AnimationType.DYING, texture[AnimationType.WALKING]);

            if (!texture.ContainsKey(AnimationType.ATTACKING))
                texture.Add(AnimationType.ATTACKING, texture[AnimationType.WALKING]);
        }
    }
}
