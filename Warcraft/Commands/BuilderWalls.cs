using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Warcraft.Managers;
using Warcraft.Units.Humans;
using Warcraft.Util;

namespace Warcraft.Commands
{
    class BuilderWalls : ICommand
    {
        private ManagerBuildings managerBuildings;
        private ManagerMouse managerMouse;
        private ManagerUnits managerUnits;
        private Builder builder;

        private Texture2D texture;

        public Vector2 startPoint = new Vector2(-1, -1);
        public Vector2 endPoint = new Vector2(-1, -1);
        private Vector2 current;

        public bool started;

        public BuilderWalls(Builder builder, ManagerMouse managerMouse, ManagerBuildings managerBuildings, ManagerUnits managerUnits)
        {
            this.builder = builder;
            this.managerMouse = managerMouse;
            this.managerBuildings = managerBuildings;
            this.managerUnits = managerUnits;

            managerMouse.MouseEventHandler += ManagerMouse_MouseEventHandler;
        }

        private void ManagerMouse_MouseEventHandler(object sender, Events.MouseEventArgs e)
        {
            if (started && !e.UI)
            {
                if (startPoint.X == -1)
                {
                    startPoint = new Vector2(e.SelectRectangle.X, e.SelectRectangle.Y);
                }
                else
                {
                    endPoint = new Vector2(e.SelectRectangle.X, e.SelectRectangle.Y);

                    started = false;

                    FinishWall();
                }
            }
        }

        public void FinishWall()
        {
			if (Normalize(startPoint.X) == Normalize(endPoint.X) && Normalize(startPoint.Y) == Normalize(endPoint.Y))
				AddWall(new Vector2(Normalize(startPoint.X), Normalize(startPoint.Y)), new Rectangle(528, 0, 32, 32));
			else
			{
				if (Math.Abs(endPoint.X - startPoint.X) > Math.Abs(endPoint.Y - startPoint.Y))
				{
					if (startPoint.X < endPoint.X)
						for (int x = Normalize(startPoint.X); x <= Normalize(endPoint.X); x += 32)
							AddWall(new Vector2(x, startPoint.Y), GetTextureOffsetX(x, startPoint.X, endPoint.X));
					else
						for (int x = Normalize(startPoint.X); x >= Normalize(endPoint.X); x -= 32)
							AddWall(new Vector2(x, startPoint.Y), GetTextureOffsetX(x, endPoint.X, startPoint.X));
				}
				else
				{
					if (startPoint.Y < endPoint.Y)
						for (int y = Normalize(startPoint.Y); y <= Normalize(endPoint.Y); y += 32)
							AddWall(new Vector2(startPoint.X, y), GetTextureOffsetY(y, startPoint.Y, endPoint.Y));
					else
						for (int y = Normalize(startPoint.Y); y >= Normalize(endPoint.Y); y -= 32)
							AddWall(new Vector2(startPoint.X, y), GetTextureOffsetY(y, endPoint.Y, startPoint.Y));
				}
			}

			managerUnits.managerMap.OrganizeWalls();

			startPoint.X = -1;
			endPoint.X = -1;
        }

        public void AddWall(Vector2 position, Rectangle textureOffset)
        {
            if (ManagerResources.CompareGold(managerUnits.index, 100))
            {
                Data.Write("Construindo muro X: " + Normalize(position.X) + " Y: " + Normalize(position.Y));

                ManagerResources.ReduceGold(managerUnits.index, 100);
                managerUnits.managerMap.AddWalls(position, textureOffset);
            }
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Summer Tiles");
        }

        public void execute()
        {
            started = true;
        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState();

            current = new Vector2(mouse.X, mouse.Y) + Warcraft.camera.center;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (startPoint.X == -1)
                spriteBatch.Draw(texture, current, new Rectangle(528, 0, 32, 32), Color.White);
            else if (startPoint.X != -1 && endPoint.X == -1)
            {
                DrawLine(spriteBatch, startPoint, current);
            }
        }

        private void DrawLine(SpriteBatch spriteBatch, Vector2 start, Vector2 end)
        {
            if (Normalize(start.X) == Normalize(end.X) && Normalize(start.Y) == Normalize(end.Y))
                spriteBatch.Draw(texture, new Vector2(Normalize(start.X), Normalize(start.Y)), new Rectangle(528, 0, 32, 32), Color.White);
            else
            {
                if (Math.Abs(end.X - start.X) > Math.Abs(end.Y - start.Y))
                {
                    if (start.X < end.X)
                        for (int x = Normalize(start.X); x <= end.X; x += 32)
                            spriteBatch.Draw(texture, new Vector2(x, Normalize(start.Y)), GetTextureOffsetX(x, start.X, end.X), Color.White);
                    else
                        for (int x = Normalize(start.X); x >= Normalize(end.X); x -= 32)
                            spriteBatch.Draw(texture, new Vector2(x, Normalize(start.Y)), GetTextureOffsetX(x, end.X, start.X), Color.White);
                }
                else
                {
                    if (start.Y < end.Y)
                        for (int y = Normalize(start.Y); y <= end.Y; y += 32)
                            spriteBatch.Draw(texture, new Vector2(Normalize(start.X), y), GetTextureOffsetY(y, start.Y, end.Y), Color.White);
                    else
                        for (int y = Normalize(start.Y); y >= Normalize(end.Y); y -= 32)
                            spriteBatch.Draw(texture, new Vector2(Normalize(start.X), y), GetTextureOffsetY(y, end.Y, start.Y), Color.White);
                }
            }
        }

        private int Normalize(float value)
        {
            return (int)(value / 32) * 32;
        }

        private Rectangle GetTextureOffsetX(int x, float start, float end)
        {
            var X = 0;

            start = Normalize(start);
            end = Normalize(end);

            if (x == start)
                X = 594;
            else if (x == end)
                X = 198;
            else
                X = 264;

            return new Rectangle(X, X == 594 ? 0 : 33, 32, 32);
        }

        private Rectangle GetTextureOffsetY(int y, float start, float end)
        {
            var X = 0;
            
            start = Normalize(start);
            end = Normalize(end);

            if (y == start)
                X = 561;
            else if (y == end)
                X = 33;
            else
                X = 66;

            return new Rectangle(X, X == 561 ? 0 : 33, 32, 32);
        }
    }
}
