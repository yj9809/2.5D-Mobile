using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;

public enum NpcType { Store, Home}

public class Npc : MonoBehaviour
{
    [SerializeField] private SpawnPoint spawnPoint;
    [EnumToggleButtons, SerializeField] private NpcType npcType = NpcType.Home;

    private NavMeshAgent na;
    private Animator anime;

    private int currentTargetNum = 0;
    private float randomTransformTime = 5;
    private float returnHome = 15f;
    private int value;
    private bool hasInitialized = false;

    private void OnEnable()
    {
        if (!hasInitialized)
        {
            value = Random.Range(0, 2);
            
            hasInitialized = true;
        }
    }

    private void Start()
    {
        na = GetComponent<NavMeshAgent>();
        anime = GetComponent<Animator>();
        spawnPoint = FindObjectOfType<SpawnPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        anime.SetBool("isMove", true);
        if (npcType == NpcType.Home)
            CheckPointMove();
        else
        {
            SetStorePosition();
            RetrunHome();
        }
    }

    private void CheckPointMove()
    {
        if (!na.pathPending && na.remainingDistance <= na.stoppingDistance)
        {
            if (currentTargetNum < spawnPoint.GetTarget.Length - 1)
            {
                if (currentTargetNum == 1)
                {
                    if (value == 1)
                    {
                        npcType = NpcType.Store;
                        na.SetDestination(spawnPoint.GetRandomPositionInPlaneBounds());
                    }
                    else
                    {
                        currentTargetNum++;
                        na.SetDestination(spawnPoint.GetTarget[currentTargetNum].position);
                    }
                }
                else
                {
                    currentTargetNum++;
                    na.SetDestination(spawnPoint.GetTarget[currentTargetNum].position);
                }

            }
            else
            {
                na.isStopped = true;
                currentTargetNum = -1;
                hasInitialized = false;
                PoolingManager.Instance.ReturnObjecte(this.gameObject);
            }
        }
    }
    private void SetStorePosition()
    {
        randomTransformTime -= Time.deltaTime;
        if (randomTransformTime <= 0)
        {
            na.SetDestination(spawnPoint.GetRandomPositionInPlaneBounds());
            randomTransformTime = 5;
        }
    }
    private void RetrunHome()
    {
        returnHome -= Time.deltaTime;
        if(returnHome <= 0)
        {
            value = 0;
            npcType = NpcType.Home;
            na.SetDestination(spawnPoint.GetTarget[currentTargetNum].position);
            returnHome = 15f;
        }
    }
    public void SetSpawnPoint(SpawnPoint spawnPoint)
    {
        this.spawnPoint = spawnPoint;
    }
}
