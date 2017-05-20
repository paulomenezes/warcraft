using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Warcraft.UI;
using Warcraft.Util;

namespace Warcraft.Map
{
    public class GenerateRooms
	{
		List<Room> rooms = new List<Room>();
		Dictionary<int, int> nodes = new Dictionary<int, int>();
        List<Triangle> triangles;

        public GenerateRooms()
        {
            Random rng = new Random();

            for (int i = 0; i < 100; i++)
            {
                int w = rng.Next(5, 10) * 32;
                int h = rng.Next(5, 10) * 32;

                Room room = new Room(new Rectangle(rng.Next(0, 50 * 32 - w), rng.Next(0, 50 * 32 - h), w, h));
                rooms.Add(room);
			}

            for (int i = rooms.Count - 1; i >= 0; i--)
            {
                for (int j = rooms.Count - 1; j >= 0; j--)
                {
                    if (i != j && rooms[i].rectangle.Intersects(rooms[j].rectangle)) 
                    {
                        rooms.RemoveAt(i);
                        break;
                    }
                }
            }

            List<TPoint> points = new List<TPoint>();

            for (int i = 0; i < rooms.Count; i++)
            {
                Vector2 center = new Vector2(rooms[i].rectangle.Width / 2, rooms[i].rectangle.Height / 2);

                points.Add(new TPoint(rooms[i].rectangle.X + center.X, rooms[i].rectangle.Y + center.Y));
                //int index = 0;
                //float distance = 9999;

                //for (int j = 0; j < rooms.Count; j++)
                //{
                //    float d = Vector2.Distance(rooms[i].GetPosition(), rooms[j].GetPosition());

                //    if (i != j && d < distance)
                //    {
                //        index = j;
                //        distance = d;
                //    }
                //}

                //nodes.Add(i, index);
            }

   //         points.Clear();
			//points.Add(new TPoint(0, 0));
			//points.Add(new TPoint(2, 0));
			//points.Add(new TPoint(1, 1));
			//points.Add(new TPoint(1, -1));

            //triangles = Delaunay.Triangulate(points);

            Prim(points);
        }

		List<Node> graph = new List<Node>();

        private void Prim(List<TPoint> points) 
        {
            for (int i = 0; i < points.Count; i++)
            {
                Node node = new Node();
                node.value = i;
                node.point = points[i];
				List<NodeEdge> adjancents = new List<NodeEdge>();

                for (int j = i; j < points.Count; j++)
                {
                    if (i != j)
                    {
                        Node adj = new Node();
                        adj.point = points[j];
                        adj.value = j;

                        float d = Vector2.Distance(points[i].GetPosition(), points[j].GetPosition());

                        NodeEdge edge = new NodeEdge();
                        edge.distance = d;
                        edge.node = adj;

                        adjancents.Add(edge);
                    }
                }

                node.adjacents = adjancents;

                graph.Add(node);
            }

            Node initial = graph[0];

            List<Node> visited = new List<Node>();
            visited.Add(initial);

            List<NodeEdge> options = initial.adjacents;

            while (visited.Count < points.Count)
            {
                int index = 0;
                float distance = int.MaxValue;
                for (int i = 0; i < options.Count; i++)
                {
                    if (options[i].distance < distance)
                    {
                        distance = options[i].distance;
                        index = i;
                    }
                }

                bool insert = true;
                for (int i = 0; i < visited.Count; i++)
                {
                    if (visited[i].value == options[index].node.value)
                    {
                        insert = false;
                        break;
                    }
                }

                if (insert)
                {
                    visited.Add(options[index].node);
                    options.AddRange(options[index].node.adjacents);
                }
            }
        }

		public void Draw(SpriteBatch spriteBatch)
        {
            rooms.ForEach(r => SelectRectangle.Draw(spriteBatch, r.rectangle));
			
            foreach (KeyValuePair<int, int> entry in nodes)
			{
                Room start = rooms[entry.Key];
                Room end = rooms[entry.Value];

                SelectRectangle.DrawLine(spriteBatch, start.GetPosition() + new Vector2(start.rectangle.Width / 2, start.rectangle.Height / 2), end.GetPosition() + new Vector2(end.rectangle.Width / 2, end.rectangle.Height / 2));
			}

            for (int i = 0; i < graph.Count; i++)
            {
                for (int j = 0; j < graph[i].adjacents.Count; j++)
                {
                    SelectRectangle.DrawLine(spriteBatch, graph[i].point.GetPosition(), graph[i].adjacents[j].node.point.GetPosition());
                }
            }
        }

        class Node
        {
            public int value;
            public TPoint point;
            public List<NodeEdge> adjacents;
        }

        class NodeEdge
        {
			public float distance;
            public Node node;
        }
    }
}
