using System.Collections.Generic;
using Warcraft.Managers;
using Warcraft.Units.Humans;
using Warcraft.Util;

namespace Warcraft.Buildings.Neutral
{
    class GoldMine : Building
    {
        public List<Peasant> workers = new List<Peasant>();

        public GoldMine(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits)
            : base(tileX, tileY, 96, 96, managerMouse, managerMap, managerUnits)
        {
            information = new InformationBuilding("Gold Mine", 25500, 0, 0, Util.Units.NONE, 0, Util.Buildings.GOLD_MINE);

            Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
            List<Sprite> spriteBuilding = new List<Sprite>();
            // NORMAL
            spriteBuilding.Add(new Sprite(26, 661, 96, 89));
            // WORKING
            spriteBuilding.Add(new Sprite(26, 757, 96, 89));

            sprites.Add(AnimationType.WALKING, spriteBuilding);

            Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
            animations.Add("normal", new Frame(0, 1));
            animations.Add("working", new Frame(1, 1));

            this.animations = new Animation(sprites, animations, "normal", width, height, false, 0);

            ui = new UI.Buildings.GoldMine(managerMouse, this);
            textureName = "Human Buildings (Summer)";

            unselected = true;
            isWorking = true;

            managerMap.AddWalls(position, width / 32, height / 32);
        }
    }
}
