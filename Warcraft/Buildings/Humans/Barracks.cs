using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Warcraft.Commands;
using Warcraft.Managers;
using Warcraft.Units.Humans;
using Warcraft.Util;
using Warcraft.Units;

namespace Warcraft.Buildings.Humans
{
    class Barracks : Neutral.Barracks
    {
        public Barracks(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) : 
            base(tileX, tileY, managerMouse, managerMap, managerUnits)
        {
            information = new InformationBuilding("Barracks", 800, 700, 450, Util.Units.PEASANT, 500, Util.Buildings.BARRACKS);

            Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
            List<Sprite> spriteBuilding = new List<Sprite>();
            // BUILDING
            spriteBuilding.Add(new Sprite(576, 708, 48, 39));
            spriteBuilding.Add(new Sprite(572, 836, 61, 52));
            spriteBuilding.Add(new Sprite(135, 132, 116, 128));
            spriteBuilding.Add(new Sprite(135, 4, 128, 128));

            sprites.Add(AnimationType.WALKING, spriteBuilding);

            Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
            animations.Add("building", new Frame(0, 4));

            this.animations = new Animation(sprites, animations, "building", width, height, false, information.BuildTime / sprites.Count);

            textureName = "Human Buildings (Summer)";

            commands.Add(new BuilderUnits(Util.Units.ELVEN_ARCHER, managerUnits, ElvenArcher.Information as InformationUnit));
            commands.Add(new BuilderUnits(Util.Units.FOOTMAN, managerUnits, Footman.Information as InformationUnit));
        }
    }
}
