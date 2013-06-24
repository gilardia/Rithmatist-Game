using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Rithmatist.Animation;
using Rithmatist.Level;
using Rithmatist.Farseer;
using Rithmatist.Entities;
namespace Rithmatist.Intelligence
{
    public class CreatureIntelligence : AbstractIntelligence
    {
        public AbstractCreature creature, target;
        float speed;
        protected Orientation lastOrientation;
        protected TimeSpan swapTimer;
        protected TimeSpan swapCooldown = TimeSpan.FromSeconds(1d);
        public CreatureIntelligence(AbstractCreature creature, AbstractCreature target, float speed)
        {
            this.creature = creature;
            this.target = target;
            this.speed = speed;
        }

        public bool canSwap(Orientation current)
        {
            bool canSwap = true;
            if(swapTimer < swapCooldown)
            {
                switch (current)
                {
                    case Orientation.Down: if (lastOrientation == Orientation.DownLeft || lastOrientation == Orientation.DownRight) canSwap = false; break;
                    case Orientation.Up: if (lastOrientation == Orientation.UpLeft || lastOrientation == Orientation.UpRight) canSwap = false; break;
                    case Orientation.Left: if (lastOrientation == Orientation.DownLeft || lastOrientation == Orientation.UpLeft) canSwap = false; break;
                    case Orientation.Right: if (lastOrientation == Orientation.DownRight || lastOrientation == Orientation.UpRight) canSwap = false; break;
                    case Orientation.DownLeft: if (lastOrientation == Orientation.Left || lastOrientation == Orientation.Down) canSwap = false; break;
                    case Orientation.DownRight: if (lastOrientation == Orientation.Down || lastOrientation == Orientation.Right) canSwap = false; break;
                    case Orientation.UpLeft: if (lastOrientation == Orientation.Left || lastOrientation == Orientation.Up) canSwap = false; break;
                    case Orientation.UpRight: if (lastOrientation == Orientation.Up || lastOrientation == Orientation.Right) canSwap = false; break;
                    default: break;
                }
            }
            return canSwap;
        }
        public void swapWalkOrientation(Orientation current)
        {
            SpriteAnimation animation = (SpriteAnimation)creature.Sprite;
            if (current == Orientation.None)
                animation.SetAnimationState(AnimationState.Stop);
            else if (canSwap(current))
            {
                animation.SetAnimationState(AnimationState.Walk);
                animation.SetOrientation(current);
                lastOrientation = current;
                swapTimer = TimeSpan.Zero;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (target == null)
                return;

            Vector2 direction = Vector2.Subtract(target.Position, creature.Position);
            direction.Normalize();
            double angle = - Math.Atan2(direction.Y, direction.X);
            SpriteAnimation sa = (SpriteAnimation) creature.Sprite;
            PhysicsBody body = creature.getBody();
            if (angle > -Math.PI / 8 && angle < Math.PI / 8) // Right
            {
                swapWalkOrientation(Orientation.Right);
                body.Move(new Vector2(speed,0));
            }
            else if (angle >  Math.PI / 8 && angle < 3 * Math.PI / 8) // Up Right
            {
                swapWalkOrientation(Orientation.UpRight);
                body.Move(new Vector2(speed, -speed));
            }
            else if (angle > 3 * Math.PI / 8 && angle < 5 * Math.PI / 8) // UP
            {
                swapWalkOrientation(Orientation.Up);
                body.Move(new Vector2(0, -speed));
            }
            else if (angle > 5 * Math.PI / 8 && angle < 7 * Math.PI / 8) // Up Left
            {
                swapWalkOrientation(Orientation.UpLeft);
                body.Move(new Vector2(-speed, -speed));
            }
            else if (angle > 7 * Math.PI / 8 || angle < -7 * Math.PI / 8) // Left
            {
                swapWalkOrientation(Orientation.Left);
                body.Move(new Vector2(-speed, 0));
            }
            else if (angle > -7 *  Math.PI / 8 && angle < -5 * Math.PI / 8) // Down Left
            {
                swapWalkOrientation(Orientation.DownLeft);
                body.Move(new Vector2(-speed, speed));
            }
            else if (angle > -5 * Math.PI / 8 && angle < -3 * Math.PI / 8) // Down
            {
                swapWalkOrientation(Orientation.Down);
                body.Move(new Vector2(0, speed));
            }
            else if (angle > -3 * Math.PI / 8 && angle < -Math.PI / 8) // Down Right
            {
                swapWalkOrientation(Orientation.DownRight);
                body.Move(new Vector2(speed, speed));
            }
            else
            {
                sa.SetAnimationState(AnimationState.Stop);
                body.Stop();
                
            }
        }
    }
}
