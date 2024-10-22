using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �̰� �Ƹ� �ǿ��� ��ǰ���ٵ�? ��������
// �׷��� ������ ���� �� �𸣴ϱ�
// ���� ����� �ٷ� �ǿ������� ���� ��Ź.
public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject[] npc;
    [SerializeField] private Transform[] target;
    [SerializeField] private Collider planeCollider;

    public Transform[] GetTarget { get { return target; } }

    private PoolingManager pool;
    private Bounds bounds;
    private Bounds planeBounds;

    private float spawnTime = 0;
    private float spawnTimer = 3f;

    private void Awake()
    {
        pool = PoolingManager.Instance;
        bounds = transform.GetComponent<Collider>().bounds;
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
            newNpc.SetSpawnPoint(this);
            newNpc.transform.position = GetRandomPositionInBounds(bounds);
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

        return new Vector3(randomX, bounds.size.y, randomZ);
    }
}
