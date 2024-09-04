using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialGuide : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform target;
    [SerializeField] private RectTransform canvas;

    // Start is called before the first frame update
    void Start()
    {
        player = GameManager.Instance.P.transform;
    }

    // Update is called once per frame
    void Update()
    {
        GuideLine();
    }

    private void GuideLine()
    {
        Vector3 direction = target.position - player.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        canvas.rotation = lookRotation * Quaternion.Euler(90, 0, 0);
        canvas.localPosition = Vector3.zero;
    }
}
