using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public enum ConveyorBeltType { Ingredient, Churu }

public class ConveyorBelt : MonoBehaviour, IItemTransferEndpoint
{
    public bool TryTransfer(CarrierInventory inventory, Transform carryParent)
    {
        return ItemTransferUtility.TryMove(inventory, input, ingredientStorage);
    }

    [TabGroup("Setting"), SerializeField] private float speed = 3f;
    [TabGroup("Setting"), SerializeField] private float placeObjectTime = 3f;
    [TabGroup("Setting"), SerializeField] private Vector3 direction = Vector3.forward;

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

    private float breakDownProb = Churub.Core.BalanceTable.BreakdownProbability;
    public float BreakDownProb
    {
        get { return breakDownProb; }
        set { breakDownProb = value; }
    }

    private float nonBreakDownTime = Churub.Core.BalanceTable.BreakdownProtection;
    private bool breakdownUnlocked;

    private bool isOn = true;
    private bool isBreakDown = false;

    [TabGroup("Transform"), SerializeField] private Transform ingredientStorage;
    private readonly ItemBuffer input = new ItemBuffer(int.MaxValue, ItemType.Ingredient);
    private readonly Dictionary<Rigidbody, Item> itemsOnBelt = new Dictionary<Rigidbody, Item>();
    private readonly List<Rigidbody> itemsToRemove = new List<Rigidbody>();

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
        if(boxStorage.IsFull)
        {
            isOn = false;
        }
        else
        {
            isOn = true;
        }

        var state = DataManager.Instance.baseCost;
        bool eligible = state.EmployeeAddCount >= 2 && Churub.Core.BalanceTable.Lines(state) >= 2;
        if (!breakdownUnlocked && eligible)
        {
            breakdownUnlocked = true;
            nonBreakDownTime = Churub.Core.BalanceTable.BreakdownProtection;
        }
        if (breakdownUnlocked && nonBreakDownTime >= 0) nonBreakDownTime -= Time.deltaTime;
    }

    private IEnumerator PlaceObject()
    {
        while (true)
        {
            float randomValue = Random.value;
            yield return new WaitForSeconds(placeObjectTime);
            if(breakdownUnlocked && input.Count > 0 && randomValue < breakDownProb && nonBreakDownTime <=0)
            {
                BreakDownEvent();
            }

            if (input.Count > 0 && isOn && !isBreakDown)
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

    // 가독성을 위해 따로 함수로 빼뒀습니다.
    private void OnConveyorObj()
    {
        if (onBelt == null || !input.TryPop(out var item)) return;
        GameObject newChuru = item.gameObject;
        newChuru.transform.DOKill();
        newChuru.transform.position = onBelt.position;
        newChuru.transform.SetParent(onBelt);

        if (!newChuru.GetComponent<Rigidbody>())
        {
            newChuru.AddComponent<Rigidbody>();
            newChuru.GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    // 고장 이벤트를 위한 테스트 함수들입니다.
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
        nonBreakDownTime = Churub.Core.BalanceTable.BreakdownProtection;
        StartCoroutine(PlaceObject());
        StartCoroutine(DisplayImgChange());
    }

    private void FixedUpdate()
    {
        float currentSpeed = isOn && !isBreakDown ? speed : 0f;
        itemsToRemove.Clear();
        foreach (var pair in itemsOnBelt)
        {
            Rigidbody rb = pair.Key;
            Item item = pair.Value;
            if (rb == null || item == null || !item.isActiveAndEnabled || item.IsStored)
            {
                itemsToRemove.Add(rb);
                continue;
            }

            if (currentSpeed > 0f && rb.IsSleeping())
                rb.WakeUp();
            rb.linearVelocity = currentSpeed * direction;
        }

        foreach (var rb in itemsToRemove)
            itemsOnBelt.Remove(rb);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.rigidbody;
        if (rb != null && rb.TryGetComponent<Item>(out var item) && !item.IsStored)
            itemsOnBelt[rb] = item;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.rigidbody != null)
            itemsOnBelt.Remove(collision.rigidbody);
    }
}
