using System.Collections.Generic;
using Warcraft.Buildings.Neutral;
using Warcraft.Managers;
using Warcraft.Util;

namespace Warcraft.Buildings.Humans
{
    class ChickenFarm : Farm
    {
        public ChickenFarm(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) : 
            base(tileX, tileY, 64, 64, managerMouse, managerMap, managerUnits)
        {
            information = new InformationBuilding("Chicken Farm", 400, 500, 450, Util.Units.PEASANT, 300, Util.Buildings.CHICKEN_FARM);

            Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
            List<Sprite> spriteBuilding = new List<Sprite>();
            // BUILDING
            spriteBuilding.Add(new Sprite(576, 708, 48, 39));
            spriteBuilding.Add(new Sprite(572, 836, 61, 52));
            spriteBuilding.Add(new Sprite(398, 73, 63, 59));
            spriteBuilding.Add(new Sprite(398, 4, 64, 64));

            sprites.Add(AnimationType.WALKING, spriteBuilding);

            Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
            animations.Add("building", new Frame(0, 4));

            this.animations = new Animation(sprites, animations, "building", width, height, false, information.BuildTime / sprites.Count);

            textureName = "Human Buildings (Summer)";

            unselected = true;
        }
    }
}
