using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Flock/Behavior/MoveTowardsPlayer")]
public class MoveTowardsPlayerBehavior : FlockBehavior
{
    public float maxDist = 10.0f;

    public override Vector2 CalculateMove(EnemyFlockAgent agent, List<Transform> context, EnemyFlock flock)
    {
        Vector2 offset = (Vector2)PlayerShipMovement.Instance.transform.position - (Vector2)agent.transform.position;
        float t = offset.magnitude / maxDist;
        var threshold = 0.9f;
        if (agent.hasTarget)
        {
            threshold = 0.2f;
        }
        if(t < threshold)
        {
            return Vector2.zero;
        }

        return offset * t;
    }
}
