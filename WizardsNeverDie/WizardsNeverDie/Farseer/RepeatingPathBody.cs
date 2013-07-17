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
/*
namespace Rithmatist.Farseer
{
    class RepeatingPathBody
    {
        public List<Body> Bodies;

        public float SPEED = 1 / 3f;

        public Path totalPath;
        public Path repeatingPath;

        private int initialLength;
        public int currentLength
        {
            get { return totalPath.ControlPoints.Count; }
        }

        float currentTimer;
        float initialTimer;

        public RepeatingPathBody(List<Vector2> points, PolygonShape shape, int bodyCount)
        {
            totalPath = new Path(points);
            repeatingPath = ExtractRepeatingPath();
            initialLength = totalPath.ControlPoints.Count;
            currentTimer = initialTimer = initialLength / (totalPath.ControlPoints.Count - 1);
            Bodies.AddRange(PathManager.EvenlyDistributeShapesAlongPath(Physics.Instance.World, totalPath, shape,
                                               BodyType.Dynamic, bodyCount));
        }

        private Path ExtractRepeatingPath()
        {
            for (int startPoint = 0; startPoint < totalPath.ControlPoints.Count() / 2; startPoint++)
            {
                for (int endPoint = totalPath.ControlPoints.Count() - 1; endPoint > totalPath.ControlPoints.Count() / 2; endPoint--)
                {
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
                            Vector2 opposingAngle = Vector2.Negate(LinearRegression());
                            Vector2 zeroing = -repeatingPath.ControlPoints[0];
                            repeatingPath.Translate(ref zeroing);

                            Extend();

                            //break outer loop
                            endPoint = int.MinValue + 1;
                            startPoint = int.MaxValue - 1;
                            break;
                        }
                    }
                }
            }
        }
        private Vector2 LinearRegression()
        {
            throw new NotImplementedException();
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
        public void Update(float time)
        {
            float timeDelta = (totalPath.ControlPoints.Count / (totalPath.ControlPoints.Count - 1f)) / totalPath.ControlPoints.Count * SPEED;
            currentTimer += timeDelta;
            if (isEndOfPath(currentTimer))
            {
                Extend();
            }
            for (int x = 0; x < Bodies.Count; x++)
            {
                PathManager.MoveBodyOnPath(totalPath, Bodies[x], currentTimer - (initialTimer * x / Bodies.Count), 1f, 1f / 60f);
            }
        }
        private int getCurrentControlPoint()
        {
            float percent = currentTimer / totalPath.ControlPoints.Count / (totalPath.ControlPoints.Count - 1);
            return (int)(percent * totalPath.ControlPoints.Count);
        }
        private void Refresh()
        {
            currentTimer = totalPath.ControlPoints.Count / (totalPath.ControlPoints.Count - 1f);
            initialTimer = initialLength / (totalPath.ControlPoints.Count + repeatingPath.ControlPoints.Count - 1f); 
        }
        private void Extend()
        {
            Vector2 endPoint = totalPath.ControlPoints[totalPath.ControlPoints.Count - 1];
            foreach (Vector2 repeatPoint in repeatingPath.ControlPoints)
            {
                Vector2 tempPoint = repeatPoint;
                Vector2 newPoint;
                Vector2.Add(ref endPoint, ref tempPoint, out newPoint);
                totalPath.Add(newPoint);
            }
            totalPath.RefreshDelta();
            Refresh();
        }
        private void RotatePath(float radians)
        {
            int currentPoint = getCurrentControlPoint();
            totalPath.ControlPoints.RemoveRange(currentPoint, totalPath.ControlPoints.Count() - currentPoint);
            repeatingPath.Rotate(radians);
            Extend();
        }
        private bool isEndOfPath(float timer)
        {
            return (timer > totalPath.ControlPoints.Count / (totalPath.ControlPoints.Count - 1));
        }
    }
}
*/