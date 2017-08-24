using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Warcraft.Buildings.Orcs;
using Warcraft.Managers;
using Warcraft.Units.Neutral;
using Warcraft.Util;

namespace Warcraft.Units.Humans
{
	class Battleship : Unit
	{
        public static bool move = false;

        public Battleship(int tileX, int tileY, int lastX, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits, ManagerBuildings managerBuildings, ManagerEnemies managerEnemies)
            : base(tileX, tileY, 96, 96, 1, managerMouse, managerMap, managerUnits)
        {
			Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
			List<Sprite> spriteWalking = new List<Sprite>();
			spriteWalking.Add(new Sprite(19, 6, 52, 81));
			spriteWalking.Add(new Sprite(18, 93, 52, 88));
			spriteWalking.Add(new Sprite(186, 7, 84, 67));
            spriteWalking.Add(new Sprite(102, 4, 67, 83));
			spriteWalking.Add(new Sprite(281, 0, 80, 88));
			sprites.Add(AnimationType.WALKING, spriteWalking);

			Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
			animations.Add("up", new Frame(0, 1));
			animations.Add("down", new Frame(1, 1));
			animations.Add("right", new Frame(2, 1));
			animations.Add("left", new Frame(2, 1, true));
			animations.Add("upRight", new Frame(3, 1));
			animations.Add("downRight", new Frame(4, 1));
			animations.Add("upLeft", new Frame(3, 1, true));
			animations.Add("downLeft", new Frame(4, 5, true));

			this.animations = new Animation(sprites, animations, "down", width, height);

			textureName.Add(AnimationType.WALKING, "Battleship");

            information = new InformationUnit("Battleship", Race.HUMAN, Faction.ALLIANCE, 9999, 0, 0, 10, 0, 0, Util.Buildings.NONE, 0, 0, 0, Util.Units.BATTLESHIP, 0);
			Information = information;

            managerMouse.MouseEventHandler += (sender, e) => {
                if (e.SelectRectangle.Intersects(rectangle) && !move) 
                {
                    int total = 0, dead = 0;
                    managerEnemies.managerUnits.units.ForEach(unit => {
                        if (!(unit is Skeleton))
                        {
                            total++;
                            if (unit.information.HitPoints <= 0)
                                dead++;
                        }
                    });

                    if (total == dead && managerEnemies.managerBuildings.buildings.Count < 2)
                    {
                        if (managerEnemies.managerBuildings.buildings.Count == 0 || (managerEnemies.managerBuildings.buildings.Count == 1 && managerEnemies.managerBuildings.buildings[0] is DarkPortal))
                        {
                            goal = new Vector2(lastX * Warcraft.TILE_SIZE, position.Y + 5 * Warcraft.TILE_SIZE);
                            transition = true;
                            move = true;

                            Warcraft.ISLAND++;

                            if (Warcraft.ISLAND > 2)
                            {
                                Warcraft.ISLAND = 0;
                            }
                        }
					}
                }
            };
		}

        public override void Update()
        {
            base.Update();

            animations.currentAnimation = AnimationType.WALKING;
            selected = false;
            target = null;
            targetBuilding = null;

            if (move)
            {
                Warcraft.camera.center = position - new Vector2(Warcraft.WINDOWS_WIDTH / 2, Warcraft.WINDOWS_HEIGHT / 2);
            }
        }
	}
}

