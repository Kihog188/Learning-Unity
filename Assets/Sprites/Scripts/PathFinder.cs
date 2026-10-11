using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    private readonly GridMap map;
    private readonly Vector2Int[] dirs =
        { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    public int LastExplored { get; private set; }
    public Pathfinder(GridMap map)
    {
        this.map = map;
    }

    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
    {
        LastExplored = 0;
        var queue = new Queue<Vector2Int>();
        var visited = new HashSet<Vector2Int>();
        var cameFrom = new Dictionary<Vector2Int, Vector2Int>();

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();
            LastExplored++;
            if (current == goal) return BuildPath(cameFrom, start, goal);

            foreach (Vector2Int dir in dirs)
            {
                Vector2Int next = current + dir;
                if (!map.IsWalkable(next) || visited.Contains(next)) continue;

                visited.Add(next);
                cameFrom[next] = current;
                queue.Enqueue(next);
            }
        }

        return new List<Vector2Int>();
    }

    public List<Vector2Int> FindPathAStar(Vector2Int start, Vector2Int goal)
    {
        var open = new List<(Vector2Int cell, int f)>();
        var gScore = new Dictionary<Vector2Int, int>();
        var cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        var closed = new HashSet<Vector2Int>();

        gScore[start] = 0;
        open.Add((start, Heuristic(start, goal)));
        LastExplored = 0;

        while (open.Count > 0)
        {
            int best = 0;
            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].f < open[best].f) best = i;
            }
            Vector2Int current = open[best].cell;
            open.RemoveAt(best);

            if (closed.Contains(current)) continue;
            closed.Add(current);
            LastExplored++;

            if (current == goal) return BuildPath(cameFrom, start, goal);

            foreach (Vector2Int dir in dirs)
            {
                Vector2Int next = current + dir;
                if (!map.IsWalkable(next) || closed.Contains(next)) continue;

                int newG = gScore[current] + 1;
                if (gScore.TryGetValue(next, out int oldG) && newG >= oldG) continue;

                gScore[next] = newG;
                cameFrom[next] = current;
                open.Add((next, newG + Heuristic(next, goal)));
            }
        }

        return new List<Vector2Int>();
    }

    private int Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private List<Vector2Int> BuildPath(Dictionary<Vector2Int, Vector2Int> cameFrom,
                                       Vector2Int start, Vector2Int goal)
    {
        var path = new List<Vector2Int>();
        Vector2Int cur = goal;
        while (cur != start)
        {
            path.Add(cur);
            cur = cameFrom[cur];
        }
        path.Reverse();
        return path;
    }
}
