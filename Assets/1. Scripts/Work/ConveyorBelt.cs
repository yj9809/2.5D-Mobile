using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public enum ConveyorBeltType { Ingredient, Churu }

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float placeObjectTime = 3f;
    [SerializeField] private Vector3 direction = Vector3.forward;
    [TabGroup("Transform"), SerializeField] private Transform ingredientStorage;
    [TabGroup("Transform"), SerializeField] private Transform onBelt;
    [TabGroup("GameObj"), SerializeField] private BoxStorage boxStorage;
    [TabGroup("BreakEvent"),SerializeField] private Image eventGauge;
    [TabGroup("BreakEvent"), SerializeField] private Image displayImg;
    [TabGroup("BreakEvent"), SerializeField] private Sprite[] displayImgArray;
    [TabGroup("BreakEvent"), SerializeField] private GameObject breakEventPoint;
    [TabGroup("BreakEvent"), ProgressBar(0, 100), SerializeField] private float currentFill;
    [EnumToggleButtons, SerializeField] private ConveyorBeltType conveyorBeltType;

    private GameManager gm;

    public float PlaceObjectTime
    {
        get { return placeObjectTime; }
        set { placeObjectTime = value; }
    }

    private float breakDownProb = 0.05f;
    public float BreakDownProb
    {
        get { return breakDownProb; }
        set { breakDownProb = value; }
    }
    private bool isOn = true;
    [SerializeField] private bool isBreakDown = false;
    public Transform IngredientStorage
    {
        get { return ingredientStorage; }
    }

    private Stack<GameObject> cbStack = new Stack<GameObject>();
    public Stack<GameObject> CbStack
    {
        get { return cbStack; }
        set { cbStack = value; }
    }
    private void Start()
    {
        gm = GameManager.Instance;

        if(transform.parent.GetChild(0).GetComponent<WorkPoint>())
            gm.cbTrans.Add(transform.parent.GetChild(0));

        StartCoroutine(PlaceObject());
        StartCoroutine(DisplayImgChange());
        eventGauge.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(boxStorage.BoxStack.Count >= 40)
        {
            isOn = false;
        }
        else
        {
            isOn = true;
        }
    }
    private IEnumerator PlaceObject()
    {
        while (true)
        {
            float randomValue = Random.value;
            yield return new WaitForSeconds(placeObjectTime);
            if(cbStack.Count > 0 && randomValue < breakDownProb)
            {
                BreakDownEvent();
            }

            if (cbStack.Count > 0 && isOn && !isBreakDown)
            {
                OnConveyorObj();
            }
        }
    }
    private IEnumerator DisplayImgChange()
    {
        while (!isBreakDown && conveyorBeltType == ConveyorBeltType.Churu)
        {
            if (displayImg.sprite != displayImgArray[0])
                displayImg.sprite = displayImgArray[0];
            else
                displayImg.sprite = displayImgArray[1];
            yield return new WaitForSeconds(2f);
        }
    }
    // �������� ���� ���� �Լ��� ���׽��ϴ�.
    
    private void OnConveyorObj()
    {
        PushStack();
        GameObject newChuru = cbStack.Pop();
        newChuru.transform.position = onBelt.position;
        newChuru.transform.SetParent(onBelt);

        if (!newChuru.GetComponent<Rigidbody>())
        {
            newChuru.AddComponent<Rigidbody>();
            newChuru.GetComponent<Rigidbody>().freezeRotation = true;
        }
    }
    // ���� �̺�Ʈ�� ���� �׽�Ʈ �Լ����Դϴ�.
    private void BreakDownEvent()
    {
        isBreakDown = true;
        StopAllCoroutines();
        breakEventPoint.SetActive(true);
        displayImg.sprite = displayImgArray[2];
    }

    public void BreakDownSolution()
    {
        eventGauge.gameObject.SetActive(true);
    }

    public void BreakDownSolutionClear()
    {
        isBreakDown = false;
        eventGauge.gameObject.SetActive(false);
        StartCoroutine(PlaceObject());
        StartCoroutine(DisplayImgChange());
    }
    // �ӽ÷� ���� ���� ���� �߻� ���� �ذ� �ڵ�.
    // �����̾� ��Ʈ �ű� ������ ���� �ʱ�ȭ �� �ڽ� ������Ʈ���� �ٽ� Ǫ���ϴ� �ڵ�� ����, ���� �޸� ������ �ٸ� ���� �߻� �� �� ������ ����.
    // ���� ���� ��� ����� �ٽ� ���� ����.
    private void PushStack()
    {
        if(ingredientStorage.childCount != cbStack.Count)
        {
            Debug.Log("���� ����");
            cbStack.Clear();
            foreach (Transform item in ingredientStorage)
            {
                cbStack.Push(item.gameObject);
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        // ������ ���� �׿��� ���� ����ؼ� ���ߴ� �ڵ� �ۼ�. (�׽�Ʈ)
        speed = isOn && !isBreakDown ? 5 : 0;
        // ������ ���� ���̸� ���߰� ������ �������� ��� �ٽ� �۵� Ȯ��.

        if (rb != null)
        {
            rb.velocity = speed * direction;
        }
    }
}
