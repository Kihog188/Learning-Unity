using System.Collections.Generic;
using UnityEngine;

public class ChaserEnemy : Enemy
{
    private enum State { Patrol, Chase, Return }

    [SerializeField] private GridMap map;
    [SerializeField] private Transform target;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float detectRange = 5f;
    [SerializeField] private float loseTime = 3f;
    [SerializeField] private float repathInterval = 0.5f;

    private State state = State.Patrol;
    private Pathfinder pathfinder;
    private List<Vector2Int> path = new List<Vector2Int>();
    private int pathIndex;
    private float repathTimer;
    private float lostTimer;
    private int waypointIndex;

    protected override void Awake()
    {
        base.Awake();
        pathfinder = new Pathfinder(map);
    }

    protected override void Move()
    {
        if (target == null || waypoints.Length == 0) return;

        bool seesPlayer = target.gameObject.activeInHierarchy &&
                          Vector2.Distance(rb.position, target.position) <= detectRange;

        switch (state)
        {
            case State.Patrol:
                if (seesPlayer) { ChangeState(State.Chase); break; }
                PatrolStep();
                break;

            case State.Chase:
                lostTimer = seesPlayer ? 0f : lostTimer + Time.fixedDeltaTime;
                if (lostTimer >= loseTime) { ChangeState(State.Return); break; }
                FollowPathTo(target.position);
                break;

            case State.Return:
                if (seesPlayer) { ChangeState(State.Chase); break; }
                Vector2 home = waypoints[waypointIndex].position;
                if (map.WorldToCell(rb.position) == map.WorldToCell(home))
                {
                    ChangeState(State.Patrol);
                    break;
                }
                FollowPathTo(home);
                break;
        }
    }

    private void ChangeState(State newState)
    {
        state = newState;
        lostTimer = 0f;
        repathTimer = 0f;
        Debug.Log($"{name}: {newState}");
    }

    private void PatrolStep()
    {
        Vector2 wp = waypoints[waypointIndex].position;
        Vector2 next = Vector2.MoveTowards(rb.position, wp, speed * Time.fixedDeltaTime);
        rb.MovePosition(next);
        if (Vector2.Distance(next, wp) < 0.05f)
        {
            waypointIndex = (waypointIndex + 1) % waypoints.Length;
        }
    }

    private void FollowPathTo(Vector2 worldTarget)
    {
        repathTimer -= Time.fixedDeltaTime;
        if (repathTimer <= 0f)
        {
            repathTimer = repathInterval;
            path = pathfinder.FindPathAStar(map.WorldToCell(rb.position), map.WorldToCell(worldTarget));
            pathIndex = 0;
        }

        if (pathIndex >= path.Count) return;

        Vector2 wp = map.CellToWorld(path[pathIndex]);
        Vector2 next = Vector2.MoveTowards(rb.position, wp, speed * Time.fixedDeltaTime);
        rb.MovePosition(next);
        if (Vector2.Distance(next, wp) < 0.05f) pathIndex++;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        if (map == null || path == null) return;
        Gizmos.color = Color.yellow;
        for (int i = 0; i < path.Count - 1; i++)
        {
            Gizmos.DrawLine(map.CellToWorld(path[i]), map.CellToWorld(path[i + 1]));
        }
    }
}