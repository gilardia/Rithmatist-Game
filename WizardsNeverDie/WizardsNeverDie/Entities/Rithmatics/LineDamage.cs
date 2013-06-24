using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Rithmatist.Entities.Rithmatics
{
    class LineDamage
    {
        //The location stuff needs work, a simple float won
        Vector2 median;
        float propagation;
        float damage;
        public LineDamage(Vector2 median, float propagation, float damage)
        {
            this.median = median;
            this.propagation = propagation;
            this.damage = damage;
        }

        //This should create a bell curve, the propagation is the standard deviation, location is 
        //This deals full damage to .5
        //half damage at 1
        //and falls off at 1.5
        public float getDamage(Vector2 location)
        {
            return damage * (float)(1/(propagation *  2.5) * Math.Pow(Math.E, -Math.Pow((Vector2.DistanceSquared(location, this.median)), 2) / 2 * Math.Pow(propagation, 2)));
        }
    }
}
