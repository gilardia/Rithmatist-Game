using System.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Rithmatist.Animation;
using Rithmatist.Farseer;
using Rithmatist.Level;
using Rithmatist.Entities;

namespace Rithmatist.Level
{
    class TutorialDemo : BaseLevel
    {

        private SpriteAnimation _spriteAnimation1;
        private SpriteAnimation _spriteAnimation2;
        private AbstractEntity ward1, vig1, vig2, forbid1;
        public enum State{
            s1,
            s2,
            s3,
            s4,
            s5,
            s6,
            s7,
            s8
        }
        private State state;
        public TutorialDemo()
        {
            levelDetails = "Tutorial Demo";
            levelName = "Tutorial Demo";
            state = State.s1;
            this.ScreenState = ScreenSystem.ScreenState.Active;
            Camera.Instance.Zoom *= 20f;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            if (state == State.s1)
            {
                ward1 = Rithmatist.Entities.Rithmatics.RithmaticFactory.CreateLineOfWarding(ScreenManager._assetCreator, new Vector2(-0, -4), 5);
                vig1 = Rithmatist.Entities.Rithmatics.RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(12, 5), new Vector2(-4, -4), 1f, 4f, 0f);
                if (vig1.OnCollision(ward1))
                {
                    state = State.s2;
                }
            }
            if (state == State.s2)
            {
            }
            
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (state == State.s1)
            {
                ward1.Update(gameTime);
                vig1.Update(gameTime);
            }
            if (state == State.s2)
            {
                ward1.Update(gameTime);
                vig1.Update(gameTime);
            }
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin(0, BlendState.AlphaBlend, null, null, null, null, Camera.Instance.View);
            ward1.Draw(ScreenManager.SpriteBatch);
            vig1.Draw(ScreenManager.SpriteBatch);
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
