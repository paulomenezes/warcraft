﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Warcraft.Buildings.Neutral;
using Warcraft.Commands;
using Warcraft.Managers;
using Warcraft.Units;
using Warcraft.Units.Humans;
using Warcraft.Util;

namespace Warcraft.Buildings.Orcs
{
    class GreatHall : CityHall
	{
		public GreatHall(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) : 
            base(tileX, tileY, managerMouse, managerMap, managerUnits)
        {
            information = new InformationBuilding("Great Hall", 1200, 1200, 800, Util.Units.PEON, 250 * Warcraft.FPS, Util.Buildings.GREAT_HALL);

			Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
			List<Sprite> spriteBuilding = new List<Sprite>();
			// BUILDING
			spriteBuilding.Add(new Sprite(560, 737, 48, 39));
			spriteBuilding.Add(new Sprite(556, 865, 61, 52));
			spriteBuilding.Add(new Sprite(25, 555, 101, 100));
			spriteBuilding.Add(new Sprite(144, 543, 116, 119));

			sprites.Add(AnimationType.WALKING, spriteBuilding);

			Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
			animations.Add("building", new Frame(0, 4));

			this.animations = new Animation(sprites, animations, "building", width, height, false, information.BuildTime / spriteBuilding.Count);

			textureName = "Orc Buildings (Summer) ";

            commands.Add(new BuilderUnits(Util.Units.PEON, managerUnits, Peon.Information as InformationUnit));
		}
	}
}
