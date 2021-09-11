using System.Collections;
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

        if(t < 0.9f)
        {
            return Vector2.zero;
        }

        return offset * t * t;
    }
}
