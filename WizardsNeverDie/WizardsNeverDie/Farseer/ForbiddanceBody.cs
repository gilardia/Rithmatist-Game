using Microsoft.Xna.Framework;
using System.Collections.Generic;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Common.Decomposition;
using Rithmatist.Entities;
using System;
using Rithmatist.Entities.Rithmatics;

namespace Rithmatist.Farseer
{
  class ForbiddanceBody : RithmaticBody
    {
        List<Vector2> points;
        float SIZE;
        public ForbiddanceBody(AbstractEntity entity, List<Vector2> points, float SIZE)
        {
            this.points = points;
            this.entity = entity;
            this.SIZE = SIZE;
            for (int x = 0; x < points.Count - 1; x++)
            {
                lengthSquared += Vector2.DistanceSquared(points[x], points[x + 1]);
                length += Vector2.Distance(points[x], points[x + 1]);
            }

        }
        public override void createBody()
        {
            Vertices verticies = pointsToLine(points);
            List<Vertices> polygons = EarclipDecomposer.ConvexPartition(verticies);
            Body body = BodyFactory.CreateCompoundPolygon(Physics.Instance.World, polygons, 1f);
          //  Body body = BodyFactory.CreatePolygon(Physics.Instance.World, verticies, 1f);
            Bodies.Add(body);
            setBodyValues();

        }
        private Vertices pointsToLine(List<Vector2> points)
        {
            Vertices path = new Vertices();
            Vector2 direction = RithmaticLine.getDirection(points);
            Vector2 perpendicular = new Vector2(-direction.Y, direction.X);
            perpendicular.Normalize();
            for (int x = 0; x < points.Count; x++)
            {
                path.Add(points[x] + perpendicular * SIZE / 2);
            }
            for (int x = points.Count - 1; x >= 0; x--)
            {
                path.Add(points[x] - perpendicular * SIZE / 2);
            }
            return path;
        }
        float length = 0;
        float lengthSquared = 0;
        public int getGraphicsCount()
        {
            return ((int)(length / SIZE));
        }
        public float[] getGraphicRotation()
        {
            int drawPoints = getGraphicsCount();
            float[] array = new float[drawPoints];
            int index = 0;
            float c = 0;
            for (int x = 0; x < points.Count; x++)
            {
                if (x + 1 < points.Count)
                    c += Vector2.DistanceSquared(points[x], points[x + 1]);
             //   else
               //     c += Vector2.DistanceSquared(points[x], points[0]);
                float circlePerc = c / lengthSquared;
                while (circlePerc > (float)index / (float)drawPoints)
                {
                    if (x + 1 < points.Count)
                    {
                        Vector2 angle = points[x + 1] - points[x];
                        array[index] = (float)Math.Atan2(angle.Y, angle.X);
                        index++;
                    }
                    else
                    {
                        Vector2 angle = points[0] - points[x];
                        array[index] = (float)Math.Atan2(angle.Y, angle.X);
                        index++;
                    }
                }
            }
            return array;
        }
        public Vector2[] getGraphicPosition()
        {
            int drawPoints = getGraphicsCount();
            Vector2[] array = new Vector2[drawPoints];
            int index = 0;
            float c = 0;
            for (int x = 0; x < points.Count; x++)
            {
                if (x + 1 < points.Count)
                    c += Vector2.DistanceSquared(points[x], points[x + 1]);
             //   else
               //     c += Vector2.DistanceSquared(points[x], points[0]);
                float linePerc = c / lengthSquared;
                float drawPerc = index / (float)drawPoints;
                float initial = drawPerc;
                while (linePerc > drawPerc)
                {
                    float progress = (drawPerc - initial) / (linePerc - initial);

                    if (x + 1 < points.Count)
                    {
                        Vector2 angle = points[1 + x] - points[x];
                        array[index] = points[x] + angle * progress;
                        index++;
                        drawPerc = index / (float)drawPoints;
                    }
                    else
                    {
                        Vector2 angle = points[0] - points[x];
                        array[index] = points[x] + angle * progress;
                        index++;
                        drawPerc = index / (float)drawPoints;
                    }
                }
            }
            return array;
        }
        public float[] getColorAlpha(Vector2[] positions)
        {
            float[] array = new float[getGraphicsCount()];
            for (int index = 0; index < getGraphicsCount(); index++)
            {
                array[index] = 1f;
            }
            return array;
        }
      }
}
