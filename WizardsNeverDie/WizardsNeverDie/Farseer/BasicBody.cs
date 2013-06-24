using Microsoft.Xna.Framework;
using System.Collections.Generic;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Factories;
using FarseerPhysics.Common.Decomposition;
using Rithmatist.Entities;

namespace Rithmatist.Farseer
{
    public class BasicBody : PhysicsBody
    {
        protected AbstractEntity entity;
        public static BasicBody CreateCircleBody(AbstractEntity entity, Vector2 position, float size)
        {
            Body body = BodyFactory.CreateCircle(Physics.Instance.World, size, 1f);
            return new BasicBody(entity, position, 0, body);
        }
        public static BasicBody CreateRectangleBody(AbstractEntity entity, Vector2 position, float height, float width, float rotation)
        {
            Body body = BodyFactory.CreateRectangle(Physics.Instance.World, width, height, 1f);
            return new BasicBody(entity, position, rotation, body);
        }
        public static BasicBody CreatePathBody(AbstractEntity entity, List<Vector2> points, float SIZE)
        {
            List<Body> _bridgeBodies;
            Path bridgePath = new Path(points);
            bridgePath.Closed = false;
            Vertices box = PolygonTools.CreateRectangle(SIZE, SIZE);
            PolygonShape shape = new PolygonShape(box, 20);
            float length = bridgePath.GetLength()/SIZE/4;
            _bridgeBodies = PathManager.EvenlyDistributeShapesAlongPath(Physics.Instance.World, bridgePath, shape,
                                                                       BodyType.Dynamic, (int)length);

            //Attach the first and last fixtures to the world
            JointFactory.CreateFixedRevoluteJoint(Physics.Instance.World, _bridgeBodies[0], new Vector2(0f, -SIZE),
                                                  _bridgeBodies[0].Position);
            JointFactory.CreateFixedRevoluteJoint(Physics.Instance.World, _bridgeBodies[_bridgeBodies.Count - 1], new Vector2(0, SIZE),
                                                  _bridgeBodies[_bridgeBodies.Count - 1].Position);

            PathManager.AttachBodiesWithRevoluteJoint(Physics.Instance.World, _bridgeBodies, new Vector2(0f, -SIZE),
                                                      new Vector2(0f, SIZE),
                                                      false, true);
            return new BasicBody(entity, _bridgeBodies);

        }
        public static BasicBody CreatePolygonBody(AbstractEntity entity, Vertices points, float SIZE)
        {
            Vertices verticies = new Vertices(points);
            List<Vertices> polygons = EarclipDecomposer.ConvexPartition(points);
            Body body = BodyFactory.CreateCompoundPolygon(Physics.Instance.World, polygons, 1f);
            List<Body> bodies = new List<Body>();
            bodies.Add(body);
            return new BasicBody(entity, bodies);

        }
        public static BasicBody CreateClosedPathBody(AbstractEntity entity, List<Vector2> points, float SIZE)
        {
            List<Body> _bridgeBodies;
            Path bridgePath = new Path(points);
            bridgePath.Closed = true;
            Vertices box = PolygonTools.CreateRectangle(SIZE, SIZE);
            PolygonShape shape = new PolygonShape(box, 20);
            float length = bridgePath.GetLength() / SIZE/4;
            _bridgeBodies = PathManager.EvenlyDistributeShapesAlongPath(Physics.Instance.World, bridgePath, shape,
                                                                       BodyType.Dynamic, (int)length);
            //Attach the first and last fixtures to the world
            JointFactory.CreateFixedRevoluteJoint(Physics.Instance.World, _bridgeBodies[0], new Vector2(0f, -SIZE),
                                                  _bridgeBodies[0].Position);
            JointFactory.CreateFixedRevoluteJoint(Physics.Instance.World, _bridgeBodies[_bridgeBodies.Count - 1], new Vector2(0, SIZE),
                                                  _bridgeBodies[_bridgeBodies.Count - 1].Position);

            PathManager.AttachBodiesWithRevoluteJoint(Physics.Instance.World, _bridgeBodies, new Vector2(0f, -SIZE),
                                                      new Vector2(0f, SIZE),
                                                      false, true);
            return new BasicBody(entity, _bridgeBodies);

        }
        protected BasicBody(AbstractEntity entity, Vector2 position, float rotation, Body body)
            : base()
        {
            Bodies.Add(body);
            this.entity = entity;
            body.Position = position;
            body.Rotation = rotation;
            setBodyValues();
        }
        protected BasicBody(AbstractEntity entity, List<Body> bodies)
            : base()
        {
            Bodies.AddRange(bodies);
            this.entity = entity;
            setBodyValues();
        }
        protected BasicBody()
            : base()
        {        }
        protected void setBodyValues()
        {
            foreach (Body body in Bodies)
            {
                foreach (Fixture fixture in body.FixtureList)
                    fixture.UserData = entity;
                body.UserData = entity;
                body.Friction = float.MaxValue;
                body.Restitution = 0.3f;
                body.BodyType = BodyType.Dynamic;
                body.OnCollision += new OnCollisionEventHandler(onCollision);
            }
        }
        protected virtual bool onCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            AbstractEntity collided = fixtureB.UserData as AbstractEntity;
            return entity.WillCollide(collided);
        }
    }
}
