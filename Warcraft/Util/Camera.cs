using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Warcraft.Managers;

namespace Warcraft.Util
{
    public class Camera
    {
        public Matrix transform;
        public Vector2 center = Vector2.Zero; //new Vector2(322, 480); // new Vector2(32 * 25 - Warcraft.WINDOWS_HEIGHT / 2, 32 * 32 - Warcraft.WINDOWS_HEIGHT / 2);
        Viewport view;

        float speed = 4;
        float zoom = 1; // Warcraft.PLAYER ? 1 : 0.5f;

        int player = 0;

        KeyboardState lastState;

        public Camera(Viewport viewport)
        {
            view = viewport;
            center = ManagerBuildings.goldMines[player].position - new Vector2(Warcraft.WINDOWS_WIDTH / 2, Warcraft.WINDOWS_HEIGHT / 2);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();

            if (keyboard.IsKeyDown(Keys.Left)   || keyboard.IsKeyDown(Keys.A)) center.X -= speed;
            if (keyboard.IsKeyDown(Keys.Right)  || keyboard.IsKeyDown(Keys.D)) center.X += speed;
            if (keyboard.IsKeyDown(Keys.Up)     || keyboard.IsKeyDown(Keys.W)) center.Y -= speed;
            if (keyboard.IsKeyDown(Keys.Down)   || keyboard.IsKeyDown(Keys.S)) center.Y += speed;

            if (keyboard.IsKeyUp(Keys.Space) && lastState.IsKeyDown(Keys.Space)) {
                player = player == 0 ? 1 : 0;
                center = ManagerBuildings.goldMines[player].position - new Vector2(Warcraft.WINDOWS_WIDTH / 2, Warcraft.WINDOWS_HEIGHT / 2);
            }
            
            //if (mouse.X > Warcraft.WINDOWS_WIDTH + 100 && mouse.X < Warcraft.WINDOWS_WIDTH + 200) center.X += speed;
            //if (mouse.Y > Warcraft.WINDOWS_HEIGHT - 100 && mouse.Y < Warcraft.WINDOWS_HEIGHT) center.Y += speed;
            //if (mouse.X > 0 && mouse.X < 100) center.X -= speed;
            //if (mouse.Y > 0 && mouse.Y < 100) center.Y -= speed;

            center.X = Math.Max(center.X, 0); //312
            center.Y = Math.Max(center.Y, 0); //796

            center.X = Math.Min(ManagerIsland.rooms[Warcraft.PLAYER_ISLAND].rectangle.Width - Warcraft.WINDOWS_WIDTH - 32, center.X);
            center.Y = Math.Min(ManagerIsland.rooms[Warcraft.PLAYER_ISLAND].rectangle.Height - Warcraft.WINDOWS_HEIGHT - 32, center.Y);

            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * 
                        Matrix.CreateScale(new Vector3(zoom, zoom, 1)) * 
                        Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));

            lastState = keyboard;
        }
    }
}
