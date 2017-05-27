using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Warcraft.Managers;
using Warcraft.Util;

namespace Warcraft.Units.Humans
{
    class ElvenArcher : Unit
    {
        public ElvenArcher(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) 
            : base(tileX, tileY, 48, 48, 2, managerMouse, managerMap, managerUnits)
        {
            Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
            List<Sprite> spriteWalking = new List<Sprite>();
            // UP
            spriteWalking.Add(new Sprite(6, 11, 40, 47));
            spriteWalking.Add(new Sprite(5, 85, 42, 49));
            spriteWalking.Add(new Sprite(5, 159, 41, 47));
            spriteWalking.Add(new Sprite(8, 234, 36, 47));
            spriteWalking.Add(new Sprite(5, 308, 41, 46));
            // DOWN
            spriteWalking.Add(new Sprite(241, 17, 44, 42));
            spriteWalking.Add(new Sprite(239, 92, 43, 41));
            spriteWalking.Add(new Sprite(241, 165, 43, 42));
            spriteWalking.Add(new Sprite(247, 238, 38, 41));
            spriteWalking.Add(new Sprite(244, 312, 41, 41));
            // LEFT
            spriteWalking.Add(new Sprite(128, 16, 38, 40));
            spriteWalking.Add(new Sprite(128, 92, 40, 41));
            spriteWalking.Add(new Sprite(129, 164, 39, 41));
            spriteWalking.Add(new Sprite(129, 240, 35, 38));
            spriteWalking.Add(new Sprite(130, 312, 35, 41));
            // UP-RIGHT
            spriteWalking.Add(new Sprite(67, 12, 42, 45));
            spriteWalking.Add(new Sprite(70, 88, 38, 41));
            spriteWalking.Add(new Sprite(68, 161, 40, 44));
            spriteWalking.Add(new Sprite(65, 230, 41, 46));
            spriteWalking.Add(new Sprite(67, 306, 40, 43));
            // DOWN-RIGHT
            spriteWalking.Add(new Sprite(186, 17, 42, 37));
            spriteWalking.Add(new Sprite(183, 92, 38, 37));
            spriteWalking.Add(new Sprite(184, 165, 40, 38));
            spriteWalking.Add(new Sprite(187, 237, 43, 36));
            spriteWalking.Add(new Sprite(187, 312, 42, 37));
            sprites.Add(AnimationType.WALKING, spriteWalking);

            List<Sprite> spriteAttacking = new List<Sprite>();
            // UP
            spriteAttacking.Add(new Sprite(10, 373, 45, 52));
            spriteAttacking.Add(new Sprite(10, 452, 44, 48));
            // DOWN
            spriteAttacking.Add(new Sprite(238, 388, 45, 36));
            spriteAttacking.Add(new Sprite(237, 460, 45, 39));
            // LEFT
            spriteAttacking.Add(new Sprite(128, 380, 46, 42));
            spriteAttacking.Add(new Sprite(130, 455, 39, 40));
            // UP-RIGHT
            spriteAttacking.Add(new Sprite(67, 382, 43, 47));
            spriteAttacking.Add(new Sprite(71, 457, 38, 46));
            // DOWN-RIGHT
            spriteAttacking.Add(new Sprite(189, 389, 36, 36));
            spriteAttacking.Add(new Sprite(184, 463, 41, 37));
            sprites.Add(AnimationType.ATTACKING, spriteAttacking);

            List<Sprite> spriteDie = new List<Sprite>();
            spriteDie.Add(new Sprite(181, 530, 44, 43));
            sprites.Add(AnimationType.DYING, spriteDie);

            Dictionary<string, Frame> animations = new Dictionary<string, Frame>();

            Dictionary<AnimationType, int> framesCount = new Dictionary<AnimationType, int>();
            framesCount.Add(AnimationType.WALKING, 5);
            framesCount.Add(AnimationType.ATTACKING, 2);
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

            ui = new UI.Units.ElvenArcher(managerMouse, this);
            textureName.Add(AnimationType.WALKING, "Elven Archer");

            information = new InformationUnit("Elven Archer", Race.HIGH_ELF, Faction.ALLIANCE, 150, 20, 360, 10, 500, 1, Util.Buildings.BARRACKS, 400, 20, 100, 4, 0, Util.Units.ELVEN_ARCHER);
            Information = information;

            Data.Write("Adicionar [ElvenArcher] X: " + tileX + " Y: " + tileY);
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
