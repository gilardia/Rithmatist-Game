using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Rithmatist.Animation;
using Rithmatist.Level;
using Rithmatist.Farseer;
using Rithmatist.Entities;

namespace Rithmatist.Intelligence
{
    public abstract class AbstractIntelligence
    {
        public abstract void Update(GameTime gameTime);
       
    }
}
