using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private Vector3 direction = Vector3.forward;
    [TabGroup("Transform"), SerializeField] private Transform ingredientStorage;
    [TabGroup("Transform"), SerializeField] private Transform onBelt;
    [TabGroup("GameObj"), SerializeField] private BoxStorage boxStorage;
    [TabGroup("BreakEvent"),SerializeField] private Image eventGauge;
    [TabGroup("BreakEvent"), SerializeField] private Image displayImg;
    [TabGroup("BreakEvent"), SerializeField] private Sprite[] displayImgArray;

    private GameManager gm;

    private float placeObjectTime = 3f;
    public float PlaceObjectTime
    {
        get { return placeObjectTime; }
        set { placeObjectTime = value; }
    }

    private float breakDownProb = 0f;
    public float BreakDownProb
    {
        get { return breakDownProb; }
        set { breakDownProb = value; }
    }
    private bool isOn = true;
    private bool isBreakDown = false;
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
        gm.cbTrans.Add(transform.parent.GetChild(0));
        StartCoroutine(PlaceObject());
        StartCoroutine(DisplayImgChange());
        eventGauge.gameObject.SetActive(false);
        //�׽�Ʈ��
        UIManager.Instance.SetConveyorBelt(this);
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
            yield return new WaitForSeconds(placeObjectTime);
            if(cbStack.Count > 0 && Random.value < breakDownProb)
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
        while (true)
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
        UIManager.Instance.ShowBreakDownEventUI();
        displayImg.sprite = displayImgArray[2];
    }
    
    public void BreakDownSolution()
    {
        UIManager.Instance.HideBreakDownEventUI();
        eventGauge.gameObject.SetActive(true);
        SolutionInProgress();
    }
    private void SolutionInProgress()
    {
        Image eventGaugeFill = eventGauge.transform.GetChild(0).GetComponent<Image>();
        eventGaugeFill.DOFillAmount(1f, 3f).SetEase(Ease.Linear).OnComplete(() =>
        {
            eventGauge.gameObject.SetActive(false);
            isBreakDown = false;
            GameManager.Instance.P.PT = PlayerType.Joystick;
            eventGaugeFill.fillAmount = 0;
            StartCoroutine(PlaceObject());
            StartCoroutine(DisplayImgChange());
        });
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
