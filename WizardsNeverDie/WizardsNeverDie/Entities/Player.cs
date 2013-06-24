using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FarseerPhysics.DebugViews;
using Rithmatist.Farseer;
using Rithmatist.Level;
using Rithmatist.Animation;
using Rithmatist.Intelligence;
using System.IO;
using Rithmatist.Entities.Rithmatics;

namespace Rithmatist.Entities
{
    public class Player : AbstractEntity
    {
        public ConstructionQueue queue = new ConstructionQueue(Vector2.Zero);
        public Player(Vector2 position)
        {
            this.body = BasicBody.CreateCircleBody(this, position, 5f);
            
        }
        public override void Update(GameTime gameTime)
        {
            queue.construct(gameTime.ElapsedGameTime);
        }
    }
}