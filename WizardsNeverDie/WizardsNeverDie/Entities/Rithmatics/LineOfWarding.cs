using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using Rithmatist.Entities.Rithmatics.Chalkling;
using Rithmatist.Farseer;
using FarseerPhysics.Common.Decomposition;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Collision.Shapes;
using Rithmatist.Animation;
using FarseerPhysics.Factories;
using FarseerPhysics.Common;

namespace Rithmatist.Entities.Rithmatics
{
    class LineOfWarding: RithmaticLine
    {
        //Size should be between .05 and .2
        public float SIZE = .1f;
        List<LineDamage> totalDamage = new List<LineDamage>();
        public const float MAX_HEALTH = 50;
        public float length;
        Vertices verts = new Vertices();
        public LineOfWarding(Animation.DrawingSystem.AssetCreator assetCreator, Vector2 position, float radius)
        {
            for (float i = 0; i < 2 * Math.PI; i+= SIZE)
            {
                float x = radius * (float)Math.Cos(i) + position.X;
                float y = radius * (float)Math.Sin(i) + position.Y;
                verts.Add(new Vector2(x,y));
            }
            this.length = 0;
            for (int i = 0; i < verts.Count - 1; i++)
                this.length += Vector2.Distance(verts[i], verts[i + 1]);
            body = new WardingBody(this, verts, SIZE);
            initializeDraw(assetCreator);
        }
        public LineOfWarding(Animation.DrawingSystem.AssetCreator assetCreator, List<Vector2> points)
        {
            this.length = 0;
            for (int i = 0; i < points.Count - 1; i++)
                this.length += Vector2.Distance(points[i], points[i + 1]);
            verts.AddRange(points);
            body = new WardingBody(this, verts, SIZE);
            initializeDraw(assetCreator);
        }
        public void initializeDraw(Animation.DrawingSystem.AssetCreator assetCreator)
        {
            Vertices box = PolygonTools.CreateRectangle(SIZE, SIZE);
            PolygonShape shape = new PolygonShape(box, 20);
            line = new Line(assetCreator.TextureFromVertices(shape.Vertices, Animation.DrawingSystem.MaterialType.Blank, Color.WhiteSmoke, 1f));
            WardingBody wBody = body as WardingBody;
            line.count =  wBody.getGraphicsCount();
            line.getPosition += wBody.getGraphicPosition;
            line.getRotation += wBody.getGraphicRotation;
            line.getColorAlpha += wBody.getColorAlpha;
        }
        public override void createBody()
        {
            (body as WardingBody).createBody();
            body.setCollision(Physics.CollisionGroup.LineOfWarding, Physics.CollisionGroup.LineOfVigor | Physics.CollisionGroup.LineOfMaking);
            foreach (Body b in body.Bodies)
                b.BodyType = BodyType.Static;
        }
        public void addDamage(Vector2 position, float damage)
        {
            Vector2 localPosition = position - body.Position;
            totalDamage.Add(new LineDamage(localPosition, 5f, damage));
        }
        public float getHealth(Vector2 position)
        {
            Vector2 localPosition = position - body.Position;
            float health = MAX_HEALTH;
            foreach (LineDamage damage in totalDamage)
                health -= damage.getDamage(localPosition);
            return health;
        }
        public override void Dispose()
        {
            body.Dispose();
            body = null;
            line.secondaryColor.A = 0;
            line = null;
        }
        public override void Update(GameTime gameTime)
        {
            if (body != null)
                body.Update(gameTime);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (line != null)
                line.Draw(spriteBatch);
        }
    }
}
