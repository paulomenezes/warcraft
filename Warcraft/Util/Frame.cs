using System.Collections.Generic;
using System.Linq;

namespace Warcraft.Util
{
    struct Frame
    {
        public int[] sequence;
        public Dictionary<AnimationType, int> startIndex;
        public bool flipX;
        public bool flipY;

        public Frame(Dictionary<AnimationType, int> start)
        {
            sequence = null;
            startIndex = start;
            flipX = false;
            flipY = false;
        }

        public Frame(int start, int count)
        {
            startIndex = null;
            sequence = Enumerable.Range(start, count).ToArray();
            flipX = false;
            flipY = false;
        }

        public Frame(Dictionary<AnimationType, int> start, bool flipX) : this(start)
        {
            this.flipX = flipX;
        }

        public Frame(int start, int count, bool flipX) : this(start, count)
        {
            this.flipX = flipX;
        }

        public Frame(Dictionary<AnimationType, int> start, bool flipX, bool flipY) : this(start, flipX)
        {
            this.flipY = flipY;
        }
    }
}
