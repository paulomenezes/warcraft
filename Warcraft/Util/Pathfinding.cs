using Warcraft.Managers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Warcraft.Util
{
    class Pathfinding
    {
        List<PathNode> openList = new List<PathNode>();
        List<PathNode> closeList = new List<PathNode>();

        ManagerMap managerMap;

        int[][] neighbourhood = { new int[] { -1, 0 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { 0, 1 },
                                  new int[] { -1, -1 }, new int[] { -1, 1 }, new int[] { 1, -1 }, new int[] { 1, 1 } };

        int goalX;
        int goalY;

        public Pathfinding(ManagerMap managerMap)
        {
            this.managerMap = managerMap;
        }

        public bool SetGoal(int posX, int posY, int goalX, int goalY)
        {
            posX = posX / 32;
            posY = posY / 32;

            this.goalX = goalX;
            this.goalY = goalY;

            if (posX == goalX && posY == goalY)
                return false;

            if (!CheckWalls(this.goalX, this.goalY))
                return Reset(posX, posY);
            else
            {
                for (int i = 0; i < neighbourhood.Length; i++)
                {
                    this.goalX = this.goalX + neighbourhood[i][0];
                    this.goalY = this.goalY + neighbourhood[i][1];

                    if (posX == this.goalX && posY == this.goalY)
                        return false;

                    if (!CheckWalls(this.goalX, this.goalY))
                        return Reset(posX, posY);
                }
            }

            return false;
        }

        private bool Reset(int posX, int posY)
        {
            PathNode current = new PathNode(posX, posY, 0, 0, null);

            openList.Clear();
            closeList.Clear();

            openList.Add(current);
            closeList.Add(current);

            FindNeighbourhood(0);

            return true;
        }

        public List<PathNode> DiscoverPath()
        {
            while (openList.Count > 0)
            {
                int F = openList[0].F;
                int index = 0;
                for (int i = 1; i < openList.Count; i++)
                {
                    if (openList[i].F < F)
                    {
                        index = i;
                        F = openList[i].F;
                    }
                }

                closeList.Add(openList[index]);
                PathNode current = openList[index];

                if (current.x == goalX && current.y == goalY)
                    break;

                FindNeighbourhood(index);
            }

            return FindPath();
        }

        public void FindNeighbourhood(int index)
        {
            for (int i = 0; i < neighbourhood.Length; i++)
            {
                int x = openList[index].x + neighbourhood[i][0];
                int y = openList[index].y + neighbourhood[i][1];

                if (!CheckWalls(x, y) && !CheckOpen(x, y))
                {
                    int h = Math.Abs(x - goalX) + Math.Abs(y - goalY) + openList[index].H;

                    PathNode c = new PathNode(x, y, i > 3 ? 14 : 10, h, openList[index]);
                    openList.Add(c);
                }
            }

            openList.RemoveAt(index);
        }

        public List<PathNode> FindPath()
        {
            List<PathNode> path = new List<PathNode>();
            PathNode p = closeList.Last();
            while (p.parent != null)
            {
                path.Add(p);
                p = p.parent;
            }

            path.Reverse();

            return path;
        }

        private bool CheckWalls(int pointX, int pointY)
        {
            if (pointX < 0 || pointY < 0 ||
                pointX + 1 > Warcraft.MAP_SIZE ||
                pointY + 1 > Warcraft.MAP_SIZE)
                return true;
            
            return managerMap.WALLS.Any(i => i.TileX == pointX && i.TileY == pointY) || managerMap.FULL_MAP[pointX][pointY].tileType == Map.TileType.WATER;
        }

        private bool CheckOpen(int pointX, int pointY)
        {
            return openList.Any(i => i.x == pointX && i.y == pointY) || closeList.Any(i => i.x == pointX && i.y == pointY);
        }
    }
}
