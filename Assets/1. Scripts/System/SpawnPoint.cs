using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject npc;
    [SerializeField] private Transform[] target;

    private PoolingManager pool;
    private Bounds bounds;

    private float spawnTime = 0;
    private float spawnTimer = 1f;

    private void Awake()
    {
        pool = PoolingManager.Instance;
        bounds = transform.GetComponent<Collider>().bounds;
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
            GameObject newNpc = pool.GetObj(npc);
            newNpc.transform.position = GetRandomPositionInBounds(bounds);
            newNpc.name = npc.name;
            newNpc.GetComponent<Npc>().Target = target;
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
}
