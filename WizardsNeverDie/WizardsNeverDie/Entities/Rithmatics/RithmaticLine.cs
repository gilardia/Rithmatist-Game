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
using FarseerPhysics.Common;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using Rithmatist.Animation;
using Rithmatist.Entities.Rithmatics;
using FarseerPhysics.Factories;
using Rithmatist.Entities;

namespace Rithmatist.Entities.Rithmatics
{
    public abstract class RithmaticLine : AbstractEntity
    {
        public delegate void _onCollision(AbstractEntity collidedWith);
        public delegate void _onDestruction();
        public _onCollision onCollision;
        public _onDestruction onDestruction;
        public abstract void createBody();
        public virtual void Dispose(){}
        public Line line;
        public static Vector2 getDirection(List<Vector2> points)
        {
            float n = points.Count;
            float sumX = 0;
            float sumXsquared = 0;
            float sumY = 0;
            float sumYsquared = 0;
            foreach (Vector2 point in points)
            {
                sumX += point.X;
                sumXsquared += point.X * point.X;
                sumY += point.Y;
                sumYsquared += point.Y * point.Y;
            }

            float b = (n * (sumX + sumY) - sumX * sumY) / (n * sumX - sumX * sumX);
            return new Vector2(1, b);
        }
        public override void OnCollision(AbstractEntity collidedWith)
        {
            if(onCollision != null)
                onCollision(collidedWith);
            base.OnCollision(collidedWith);
        }
    }
}
