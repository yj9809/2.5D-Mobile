using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Player p;
    private Vector3 cameraPosition = new Vector3(-5.25f, 12f, -4.7f);
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

    void LateUpdate()
    {

        Vector3 targetPosition = new Vector3
            (p.transform.position.x + cameraPosition.x, cameraPosition.y, p.transform.position.z + cameraPosition.z);
        transform.position = targetPosition;

        transform.rotation = Quaternion.Euler(cameraRotation);
    }

    public void ZoomScreen()
    {
        if (!isZoom)
        {
            cameraPosition = new Vector3(-10f, 20f, -10f);
            cameraRotation.x = 50f;
            isZoom = true;
        }
        else
        {
            cameraPosition = new Vector3(-5.25f, 12f, -4.7f);
            cameraRotation.x = 50f;
            isZoom = false;
        }

        Vector3 targetPosition = new Vector3
            (p.transform.position.x + cameraPosition.x, cameraPosition.y, p.transform.position.z + cameraPosition.z);

        transform.DOMove(targetPosition, 0.5f);
        transform.DORotate(new Vector3(cameraRotation.x, cameraRotation.y, cameraRotation.z), 0.5f);
    }
}
