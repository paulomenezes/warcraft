using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Warcraft.Managers;
using Warcraft.Util;

namespace Warcraft.Units.Orcs
{
    class TrollAxethrower : UnitEnemy
    {
        public TrollAxethrower(InformationUnit information, ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings) 
            : base(52, 52, 1, managerMouse, managerMap, managerBuildings)
        {
            Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
            List<Sprite> spriteWalking = new List<Sprite>();
            // UP
            spriteWalking.Add(new Sprite(21, 9, 34, 46));
            spriteWalking.Add(new Sprite(25, 60, 29, 48));
            spriteWalking.Add(new Sprite(22, 113, 32, 46));
            spriteWalking.Add(new Sprite(18, 163, 38, 47));
            spriteWalking.Add(new Sprite(19, 220, 37, 47));
            // DOWN
            spriteWalking.Add(new Sprite(256, 11, 34, 43));
            spriteWalking.Add(new Sprite(258, 63, 32, 44));
            spriteWalking.Add(new Sprite(257, 114, 33, 42));
            spriteWalking.Add(new Sprite(265, 166, 39, 44));
            spriteWalking.Add(new Sprite(257, 221, 33, 41));
            // LEFT
            spriteWalking.Add(new Sprite(151, 11, 32, 43));
            spriteWalking.Add(new Sprite(144, 63, 39, 42));
            spriteWalking.Add(new Sprite(144, 114, 38, 42));
            spriteWalking.Add(new Sprite(146, 165, 36, 42));
            spriteWalking.Add(new Sprite(146, 220, 31, 41));
            // UP-RIGHT
            spriteWalking.Add(new Sprite(83, 11, 28, 44));
            spriteWalking.Add(new Sprite(82, 63, 34, 42));
            spriteWalking.Add(new Sprite(82, 113, 32, 41));
            spriteWalking.Add(new Sprite(75, 164, 47, 50));
            spriteWalking.Add(new Sprite(79, 219, 37, 48));
            // DOWN-RIGHT
            spriteWalking.Add(new Sprite(200, 11, 39, 42));
            spriteWalking.Add(new Sprite(197, 62, 41, 45));
            spriteWalking.Add(new Sprite(200, 113, 38, 44));
            spriteWalking.Add(new Sprite(201, 165, 33, 39));
            spriteWalking.Add(new Sprite(201, 223, 34, 37));
            sprites.Add(AnimationType.WALKING, spriteWalking);

            List<Sprite> spriteAttacking = new List<Sprite>();
            // UP
            spriteAttacking.Add(new Sprite(12, 280, 37, 46));
            spriteAttacking.Add(new Sprite(20, 333, 29, 58));
            spriteAttacking.Add(new Sprite(4, 401, 52, 45));
            spriteAttacking.Add(new Sprite(24, 450, 34, 47));
            // DOWN
            spriteAttacking.Add(new Sprite(261, 271, 43, 47));
            spriteAttacking.Add(new Sprite(261, 330, 26, 48));
            spriteAttacking.Add(new Sprite(255, 403, 53, 39));
            spriteAttacking.Add(new Sprite(249, 458, 38, 44));
            // LEFT
            spriteAttacking.Add(new Sprite(140, 272, 42, 50));
            spriteAttacking.Add(new Sprite(127, 337, 55, 45));
            spriteAttacking.Add(new Sprite(146, 396, 34, 51));
            spriteAttacking.Add(new Sprite(146, 456, 34, 43));
            // UP-RIGHT
            spriteAttacking.Add(new Sprite(75, 278, 37, 45));
            spriteAttacking.Add(new Sprite(64, 333, 45, 52));
            spriteAttacking.Add(new Sprite(74, 396, 45, 51));
            spriteAttacking.Add(new Sprite(84, 458, 32, 42));
            // DOWN-RIGHT
            spriteAttacking.Add(new Sprite(203, 270, 36, 51));
            spriteAttacking.Add(new Sprite(196, 330, 45, 51));
            spriteAttacking.Add(new Sprite(205, 394, 35, 51));
            spriteAttacking.Add(new Sprite(191, 454, 41, 45));
            sprites.Add(AnimationType.ATTACKING, spriteAttacking);

            List<Sprite> spriteDiying = new List<Sprite>();
            spriteDiying.Add(new Sprite(16, 568, 45, 50));
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

            ui = new UI.Units.TrollAxethrower(managerMouse, this);
            textureName.Add(AnimationType.WALKING, "Troll Axethrower");

            this.information = information;
            Information = information;

            Data.Write("Adicionar [TrollAxeTrower] X: " + Math.Floor(position.X / 32) + " Y: " + Math.Floor(position.Y / 32));
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

            if (animations.currentAnimation == AnimationType.WALKING)
                animations.speed = 5;
            else if (animations.currentAnimation == AnimationType.ATTACKING)
                animations.speed = 7;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
