using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rithmatist.Animation
{
    class WizardAnimation : SpriteAnimation
    {
        private bool spell1 = false, spell2 = false;
        public WizardAnimation(Texture2D Texture, int frames, int animations)
            : base(Texture, frames, animations)
        {
        }

        public WizardAnimation(Texture2D texture, StreamReader sr)
            : base(texture, sr)
        {
        }

        //update current frame 
        public override void Update(GameTime gameTime)
        {
            //if(timeElapsed>TimeToUpdate)
            base.Update(gameTime);
            
            if (this.GetAnimationState() == AnimationState.Spell1)
            {
                
                Spell1(gameTime);
            }
        }
        private void Spell1 (GameTime gameTime)
        {
            if (timeElapsed > TimeToUpdate)
            {
                FrameIndex++;
                if (FrameIndex > Animations[Animation].NumOfFrames - 1)
                {
                    InChainAnimation = false; 
                    this.SetAnimationState(AnimationState.Walk);
                    FrameIndex = 0;
                    timeElapsed -= TimeToUpdate;
                }
                else
                {
                    timeElapsed -= TimeToUpdate;
                    FrameIndex %= Animations[Animation].NumOfFrames;
                }
                    
                //else
                //{
                //    //FrameIndex %= Animations[this.Animation].NumOfFrames;
                //    // Keep the Frame between 0 and the total frames, minus one.
                //    FrameIndex++;
                //    timeElapsed -= 1 / this.FramesPerSecond;
                //}
            }
        }
        public override void SetAnimationState(AnimationState state)
        {
            if (state != GetAnimationState() && !InChainAnimation)
            {
                if (state == AnimationState.Spell1)
                {
                    //this.FramesPerSecond = 5f;
                    Animation = Animation.Split('_')[0] + '_' + Animation.Split('_')[1] + '_' + "spell1";
                    FrameIndex = 0;
                    IsMoving = true;
                    InChainAnimation = true;
                }
                else
                {
                    InChainAnimation = false;
                    //this.FramesPerSecond = 5f;
                    base.SetAnimationState(state);
                }
            }
        }
        public override AnimationState GetAnimationState()
        {
            if (!IsMoving)
            {
                return AnimationState.Stop;
            }
            string state = Animation.Split('_')[2];
            if(state == "spell1")
            {
                return AnimationState.Spell1;
            }
            else
            {
                return base.GetAnimationState(state);
            }
        }
    }
}
