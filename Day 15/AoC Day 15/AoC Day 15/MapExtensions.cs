using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC_Day_15
{
    public static class MapExtensions
    {
        public static ushort[][] DeepClone(this ushort[][] source)
        {
            var target = new ushort[source.Length][];
            for (var i = 0; i < source.Length; i++)
            {
                target[i] = new ushort[source[i].Length];

                for (var j = 0; j < source[i].Length; j++)
                    target[i][j] = source[i][j];
            }

            return target;
        }

        public static List<Coordinate> GetAdjacentPoints(this ushort[][] map, Coordinate pt, bool includeDiagonals = true)
        {
            var adjacentPoints = new List<Coordinate>();

            if (pt.Y - 1 >= 0)
            {
                adjacentPoints.Add(new Coordinate(pt.X, pt.Y - 1));
                if (pt.X - 1 >= 0 && includeDiagonals) adjacentPoints.Add(new Coordinate(pt.X - 1, pt.Y - 1));
                if (pt.X + 1 < map[pt.Y].Length && includeDiagonals) adjacentPoints.Add(new Coordinate(pt.X + 1, pt.Y - 1));
            }
            if (pt.X - 1 >= 0) adjacentPoints.Add(new Coordinate(pt.X - 1, pt.Y));
            if (pt.X + 1 < map[pt.Y].Length) adjacentPoints.Add(new Coordinate(pt.X + 1, pt.Y));
            if (pt.Y + 1 < map.Length)
            {
                adjacentPoints.Add(new Coordinate(pt.X, pt.Y + 1));
                if (pt.X - 1 >= 0 && includeDiagonals) adjacentPoints.Add(new Coordinate(pt.X - 1, pt.Y + 1));
                if (pt.X + 1 < map[pt.Y].Length && includeDiagonals) adjacentPoints.Add(new Coordinate(pt.X + 1, pt.Y + 1));
            }

            return adjacentPoints;
        }

        public static double Distance(this Coordinate a, Coordinate b)
        {
            //d = s rt( (X2-X1)^2 + (Y2-Y1)^2)
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }

        public static Stack<Coordinate> A_Star(this ushort[][] map, Coordinate start, Coordinate goal)
        {
            var comparer = new CoordinateEqualityComparer();
            //TIL .NET 5 doesn't have PriorityQueue, so here's a janky substitute
            var openSet = new Dictionary<Coordinate, double>(comparer) { { start, 0 } };
            var cameFrom = new Dictionary<Coordinate, Coordinate>(comparer);

            //Actual score is the value at map[Y][X] (aka the "risk level")
            var gScore = new Dictionary<Coordinate, double>(comparer);
            gScore.Add(start, 0); // No cost to enter where you already are

            //Heuristic Score is the distance between any given coordinate and the goal
            var fScore = new Dictionary<Coordinate, double>(comparer);
            fScore.Add(start, 0);

            while (openSet.Count > 0)
            {
                //Find node in openSet with lowest estimated score
                var currPt = openSet.OrderBy(kvp => kvp.Value).First().Key;

                //Reconstruct path if we've reached the goal
                if (currPt.Equals(goal))
                {
                    var path = new Stack<Coordinate>();
                    path.Push(goal);

                    var pt = currPt;
                    while (cameFrom.ContainsKey(pt))
                    {
                        pt = cameFrom[pt];
                        path.Push(pt);
                    }
                    return path;
                }

                //Remove node from openSet
                openSet.Remove(currPt);

                //Add all adjacent nodes to openSet
                foreach (var neighbor in map.GetAdjacentPoints(currPt, false))
                {
                    if (cameFrom.ContainsKey(neighbor))
                        continue;

                    var tentative_gScore = gScore[currPt] + map[neighbor.Y][neighbor.X];
                    
                    if (!gScore.ContainsKey(neighbor) || tentative_gScore < gScore[neighbor])
                    {
                        gScore.InsertOrUpdate(neighbor, tentative_gScore);
                        fScore.InsertOrUpdate(neighbor, tentative_gScore + neighbor.Distance(goal));

                        cameFrom.Add(neighbor, currPt);

                        if (!openSet.ContainsKey(neighbor))
                            openSet.Add(neighbor, tentative_gScore);
                    }
                }
            }

            return null; //No path found
        }

        
    }
}
