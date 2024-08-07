using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Player p;

    private GameManager gm;

    private void Awake()
    {
        gm = GameManager.Instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        p = gm.P;   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(p.transform.position.x -3, 10, p.transform.position.z - 2.6f);
    }
}
