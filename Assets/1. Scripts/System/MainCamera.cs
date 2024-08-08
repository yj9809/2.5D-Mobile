using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Player p;
    private Vector3 cameraPosition = new Vector3(-4.5f, 12f, -4);
    private Vector3 cameraRotation = new Vector3(50f, 45f, 0);
    private bool isZoom;

    private GameManager gm;

    private float positionY;
    private float rotationX;

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
            (p.transform.position.x + cameraPosition.x, cameraPosition.y, p.transform.position.z + -4);
        transform.rotation = Quaternion.Euler
            (cameraRotation.x, cameraRotation.y, cameraRotation.z);
    }

    public void ZoomScreen()
    {
        if (!isZoom)
        {
            positionY = 30f;
            rotationX = 70f;
            isZoom = true;
        }
        else
        {
            positionY = 12f;
            rotationX = 50f;
            isZoom = false;
        }
        transform.DOMoveY(positionY, 0.5f)
            .OnComplete(() => cameraPosition.y = positionY);
        transform.DORotate(new Vector3(rotationX, cameraRotation.y, cameraRotation.z), 0.5f)
            .OnComplete(() => cameraRotation.x = rotationX);
    }
}
