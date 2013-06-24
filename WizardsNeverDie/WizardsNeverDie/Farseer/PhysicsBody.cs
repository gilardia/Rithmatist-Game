using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Controllers;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Common;
using FarseerPhysics.Collision;
using Rithmatist.Level;

namespace Rithmatist.Farseer
{
    public abstract class PhysicsBody
    {
        private List<Body> bodies = new List<Body>();
        public PhysicsBody() 
        { 
        }
        public virtual void Dispose()
        {
            foreach (Body body in bodies)
            {
                Physics.Instance.World.RemoveBody(body);
            }
        }
        public Vector2 DisplayPosition
        {
            get { return Utility.ConvertUnits.ToDisplayUnits(Position); }
        }
        public List<Body> Bodies
        {
            get { return bodies; }
        }
        public virtual void Update(GameTime gameTime)
        { }
        public virtual Vector2 Position
        {
            get 
            {
                Vector2 position = Vector2.Zero;
                foreach (Body body in bodies)
                    position += body.Position;
                position /= bodies.Count;
                return position;
            }
            set 
            {
                Vector2 position = Vector2.Zero;
                foreach (Body body in bodies)
                    position += body.Position;
                position /= bodies.Count;
            }
        }
        public virtual Vector2 Velocity
        {
            get 
            {
                Vector2 velocity = Vector2.Zero;
                foreach (Body body in bodies)
                    velocity += body.LinearVelocity;
                velocity /= bodies.Count;
                return velocity;
            }
        }
        public virtual float Rotation
        {
            get
            {
                float rotation = 0f;
                foreach (Body body in bodies)
                    rotation += body.Rotation;
                rotation /= bodies.Count;
                return rotation;
            }
            set
            {
                foreach (Body body in bodies)
                    body.Rotation = value;
            }
        }
        public void Move(double angle, float speed)
        {
            Vector2 distance = new Vector2((float)Math.Sin(angle), (float)Math.Cos(angle)) * speed;
            foreach (Body body in bodies)
                body.LinearVelocity = distance;
        }
        public void Move(Vector2 distance)
        {
            foreach (Body body in bodies)
                body.LinearVelocity = distance;
        }
        public void Stop()
        {
            foreach (Body body in bodies)
                body.LinearVelocity = Vector2.Zero;
        }
        public void setCollision(Physics.CollisionGroup collisionCategory, Physics.CollisionGroup collidesWith)
        {
            foreach (Body body in Bodies)
            {
                body.CollisionCategories = (Category)collisionCategory;
                body.CollidesWith = (Category)collidesWith;
            }
        }
        public float GetArea()
        {
            float area = 0;
            foreach (Body body in bodies)
                foreach (Fixture fixture in body.FixtureList)
                    area += fixture.Shape.MassData.Area;
            return area;
        }
        public bool isInArea(Vector2 point)
        {
            foreach (Body body in bodies)
                foreach (Fixture fixture in body.FixtureList)
                    if (!fixture.TestPoint(ref point))
                        return false;
            return true;
        }
    }
}
