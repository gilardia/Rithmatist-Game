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

namespace Rithmatist.Entities
{
    class Enemy: AbstractCreature
    {
        public Enemy(SpriteManager spriteManager, AbstractCreature target, Vector2 position)
        {
            this.sprite = spriteManager;
            this.body = BasicBody.CreateCircleBody(this, position, 1f);
            this.intelligence = new CreatureIntelligence(this, target, 5f);
        }
        public void Update(GameTime gameTime)
        {
            intelligence.Update(gameTime);
            sprite.Update(gameTime);
        }
        public AbstractCreature Target
        {
            get
            {
                return ((CreatureIntelligence)intelligence).target;
            }
            set
            {
                ((CreatureIntelligence)intelligence).target = value;
            }
        }
    }
}
