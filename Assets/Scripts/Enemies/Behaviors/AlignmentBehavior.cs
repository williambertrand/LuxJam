using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior: FlockBehavior
{

    public override Vector2 CalculateMove(EnemyFlockAgent agent, List<Transform> context, EnemyFlock flock)
    {
        if (context.Count == 0)
            return agent.transform.up;

        // Add all points of neighbors and get average
        Vector2 alignMove = Vector2.zero;
        foreach (Transform neighbor in context)
        {
            alignMove += (Vector2)neighbor.up;
        }
        alignMove /= context.Count;

        // Return offset from the agent's position
        return alignMove;
    }
}
