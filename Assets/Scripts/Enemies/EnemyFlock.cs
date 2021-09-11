using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlock : MonoBehaviour
{

    public string agentPrefabName;
    //public EnemyFlockAgent agentPrefab;
    public List<EnemyFlockAgent> flockAgents = new List<EnemyFlockAgent>();
    public FlockBehavior flockBehavior;

    //TESTING Agents to create in the scene
    public int startCount = 100;
    [Range(0.2f, 20f)]
    public float agentSpread = 0.5f;

    // MARK - params for how enemies behave

    [Range(1.0f, 100f)]
    public float driveFactor = 10;
    [Range(1.0f, 20f)]
    public float maxSpeed = 5;
    [Range(1.0f, 10f)]
    public float neighborRadius = 1.5f;

    [Range(0.0f, 1f)]
    public float neighborAvoidanceFactor = 1.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius
    {
        get
        {
            return squareAvoidanceRadius;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        // Calc some squares to save us from doing sqrts later
        squareMaxSpeed = (maxSpeed * maxSpeed);
        squareNeighborRadius = (neighborRadius * neighborRadius);
        squareAvoidanceRadius = squareNeighborRadius * neighborAvoidanceFactor * neighborAvoidanceFactor;

        for(int i = 0; i < startCount; i++)
        {
            PoolableObject newAgent = ObjectPooler.Instance.SpawnFromPool(
                agentPrefabName,
                Random.insideUnitCircle * startCount * agentSpread,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f))
            );
            newAgent.name = "Agent " + i;
            flockAgents.Add(newAgent.GetComponent<EnemyFlockAgent>());
        }

    }

    void Update()
    {
        foreach(EnemyFlockAgent agent in flockAgents)
        {
            List<Transform> context = GetNeighbors(agent);

            Vector2 move = flockBehavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
                move = move.normalized * maxSpeed;
            agent.Move(move);
        }
    }


    // Check for any other enemies within neighborRadius
    private List<Transform> GetNeighbors(EnemyFlockAgent agent)
    {
        List<Transform> nearby = new List<Transform>();
        Collider2D[] colliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach(Collider2D col in colliders)
        {
            if (col.CompareTag("Enemy") && col != agent.AgentCollider)
            {
                nearby.Add(col.transform);
            }
        }
        return nearby;
    }
}
