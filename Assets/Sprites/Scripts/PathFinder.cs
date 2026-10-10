using System.Collections.Generic;
using UnityEngine;

public class Pathfinder
{
    private readonly GridMap map;
    private readonly Vector2Int[] dirs =
        { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };

    public Pathfinder(GridMap map)
    {
        this.map = map;
    }

    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int goal)
    {
        var queue = new Queue<Vector2Int>();
        var visited = new HashSet<Vector2Int>();
        var cameFrom = new Dictionary<Vector2Int, Vector2Int>();

        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            Vector2Int current = queue.Dequeue();
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
