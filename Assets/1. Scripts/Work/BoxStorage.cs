using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

// �̰� �� ��ǰ
// �� enum�� �� ���� �ڽ� ���丮�� Ÿ�� ������ ���Ѱ���
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

    private void OnCollisionStay(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Churu")) && boxStack.Count < 40)
        {
            boxTransformNum = Mathf.Clamp(boxStack.Count / 10, 0, boxTransform.Length - 1);
            Rigidbody rd = collision.transform.GetComponent<Rigidbody>();
            if(rd != null)
                Destroy(collision.transform.GetComponent<Rigidbody>());

            Utility.ObjectDrop(boxTransform[boxTransformNum], collision.gameObject, null, boxStack, 2);
        }
        else if(collision.gameObject.CompareTag("Ingredient"))
        {
            boxTransformNum = Mathf.Clamp(boxStack.Count / 10, 0, boxTransform.Length - 1);
            Rigidbody rd = collision.transform.GetComponent<Rigidbody>();
            if (rd != null)
                Destroy(collision.transform.GetComponent<Rigidbody>());
            
            Utility.ObjectDrop(boxTransform[0], collision.gameObject, null, ingredientMaker.ChuruStack, 0);
        }
    }

    // �̰� ���� �������� ��� ���� ���� ���� �ҷ����� �Լ���
    // ���� �ҷ����� �Լ� �߰��� ��� ��ųʸ� Ű �� ����� �Է��ؼ� �߰� �ٶ�
    public void SetSaveStackObj()
    {
        if (bsType == BoxStorageType.ChuruStorage)
        {
            for (int i = 0; i < data.baseCost.objectData["churuStorageStackCount"]; i++)
            {
                boxTransformNum = Mathf.Clamp(boxStack.Count / 10, 0, boxTransform.Length - 1);
                GameObject churu = PoolingManager.Instance.GetObj(this.churu);
                churu.transform.parent = transform;

                Utility.ObjectDrop(boxTransform[boxTransformNum], churu, null, boxStack, 2);
            }
        }
        else if(bsType == BoxStorageType.BoxStorage)
        {
            for (int i = 0; i < data.baseCost.objectData["packagingBoxStorageStackCount"]; i++)
            {
                boxTransformNum = Mathf.Clamp(boxStack.Count / 10, 0, boxTransform.Length - 1);
                GameObject box = PoolingManager.Instance.GetObj(this.box);
                box.transform.parent = transform;

                Utility.ObjectDrop(boxTransform[boxTransformNum], box, null, boxStack, 2);
            }
        }
    }

    // �̰� �������� ���� ���� �ľ��ؼ� �ٸ� �����⸦ ã�ƾ��ϱ� ������
    // �׼� ����ϰ� ���� ī��Ʈ�� �׻� ������Ʈ ���ִ°���
    public int GetStackCount() => boxStack.Count;
    // �̰͵� �������� ���� Ÿ�� ��ũ ����Ʈ ã���ִ°�
    // ���� �������� �̻��ϴ� ������ �� �κ� ���� �ش� ��迡 ��ũ ����Ʈ��
    // �ڽ� 0���� �ֳ� �� Ȯ���غ� 
    public Transform GetTransform() => transform.GetChild(0).transform;
    // �̰� ���� �ʿ� ���°� ������ ���� ������ �־ �� �������
    // ���߿� �� Ȯ���ؼ� �ʿ���� ������ ��������
    public int GetTypeNum() => 1;

    // �� �κ��� �ٷ� ���� �߰� �κ���
    // �����ϰ� ���� �ٽ� �ҷ��� �� �̻��ϴ� ������ �� �κ� Ȯ�� �ʿ�
    public void ObjectDataSave()
    {
        if (bsType == BoxStorageType.ChuruStorage)
            DataManager.Instance.baseCost.objectData["churuStorageStackCount"]  = boxStack.Count;
        else
            DataManager.Instance.baseCost.objectData["packagingBoxStorageStackCount"] = boxStack.Count;
    }
}
