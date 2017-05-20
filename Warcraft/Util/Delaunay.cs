using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Warcraft.Util
{
    public class Delaunay
    {
        private static float convexMultiplier = 1000;

        public static List<Triangle> Triangulate(List<TPoint> vertices)
        {
            int nvertices = vertices.Count;

			if (vertices.Count < 3)
                return null;

            int trmax = nvertices * 4;

            float minX = vertices[0].x;
            float minY = vertices[0].y;
            float maxX = minX;
            float maxY = minY;

            for (int i = 0; i < nvertices; i++)
            {
                TPoint vertex = vertices[i];
                vertex.id = i;

				if (vertex.x < minX) minX = vertex.x;
				if (vertex.y < minY) minY = vertex.y;
                if (vertex.x > maxX) maxX = vertex.x;
                if (vertex.y > maxY) maxY = vertex.y;
            }

            float dx = (maxX - minX) * convexMultiplier;
            float dy = (maxY - minY) * convexMultiplier;

            float deltaMax = Math.Max(dx, dy);

            float midx = (minX + maxX) * 0.5f;
            float midy = (minY + maxY) * 0.5f;

            TPoint p1 = new TPoint(midx - 2 * deltaMax, midy - deltaMax);
			TPoint p2 = new TPoint(midx, midy + 2 * deltaMax);
			TPoint p3 = new TPoint(midx + 2 * deltaMax, midy - deltaMax);

            p1.id = nvertices;
            p2.id = nvertices + 1;
            p3.id = nvertices + 2;

            vertices.Add(p1);
            vertices.Add(p2);
            vertices.Add(p3);

            List<Triangle> triangles = new List<Triangle>();
            triangles.Add(new Triangle(vertices[nvertices], vertices[nvertices + 1], vertices[nvertices + 2]));

            for (int i = 0; i < nvertices; i++)
            {
                List<Edge> edges = new List<Edge>();
                int nTriangles = triangles.Count;

                for (int j = triangles.Count - 1; j >= 0; j--)
                {
                    Triangle curTriangle = triangles[j];
                    if (curTriangle.InCircumCircle(vertices[i]))
                    {
						edges.Add(curTriangle.e1);
						edges.Add(curTriangle.e2);
						edges.Add(curTriangle.e3);

                        triangles.RemoveAt(j);
                    }
                }

                for (int j = edges.Count - 2; j >= 0; j--)
                {
                    for (int k = edges.Count - 1; k >= j + 1; k--)
                    {
                        if (j < edges.Count && k < edges.Count && edges[j].Same(edges[k]))
                        {
                            edges.RemoveAt(j);

                            if (edges.Count > 0)
                                edges.RemoveAt(k - 1);
                        }
                    }
                }

                for (int j = 0; j < edges.Count; j++)
                {
                    int n = triangles.Count;

                    triangles.Add(new Triangle(edges[j].p1, edges[j].p2, vertices[i]));
                }
            }

            for (int i = triangles.Count - 1; i >= 0; i--)
            {
                Triangle triangle = triangles[i];
                if (triangle.p1.id > nvertices || 
                    triangle.p2.id > nvertices ||
                    triangle.p3.id > nvertices)
                {
                    triangles.RemoveAt(i);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                vertices.RemoveAt(i);
            }

            return triangles;
        }
    }

    public class Triangle 
    {
        public TPoint p1, p2, p3;
        public Edge e1, e2, e3;

        public Triangle(TPoint p1, TPoint p2, TPoint p3)
        {
            if (Helpers.IsFlatAngle(p1, p2, p3)) 
            {
                throw new Exception("Flat Triangle");
            }

            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;

			this.e1 = new Edge(p1, p2);
			this.e2 = new Edge(p2, p3);
			this.e3 = new Edge(p3, p1);
        }

        public bool IsCW()
        {
            return Helpers.CrossProduct(p1, p2, p3) < 0;
        }

        public bool IsCCW()
        {
            return Helpers.CrossProduct(p1, p2, p3) > 0;
        }

        public float[] GetSideLength()
        {
            return new float [3] { e1.Length(), e2.Length(), e3.Length() };
        }

        public TPoint GetCenter()
        {
            float x = (p1.x + p2.x + p3.x) / 3;
            float y = (p1.y + p2.y + p3.y) / 3;

            return new TPoint(x, y);
        }

        public float[] GetCircumCircle()
        {
            TPoint center = GetCircumCenter();
            float r = GetCircumRadius();

            return new float[3] { center.x, center.y, r };
        }

        public TPoint GetCircumCenter()
        {
            float D = (p1.x * (p2.y - p3.y) +
                       p2.x * (p3.y - p1.y) +
                       p3.x * (p1.y - p2.y)) * 2;

            float x = ((p1.x * p1.x + p1.y * p1.y) * (p2.y - p3.y) +
                       (p2.x * p2.x + p2.y * p2.y) * (p3.y - p1.y) +
                       (p3.x * p3.x + p3.y * p3.y) * (p1.y - p2.y));

            float y = ((p1.x * p1.x + p1.y * p1.y) * (p3.x - p2.x) +
                       (p2.x * p2.x + p2.y * p2.y) * (p1.x - p3.x) +
                       (p3.x * p3.x + p3.y * p3.y) * (p2.x - p1.x));

            return new TPoint(x / D, y / D);
        }

        public float GetCircumRadius()
        {
            float[] sides = GetSideLength();
            return (sides[0] * sides[1] * sides[2]) / Helpers.QuartCross(sides);
        }

        public float GetArea()
        {
            float[] sides = GetSideLength();
            return Helpers.QuartCross(sides) / 4;
        }

        public bool InCircumCircle(TPoint p)
        {
            float[] circle = GetCircumCircle();
            return p.IsInCircle(circle[0], circle[1], circle[2]);
        }
	}

    public class Edge
    {
        public TPoint p1, p2;

        public Edge(TPoint p1, TPoint p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        public bool Same(Edge edge)
        {
            return (p1 == edge.p1 && p2 == edge.p2) ||
                   (p1 == edge.p2 && p2 == edge.p1);
        }

        public float Length()
        {
            return p1.Distance(p2);
        }

        public TPoint GetMidPoint()
        {
            float dx = p1.x + (p2.x - p1.x) / 2;
            float dy = p1.y + (p2.y - p1.y) / 2;
            return new TPoint(dx, dy);
        }
    }

    public class TPoint 
    {
        public int id;
        public float x, y;
        public float value;

        public TPoint(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 GetPosition()
        {
            return new Vector2(x, y);
        }

        public float Distance2(TPoint p)
        {
            float dx = (x - p.x);
            float dy = (y - p.y);

            return dx * dx + dy + dy;
        }

        public float Distance(TPoint p)
        {
            return (float)Math.Sqrt(Distance2(p));
        }

        public bool IsInCircle(float cx, float cy, float r)
        {
            float dx = cx - x;
            float dy = cy - y;
            return (dx * dx + dy * dy) <= (r * r);
        }

        public override bool Equals(object point)
        {
            TPoint p = point as TPoint;
            return p.x == x && p.y == y;
        }
    }

    public class Helpers
	{
		public static float QuartCross(float[] sides)
		{
            return QuartCross(sides[0], sides[1], sides[2]);
		}

        public static float QuartCross(float a, float b, float c)
        {
            float p = (a + b + c) * (a + b - c) * (a - b + c) * (-a + b + c);
            return (float)Math.Sqrt(p);
        }

        public static float CrossProduct(TPoint p1, TPoint p2, TPoint p3)
        {
            float x1 = p2.x - p1.x;
            float x2 = p3.x - p2.x;
            float y1 = p2.y - p1.y;
            float y2 = p3.y - p2.y;

            return x1 * y2 - y1 * x2;
        }

        public static bool IsFlatAngle(TPoint p1, TPoint p2, TPoint p3)
        {
            return CrossProduct(p1, p2, p3) == 0;
        }
    }
}
