using Microsoft.Xna.Framework;
using Rithmatist.Farseer;
using Rithmatist.Animation;
using Rithmatist.Intelligence;
using Rithmatist.Entities.Rithmatics.Chalkling;

namespace Rithmatist.Entities.Rithmatics
{
    class LineOfMaking : RithmaticLine
    {
        ChalklingIntelligence intelligence;
        public LineOfMaking(ChalklingInstructions instructions, ChalklingStats stats, Vector2 position)
        {
            //This does not have a spritemanager
            this.body = BasicBody.CreateCircleBody(this, position, 1f);
            this.intelligence = new ChalklingIntelligence(this, instructions, stats);
        }
        public override void createBody()
        {
            throw new System.NotImplementedException();
        }
        public override void Update(GameTime gameTime)
        {
            intelligence.Update(gameTime);
            sprite.Update(gameTime);
        }
    }
}
