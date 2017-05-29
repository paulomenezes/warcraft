using System;
using Microsoft.Xna.Framework;

namespace Warcraft.Util
{
    abstract class Base
    {
		public Animation animations;

		public Vector2 position;
		public Vector2 Position
		{
            get; set;
		}


		public Information information;
		public static Information Information;

		public Base()
        {
	    }
    }
}
