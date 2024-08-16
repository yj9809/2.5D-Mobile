using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc : MonoBehaviour
{
    [SerializeField] private Transform[] target;
    public Transform[] Target
    {
        get { return target; }
        set { target = value; }
    }

    private NavMeshAgent na;

    private int currentTargetNum = 0;

    private void Start()
    {
        na = GetComponent<NavMeshAgent>();
        na.SetDestination(target[currentTargetNum].position);
    }

    // Update is called once per frame
    void Update()
    {
        CheckPointMove();
    }
    private void CheckPointMove()
    {
        if (!na.pathPending && na.remainingDistance <= na.stoppingDistance)
        {
            if (currentTargetNum < target.Length - 1)
            {
                currentTargetNum++;
                na.SetDestination(target[currentTargetNum].position);
            }
            else
            {
                na.isStopped = true;
                currentTargetNum = -1;
                PoolingManager.Instance.ReturnObjecte(this.gameObject);
            }
        }
    }
}
