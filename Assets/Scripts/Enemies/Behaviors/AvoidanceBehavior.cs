using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FlockBehavior
{
    public override Vector2 CalculateMove(EnemyFlockAgent agent, List<Transform> context, EnemyFlock flock)
    {
        if (context.Count == 0)
            return Vector2.zero;

        // Add all points of neighbors and get average
        Vector2 avoidMove = Vector2.zero;
        int avoidCount = 0;
        foreach (Transform neighbor in context)
        {
            if(Vector2.SqrMagnitude(neighbor.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                avoidMove += (Vector2)(agent.transform.position - neighbor.position);
                avoidCount++;
            }
        }
        if (avoidCount > 0)
            avoidMove /= avoidCount;

        return avoidMove;
    }
}
