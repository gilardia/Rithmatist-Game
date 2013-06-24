using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rithmatist.Entities.Rithmatics.Chalkling;
using Rithmatist.Entities;
using Rithmatist.Farseer;
namespace Rithmatist.Intelligence
{
    class ChalklingIntelligence : AbstractIntelligence
    {
        private const float SPEED = 10;
        private const float TURNSPEED = 10;
        AbstractEntity chalkling;
        ChalklingInstructions instructions;
        ChalklingStats stats;
        TimeSpan elapsedTime;
        int instructionNumber;

        public ChalklingIntelligence(AbstractEntity chalkling, ChalklingInstructions instructions, ChalklingStats stats)
        {
            this.chalkling = chalkling;
            this.instructions = instructions;
            this.stats = stats;
            this.elapsedTime = TimeSpan.Zero;
            instructionNumber = 0;
        }
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            ChalklingInstructions.Instruction instruction = instructions.instructions[instructionNumber];
            switch (instruction.order)
            {
                case ChalklingInstructions.Order.ATTACK: AttackOrder(); break;
                case ChalklingInstructions.Order.GUARD: GuardOrder(); break;
                case ChalklingInstructions.Order.MOVE: MoveOrder(); break;
                case ChalklingInstructions.Order.TURNLEFT: TurnLeftOrder(); break;
                case ChalklingInstructions.Order.TURNRIGHT: TurnRightOrder(); break;
                case ChalklingInstructions.Order.WAIT: WaitOrder(); break;
            }
            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime.Seconds > instruction.duration)
            {
                instructionNumber += 1;
                elapsedTime = TimeSpan.Zero;

            }
        }
        //Attack has a higher range
        public void AttackOrder() { }
        //Guard returns to the basic position
        public void GuardOrder() { }
        public void MoveOrder() {
            PhysicsBody body = chalkling.getBody();
            body.Move(body.Rotation, stats.Speed * SPEED);
        }
        public void TurnLeftOrder() {
            PhysicsBody body = chalkling.getBody();
            body.Rotation -= TURNSPEED;
        }
        public void TurnRightOrder()
        {
            PhysicsBody body = chalkling.getBody();
            body.Rotation += TURNSPEED;
        }
        public void WaitOrder() { }
    }
}
