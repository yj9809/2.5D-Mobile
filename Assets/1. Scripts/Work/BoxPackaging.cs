using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
    [SerializeField] private BoxStorage boxStorage;

    private GameObject newBox;
    private TMP_Text boxCountTxt;

    public Transform StorageParent { get { return storageParent; } }

    private Stack<GameObject> churuStorage = new Stack<GameObject>();
    public Stack<GameObject> ChuruStorage
    {
        get { return churuStorage; }
        set
        {
            churuStorage = value;
        }
    }

    private PackagingType packaging = PackagingType.On;

    private int count = 0;
    public int Count
    {
        get { return count; }
    }
    private const int maxCount = 5;

    public void Packaging(Player p, Employee employee)
    {
        if (newBox == null && churuStorage.Count != 0 && boxStorage.BoxStack.Count < 40)
        { 
            newBox = Instantiate(box, boxParent);
            newBox.name = box.name;
            boxCountTxt = newBox.transform.GetChild(0).transform.GetChild(0).GetComponent<TMP_Text>();
            newBox.transform.GetChild(0).gameObject.SetActive(true);
        }

        if (churuStorage.Count != 0 && newBox != null)
        {
            ChuruMove(p, employee);
        }
        else
        {
            if (p != null)
                p.StopBoxPackagingAnimationPlayer();
            if (employee != null)
                employee.StopBoxPackagingAnimationEmployee();
        }
    }
    private void ChuruMove(Player p, Employee employee)
    {
        if(packaging != PackagingType.Off)
        {
            GameObject churu = churuStorage.Pop();

            if(p != null)
                GameManager.Instance.P.DoBoxPackagingAnimationPlayer();
            if (employee != null)
                employee.DoBoxPackagingAnimationEmployee();

            churu.transform.SetParent(newBox.transform);
            churu.transform.DOLocalMove(Vector3.zero, 0.3f).SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    PoolingManager.Instance.ReturnObjecte(churu);
                    count++;
                    boxCountTxt.text = $"{count}/{maxCount}";
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
            newBox.AddComponent<Rigidbody>();
            newBox.transform.DOMove(packagingBoxParent.position, 0.3f).SetEase(Ease.InBack)
                .OnComplete(() =>
                {
                    this.newBox.transform.GetChild(0).gameObject.SetActive(false);
                    this.newBox = null;
                    this.count = 0;
                    packaging = PackagingType.On;
                });
            
        }
        else
        {
            packaging = PackagingType.On;
        }
    }
    
}
