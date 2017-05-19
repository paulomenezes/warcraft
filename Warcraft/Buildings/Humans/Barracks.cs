using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Warcraft.Commands;
using Warcraft.Managers;
using Warcraft.Units.Humans;
using Warcraft.Util;

namespace Warcraft.Buildings.Humans
{
    class Barracks : Building
    {
        public Barracks(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) : 
            base(tileX, tileY, 128, 128, managerMouse, managerMap, managerUnits)
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

            ui = new UI.Buildings.Barracks(managerMouse, this);
            textureName = "Human Buildings (Summer)";

            commands.Add(new BuilderUnits(Util.Units.ELVEN_ARCHER, ElvenArcher.Information));
            commands.Add(new BuilderUnits(Util.Units.FOOTMAN, Footman.Information));

            Data.Write("Adicionar [Barracks] X: " + tileX + " Y: " + tileY);
        }

        public override void Update()
        {
            base.Update();

            for (int i = 0; i < commands.Count; i++)
            {
                var c = (commands[i] as BuilderUnits);
                c.Update();

                if (c.completed)
                {
                    var p = new Point(((int)Position.X / 32) + ((width / Warcraft.TILE_SIZE) / 2), ((int)Position.Y / 32) + ((height / Warcraft.TILE_SIZE)));
                    managerUnits.Factory(c.type, p.X, p.Y, target.X, target.Y);
                    c.completed = false;
                    c.remove = true;
                }
            }
        }
    }
}
