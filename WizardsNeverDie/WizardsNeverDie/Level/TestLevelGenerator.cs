using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;

namespace Rithmatist.Level
{
    public class TestLevelGenerator : ILevelGenerator
    {
        public TestLevelGenerator() { }

        public Level Generate(World world)
        {
            world.Clear();
            return new Level();
        }

    }
}
