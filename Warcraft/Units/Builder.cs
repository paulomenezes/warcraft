using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.Util;
using Warcraft.Managers;
using Warcraft.Commands;

namespace Warcraft.Units.Humans
{
    abstract class Builder : Unit
	{
		public List<ICommand> commands = new List<ICommand>();

		//public static InformationUnit Information;

		public Builder(int tileX, int tileY, ManagerMouse managerMouse, ManagerMap managerMap, ManagerBuildings managerBuildings, ManagerUnits managerUnits)
			: base(tileX, tileY, 32, 32, 2, managerMouse, managerMap, managerBuildings)
		{
			
		}

		public override void LoadContent(ContentManager content)
		{
			base.LoadContent(content);

			if (!texture.ContainsKey(AnimationType.GOLD))
				texture.Add(AnimationType.GOLD, content.Load<Texture2D>(textureName[AnimationType.GOLD]));
			if (!texture.ContainsKey(AnimationType.DYING))
				texture.Add(AnimationType.DYING, content.Load<Texture2D>(textureName[AnimationType.DYING]));

			for (int i = 0; i < commands.Count; i++)
			{
				if (commands[i] is BuilderBuildings)
					(commands[i] as BuilderBuildings).LoadContent(content);

				if (commands[i] is BuilderWalls)
					(commands[i] as BuilderWalls).LoadContent(content);
			}
		}

		public override void Update()
		{
			base.Update();

			for (int i = 0; i < commands.Count; i++)
			{
				if (commands[i] is BuilderBuildings)
				{
					var cmd = commands[i] as BuilderBuildings;

					if (workState == WorkigState.WORKING &&
						cmd.building.isBuilding &&
						cmd.building.isPlaceSelected &&
						!cmd.building.isStartBuilding)
						cmd.building.StartBuilding();

					if (workState == WorkigState.WAITING_PLACE && cmd.building.isPlaceSelected)
					{
						workState = WorkigState.GO_TO_WORK;
						Move((int)cmd.building.Position.X / 32, (int)cmd.building.Position.Y / 32);
						selected = false;
					}

					cmd.Update();
				}
				else if (commands[i] is Miner)
				{
					var cmd = commands[i] as Miner;

					if (cmd.started)
						cmd.Update();
				}
				else if (commands[i] is BuilderWalls)
				{
					var cmd = commands[i] as BuilderWalls;

					if (cmd.started)
						cmd.Update();
				}
			}
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			base.Draw(spriteBatch);

			for (int i = 0; i < commands.Count; i++)
			{
				if (commands[i] is BuilderBuildings)
					(commands[i] as BuilderBuildings).Draw(spriteBatch);

				if (commands[i] is BuilderWalls && (commands[i] as BuilderWalls).started)
					(commands[i] as BuilderWalls).Draw(spriteBatch);
			}
		}
	}
}
