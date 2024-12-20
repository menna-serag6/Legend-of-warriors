using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour
{
    public Transform Player;
    NavMeshAgent agent;
    public float attackRadius = 5;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.position);
        if (distance < attackRadius)
            agent.SetDestination(Player.position);
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            //Debug.Log("Player Contacted");
            Destroy(gameObject);
    }

}
