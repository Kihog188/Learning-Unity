using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemy : Enemy
{
    [SerializeField] private GridMap map;
    [SerializeField] private Transform target;
    [SerializeField] private float repathInterval = 0.5f;

    private Pathfinder pathfinder;
    private List<Vector2Int> path = new List<Vector2Int>();
    private int pathIndex;
    private float repathTimer;

    protected override void Awake()
    {
        base.Awake();
        pathfinder = new Pathfinder(map);
    }

    protected override void Move()
    {
        if (target == null) return;

        repathTimer -= Time.fixedDeltaTime;
        if (repathTimer <= 0f)
        {
            repathTimer = repathInterval;
            Vector2Int from = map.WorldToCell(rb.position);
            Vector2Int to = map.WorldToCell(target.position);
            path = pathfinder.FindPath(from, to);
            pathIndex = 0;
        }

        if (pathIndex >= path.Count) return;

        Vector2 waypoint = map.CellToWorld(path[pathIndex]);
        Vector2 next = Vector2.MoveTowards(rb.position, waypoint, speed * Time.fixedDeltaTime);
        rb.MovePosition(next);

        if (Vector2.Distance(next, waypoint) < 0.05f) pathIndex++;
    }

    void OnDrawGizmos()
    {
        if (map == null || path == null) return;
        Gizmos.color = Color.yellow;
        for (int i = 0; i < path.Count - 1; i++)
        {
            Gizmos.DrawLine(map.CellToWorld(path[i]), map.CellToWorld(path[i + 1]));
        }
    }
}