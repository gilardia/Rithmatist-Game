using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;

namespace Rithmatist.Animation
{
    public abstract class SpriteManager: BasicSprite
    {
        protected Texture2D Texture;
        public Vector2 Position = Vector2.Zero;
        protected Dictionary<string, AnimationClass> Animations = new Dictionary<string, AnimationClass>();
        protected int FrameIndex = 0;
        protected Vector2 Origin;

        private int height;
        private int width;

        private string animation;

        public string Animation
        {
            get { return animation; }
            set
            {
                animation = value;
                //FrameIndex = 0;
            }
        }

        protected SpriteManager(Texture2D Texture, int Frames, int animations)
        {
            this.Texture = Texture;
            width = Texture.Width / Frames;
            height = Texture.Height / animations;
            Origin = new Vector2(width / 2, height / 2);
        }

        protected SpriteManager(Texture2D Texture, StreamReader sr)
        {
            this.Texture = Texture;
            AddAnimation(sr);
        }

        public void AddAnimation(string name, int row,
            int frames, AnimationClass animation)
        {
            Rectangle[] recs = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
            {
                recs[i] = new Rectangle(i * width,
                    (row - 1) * height, width, height);
            }
            //animation.Frames = frames;
            animation.Rectangles = recs;
            Animations.Add(name, animation);
        }

        public void AddAnimation(string name, AnimationClass animation)
        {
            Animations.Add(name, animation);
        }

        public void AddAnimation(StreamReader sr)
        {
            List<Rectangle> recs = new List<Rectangle>();
            string line, name, currentName = null;
            string[] strs;
            int x, y, width, height;
            AnimationClass animation = new AnimationClass();
            //line = sr.ReadLine();
            while ((line = sr.ReadLine()) != null)
            {
                strs = line.Split(' ');

                //get the name  <name>_<orientation>_<Action>-<FrameNumber>
                name = strs[0].Substring(0, strs[0].LastIndexOf('-'));
                if (currentName == null)
                    currentName = name;
                else if (name != currentName)    // we got all the frames for the animation
                {
                    animation.Rectangles = recs.ToArray();
                    AddAnimation(currentName, animation);

                    animation = new AnimationClass();
                    recs = new List<Rectangle>();
                    currentName = name;
                }
                x = Convert.ToInt32(strs[2]);
                y = Convert.ToInt32(strs[3]);
                width = Convert.ToInt32(strs[4]);
                height = Convert.ToInt32(strs[5]);
                Origin = new Vector2(width / 2, height / 2);
                recs.Add(new Rectangle(x, y, width, height));
            }
            animation.Rectangles = recs.ToArray();
            AddAnimation(currentName, animation);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (FrameIndex < Animations[Animation].Rectangles.Count())
            {
                Rectangle rec = Animations[Animation].Rectangles[FrameIndex];
                spriteBatch.Draw(Texture, Utility.ConvertUnits.ToDisplayUnits(Position),
                    rec,
                    Animations[Animation].Color,
                    Animations[Animation].Rotation, Origin,
                    Animations[Animation].Scale,
                    Animations[Animation].SpriteEffect, 0f);
            }
        }
    }
}
