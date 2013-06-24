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
using Rithmatist.Level;
using Rithmatist.Farseer;
using Rithmatist.Utility;
using Rithmatist.Animation;

namespace Rithmatist.Entities
{
    public abstract class AbstractSprite
    {
        protected BasicSprite sprite;

        public virtual BasicSprite Sprite
        {
            get { return sprite; }
        }
        public abstract Vector2 DisplayPosition();

        public virtual void Initialize()
        {}
        public virtual void Update(GameTime gameTime)
        { }
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            this.sprite.Draw(spriteBatch);
        }
    }
}

