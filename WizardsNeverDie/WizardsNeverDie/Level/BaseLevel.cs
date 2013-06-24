using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Rithmatist.ScreenSystem;
using FarseerPhysics.DebugViews;
using FarseerPhysics;
using Microsoft.Xna.Framework;

namespace Rithmatist.Level 
{
    public abstract class BaseLevel : PhysicsGameScreen
    {
        protected String levelDetails, levelName, backgroundTextureStr;
        private BackgroundScreen background;

        public override void LoadContent()
        {

            base.LoadContent();

            if (backgroundTextureStr != null)
            {
                background = new BackgroundScreen("Materials/ground");
                this.ScreenManager.AddScreen(background);
                this.AttachScreen(background);
            }
        }

        public string GetTitle()
        {
            return levelName;
        }

        public string GetDetails()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(levelDetails);
            return sb.ToString();
        }


}
}
