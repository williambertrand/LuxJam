using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehavior : ScriptableObject
{
    public abstract Vector2 CalculateMove(EnemyFlockAgent agent, List<Transform> context, EnemyFlock flock);
}
