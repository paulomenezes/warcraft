using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Warcraft.Util;

namespace Warcraft.EA
{
    class GeneBuilding : Gene
    {
        public Vector2 position;

        public GeneBuilding(int action) 
            : base(action)
        {
        }
    }
}
