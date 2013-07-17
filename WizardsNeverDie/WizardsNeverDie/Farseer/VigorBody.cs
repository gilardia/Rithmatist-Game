using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using Rithmatist.Entities.Rithmatics;
using FarseerPhysics.Factories;
using Rithmatist.Entities;

namespace Rithmatist.Farseer
{
    class VigorBody : RithmaticBody
    {
        private List<AbstractEntity> collided;
        private Path totalPath;
        private Path repeatingPath;
        private float originalLength;
        private float originalTimer;
        float SPEED = 1/3f;
        int count;
        private float WIDTH;
        private float HEIGHT;
        public VigorBody(AbstractEntity entity, Vector2 intendedAngle, List<Vector2> points, float SIZE)
            : base()
        {
            HEIGHT = SIZE;
            WIDTH = SIZE;
            this.entity = entity;
            collided = new List<AbstractEntity>();
            totalPath = new Path(points);
            originalLength = totalPath.ControlPoints.Count;
            repeatingPath = new Path();
            totalPath.Closed = false;
            extractRepeatingPath(intendedAngle);
            timer = originalTimer = originalLength / (totalPath.ControlPoints.Count - 1);
            count = getGraphicsCount();
        }
        public override void createBody()
        {
            Vertices box = PolygonTools.CreateRectangle(WIDTH, HEIGHT);
            PolygonShape shape = new PolygonShape(box, 20);
            Bodies.Clear();
            Bodies.AddRange(PathManager.EvenlyDistributeShapesAlongPath(Physics.Instance.World, totalPath, shape,
                                                           BodyType.Dynamic, 10));
            setBodyValues();
        }
        private void extractRepeatingPath(Vector2 intendedAngle)
        {
            //Note, this should actually be pretty fast because the average case is close to the best case
            for (int startPoint = 0; startPoint < totalPath.ControlPoints.Count() / 2; startPoint++)
            {
                for (int endPoint = totalPath.ControlPoints.Count() - 1; endPoint > totalPath.ControlPoints.Count() / 2; endPoint--)
                {
                    //We need to figure out if the line segment of line2 intersects with line2

                    if (ProcessIntersection(totalPath.ControlPoints[endPoint - 1], totalPath.ControlPoints[endPoint], totalPath.ControlPoints[startPoint], totalPath.ControlPoints[startPoint] + intendedAngle))
                    {
                        //Confirm that the two line segments are going in the same relative direction
                        Vector2 angle1 = totalPath.ControlPoints[endPoint - 1] - totalPath.ControlPoints[endPoint];
                        Vector2 angle2 = totalPath.ControlPoints[startPoint] - totalPath.ControlPoints[startPoint + 1];
                        angle1.Normalize();
                        angle2.Normalize();
                        float dot = Vector2.Dot(angle1, angle2);
                        float angle = (float)Math.Acos(dot);
                        if (angle < Math.PI / 8)
                        {
                            repeatingPath.ControlPoints.Clear();
                            for (int x = startPoint; x < endPoint; x++)
                                repeatingPath.Add(new Vector2(totalPath.ControlPoints[x].X, totalPath.ControlPoints[x].Y));

                            totalPath.ControlPoints.RemoveRange(endPoint, totalPath.ControlPoints.Count() - endPoint);


                            //Zero the repeating path
                            Vector2 opposingAngle = Vector2.Negate(intendedAngle);
                            Vector2 zeroing = -repeatingPath.ControlPoints[0];
                            repeatingPath.Translate(ref zeroing);

                            extendPath();

                            //break outer loop
                            endPoint = int.MinValue + 1;
                            startPoint = int.MaxValue - 1;
                            break;
                        }
                    }
                }
            }

        }
        private bool ProcessIntersection(Vector2 point1, Vector2 point2, Vector2 point3, Vector2 point4)
        {
            //Note, method taken from the internet
            Vector2 intersectionPoint;
            float ua = (point4.X - point3.X) * (point1.Y - point3.Y) - (point4.Y - point3.Y) * (point1.X - point3.X);
            float ub = (point2.X - point1.X) * (point1.Y - point3.Y) - (point2.Y - point1.Y) * (point1.X - point3.X);
            float denominator = (point4.Y - point3.Y) * (point2.X - point1.X) - (point4.X - point3.X) * (point2.Y - point1.Y);

            if (Math.Abs(denominator) <= 0.00001f)
            {
                if (Math.Abs(ua) <= 0.00001f && Math.Abs(ub) <= 0.00001f)
                {
                    intersectionPoint = (point1 + point2) / 2;
                    return true;
                }
            }
            else
            {
                ua /= denominator;
                ub /= denominator;

                if (ua >= 0 && ua <= 1 && ub >= 0 && ub <= 1)
                {
                    intersectionPoint.X = point1.X + ua * (point2.X - point1.X);
                    intersectionPoint.Y = point1.Y + ua * (point2.Y - point1.Y);
                    return true;
                }
            }
            return false;
        }
        private void extendPath()
        {
            Vector2 endPoint = totalPath.ControlPoints[totalPath.ControlPoints.Count - 1];
            foreach (Vector2 repeatPoint in repeatingPath.ControlPoints)
            {
                Vector2 tempPoint = repeatPoint;
                Vector2 newPoint;
                Vector2.Add(ref endPoint, ref tempPoint, out newPoint);
                totalPath.Add(newPoint);
            }
            timer = totalPath.ControlPoints.Count / (totalPath.ControlPoints.Count - 1f);
            originalTimer = originalLength / (totalPath.ControlPoints.Count + repeatingPath.ControlPoints.Count - 1f); 
            totalPath.RefreshDelta();
        }
        private void rotatePath(float radians)
        {
            repeatingPath.Rotate(radians);
        }
        private Vector2 pathVector()
        {
            return repeatingPath.ControlPoints[repeatingPath.ControlPoints.Count - 1] - repeatingPath.ControlPoints[0];
        }
        public int getGraphicsCount()
        {           
            float pathLength = totalPath.ControlPoints.Count / (totalPath.ControlPoints.Count - 1);
            return (int)((originalTimer / pathLength) * totalPath.GetLength() / HEIGHT);
        }
        public float[] getGraphicRotation()
        {
            float[] array = new float[count];
            for (int index = 0; index < count; index++)
            {
                float wavePosition = timer - originalTimer * ((float)index / (float)count); ;
                Vector2 normal = totalPath.GetPositionNormal(wavePosition);
                array[index] = (float)Math.Atan2(normal.Y, normal.X);
            }
            return array;
        }
        public Vector2[] getGraphicPosition()
        {
            Vector2[] array = new Vector2[count];
            for (int index = 0; index < count; index++)
            {
                float wavePosition = timer - originalTimer * ((float)index / (float)count); ;
                array[index] = totalPath.GetPosition(wavePosition);
            }
            return array;
        }
        public float[] getColorAlpha(Vector2[] positions)
        {
            float[] array = new float[count];
            for (int index = 0; index < count; index++)
            {
                array[index] = 1f;
            }
            return array;
        }
        float timer = 0;
        public override void Update(GameTime gameTime)
        {
            //Progress the bodies along the total path
            //When they reach the end of the path extend the path
            //After a long enough period of time delete this object.
            float temp1 = (totalPath.ControlPoints.Count / (totalPath.ControlPoints.Count - 1f));
            float temp2 = temp1 / totalPath.ControlPoints.Count * SPEED;
            timer += temp2;
            if (isEndOfPath())
            {
                extendPath();
            }
            for (int x = 0; x < Bodies.Count; x++)
            {
                PathManager.MoveBodyOnPath(totalPath, Bodies[x], timer - (originalTimer * x / Bodies.Count), 1f, 1f / 60f);
            }
        }

