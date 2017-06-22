using System.Collections.Generic;
using Warcraft.Managers;
using Warcraft.Units;
using Warcraft.Units.Humans;
using Warcraft.Util;

namespace Warcraft.Buildings.Neutral
{
    class GoldMine : Building
    {
        public List<Builder> workers = new List<Builder>();
        public int QUANITY = 10000;

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

            managerMap.AddWalls(Position, width / 32, height / 32);

            Data.Write("Adicionar [Gold Mine] X: " + tileX + " Y: " + tileY);
        }

        public void Fire() 
        {
			for (int i = 0; i < workers.Count; i++)
			{
				workers[i].workState = WorkigState.NOTHING;
				workers[i].animations.currentAnimation = Util.AnimationType.WALKING;
			}

			workers.Clear();
			animations.Change("normal");
		}
    }
}
