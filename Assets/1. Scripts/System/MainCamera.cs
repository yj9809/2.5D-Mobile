using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Player p;
    private Vector3 cameraPosition = new Vector3(-4.5f, 12f, -4);
    private Vector3 cameraRotation = new Vector3(50f, 45f, 0);
    private bool isZoom;

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
        transform.position = new Vector3
            (p.transform.position.x + cameraPosition.x, cameraPosition.y, p.transform.position.z + cameraPosition.z);
        transform.rotation = Quaternion.Euler
            (cameraRotation.x, cameraRotation.y, cameraRotation.z);
    }

    public void ZoomScreen()
    {
        if (!isZoom)
        {
            cameraPosition.y = 30f;
            cameraRotation.x = 70f;
            isZoom = true;
        }
        else
        {
            cameraPosition.y = 12f;
            cameraRotation.x = 50f;
            isZoom = false;
        }
    }
}
