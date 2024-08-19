using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainCamera : MonoBehaviour
{
    [SerializeField] private Player p;
    private Vector3 inCameraPosition = new Vector3(-5.25f, 12f, -4.7f);
    private Vector3 outCameraPosition = new Vector3(-10f, 20f, -10f);
    private Vector3 targetCameraPosition;
    private bool isZoom;

    private Tween cameraTween;

    void Start()
    {
        p = GameManager.Instance.P;
        targetCameraPosition = inCameraPosition;
        transform.position = GetTargetPosition(targetCameraPosition);
    }

    void Update()
    {
        if (cameraTween == null || !cameraTween.IsActive() || !cameraTween.IsPlaying())
        {
            Vector3 targetPosition = GetTargetPosition(targetCameraPosition);
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10f);
        }
    }

    public void ZoomScreen()
    {
        if (!isZoom)
        {
            targetCameraPosition = outCameraPosition;
            isZoom = true;
        }
        else
        {
            targetCameraPosition = inCameraPosition;
            isZoom = false;
        }

        Vector3 targetPosition = GetTargetPosition(targetCameraPosition);

        if (cameraTween != null)
        {
            cameraTween.Kill(true);
        }

        cameraTween = transform.DOMove(targetPosition, 1f).SetEase(Ease.InOutQuad);
    }

    private Vector3 GetTargetPosition(Vector3 cameraPosition)
    {
        return new Vector3(
            p.transform.position.x + cameraPosition.x,
            cameraPosition.y,
            p.transform.position.z + cameraPosition.z
        );
    }
}
