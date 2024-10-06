using UnityEngine;
using UnityEngine.AI;

public class Enemy_2 : MonoBehaviour
{
    public Transform target; // �߰� ���
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

    // �߰� ����
    public void StopChase()
    {
        isChasing = false;
        agent.isStopped = true;
    }

    // �߰� �簳
    public void ResumeChase()
    {
        isChasing = true;
        if(agent != null)
        {
            agent.isStopped = false;
        }
    }
}
