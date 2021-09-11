using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/SteeredCohesion")]
public class SteeredCohesionBehavior : FlockBehavior
{
    Vector2 currentVelocity;
    public float agentSmoothTime = 0.5f;

    public override Vector2 CalculateMove(EnemyFlockAgent agent, List<Transform> context, EnemyFlock flock)
    {
        if (context.Count == 0)
            return Vector2.zero;

        // Add all points of neighbors and get average
        Vector2 cohesionMove = Vector2.zero;
        foreach (Transform neighbor in context)
        {
            cohesionMove += (Vector2)neighbor.position;
        }
        cohesionMove /= context.Count;

        // Return offset from the agent's position
        cohesionMove -= (Vector2)agent.transform.position;

        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);

        return cohesionMove;
    }
}
