using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform Player; // The target the enemy will move towards
    private NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            navMeshAgent.SetDestination(Player.position); // Set the destination to the player's position
        }
    }
}
