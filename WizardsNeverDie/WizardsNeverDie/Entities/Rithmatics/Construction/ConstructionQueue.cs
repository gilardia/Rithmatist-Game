using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Rithmatist.Animation;
using Rithmatist.Level;
using Rithmatist.Farseer;

namespace Rithmatist.Entities.Rithmatics
{
    public class ConstructionQueue
    {
        public List<ConstructionTask> tasks = new List<ConstructionTask>();
        Vector2 origin;
        public ConstructionQueue(Vector2 origin)
        {
            this.origin = origin;
        }
        public void add(Vector2 destination, RithmaticLine entity, TimeSpan time)
        {
            entity.line.Percent = 0;
            tasks.Add(new ConstructionTask(destination, entity, time));
        }
        public Vector2 getNextLocation()
        {
            if (tasks.Count != 0)
                return tasks[0].destination;
            else
                return origin;
        }
        public void construct(TimeSpan elapsedTime)
        {
            if (tasks.Count != 0)
            {
                tasks[0].construct(elapsedTime);
                if (tasks[0].entity.line != null)
                    tasks[0].entity.line.Percent = tasks[0].getProgress();
                if (tasks[0].isCompleted())
                    tasks.RemoveAt(0);
            }
        }
    }
}
