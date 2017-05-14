using Warcraft.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Warcraft.UI;

namespace Warcraft.Managers
{
    class ManagerMouse
    {
        private Vector2 position;
        private Vector2 selectCorner;
        private Rectangle selectRectangle;

        MouseState oldState;

        private bool mouseDown = false;

        private event EventHandler<MouseEventArgs> mouseEventHandler;
        private event EventHandler<MouseClickEventArgs> mouseClickEventHandler;

        public event EventHandler<MouseEventArgs> MouseEventHandler
        {
            add { mouseEventHandler += value; }
            remove { mouseEventHandler -= value; }
        }

        public event EventHandler<MouseClickEventArgs> MouseClickEventHandler
        {
            add { mouseClickEventHandler += value; }
            remove { mouseClickEventHandler -= value; }
        }

        public void Update()
        {
            MouseState mouse = Mouse.GetState();

            if (mouse.RightButton == ButtonState.Pressed)
            {
                mouseClickEventHandler?.Invoke(this, new MouseClickEventArgs(new Vector2(mouse.Position.X, mouse.Position.Y) + Warcraft.camera.center));
            }

            if (mouse.LeftButton == ButtonState.Pressed && !mouseDown)
            {
                mouseDown = true;

                position = new Vector2(mouse.X, mouse.Y) + Warcraft.camera.center;
                selectCorner = position;

                selectRectangle = new Rectangle((int)position.X, (int)position.Y, 0, 0);
            }
            else if (mouse.LeftButton == ButtonState.Pressed)
            {
                selectCorner = new Vector2(mouse.X, mouse.Y) + Warcraft.camera.center;

                if (selectCorner.X > position.X)
                    selectRectangle.X = (int)position.X;
                else
                    selectRectangle.X = (int)selectCorner.X;

                if (selectCorner.Y > position.Y)
                    selectRectangle.Y = (int)position.Y;
                else
                    selectRectangle.Y = (int)selectCorner.Y;

                selectRectangle.Width = (int)Math.Abs(position.X - selectCorner.X);
                selectRectangle.Height = (int)Math.Abs(position.Y - selectCorner.Y);
            }
            else
            {
                if (oldState.LeftButton == ButtonState.Pressed)
                {
                    if (mouse.X > Warcraft.WINDOWS_WIDTH)
                    {
                        Rectangle rect = new Rectangle((int)mouse.X, (int)mouse.Y, 0, 0);
                        mouseEventHandler?.Invoke(this, new MouseEventArgs(true, rect));
                    }
                    else
                        mouseEventHandler?.Invoke(this, new MouseEventArgs(false, selectRectangle));
                }

                mouseDown = false;
            }

            oldState = mouse;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (mouseDown)
                SelectRectangle.Draw(spriteBatch, selectRectangle);
        }
    }
}
