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
    private CompactSalesCounter compactSales;
    private Vector3 compactQueue;
    private Vector3 compactExit;
    private int compactPhase;
    private float compactWait;
    private ObstacleAvoidanceType originalAvoidance;

    public bool BeginCompactVisit(CompactSalesCounter sales, Vector3 entry, Vector3 queue, Vector3 exit)
    {
        if (na == null) na = GetComponent<NavMeshAgent>();
        if (na == null || !na.isActiveAndEnabled
            || !NavMesh.SamplePosition(entry, out var start, 1f, NavMesh.AllAreas)
            || !NavMesh.SamplePosition(queue, out var purchase, 1f, NavMesh.AllAreas)
            || !NavMesh.SamplePosition(exit, out var leave, 1f, NavMesh.AllAreas))
            return false;
        var path = new NavMeshPath();
        if (!NavMesh.CalculatePath(start.position, purchase.position, NavMesh.AllAreas, path)
            || path.status != NavMeshPathStatus.PathComplete
            || !NavMesh.CalculatePath(purchase.position, leave.position, NavMesh.AllAreas, path)
            || path.status != NavMeshPathStatus.PathComplete
            || !na.Warp(start.position))
            return false;
        compactSales = sales;
        compactQueue = purchase.position;
        compactExit = leave.position;
        compactPhase = sales.StockCount > 0 ? 0 : 2;
        compactWait = 0;
        originalAvoidance = na.obstacleAvoidanceType;
        na.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
        na.isStopped = false;
        if (!na.SetDestination(compactPhase == 0 ? compactQueue : compactExit))
        {
            compactSales = null;
            na.obstacleAvoidanceType = originalAvoidance;
            return false;
        }
        return true;
    }

    private void OnEnable()
    {
        if (!hasInitialized)
        {
            value = Random.Range(0, 2);
            
            hasInitialized = true;
        }
    }

    private void OnDisable()
    {
        if (compactSales == null) return;
        compactSales = null;
        compactPhase = 0;
        if (na != null)
        {
            if (na.isActiveAndEnabled && na.isOnNavMesh) na.ResetPath();
            na.obstacleAvoidanceType = originalAvoidance;
        }
    }

    private void Start()
    {
        na = GetComponent<NavMeshAgent>();
        anime = GetComponent<Animator>();
        if (compactSales != null) return;
        spawnPoint = FindObjectOfType<SpawnPoint>();
        na.SetDestination(spawnPoint.GetTarget[currentTargetNum].position);
    }

    // Update is called once per frame
    void Update()
    {
        if (compactSales != null)
        {
            UpdateCompactVisit();
            return;
        }
        anime.SetBool("isMove", true);
        if (npcType == NpcType.Home)
            CheckPointMove();
        else
        {
            SetStorePosition();
            RetrunHome();
        }
    }

    private void UpdateCompactVisit()
    {
        if (compactPhase == 0)
        {
            if (!ReachedCompactDestination()) return;
            if (compactSales.StockCount > 0)
            {
                compactPhase = 1;
                compactWait = .8f;
            }
            else
            {
                BeginCompactExit();
                return;
            }
        }
        if (compactPhase == 1)
        {
            if (anime != null) anime.SetBool("isMove", false);
            compactWait -= Time.deltaTime;
            if (compactWait > 0) return;
            compactSales.TrySellOne();
            BeginCompactExit();
            return;
        }
        if (compactPhase == 2 && ReachedCompactDestination())
        {
            PoolingManager.Instance.ReturnObjecte(gameObject);
            return;
        }
        if (anime != null && compactPhase != 1)
            anime.SetBool("isMove", na.velocity.sqrMagnitude > .01f);
    }

    private void BeginCompactExit()
    {
        compactPhase = 2;
        if (!na.SetDestination(compactExit))
            PoolingManager.Instance.ReturnObjecte(gameObject);
    }

    private bool ReachedCompactDestination() => na.isOnNavMesh && !na.pathPending
        && na.pathStatus == NavMeshPathStatus.PathComplete
        && na.remainingDistance <= na.stoppingDistance + .1f;

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
