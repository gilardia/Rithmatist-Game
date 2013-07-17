using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using Rithmatist.Entities.Rithmatics.Chalkling;
using Rithmatist.Farseer;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using Rithmatist.Animation;
using FarseerPhysics.Factories;
using FarseerPhysics.Common;

namespace Rithmatist.Farseer
{
    class Health
    {
        float max;
        List<LineDamage> totalDamage = new List<LineDamage>();
        public const float MAX_HEALTH = 50;
        public void addDamage(Vector2 attackPosition, Vector2 bodyPosition, float damage)
        {
            Vector2 localPosition = bodyPosition - attackPosition;
            totalDamage.Add(new LineDamage(localPosition, 5f, damage));
        }
        public void addDamage(Vector2 attackPosition, Vector2 bodyPosition, float damage, float range)
        {
            Vector2 localPosition = bodyPosition - attackPosition;
            totalDamage.Add(new LineDamage(localPosition, range, damage));
        }
        public float getHealth(Vector2 position, Vector2 bodyPosition)
        {
            Vector2 localPosition = bodyPosition - position;
            float health = MAX_HEALTH;
            foreach (LineDamage damage in totalDamage)
            {
                health -= Math.Max(0, damage.getDamage(localPosition));
            }
            return health;
        }
        public float getPerc(Vector2 position, Vector2 bodyPosition, float max)
        {
            return getHealth(position, bodyPosition) / MAX_HEALTH * max;
        }
    }
}
