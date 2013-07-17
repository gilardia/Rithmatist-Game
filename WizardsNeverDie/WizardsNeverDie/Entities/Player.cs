using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using FarseerPhysics.DebugViews;
using Rithmatist.Farseer;
using Rithmatist.Level;
using Rithmatist.Animation;
using Rithmatist.Intelligence;
using System.IO;
using Rithmatist.Entities.Rithmatics;
using Rithmatist.Farseer;
using FarseerPhysics.Dynamics;

namespace Rithmatist.Entities
{
    public class Player : AbstractEntity
    {
        public ConstructionQueue queue;
        float rangeSquared = 1f;
        float SPEED = 10f;
        Vector2 initialPosition;
        public Player(Vector2 position)
        {
            this.initialPosition = position;
            queue = new ConstructionQueue(position);
        }
        public void LoadContent(ContentManager content)
        {
            Texture2D texture = content.Load<Texture2D>("Sprites/Sprite");
            this.body = BasicBody.CreateRectangleBody(this, initialPosition, Utility.ConvertUnits.ToSimUnits(texture.Height), Utility.ConvertUnits.ToSimUnits(texture.Width), 0f);
            sprite = new Sprite(texture, body.Position, 0f);
            foreach (Body part in body.Bodies)
            {
                part.AngularDamping = float.MaxValue;
                body.setCollision(Farseer.Physics.CollisionGroup.None, Farseer.Physics.CollisionGroup.None);
            }

        }
        public override void Update(GameTime gameTime)
        {
            body.Update(gameTime);
            (sprite as Sprite).Position = body.Position;
            updateQueue(gameTime);
        }
        public void updateQueue(GameTime gameTime)
        {
            Vector2 location = queue.getNextLocation();
            Vector2 travel = location - body.Position;
            if (travel.LengthSquared() < rangeSquared)
            {
                queue.construct(gameTime.ElapsedGameTime);
                body.Move(Vector2.Zero);
            }
            else
            {
                travel.Normalize();
                travel *= SPEED;
                body.Move(travel);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}