﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Warcraft.Buildings.Neutral;
using Warcraft.Commands;
using Warcraft.Managers;
using Warcraft.Units.Humans;
using Warcraft.Util;

namespace Warcraft.Buildings.Orcs
{
    class GreatHall : CityHall
	{
		public GreatHall(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits) : 
            base(tileX, tileY, managerMouse, managerMap, managerUnits)
        {
            information = new InformationBuilding("Great Hall", 1200, 1200, 800, Util.Units.PEON, 300, Util.Buildings.GREAT_HALL);

			Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
			List<Sprite> spriteBuilding = new List<Sprite>();
			// BUILDING
			spriteBuilding.Add(new Sprite(25, 555, 101, 100));
			spriteBuilding.Add(new Sprite(144, 543, 116, 119));
			//spriteBuilding.Add(new Sprite(270, 154, 111, 95));
			//spriteBuilding.Add(new Sprite(270, 17, 119, 104));

			sprites.Add(AnimationType.WALKING, spriteBuilding);

			Dictionary<string, Frame> animations = new Dictionary<string, Frame>();
			animations.Add("building", new Frame(0, 2));

			this.animations = new Animation(sprites, animations, "building", width, height, false, information.BuildTime / sprites.Count);

			textureName = "Orc Buildings (Summer) ";

            commands.Add(new BuilderUnits(Util.Units.PEON, managerUnits, Peon.Information));
		}
	}
}
