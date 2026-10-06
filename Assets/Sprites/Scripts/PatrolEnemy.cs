using UnityEngine;

public class PatrolEnemy : Enemy
{
    [SerializeField] private Transform[] waypoints;
    private int index = 0;

    protected override void Move()
    {
        if (waypoints.Length == 0) return;

        Vector2 target = waypoints[index].position;
        Vector2 next = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(next);

        if (Vector2.Distance(next, target) < 0.05f)
        {
            index = (index + 1) % waypoints.Length;
        }
    }
}