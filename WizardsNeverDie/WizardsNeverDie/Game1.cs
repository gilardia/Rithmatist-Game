using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Rithmatist.Level;
using Rithmatist.ScreenSystem;
using System.Net.Sockets;
using System.Net;

namespace Rithmatist
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _graphics;
        private static Game1 instance;
        public Game1()
        {
            Window.Title = "The Rithmatist";
            instance = this;

            _graphics = new GraphicsDeviceManager(this);

            _graphics.PreferMultiSampling = true;
#if WINDOWS || XBOX
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            Rithmatist.Utility.ConvertUnits.SetDisplayUnitToSimUnitRatio(24f);
            IsFixedTimeStep = true;
#elif WINDOWS_PHONE
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 480;
            ConvertUnits.SetDisplayUnitToSimUnitRatio(16f);
            IsFixedTimeStep = false;
#endif
#if WINDOWS
            _graphics.IsFullScreen = false;
#elif XBOX || WINDOWS_PHONE
            _graphics.IsFullScreen = true;
#endif
            Content.RootDirectory = "Content";

            //new-up components and add to Game.Components
            ScreenManager = new ScreenManager(this);
            Components.Add(ScreenManager);
            FrameRateCounter frameRateCounter = new FrameRateCounter(ScreenManager);
            frameRateCounter.DrawOrder = 101;
            Components.Add(frameRateCounter);

            //added coordinates
            MousePosition displayInfo = new MousePosition(ScreenManager);
            displayInfo.DrawOrder = 101;
            Components.Add(displayInfo);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            NetworkDemoC netDemoc = new NetworkDemoC();
            WizardDemo wizardDemo = new WizardDemo();
            TutorialDemo tutorialDemo = new TutorialDemo();
            MenuScreen menuScreen = new MenuScreen("The Rithmatist");

            menuScreen.AddMenuItem("Levels", EntryType.Separator, null);
            menuScreen.AddMenuItem(netDemoc.GetTitle(), EntryType.Screen, netDemoc);
            menuScreen.AddMenuItem(tutorialDemo.GetTitle(), EntryType.Screen, tutorialDemo);
            menuScreen.AddMenuItem(wizardDemo.GetTitle(), EntryType.Screen, wizardDemo);


            ScreenManager.AddScreen(new BackgroundScreen("Common/title_screen"));
            ScreenManager.AddScreen(menuScreen);
           // ScreenManager.AddScreen(new LogoScreen(TimeSpan.FromSeconds(3.0)));
            base.Initialize();
        }
        public ScreenManager ScreenManager { get; set; }
    }
}
