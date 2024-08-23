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
        //테스트용
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
    // 가독성을 위해 따로 함수로 빼뒀습니다.
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
    // 고장 이벤트를 위한 테스트 함수들입니다.
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
    // 임시로 스택 관련 버그 발생 문제 해결 코드.
    // 컨베이어 벨트 옮길 때마다 스택 초기화 후 자식 오브젝트들을 다시 푸쉬하는 코드로 변경, 추후 메모리 문제나 다른 문제 발생 할 수 있을꺼 같음.
    // 추후 좋은 방법 생기면 다시 수정 예정.
    private void PushStack()
    {
        if(ingredientStorage.childCount != cbStack.Count)
        {
            Debug.Log("스택 수정");
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

        // 스택이 가득 쌓였을 때를 대비해서 멈추는 코드 작성. (테스트)
        speed = isOn && !isBreakDown ? 5 : 0;
        // 스택이 가득 쌓이면 멈추고 스택이 없어졌을 경우 다시 작동 확인.

        if (rb != null)
        {
            rb.velocity = speed * direction;
        }
    }
}
