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
        enabled = compactRouteReady;
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

        if(spawnTime >= spawnTimer)
        {
            int npcRandom = Random.Range(0, npc.Length);
            Npc newNpc = pool.GetObj(npc[npcRandom]).GetComponent<Npc>();
            if (compactRouteReady)
            {
                if (!newNpc.BeginCompactVisit(compactSales, compactEntry.position,
                    compactQueue.position, compactExit.position))
                    PoolingManager.Instance.ReturnObjecte(newNpc.gameObject);
            }
            else
                newNpc.transform.position = transform.position;
            spawnTime = 0;
        }
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
