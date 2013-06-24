using System;
using FarseerPhysics.DebugViews;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Joints;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics;

namespace Rithmatist.ScreenSystem
{
    public class PhysicsGameScreen : GameScreen
    {
        protected PhysicsGameScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0);
            TransitionOffTime = TimeSpan.FromSeconds(0);
            HasCursor = true;
            EnableCameraControl = true;
        }

        public bool EnableCameraControl { get; set; }

        public override void LoadContent()
        {
            Farseer.Physics.Instance.LoadContent(ScreenManager.Game.GraphicsDevice, ScreenManager.Game.Content);
            base.LoadContent();

            //We enable diagnostics to show get values for our performance counters.

            Camera.Instance.Initialize(ScreenManager.GraphicsDevice);

            // Loading may take a while... so prevent the game from "catching up" once we finished loading
            ScreenManager.Game.ResetElapsedTime();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            if (!coveredByOtherScreen && !otherScreenHasFocus)
            {
                // variable time step but never less then 30 Hz
                Farseer.Physics.Instance.Update(gameTime);
            }
            Camera.Instance.Update(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void HandleInput(InputHelper input, GameTime gameTime)
        {
         
            if (input.IsNewButtonPress(Buttons.Back) || input.IsNewKeyPress(Keys.Escape))
            {
                ExitScreen();
            }

            base.HandleInput(input, gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}