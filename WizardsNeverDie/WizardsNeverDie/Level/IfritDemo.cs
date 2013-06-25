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
    class IfritDemo : BaseLevel
    {
        Player player = new Player(Vector2.Zero);

        public IfritDemo()
        {
            levelDetails = "Ifrit Demo";
            levelName = "Ifrit Demo";
            this.ScreenState = ScreenSystem.ScreenState.Active;
            Camera.Instance.Zoom *= 20f;
            Farseer.Physics.Instance.World.Clear();
        }

        public override void LoadContent()
        {
            base.LoadContent();
            player.queue.add(Vector2.Zero, RithmaticFactory.CreateLineOfWarding(ScreenManager._assetCreator, new Vector2(-6, -0), 6), TimeSpan.FromSeconds(5));
            player.queue.add(Vector2.Zero, RithmaticFactory.CreateLineOfWarding(ScreenManager._assetCreator, new Vector2(-14, -0), 2), TimeSpan.FromSeconds(5));
            player.queue.add(Vector2.Zero, RithmaticFactory.CreateLineOfForbiddance(ScreenManager._assetCreator, new Vector2(5, 5), new Vector2(5, 8)), TimeSpan.FromSeconds(2));
            player.queue.add(Vector2.Zero, RithmaticFactory.CreateLineOfWarding(ScreenManager._assetCreator, new Vector2(2, -0), 2), TimeSpan.FromSeconds(5));
            player.queue.add(Vector2.Zero, RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(12, -5), new Vector2(-3, 2), 1f, 5f, 0f), TimeSpan.FromSeconds(5));
            player.queue.add(Vector2.Zero, RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(12, 5), new Vector2(-3, 0), 1f, 5f, 0f), TimeSpan.FromSeconds(5));
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
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
