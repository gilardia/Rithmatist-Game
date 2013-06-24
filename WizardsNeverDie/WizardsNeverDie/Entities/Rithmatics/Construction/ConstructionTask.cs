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

namespace Rithmatist.Entities.Rithmatics
{
    public class ConstructionTask
    {
        public Vector2 destination;
        public RithmaticLine entity;
        public TimeSpan elapsedTime;
        public TimeSpan totalTime = TimeSpan.Zero;
        public Boolean constructed = false;

        public ConstructionTask(Vector2 destination, RithmaticLine entity, TimeSpan time)
        {
            this.destination = destination;
            this.entity = entity;
            this.totalTime = time;
        }
        public void construct(TimeSpan elapsedTime)
        {
            this.elapsedTime = this.elapsedTime.Add(elapsedTime);
            if (isCompleted() && constructed == false)
            {
                entity.createBody();
            }
        }
        public float getProgress()
        {
            if (totalTime.TotalMilliseconds == 0)
                return 100;
            return (float)(elapsedTime.TotalMilliseconds / totalTime.TotalMilliseconds * 100f);
        }
        public Boolean isCompleted()
        {
            return elapsedTime > totalTime;
        }
    }
}
