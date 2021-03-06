﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.IO;

namespace Rithmatist.Animation
{
    public enum AnimationState
    {
        Walk,
        Run,
        Attack,
        Spell1,
        Stop
    }
    //C# enum type is fucking useless
    [Flags]
    public enum Orientation
    {
        None,
        Left = 1,
        Right = 2,
        Up = 4,
        Down = 8,
        DownLeft = 5,
        DownRight = 6,
        UpLeft = 9,
        UpRight = 10
    }

    public class SpriteAnimation : SpriteManager
    {
        
        protected float framesPerSecond = 2f;

        // default to 20 frames per second
        //private float  = 1/10f; // framespersecond
        protected float timeElapsed = 0;
        protected bool  InChainAnimation = false, left = false, right = false;
        private bool _isMoving = false;

        public SpriteAnimation(Texture2D Texture, int frames, int animations)
            : base(Texture, frames, animations)
        {
        }

        public SpriteAnimation(Texture2D texture, StreamReader sr)
            : base(texture, sr)
        {
        }

        public float FramesPerSecond
        {
            set
            {
                framesPerSecond = value;
            }
        }

        public float TimeToUpdate
        {
            get
            {
                return 1 / framesPerSecond;
            }
        }
        public bool IsMoving
        {
            get { return _isMoving; }
            set { _isMoving = value; }
        }


        public override void Update(GameTime gameTime)
        {
            timeElapsed += (float)
                            gameTime.ElapsedGameTime.TotalSeconds;
            if (GetAnimationState() == AnimationState.Stop)
            {
                //FrameIndex %= Animations[Animation].NumOfFrames;
                ////timeElapsed -= timeToUpdate;
                if (timeElapsed > this.TimeToUpdate)
                    timeElapsed -= TimeToUpdate;
                return;
            }
            else if (GetAnimationState() == AnimationState.Walk && timeElapsed>this.TimeToUpdate)
            {
                // Keep the Frame between 0 and the total frames, minus one.
                FrameIndex++;
                FrameIndex %= Animations[Animation].NumOfFrames;
                timeElapsed -= TimeToUpdate;
            }
        }
        public String GetOrientation(Orientation orientation)
        {
            if (orientation == Orientation.Left)
                return "dl";
            else if (orientation == Orientation.Right)
                return "dr";
            else if (orientation == Orientation.Up)
                return "u";
            else if (orientation == Orientation.Down)
                return "d";
            else if (orientation == Orientation.UpLeft)
                return "ul";
            else if (orientation == Orientation.UpRight)
                return "ur";
            else if (orientation == Orientation.DownLeft)
                return "dl";
            else if (orientation == Orientation.DownRight)
                return "dr";
            else
                return "";
        }
        public Orientation GetOrientation()
        {
            if (left)
                return Orientation.Left;
            else if (right)
                return Orientation.Right;

            string orientation = Animation.Split('_')[1];

            if (orientation == "u")
                return Orientation.Up;
            else if (orientation == "ul")
                return Orientation.UpLeft;
            else if (orientation == "ur")
                return Orientation.UpRight;
            else if (orientation == "dl")
                return Orientation.DownLeft;
            else if (orientation == "d")
                return Orientation.Down;
            else if (orientation == "dr")
                return Orientation.DownRight;
            else
                return Orientation.None;
        }
        public void SetOrientation(Orientation orentation)
        {
            string o = Animation.Split('_')[0] + '_' + GetOrientation(orentation) + '_' + Animation.Split('_')[2];
            if (GetOrientation(orentation) != o && IsMoving)
            {
                if (orentation == Orientation.Left)
                    left = true;
                else if (orentation == Orientation.Right)
                    right = true;
                else
                {
                    left = false;
                    right = false;
                }
                this.Animation = Animation.Split('_')[0] + '_' + GetOrientation(orentation) + '_' + Animation.Split('_')[2];
                FrameIndex %= Animations[Animation].Rectangles.Count();
            }
        }
        public virtual void SetAnimationState(AnimationState state)
        {
            switch (state)
            {
                case AnimationState.Attack:
                    Animation = Animation.Split('_')[0] + '_' + Animation.Split('_')[1] + '_' + "attack";
                    _isMoving = true;
                    break;
                case AnimationState.Walk:
                    Animation = Animation.Split('_')[0] + '_' + Animation.Split('_')[1] + '_' + "walk";
                    _isMoving = true;
                    break;
                case AnimationState.Stop:
                    Animation = Animation.Split('_')[0] + '_' + Animation.Split('_')[1] + '_' + "walk";
                    _isMoving = false;
                    break;
            }
        }

        public virtual AnimationState GetAnimationState()
        {
            return GetAnimationState(Animation.Split('_')[2]);
        }
        protected AnimationState GetAnimationState(string state)
        {
            //if (state == "attack")
            //    return AnimationState.Attack;
            //else (state == "walk")
            if(state == "walk" && _isMoving)
            {
                return AnimationState.Walk;
            }
            else
            {
                return AnimationState.Stop;
            }
        }

        //public void SetOrientation()
        //{
        //    Animation.Split('_')
        //}
    }
}
