using Microsoft.Xna.Framework;
using System;

namespace Warcraft.Events
{
    class MouseEventArgs : EventArgs
    {
        public Rectangle SelectRectangle;
        public bool UI;

        public MouseEventArgs(bool ui, Rectangle selectRectangle)
        {
            UI = ui;
            SelectRectangle = selectRectangle;
        }
    }
}
