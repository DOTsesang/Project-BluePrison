using UnityEngine;
using UnityEngine.AI;

public class Enemy_2 : MonoBehaviour
{
    public Transform target; // 추격 대상
    private NavMeshAgent agent;
    private bool isChasing = true;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        
        if (isChasing && target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    // 추격 중지
    public void StopChase()
    {
        isChasing = false;
        agent.isStopped = true;
    }

    // 추격 재개
    public void ResumeChase()
    {
        isChasing = true;
        if(agent != null)
        {
            agent.isStopped = false;
        }
    }
}
