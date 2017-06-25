using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Util;
using Warcraft.Managers;
using Warcraft.Commands;

namespace Warcraft.Units.Humans
{
    class Peasant : Builder
    {
        public Peasant(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits, ManagerBuildings managerBuildings) 
            : base(tileX, tileY, managerMouse, managerMap, managerUnits)
        {
            Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
            List<Sprite> spriteWalking = new List<Sprite>();
            // UP
            spriteWalking.Add(new Sprite(16, 8, 26, 23));
            spriteWalking.Add(new Sprite(18, 46, 24, 28));
            spriteWalking.Add(new Sprite(17, 86, 25, 26));
            spriteWalking.Add(new Sprite(19, 122, 23, 30));
            spriteWalking.Add(new Sprite(18, 159, 24, 27));
            // DOWN
            spriteWalking.Add(new Sprite(166, 7, 25, 26));
            spriteWalking.Add(new Sprite(168, 45, 24, 26));
            spriteWalking.Add(new Sprite(167, 85, 25, 27));
            spriteWalking.Add(new Sprite(168, 121, 23, 26));
            spriteWalking.Add(new Sprite(167, 158, 24, 27));
            // LEFT
            spriteWalking.Add(new Sprite(97, 4, 14, 31));
            spriteWalking.Add(new Sprite(91, 42, 24, 30));
            spriteWalking.Add(new Sprite(96, 82, 16, 31));
            spriteWalking.Add(new Sprite(91, 118, 23, 30));
            spriteWalking.Add(new Sprite(95, 155, 20, 30));
            // UP-RIGHT
            spriteWalking.Add(new Sprite(56, 6, 22, 26));
            spriteWalking.Add(new Sprite(55, 44, 26, 30));
            spriteWalking.Add(new Sprite(56, 84, 24, 29));
            spriteWalking.Add(new Sprite(59, 119, 23, 29));
            spriteWalking.Add(new Sprite(57, 156, 21, 28));
            // DOWN-RIGHT
            spriteWalking.Add(new Sprite(127, 3, 22, 31));
            spriteWalking.Add(new Sprite(128, 40, 20, 27));
            spriteWalking.Add(new Sprite(130, 80, 19, 28));
            spriteWalking.Add(new Sprite(126, 119, 26, 29));
            spriteWalking.Add(new Sprite(126, 156, 26, 28));

            sprites.Add(AnimationType.WALKING, spriteWalking);

            List<Sprite> spriteGold = new List<Sprite>();
            // MINER UP
            spriteGold.Add(new Sprite(29, 10, 24, 27));
            spriteGold.Add(new Sprite(30, 52, 23, 30));
            spriteGold.Add(new Sprite(30, 95, 23, 28));
            spriteGold.Add(new Sprite(30, 137, 24, 32));
            spriteGold.Add(new Sprite(30, 180, 24, 30));
            // MINER DOWN
            spriteGold.Add(new Sprite(159, 1, 23, 36));
            spriteGold.Add(new Sprite(160, 43, 23, 36));
            spriteGold.Add(new Sprite(160, 86, 23, 37));
            spriteGold.Add(new Sprite(158, 131, 24, 34));
            spriteGold.Add(new Sprite(158, 173, 24, 36));
            // MINER RIGHT
            spriteGold.Add(new Sprite(97, 7, 21, 31));
            spriteGold.Add(new Sprite(96, 50, 24, 30));
            spriteGold.Add(new Sprite(96, 93, 23, 30));
            spriteGold.Add(new Sprite(98, 134, 22, 32));
            spriteGold.Add(new Sprite(97, 177, 23, 31));
            // MINER UP RIGHT
            spriteGold.Add(new Sprite(62, 7, 24, 31));
            spriteGold.Add(new Sprite(62, 49, 25, 34));
            spriteGold.Add(new Sprite(62, 91, 25, 33));
            spriteGold.Add(new Sprite(62, 135, 25, 30));
            spriteGold.Add(new Sprite(62, 178, 24, 30));
            // MINER DOWN RIGHT
            spriteGold.Add(new Sprite(129, 1, 21, 37));
            spriteGold.Add(new Sprite(127, 44, 22, 34));
            spriteGold.Add(new Sprite(129, 87, 21, 37));
            spriteGold.Add(new Sprite(128, 130, 22, 37));
            spriteGold.Add(new Sprite(129, 173, 21, 37));
            sprites.Add(AnimationType.GOLD, spriteGold);

            List<Sprite> spriteDie = new List<Sprite>();
            spriteDie.Add(new Sprite(10, 50, 33, 33));
            sprites.Add(AnimationType.DYING, spriteDie);

            Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
            animations.Add("up", new Frame(0, 5));
            animations.Add("down", new Frame(5, 5));
            animations.Add("right", new Frame(10, 5));
            animations.Add("left", new Frame(10, 5, true));
            animations.Add("upRight", new Frame(15, 5));
            animations.Add("downRight", new Frame(20, 5));
            animations.Add("upLeft", new Frame(15, 5, true));
            animations.Add("downLeft", new Frame(20, 5, true));
            animations.Add("dying", new Frame(0, 1));

            this.animations = new Animation(sprites, animations, "down", width, height);

            ui = new UI.Units.Builder(managerMouse, this);
            textureName.Add(AnimationType.WALKING, "Peasant_walking");
            textureName.Add(AnimationType.GOLD, "Peasant_gold");
            textureName.Add(AnimationType.DYING, "Peasant_dying");

            information = new InformationUnit("Peasant", Race.HUMAN, Faction.ALLIANCE, 30, 0, 4, 10, 400, 1, Util.Buildings.TOWN_HALL, 200, 3, 5, 1, 0, Util.Units.PEASANT);
            Information = information;

            commands.Add(new BuilderBuildings(Util.Buildings.TOWN_HALL, this, managerMouse, managerBuildings, managerUnits));
            commands.Add(new BuilderBuildings(Util.Buildings.BARRACKS, this, managerMouse, managerBuildings, managerUnits));
            commands.Add(new BuilderBuildings(Util.Buildings.CHICKEN_FARM, this, managerMouse, managerBuildings, managerUnits));
            commands.Add(new BuilderWalls(this, managerMouse, managerBuildings, managerUnits));
            commands.Add(new Miner(managerBuildings, managerUnits, this));
        }
    }
}
