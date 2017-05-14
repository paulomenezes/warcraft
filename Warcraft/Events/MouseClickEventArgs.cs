using Microsoft.Xna.Framework;
using System;

namespace Warcraft.Events
{
    class MouseClickEventArgs : EventArgs
    {
        public Vector2 Position;

        public int XTile { get { return (int)Position.X / 32; } }
        public int YTile { get { return (int)Position.Y / 32; } }

        public MouseClickEventArgs(Vector2 position)
        {
            Position = position;
        }
    }
}
