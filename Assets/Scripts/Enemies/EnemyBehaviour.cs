using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    string targetTag;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (target)
        {
            agent.SetDestination(target.position);
        }
    }

    public void SetTarget(GameObject new_target)
    {
        target = new_target.transform;
        agent.isStopped = false;
    }

    public void RemoveTarget()
    {
        target = null;
        agent.isStopped = true;
    }
}
