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
    private float compactSpeed;

    public void BeginCompactVisit(CompactSalesCounter sales, Vector3 queue, Vector3 exit)
    {
        compactSales = sales;
        compactQueue = queue;
        compactExit = exit;
        compactPhase = 0;
        compactWait = 0;
        if (na == null) na = GetComponent<NavMeshAgent>();
        compactSpeed = na != null ? na.speed : 1.8f;
        if (na != null) na.enabled = false;
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
        if (na != null) na.enabled = true;
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
            if (!WalkTo(compactQueue)) return;
            compactPhase = compactSales.StockCount > 0 ? 1 : 2;
            compactWait = .8f;
        }
        if (compactPhase == 1)
        {
            if (anime != null) anime.SetBool("isMove", false);
            compactWait -= Time.deltaTime;
            if (compactWait > 0) return;
            compactSales.TrySellOne();
            compactPhase = 2;
        }
        if (compactPhase == 2 && WalkTo(compactExit))
            PoolingManager.Instance.ReturnObjecte(gameObject);
    }

    private bool WalkTo(Vector3 destination)
    {
        Vector3 direction = destination - transform.position;
        direction.y = 0;
        if (direction.sqrMagnitude <= .01f) return true;
        if (anime != null) anime.SetBool("isMove", true);
        transform.rotation = Quaternion.LookRotation(direction);
        transform.position = Vector3.MoveTowards(transform.position,
            new Vector3(destination.x, transform.position.y, destination.z),
            compactSpeed * Time.deltaTime);
        return false;
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
