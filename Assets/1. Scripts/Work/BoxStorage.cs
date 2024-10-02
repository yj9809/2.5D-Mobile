using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum BoxStorageType
{
    ChuruStorage,
    BoxStorage,
    IngredientStorage
}
public class BoxStorage : MonoBehaviour, IStackable, IObjectDataSave
{
    [TabGroup("Storage Transform"), SerializeField] private Transform[] boxTransform;
    [EnumToggleButtons] public BoxStorageType bsType;

    [TabGroup("Game Object"), SerializeField] private GameObject churu;
    [TabGroup("Game Object"), SerializeField] private GameObject box;
    [TabGroup("Game Object"), SerializeField] private IngredientMaker ingredientMaker;

    private DataManager data;

    private Stack<GameObject> boxStack = new Stack<GameObject>();

    public Stack<GameObject> BoxStack
    {
        get { return boxStack; }
        set { boxStack = value; }
    }

    private int boxTransformNum = 0;

    private void Start()
    {
        data = DataManager.Instance;
        data.AddObjStackCountList(this);

        SetSaveStackObj();

        if (bsType == BoxStorageType.ChuruStorage)
            GameManager.Instance.stackCount.Add(this);
    }

    private void Update()
    {
        boxTransformNum = Mathf.Clamp(boxStack.Count / 10, 0, boxTransform.Length - 1);
    }

    private void OnCollisionStay(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Churu")) && boxStack.Count < 40)
        {
            Rigidbody rd = collision.transform.GetComponent<Rigidbody>();
            if(rd != null)
                Destroy(collision.transform.GetComponent<Rigidbody>());

            Utility.ObjectDrop(boxTransform[boxTransformNum], collision.gameObject, null, boxStack, 2);
        }
        else if(collision.gameObject.CompareTag("Ingredient"))
        {
            Rigidbody rd = collision.transform.GetComponent<Rigidbody>();
            if (rd != null)
                Destroy(collision.transform.GetComponent<Rigidbody>());

            Utility.ObjectDrop(boxTransform[0], collision.gameObject, null, ingredientMaker.ChuruStack, 0);
        }
    }

    public void SetSaveStackObj()
    {
        if (bsType == BoxStorageType.ChuruStorage)
        {
            for (int i = 0; i < data.baseCost.objectData["churuStorageStackCount"]; i++)
            {
                boxTransformNum = Mathf.Clamp(boxStack.Count / 10, 0, boxTransform.Length - 1);
                GameObject churu = Instantiate(this.churu, transform);

                Utility.ObjectDrop(boxTransform[boxTransformNum], churu, null, boxStack, 2);
            }
        }
        else
        {
            for (int i = 0; i < data.baseCost.objectData["packagingBoxStorageStackCount"]; i++)
            {
                boxTransformNum = Mathf.Clamp(boxStack.Count / 10, 0, boxTransform.Length - 1);
                GameObject box = Instantiate(this.box, transform);

                Utility.ObjectDrop(boxTransform[boxTransformNum], box, null, boxStack, 2);
            }
        }
    }

    public int GetStackCount() => boxStack.Count;

    public Transform GetTransform() => transform.GetChild(0).transform;

    public int GetTypeNum() => 1;

    public void ObjectDataSave()
    {
        if (bsType == BoxStorageType.ChuruStorage)
            DataManager.Instance.baseCost.objectData["churuStorageStackCount"]  = boxStack.Count;
        else
            DataManager.Instance.baseCost.objectData["packagingBoxStorageStackCount"] = boxStack.Count;
    }
}
