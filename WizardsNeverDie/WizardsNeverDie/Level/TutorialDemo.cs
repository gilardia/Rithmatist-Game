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
            LineOfWarding e1 = RithmaticFactory.CreateLineOfWarding(ScreenManager._assetCreator, new Vector2(-0, -0), 6) as LineOfWarding;
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
            RithmaticFactory.onVigorCreation += (y) =>
            {
                y.onDestruction += () =>
                {
                    state = State.s3;
                    timer = TimeSpan.Zero;
                    InitializeState3();
                };
            };
        }
        public void InitializeState3()
        {
            timer = TimeSpan.Zero;
            RithmaticFactory.onVigorCreation += (z) =>
            {
                z.onDestruction += () =>
                {
                    state = State.s4;
                    timer = TimeSpan.Zero;
                    InitializeState4();
                };
            };
        }
        public void InitializeState4()
        {
            timer = TimeSpan.Zero;
            LineOfForbiddance e2 = RithmaticFactory.CreateLineOfForbiddance(ScreenManager._assetCreator, new Vector2(-15, 5), new Vector2(6, 7)) as LineOfForbiddance;
            e2.createBody();
            entities.Add(e2);
        }
        /*public void InitializeState5()
        {
            timer = TimeSpan.Zero;

        }*/
        TimeSpan timer = TimeSpan.Zero;
        TimeSpan max = TimeSpan.FromSeconds(6);
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            timer += gameTime.ElapsedGameTime;
            if (timer > max)
            {
                timer = TimeSpan.Zero;
                switch (state)
                {
                    case State.s1:
                        {
                        player.queue.add(Vector2.Zero, RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(12, 5), new Vector2(-4, -4), 1f, 4f, 0f), TimeSpan.FromSeconds(4));
                        break;
                        }
                    case State.s2:
                        {
                        player.queue.add(Vector2.Zero, RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(14, -10), new Vector2(-5, 2), 1f, 3f, 0f), TimeSpan.FromSeconds(4));
                        break;
                        }
                    case State.s3:
                        {
                            player.queue.add(Vector2.Zero, RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(-20, 8), new Vector2(5f, -3.5f), 1f, 2f, 0f), TimeSpan.FromSeconds(3));
                            break;
                        }
                    case State.s4:
                        {
                            player.queue.add(Vector2.Zero, RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(12, 9), new Vector2(-6, .1f), 1f, 5f, 0f), TimeSpan.FromSeconds(4));
                            break;
                        }
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
