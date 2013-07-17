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
using Rithmatist.Animation.ParticleSystem;
using Rithmatist.Networking;

namespace Rithmatist.Level
{
    class NetworkDemoC : BaseLevel
    {
        Player player;
        private List<AbstractEntity> Lines;
        Client client;
        public NetworkDemoC()
        {
            levelDetails = "Client Demo";
            levelName = "Client Demo";
            this.ScreenState = ScreenSystem.ScreenState.Active;
            Camera.Instance.Zoom *= 20f;
            client = new Client("127.0.0.1", Rithmatist.Properties.Rithmatist.Default.Port);
        }
        public override void UnloadContent()
        {
            Farseer.Physics.Instance.World.Clear();
            base.UnloadContent();
        }
        public override void LoadContent()
        {
            Lines = new List<AbstractEntity>();
            base.LoadContent();
            player = new Player(Vector2.One * 10f);
            player.LoadContent(ScreenManager.Content);
            ParticleSystem.Instance.Initialize(this.ScreenManager.Game);
            ParticleFactory.Instance.Initialize(ScreenManager.Content);
            player.queue.add(new Vector2(12, 5), RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(12, 5), new Vector2(-3, 0f), 1f, 5f, 0f), TimeSpan.FromSeconds(3));
            player.queue.add(new Vector2(-6, -0), RithmaticFactory.CreateLineOfWarding(ScreenManager._assetCreator, new Vector2(-6, -0), 6), TimeSpan.FromSeconds(10));
            player.queue.add(new Vector2(5, 5), RithmaticFactory.CreateLineOfForbiddance(ScreenManager._assetCreator, new Vector2(5, 5), new Vector2(5, 8)), TimeSpan.FromSeconds(2));
            player.queue.add(new Vector2(18, 5), RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(18, 5), new Vector2(-3, 0f), 1f, 5f, 0f), TimeSpan.FromSeconds(3));
            player.queue.add(new Vector2(-14, -0), RithmaticFactory.CreateLineOfWarding(ScreenManager._assetCreator, new Vector2(-14, -0), 2), TimeSpan.FromSeconds(4));
            player.queue.add(new Vector2(2, -0), RithmaticFactory.CreateLineOfWarding(ScreenManager._assetCreator, new Vector2(2, -0), 2), TimeSpan.FromSeconds(4));
            player.queue.add(new Vector2(-25, 0), RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(-25, 2), new Vector2(2, 0), 1f, 5f, 0f), TimeSpan.FromSeconds(3));
            player.queue.add(new Vector2(12, -5), RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(12, -5), new Vector2(-3, 2), 1f, 5f, 1f), TimeSpan.FromSeconds(3));
            player.queue.add(new Vector2(12, -5), RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(12, -5), new Vector2(-3, 2), 1f, 5f, 1f), TimeSpan.FromSeconds(3));
            player.queue.add(new Vector2(12, -5), RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(12, -5), new Vector2(-3, 2), 1f, 5f, 1f), TimeSpan.FromSeconds(3));
            player.queue.add(new Vector2(12, -5), RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(12, -5), new Vector2(-3, 2), 1f, 5f, 1f), TimeSpan.FromSeconds(3));
            player.queue.add(new Vector2(12, -5), RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(12, -5), new Vector2(-3, 2), 1f, 5f, 1f), TimeSpan.FromSeconds(3));
            player.queue.add(new Vector2(12, -5), RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(12, -5), new Vector2(-3, 2), 1f, 5f, 1f), TimeSpan.FromSeconds(3));
            player.queue.add(new Vector2(12, -5), RithmaticFactory.CreateLineOfVigor(ScreenManager._assetCreator, new Vector2(12, -5), new Vector2(-3, 2), 1f, 5f, 1f), TimeSpan.FromSeconds(3));
            
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            player.Update(gameTime);
            RithmaticFactory.Update(gameTime);
            ParticleSystem.Instance.Update(gameTime);
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        }

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.SpriteBatch.Begin(0, BlendState.AlphaBlend, null, null, null, null, Camera.Instance.View);
            RithmaticFactory.Draw(ScreenManager.SpriteBatch);
            ParticleSystem.Instance.Draw(gameTime, ScreenManager.SpriteBatch);
            player.Draw(ScreenManager.SpriteBatch);
            ScreenManager.SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
