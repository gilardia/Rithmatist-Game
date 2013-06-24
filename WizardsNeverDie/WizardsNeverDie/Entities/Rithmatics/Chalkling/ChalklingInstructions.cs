using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rithmatist.Entities.Rithmatics.Chalkling
{
    class ChalklingInstructions
    {
        public class Instruction
        {
            public Instruction(Order order, int duration)
            {
                this.order = order;
                this.duration = duration;
            }
            public Order order;
            public int duration;
        }
        public enum Order
        {
            MOVE,
            ATTACK,
            GUARD,
            WAIT,
            TURNLEFT,
            TURNRIGHT
        }
        public List<Instruction> instructions;

    }

}
