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
using Rithmatist.Entities.Rithmatics;

namespace Rithmatist.Level
{
    class TutorialDemo : BaseLevel
    {
        KeyboardState kstate = Keyboard.GetState();
        Player player = new Player(Vector2.Zero);
        List<AbstractEntity> entities = new List<AbstractEntity>();
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
            Farseer.Physics.Instance.World.Clear();
        }
        public override void LoadContent()
        {
            base.LoadContent();
            InitializeState1();       
        }
        public void InitializeState1()
        {
            LineOfWarding e1 = Rithmatist.Entities.Rithmatics.RithmaticFactory.CreateLineOfWarding(ScreenManager._assetCreator, new Vector2(-0, -0), 5) as LineOfWarding;
            e1.createBody();
            entities.Add(e1);

            RithmaticFactory.onVigorCreation += (x) =>
            {
                x.onDestruction += () =>
                {
                    state = State.s2;
                    timer = TimeSpan.Zero;
                    InitializeState2();
                };
            };
        }
        public void InitializeState2()
        {
            timer = TimeSpan.Zero;

        }
        TimeSpan timer = TimeSpan.Zero;
        TimeSpan max = TimeSpan.FromSeconds(5);
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            timer += gameTime.ElapsedGameTime;
            if (timer > max)
            {
                timer = TimeSpan.Zero;
                switch (state)
                {
                    case State.s1: player.queue.add(Vector2.Zero, Rithmatist.Entities.Rithmatics.RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(12, 5), new Vector2(-4, -4), 1f, 4f, 0f), TimeSpan.FromSeconds(2)); break;
                    case State.s2: player.queue.add(Vector2.Zero, Rithmatist.Entities.Rithmatics.RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(14, -10), new Vector2(-5, 2), 1f, 4f, 0f), TimeSpan.FromSeconds(2)); break;
                }

            }

            player.queue.construct(gameTime.ElapsedGameTime);
            RithmaticFactory.Update(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin(0, BlendState.AlphaBlend, null, null, null, null, Camera.Instance.View);
            RithmaticFactory.Draw(ScreenManager.SpriteBatch);
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
