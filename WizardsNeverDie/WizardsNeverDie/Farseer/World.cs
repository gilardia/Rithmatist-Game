using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Controllers;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Common;
using FarseerPhysics.Collision;
using FarseerPhysics;
using FarseerPhysics.DebugViews;

namespace Rithmatist.Farseer
{
    public sealed class Physics
    {
        public enum CollisionGroup
        {
            None = FarseerPhysics.Dynamics.Category.None,
            All = FarseerPhysics.Dynamics.Category.All,
            LineOfWarding = FarseerPhysics.Dynamics.Category.Cat2,
            LineOfMaking = FarseerPhysics.Dynamics.Category.Cat3,
            LineOfVigor =FarseerPhysics.Dynamics.Category.Cat4,
            LineOfForbiddance = FarseerPhysics.Dynamics.Category.Cat5,
            Player = FarseerPhysics.Dynamics.Category.Cat6
        
        }
        private static readonly Physics instance = new Physics();
        public readonly World World;
        public readonly DebugViewXNA DebugView;
        private Physics()
        {
            World = new World(Vector2.Zero);
            DebugView = new DebugViewXNA(World);
            DebugView.AppendFlags(DebugViewFlags.Shape);
            DebugView.AppendFlags(DebugViewFlags.AABB);
            DebugView.AppendFlags(DebugViewFlags.CenterOfMass);
       //     DebugView.AppendFlags(DebugViewFlags.DebugPanel);
            DebugView.Enabled = false;
        }
        public void LoadContent(GraphicsDevice graphics, ContentManager content)
        {
            DebugView.LoadContent(graphics, content);
            DebugView.Enabled = true;
        }
        public void Update(GameTime gameTime)
        {
            World.Step((float)(gameTime.ElapsedGameTime.TotalSeconds));
        }
        public void Draw ()
        {
            //The farseer drawing calls should be moved at some point
            Matrix projection = Camera.Instance.SimProjection;
            Matrix view = Camera.Instance.SimView;
         //   if (Farseer.Physics.Instance.DebugView != null && Farseer.Physics.Instance.DebugView.Enabled == true)
         //       Farseer.Physics.Instance.DebugView.RenderDebugData(ref projection, ref view);
        }
        public static Physics Instance
        {
            get
            {
                return instance;
            }
        }

    }
}
