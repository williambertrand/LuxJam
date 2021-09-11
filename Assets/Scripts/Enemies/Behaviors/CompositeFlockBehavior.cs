using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/CompositeFlock")]
public class CompositeFlockBehavior : FlockBehavior
{
    [SerializeField] private FlockBehavior[] behaviors;
    [SerializeField] private float[] weights;

    public override Vector2 CalculateMove(EnemyFlockAgent agent, List<Transform> context, EnemyFlock flock)
    {
        Vector2 move = Vector2.zero;
        for(int i = 0; i < behaviors.Length; i++)
        {
            Vector2 partialMove = (behaviors[i].CalculateMove(agent, context, flock) * weights[i]);
            if(partialMove != Vector2.zero)
            {
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }

        return move;
    }
}
