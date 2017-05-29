using System;
using Microsoft.Xna.Framework;

namespace Warcraft.EA
{
    class GeneBuildWall : Gene
    {
        public Vector2 start;
        public Vector2 end;

        public GeneBuildWall(int action) 
            : base(action)
        {
        }
    }
}
