using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;

namespace Rithmatist.Level
{
    interface ILevelGenerator
    {
        Level Generate(World world);
    }
}
