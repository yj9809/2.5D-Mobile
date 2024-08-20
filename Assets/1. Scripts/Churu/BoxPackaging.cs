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

    public void Packaging()
    {
        if (newBox == null && churuStorage.Count != 0)
        { 
            newBox = Instantiate(box, boxParent);
            newBox.name = box.name;
        }

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
            churu.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InBack)
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
            //newBox.transform.SetParent(packagingBoxParent);
            newBox.AddComponent<Rigidbody>();
            newBox.transform.DOMove(packagingBoxParent.position, 1f).SetEase(Ease.InBack);
            this.newBox = null;
            this.count = 0;
        }
        packaging = PackagingType.On;
    }
}
