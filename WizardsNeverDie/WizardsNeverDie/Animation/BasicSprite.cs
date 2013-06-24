using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Rithmatist.Animation
{
    public abstract class BasicSprite
    {
        public abstract void Update(GameTime theGameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
