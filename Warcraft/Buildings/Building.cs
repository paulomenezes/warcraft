using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Warcraft.Managers;
using Warcraft.Util;
using Warcraft.Events;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Warcraft.Commands;
using Warcraft.UI;

namespace Warcraft.Buildings
{
    abstract class Building
    {
        public static Dictionary<string, Texture2D> texture = new Dictionary<string, Texture2D>();
        public Animation animations;

        protected Point target;
        private Vector2 position;
        public Vector2 Position 
        {
            get { return position; }
            set 
            {
				position = value;
                Data.Write("Mover [" + (information as InformationBuilding).Type + "] X: " + Math.Floor(position.X / 32) + " Y: " + Math.Floor(position.Y / 32));
            }
        }

        public bool selected;
        public bool unselected = false;
        public int width;
        public int height;

        private Rectangle rectangle;

        public UI.UI ui;
        protected string textureName;

        public Information information;

        public bool isBuilding = false;
        public bool isPlaceSelected = false;
        public bool isStartBuilding = false;
        public bool isWorking = false;

        ManagerMap managerMap;
        protected ManagerUnits managerUnits;

        public List<ICommand> commands = new List<ICommand>();

        public Building(int tileX, int tileY, int width, int height, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits)
        {
            this.width = width;
            this.height = height;

            this.managerMap = managerMap;
            this.managerUnits = managerUnits;

            position = new Vector2(tileX * Warcraft.TILE_SIZE, tileY * Warcraft.TILE_SIZE);
            target = new Point(tileX + ((width / Warcraft.TILE_SIZE) / 2), tileY + ((height / Warcraft.TILE_SIZE) / 2));

            managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
            managerMouse.MouseClickEventHandler += ManagerMouse_MouseClickEventHandler;

            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public static Building Factory(Util.Buildings type, ManagerMouse managerMouse, ManagerMap managerMap, ManagerUnits managerUnits)
        {
            Building building = null;
            switch (type)
            {
                case Util.Buildings.TOWN_HALL:
                    building = new Humans.TownHall(0, 0, managerMouse, managerMap, managerUnits);
                    break;
                case Util.Buildings.BARRACKS:
                    building = new Humans.Barracks(0, 0, managerMouse, managerMap, managerUnits);
                    break;
                case Util.Buildings.CHICKEN_FARM:
                    building = new Humans.ChickenFarm(0, 0, managerMouse, managerMap, managerUnits);
                    break;

                case Util.Buildings.GREAT_HALL:
                    building = new Orcs.GreatHall(0, 0, managerMouse, managerMap, managerUnits);
					break;
                case Util.Buildings.ORC_BARRACKS:
                    building = new Orcs.Barracks(0, 0, managerMouse, managerMap, managerUnits);
					break;
                case Util.Buildings.PIG_FARM:
                    building = new Orcs.PigFarm(0, 0, managerMouse, managerMap, managerUnits);
					break;
            }

            return building;
        }

        private void ManagerMouse_MouseEventHandler(object sender, MouseEventArgs e)
        {
            if (!e.UI)
            {
                position.X = (float)Math.Floor(position.X / 32) * 32;
                position.Y = (float)Math.Floor(position.Y / 32) * 32;

                if (isBuilding && e.SelectRectangle.Width == 0 && e.SelectRectangle.Height == 0 &&
                    !managerMap.CheckWalls(position, width / 32, height / 32))
                {
					isPlaceSelected = true;
					Data.Write("Adicionar [" + (information as InformationBuilding).Type + "] X: " + Math.Floor(position.X / 32) + " Y: " + Math.Floor(position.Y / 32));
                }

                if (isWorking)
                {
                    if (rectangle.Intersects(e.SelectRectangle))
                        selected = true;
                    else
                        selected = false;
                }
            }
        }

        private void ManagerMouse_MouseClickEventHandler(object sender, MouseClickEventArgs e)
        {
            if (selected)
                target = new Point(e.XTile, e.YTile);
        }

        public void StartBuilding()
        {
            animations.Play("building");
            isStartBuilding = true;

            managerMap.AddWalls(position, width / 32, height / 32);

            target = new Point(((int)position.X / 32) + ((width / Warcraft.TILE_SIZE) / 2), ((int)position.Y / 32) + ((height / Warcraft.TILE_SIZE)));
            rectangle = new Rectangle((int)position.X, (int)position.Y, width, height);
        }

        public virtual void LoadContent(ContentManager content)
        {
            if (!texture.ContainsKey(textureName))
            {
                texture.Add(textureName, content.Load<Texture2D>(textureName));
            }

            if (ui != null)
                ui.LoadContent(content);
        }

        public virtual void Update()
        {
            animations.Update();

            if (ui != null)
                ui.Update();

            if (animations.completed && !isWorking)
            {
                isBuilding = false;
                isPlaceSelected = false;
                isStartBuilding = false;

                isWorking = true;
            }

            if (isBuilding && !isPlaceSelected)
            {
                MouseState mouse = Mouse.GetState();
                position = new Vector2(Normalize(mouse.X - width / 2), Normalize(mouse.Y - height / 2)) + Warcraft.camera.center;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isBuilding && !isPlaceSelected)
                spriteBatch.Draw(texture[textureName], position, new Rectangle(animations.Last().x, animations.Last().y, animations.Last().width, animations.Last().height), Color.White);
            else if ((isBuilding && isPlaceSelected && isStartBuilding) || isWorking)
                spriteBatch.Draw(texture[textureName], position, animations.rectangle, Color.White);

            if (selected)
            {
                SelectRectangle.Draw(spriteBatch, rectangle);

                if (!unselected)
                    SelectRectangle.DrawTarget(spriteBatch, new Rectangle(target.X * 32, target.Y * 32, 32, 32));
            }
        }

        public void DrawUI(SpriteBatch spriteBatch)
        {
            if (selected)
            {
                ui.Draw(spriteBatch);
            }
        }

        public void builder()
        {
            isBuilding = true;
        }
		
        private int Normalize(float value)
		{
			return (int)(value / 32) * 32;
		}
	}
}