        private bool isEndOfPath()
        {
            return (timer > totalPath.ControlPoints.Count/(totalPath.ControlPoints.Count - 1));
        }
        private void setBodyValues()
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
        protected override bool onCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
          
            AbstractEntity collidedEntity = fixtureB.UserData as AbstractEntity;
            if (collidedEntity as LineOfWarding != null)
            {
                Vector2 normal;
                FixedArray2<Vector2> points;
                contact.GetWorldManifold(out normal, out points);
                Vector2 contactPoint = points[0];
                LineOfWarding warding = collidedEntity as LineOfWarding;
                WardingBody wardingBody = warding.getBody() as WardingBody;
                if (wardingBody.health.getHealth(contactPoint, collidedEntity.Position) > 0)
                {
                    Rithmatist.Animation.ParticleSystem.ParticleFactory.Instance.CreateChalkDust(3, contactPoint, Color.Black);
                    wardingBody.health.addDamage(contactPoint, collidedEntity.Position, 25f, 1f);
                    (entity as RithmaticLine).Dispose();
                }
                else
                    return false;
            }
            if (collidedEntity as LineOfForbiddance != null && !collided.Contains(collidedEntity))
            {
                collided.Add(collidedEntity);

                Vector2 position = fixtureA.Body.Position;
                int lowestDistanceNumber = getClosestPathPoint(ref position);
                totalPath.ControlPoints.RemoveRange(lowestDistanceNumber, totalPath.ControlPoints.Count - lowestDistanceNumber);
                
                Physics.Instance.World.RayCast(RayCallBack, totalPath.ControlPoints[totalPath.ControlPoints.Count - 1],  pathVector());
                if (rotationCallback != null && rotationCallback.IsValid())
                {
                    Vector2 pathvector = pathVector();
                    pathvector.Normalize();
                    rotationCallback.Normalize();
                    float dot = Vector2.Dot(pathvector, rotationCallback);
                    rotatePath((float)Math.Acos(dot));
                    extendPath();
                }
            }
            return entity.WillCollide(collidedEntity);
        }

        private int getClosestPathPoint(ref Vector2 position)
        {
            float lowestDistanceSquared = float.MaxValue;
            int lowestDistanceNumber = 0;
            for (int pathIndex = totalPath.ControlPoints.Count - 1; pathIndex > totalPath.ControlPoints.Count - repeatingPath.ControlPoints.Count; pathIndex--)
            {
                float distanceSquared = (float)(Math.Pow(totalPath.ControlPoints[pathIndex].X - position.X, 2) + Math.Pow(totalPath.ControlPoints[pathIndex].Y - position.Y, 2));
                if (distanceSquared < lowestDistanceSquared)
                {
                    lowestDistanceSquared = distanceSquared;
                    lowestDistanceNumber = pathIndex;
                }
            }
            return lowestDistanceNumber;
        }
        static Vector2 rotationCallback = Vector2.Zero;

        private static float RayCallBack(Fixture fixture, Vector2 point, Vector2 normal, float fraction)
        {
            if (fixture.UserData as LineOfForbiddance != null)
            {
                rotationCallback = normal;
                return fraction;
            }
            else
            {
                return -1f;
            }
        }
    }
}
