using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 이건 아마 권오석 작품일텐데? 맞을꺼임
// 그래서 솔직히 나도 잘 모르니까
// 문제 생기면 바로 권오석한테 문의 부탁.
public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] npc;
    [SerializeField] private Transform[] target;
    [SerializeField] private Collider planeCollider;

    public Transform[] GetTarget { get { return target; } }

    private PoolingManager pool;
    private Bounds planeBounds;

    private float spawnTime = 0;
    private float spawnTimer = 3f;
    private CompactSalesCounter compactSales;
    private Transform compactEntry;
    private Transform compactQueue;
    private Transform compactExit;
    private bool compactRouteReady;
    [SerializeField, Min(.1f)] private float compactMinSpawnDelay = .65f;
    [SerializeField, Min(.1f)] private float compactMaxSpawnDelay = 2.4f;
    [SerializeField, Range(0f, 1f)] private float compactBurstChance = .3f;
    [SerializeField, Range(0f, 1f)] private float compactPurchaseChance = .5f;
    [SerializeField, Range(0f, 1f)] private float compactReverseDirectionChance = .4f;
    [SerializeField, Min(1)] private int compactMaxActiveNpcs = 7;
    [SerializeField, Range(0f, .75f)] private float compactLaneJitter = .35f;
    private int activeCompactNpcs;
    private float compactSpawnTimer;

    public void ConfigureCompactSales(CompactSalesCounter sales, Transform entry,
        Transform queue, Transform exit)
    {
        compactSales = sales;
        compactEntry = entry;
        compactQueue = queue;
        compactExit = exit;
        compactRouteReady = IsCompleteRoute(entry.position, queue.position, exit.position);
        if (!compactRouteReady)
            Debug.LogError("Compact customer NavMesh route is incomplete.", this);
        activeCompactNpcs = 0;
        spawnTime = 0f;
        ScheduleNextCompactSpawn(true);
        enabled = compactRouteReady;
    }

    public void NotifyCompactNpcReturned()
    {
        activeCompactNpcs = Mathf.Max(0, activeCompactNpcs - 1);
    }

    private static bool IsCompleteRoute(Vector3 entry, Vector3 queue, Vector3 exit)
    {
        var path = new NavMeshPath();
        return NavMesh.SamplePosition(entry, out var start, 1f, NavMesh.AllAreas)
            && NavMesh.SamplePosition(queue, out var purchase, 1f, NavMesh.AllAreas)
            && NavMesh.SamplePosition(exit, out var leave, 1f, NavMesh.AllAreas)
            && NavMesh.CalculatePath(start.position, purchase.position, NavMesh.AllAreas, path)
            && path.status == NavMeshPathStatus.PathComplete
            && NavMesh.CalculatePath(purchase.position, leave.position, NavMesh.AllAreas, path)
            && path.status == NavMeshPathStatus.PathComplete
            && NavMesh.CalculatePath(leave.position, purchase.position, NavMesh.AllAreas, path)
            && path.status == NavMeshPathStatus.PathComplete
            && NavMesh.CalculatePath(purchase.position, start.position, NavMesh.AllAreas, path)
            && path.status == NavMeshPathStatus.PathComplete;
    }

    private void Awake()
    {
        pool = PoolingManager.Instance;
        planeBounds = planeCollider.bounds;
    }
    // Update is called once per frame
    void Update()
    {
        SpawnNpc();
    }
    private void SpawnNpc()
    {
        spawnTime += Time.deltaTime;

        float currentTimer = compactRouteReady ? compactSpawnTimer : spawnTimer;
        if (spawnTime < currentTimer) return;
        spawnTime = 0f;

        if (compactRouteReady)
        {
            ScheduleNextCompactSpawn(false);
            if (activeCompactNpcs >= compactMaxActiveNpcs) return;
        }

        if (pool == null || npc == null || npc.Length == 0) return;
        int npcRandom = Random.Range(0, npc.Length);
        GameObject spawned = pool.GetObj(npc[npcRandom]);
        if (spawned == null || !spawned.TryGetComponent<Npc>(out var newNpc))
        {
            PoolingManager.Instance.ReturnObjecte(spawned);
            return;
        }

        if (compactRouteReady)
        {
            bool reverse = Random.value < compactReverseDirectionChance;
            Transform entryMarker = reverse ? compactExit : compactEntry;
            Transform exitMarker = reverse ? compactEntry : compactExit;
            Vector3 entry = entryMarker.position + entryMarker.forward *
                Random.Range(-compactLaneJitter, compactLaneJitter);
            Vector3 queue = compactQueue.position + compactQueue.right * Random.Range(-.28f, .28f);
            Vector3 exit = exitMarker.position + exitMarker.forward *
                Random.Range(-compactLaneJitter, compactLaneJitter);
            bool willPurchase = compactSales.StockCount > 0 &&
                Random.value < compactPurchaseChance;
            if (newNpc.BeginCompactVisit(compactSales, entry, queue, exit,
                willPurchase, this))
                activeCompactNpcs++;
            else
                PoolingManager.Instance.ReturnObjecte(newNpc.gameObject);
        }
        else
            newNpc.transform.position = transform.position;
    }

    private void ScheduleNextCompactSpawn(bool initial)
    {
        float minimum = Mathf.Min(compactMinSpawnDelay, compactMaxSpawnDelay);
        float maximum = Mathf.Max(compactMinSpawnDelay, compactMaxSpawnDelay);
        if (initial)
            compactSpawnTimer = Random.Range(.25f, .9f);
        else if (Random.value < compactBurstChance)
            compactSpawnTimer = Random.Range(.25f, .5f);
        else
            compactSpawnTimer = Random.Range(minimum, maximum);
    }
    private Vector3 GetRandomPositionInBounds(Bounds bounds)
    {
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomY = Random.Range(bounds.min.y, bounds.max.y);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(randomX, randomY, randomZ);
    }
    public Vector3 GetRandomPositionInPlaneBounds()
    {
        float randomX = Random.Range(planeBounds.min.x, planeBounds.max.x);
        float randomZ = Random.Range(planeBounds.min.z, planeBounds.max.z);

        return new Vector3(randomX, planeBounds.size.y, randomZ);
    }
}
