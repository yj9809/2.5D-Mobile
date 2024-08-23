using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private Vector3 direction = Vector3.forward;
    [SerializeField] private Transform ingredientStorage;
    [SerializeField] private Transform onBelt;
    [SerializeField] private BoxStorage boxStorage;
    [SerializeField] private Button breakDownEventButton;
    [SerializeField] private Image eventGauge;

    private float placeObjectTime = 3f;
    public float PlaceObjectTime
    {
        get { return placeObjectTime; }
        set { placeObjectTime = value; }
    }

    private float breakDownProb = 0.1f;
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
        StartCoroutine(PlaceObject());
        breakDownEventButton.onClick.AddListener(BreakDownEventButton);
        breakDownEventButton.gameObject.SetActive(false);
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
                yield break;
            }

            if (cbStack.Count > 0 && isOn && !isBreakDown)
            {
                OnConveyorObj();
            }
        }
    }
    // �������� ���� ���� �Լ��� ���׽��ϴ�.
    private void BreakDownEvent()
    {
        isBreakDown = true;
        breakDownEventButton.gameObject.SetActive(true);
    }
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
    private void BreakDownEventButton()
    {
        GameManager.Instance.P.PlayerAutoMove(transform.GetChild(0), BreakDownSolution);
    }
    private void BreakDownSolution()
    {
        breakDownEventButton.gameObject.SetActive(false);
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
