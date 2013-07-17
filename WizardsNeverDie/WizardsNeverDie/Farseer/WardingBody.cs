using Microsoft.Xna.Framework;
using System.Collections.Generic;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Common.Decomposition;
using Rithmatist.Entities;
using System;


namespace Rithmatist.Farseer
{
    class WardingBody : RithmaticBody
    {
        Vertices points;
        float SIZE;
        public Health health;
        public WardingBody(AbstractEntity entity, Vertices points, float SIZE)
        {
            this.points = points;
            this.entity = entity;
            this.SIZE = SIZE;
            for (int x = 0; x < points.Count - 1; x++)
            {
                circumferenceSquared += Vector2.DistanceSquared(points[x], points[x + 1]);
                circumference += Vector2.Distance(points[x], points[x + 1]);
            }
            circumferenceSquared += Vector2.DistanceSquared(points[points.Count - 1], points[0]);
            circumference += Vector2.Distance(points[points.Count - 1], points[0]);
            health = new Health();
        }
        public override void createBody()
        { 
            Vertices verticies = new Vertices(points);
            List<Vertices> polygons = EarclipDecomposer.ConvexPartition(points);
            Body body = BodyFactory.CreateCompoundPolygon(Physics.Instance.World, polygons, 1f);
            Bodies.Add(body);
            setBodyValues();
            
        }
        float circumference = 0;
        float circumferenceSquared = 0;
        public int getGraphicsCount()
        {
            return (int)(circumference / SIZE);
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
                else
                    c += Vector2.DistanceSquared(points[x], points[0]);
                float circlePerc = c / circumferenceSquared;
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
                if(x + 1 < points.Count)
                    c += Vector2.DistanceSquared(points[x], points[x + 1]);
                else
                    c += Vector2.DistanceSquared(points[x], points[0]);
                float circlePerc = c / circumferenceSquared;
                float drawPerc = index / (float)drawPoints;
                float initial = drawPerc;
                while (circlePerc > drawPerc)
                {
                    float progress = (drawPerc - initial) / (circlePerc - initial);

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
                float h = health.getPerc(positions[index], Position, 1);
                if (h > 0 && h < .3f)
                    h = .3f;
                array[index] = h;
            }
            return array;
        }
    }
}
