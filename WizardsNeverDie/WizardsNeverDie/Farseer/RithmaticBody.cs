using Microsoft.Xna.Framework;
using System.Collections.Generic;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Factories;
using FarseerPhysics.Common.Decomposition;
using Rithmatist.Entities;
using Rithmatist.Entities.Rithmatics;

namespace Rithmatist.Farseer
{
    public abstract class RithmaticBody: BasicBody
    {
        public bool bodyIsCreated = false;
        public abstract void createBody();
        public override void Dispose()
        {
            if (entity as RithmaticLine != null && (entity as RithmaticLine).onDestruction != null)
                (entity as RithmaticLine).onDestruction();
            base.Dispose();
        }
    }
}
