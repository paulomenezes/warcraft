using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Warcraft.Util
{
    enum AnimationType
    {
        WALKING,
        DYING,
        GOLD,
        WOOD,
        WOOD_WORKER,
        ATTACKING,
        OTHER
    }

    class Animation
    {
        public Dictionary<string, Frame> animations = new Dictionary<string, Frame>();

        public Dictionary<AnimationType, List<Sprite>> sprites = new Dictionary<AnimationType, List<Sprite>>();
        private bool play = false;

        public Rectangle rectangle;

        public int speed = 5;
        private int elapsed;
        private int index;

        private int width;
        private int height;

        public string current;

        public bool isLooping = true;
        public bool completed = false;

        public AnimationType currentAnimation;

        Dictionary<string, int> animationIndex = new Dictionary<string, int>();

        public Animation(Dictionary<AnimationType, List<Sprite>> sprites, Dictionary<string, Frame> animations, string initial, int width, int height)
        {
            animationIndex.Add("up", 0);
            animationIndex.Add("down", 1);
            animationIndex.Add("right", 2);
            animationIndex.Add("left", 2);
            animationIndex.Add("upRight", 3);
            animationIndex.Add("downRight", 4);
            animationIndex.Add("upLeft", 3);
            animationIndex.Add("downLeft", 4);
            animationIndex.Add("dying", 5);
            //= new string[8] { "up", "down", "right", "left", "upRight", "downRight", "upLeft", "downLeft" }.Select((value, index) => new { value, index }).ToDictionary(pair => pair.value, pair => pair.index);

            this.sprites = sprites;
            this.animations = animations;
            this.current = initial;

            this.width = width;
            this.height = height;

            currentAnimation = AnimationType.WALKING;
        }

        public Animation(Dictionary<AnimationType, List<Sprite>> sprites, Dictionary<string, Frame> animations, string initial, int width, int height, bool repeat, int speed)
            : this(sprites, animations, initial, width, height)
        {
            isLooping = repeat;
            this.speed = speed;
        }

        public void Play(string animation)
        {
            current = animation;
            play = true;
        }

        public void Change(string name)
        {
            current = name;
        }

        public void Stop()
        {
            play = false;
        }

        public bool FlipX()
        {
            return animations[current].flipX;
        }

        public bool FlipY()
        {
            return animations[current].flipY;
        }

        public Sprite Last()
        {
            return sprites[currentAnimation][sprites[currentAnimation].Count - 1];
        }
       
        public void Update()
        {
            if (play)
            {
                elapsed++;
                if (elapsed > speed)
                {
                    index++;
                    elapsed = 0;

                    if (index >= (animations[current].sequence != null ? animations[current].sequence.Length : animations[current].startIndex[currentAnimation]))
                    {
                        if (isLooping)
                            index = 0;
                        else
                        {
                            index--;
                            play = false;
                            completed = true;
                        }
                    }
                }
            }
            else
            {
                if (isLooping)
                    index = 0;
            }

            if (animations[current].sequence != null)
            {
                if (currentAnimation == AnimationType.DYING) current = "dying";
                if (current == "dying") currentAnimation = AnimationType.DYING;

                if (currentAnimation == AnimationType.DYING) index = 0;

                rectangle = new Rectangle(sprites[currentAnimation][animations[current].sequence[index]].x - (width - sprites[currentAnimation][animations[current].sequence[index]].width) / 2,
                                          sprites[currentAnimation][animations[current].sequence[index]].y - (height - sprites[currentAnimation][animations[current].sequence[index]].height) / 2,
                                          width, height);
            }
            else
            {
                var i = animationIndex[current] * animations[current].startIndex[currentAnimation] + index;
                if (i == sprites[currentAnimation].Count) i--;

                if (currentAnimation == AnimationType.DYING) i = 0;

                if (i < sprites[currentAnimation].Count)
                    rectangle = new Rectangle(sprites[currentAnimation][i].x - (width - sprites[currentAnimation][i].width) / 2,
                                              sprites[currentAnimation][i].y - (height - sprites[currentAnimation][i].height) / 2,
                                              width, height);
            }
        }
    }
}
