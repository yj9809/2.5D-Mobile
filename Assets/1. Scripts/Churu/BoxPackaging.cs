using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

public enum PackagingType
{
    On,
    Off
}
public class BoxPackaging : MonoBehaviour
{
    [SerializeField] private Transform storageParent;
    [SerializeField] private Transform boxParent;
    [SerializeField] private Transform packagingBoxParent;
    [SerializeField] private GameObject box;

    private GameObject newBox;

    private Stack<GameObject> churuStorage = new Stack<GameObject>();
    private Stack<GameObject> boxStorage = new Stack<GameObject>();
    public Transform StorageParent { get { return storageParent; } }
    public Stack<GameObject> ChuruStorage
    {
        get { return churuStorage; }
        set
        {
            churuStorage = value;
        }
    }

    PackagingType packaging = PackagingType.On;

    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Packaging()
    {
        if (newBox == null && churuStorage.Count != 0)
            newBox = Instantiate(box, boxParent);

        if (churuStorage.Count != 0 && newBox != null)
        {
            ChuruMove();
        }

        //if (count <= 5)
        //    packaging = PackagingType.On;
    }
    private void ChuruMove()
    {
        if(packaging != PackagingType.Off)
        {
            GameObject churu = churuStorage.Pop();

            churu.transform.SetParent(newBox.transform);
            churu.transform.DOLocalMove(new Vector3(0, 0, 0), 1f).SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    PoolingManager.Instance.ReturnObjecte(churu);
                    count++;
                    BoxMove(newBox, count);
                }
                );
            packaging = PackagingType.Off;
        }
    }
    private void BoxMove(GameObject newBox, int count)
    {
        if(count == 5)
        {
            boxStorage.Push(newBox);
            newBox.transform.SetParent(packagingBoxParent);
            newBox.transform.DOLocalMove(new Vector3(0, 0 + (Utility.ObjRendererCheck(newBox) * boxStorage.Count), 0), 0.2f).SetEase(Ease.InBack);
            this.newBox = null;
            this.count = 0;
        }
        packaging = PackagingType.On;
    }
}
