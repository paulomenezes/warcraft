using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Util;
using Warcraft.Managers;
using Warcraft.Commands;

namespace Warcraft.Units.Humans
{
	class Peon : Builder
	{
        public Peon(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits, ManagerBuildings managerBuildings)
			: base(tileX, tileY, managerMouse, managerMap, managerUnits)
		{
			Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
			List<Sprite> spriteWalking = new List<Sprite>();
			// UP
			spriteWalking.Add(new Sprite(14, 2, 29, 32));
			spriteWalking.Add(new Sprite(17, 40, 24, 24));
			spriteWalking.Add(new Sprite(17, 82, 25, 33));
			spriteWalking.Add(new Sprite(12, 122, 31, 33));
			spriteWalking.Add(new Sprite(13, 164, 28, 33));
			// DOWN
			spriteWalking.Add(new Sprite(209, 6, 30, 25));
			spriteWalking.Add(new Sprite(210, 46, 25, 29));
			spriteWalking.Add(new Sprite(210, 87, 26, 28));
			spriteWalking.Add(new Sprite(210, 128, 31, 29));
			spriteWalking.Add(new Sprite(211, 169, 30, 28));
			// LEFT
			spriteWalking.Add(new Sprite(122, 1, 18, 33));
			spriteWalking.Add(new Sprite(118, 40, 28, 31));
			spriteWalking.Add(new Sprite(119, 81, 24, 32));
			spriteWalking.Add(new Sprite(119, 121, 28, 32));
			spriteWalking.Add(new Sprite(118, 163, 22, 34));
			// UP-RIGHT
			spriteWalking.Add(new Sprite(69, 0, 26, 34));
			spriteWalking.Add(new Sprite(68, 39, 26, 31));
			spriteWalking.Add(new Sprite(68, 80, 25, 31));
			spriteWalking.Add(new Sprite(63, 121, 37, 36));
			spriteWalking.Add(new Sprite(67, 162, 29, 36));
			// DOWN-RIGHT
			spriteWalking.Add(new Sprite(164, 2, 23, 29));
			spriteWalking.Add(new Sprite(159, 41, 33, 32));
			spriteWalking.Add(new Sprite(161, 83, 31, 31));
			spriteWalking.Add(new Sprite(164, 122, 25, 30));
			spriteWalking.Add(new Sprite(162, 165, 25, 27));

			sprites.Add(AnimationType.WALKING, spriteWalking);

			List<Sprite> spriteGold = new List<Sprite>();
			// MINER UP
			spriteGold.Add(new Sprite(36, 536, 27, 34));
			spriteGold.Add(new Sprite(37, 575, 26, 36));
			spriteGold.Add(new Sprite(37, 620, 27, 35));
			spriteGold.Add(new Sprite(35, 662, 27, 35));
			spriteGold.Add(new Sprite(34, 706, 28, 35));
			// MINER DOWN
			spriteGold.Add(new Sprite(180, 534, 30, 34));
			spriteGold.Add(new Sprite(180, 575, 29, 38));
			spriteGold.Add(new Sprite(180, 619, 29, 37));
			spriteGold.Add(new Sprite(181, 662, 29, 38));
			spriteGold.Add(new Sprite(181, 705, 29, 37));
			// MINER RIGHT
			spriteGold.Add(new Sprite(109, 539, 25, 32));
			spriteGold.Add(new Sprite(109, 579, 26, 30));
			spriteGold.Add(new Sprite(110, 623, 25, 32));
			spriteGold.Add(new Sprite(110, 665, 24, 31));
			spriteGold.Add(new Sprite(109, 709, 25, 30));
			// MINER UP RIGHT
			spriteGold.Add(new Sprite(75, 537, 24, 34));
			spriteGold.Add(new Sprite(72, 577, 27, 31));
			spriteGold.Add(new Sprite(72, 621, 27, 32));
			spriteGold.Add(new Sprite(77, 664, 22, 36));
			spriteGold.Add(new Sprite(77, 707, 22, 36));
			// MINER DOWN RIGHT
			spriteGold.Add(new Sprite(142, 539, 28, 28));
			spriteGold.Add(new Sprite(143, 579, 29, 32));
			spriteGold.Add(new Sprite(144, 624, 29, 31));
			spriteGold.Add(new Sprite(144, 665, 29, 29));
			spriteGold.Add(new Sprite(144, 710, 28, 26));
			sprites.Add(AnimationType.GOLD, spriteGold);

			List<Sprite> spriteDie = new List<Sprite>();
			spriteDie.Add(new Sprite(13, 494, 34, 32));
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
			textureName.Add(AnimationType.WALKING, "Orcs/Peon");
			textureName.Add(AnimationType.GOLD, "Orcs/Peon");
			textureName.Add(AnimationType.DYING, "Orcs/Peon");

            information = new InformationUnit("Peon", Race.ORC, Faction.HORDE, 30, 0, 4, 10, 400, 1, Util.Buildings.GREAT_HALL, 45 * Warcraft.FPS, 3, 1, Util.Units.PEON, 2);
			Information = information;

            commands.Add(new BuilderBuildings(Util.Buildings.GREAT_HALL, this, managerMouse, managerBuildings, managerUnits));
			commands.Add(new BuilderBuildings(Util.Buildings.ORC_BARRACKS, this, managerMouse, managerBuildings, managerUnits));
            commands.Add(new BuilderBuildings(Util.Buildings.PIG_FARM, this, managerMouse, managerBuildings, managerUnits));
			commands.Add(new BuilderWalls(this, managerMouse, managerBuildings, managerUnits));
            commands.Add(new Miner(managerBuildings, managerUnits, this));
            commands.Add(new BuilderBuildings(Util.Buildings.ALTAR_OF_STORMS, this, managerMouse, managerBuildings, managerUnits));
		}
	}
}
