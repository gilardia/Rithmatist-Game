
using System.IO;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Rithmatist.Animation;
using Rithmatist.Entities;

namespace Rithmatist.Level
{
    class WizardDemo : BaseLevel
    {

        WizardAnimation wizard;
        Player _player;
        public WizardDemo()
        {
            levelDetails = "Wizard Demo";
            levelName = "Wizard Demo";
            this.backgroundTextureStr = "Materials/ground";
        }

        public override void LoadContent()
        {
            
            wizard = new WizardAnimation(ScreenManager.Content.Load<Texture2D>("Sprites\\Wizard\\wizard"), new StreamReader(@"Content/Sprites/Wizard/wizard.txt"));
            wizard.Animation = "wizard_d_walk";
      //      _player = new Player(wizard, Vector2.Zero);
            base.LoadContent();
        }
        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            _player.Update(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }
        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin(0, null, null, null, null, null, Camera.Instance.View);
            //Draw stuff in here
            _player.Sprite.Draw(ScreenManager.SpriteBatch);
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
