using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public enum PlayerType
{
    Joystick,
    None
}
public class Player : MonoBehaviour, IObjectDataSave
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject cart;
    [SerializeField] private Transform cartTransform;

    public List<Employee> employee;

    private PlayerType pT = PlayerType.Joystick;
    public PlayerType PT
    {
        get { return pT; }
        set { pT = value; }
    }

    private CharacterController cc;
    private Animator animator;
    private Camera mainCamera;
    private BaseCost baseCost;
    public float MaxObjStackCount 
    {
        get { return baseCost.playerData["maxObjStackCount"]; }
        set { baseCost.playerData["maxObjStackCount"] = value; }
    }
    public float BaseSpeed
    {
        get { return baseCost.playerData["baseSpeed"]; }
        set { baseCost.playerData["baseSpeed"] = value; }
    }
    public float CartSpeed
    {
        get { return baseCost.playerData["baseCartSpeed"]; }
        set { baseCost.playerData["baseCartSpeed"] = value; }
    }
    public float Gold
    {
        get { return baseCost.playerData["gold"]; }
        set { baseCost.playerData["gold"] = value; }
    }
    public float GoldPerBox
    {
        get { return baseCost.playerData["goldPerBox"]; }
        set { baseCost.playerData["goldPerBox"] = value; }
    }

    private Stack<GameObject> ingredientStack = new Stack<GameObject>();
    public Stack<GameObject> IngredientStack
    {
        get { return ingredientStack; }
        set { ingredientStack = value; }
    }

    private Stack<GameObject> churuStack = new Stack<GameObject>();
    public Stack<GameObject> ChuruStack
    {
        get { return churuStack; }
        set { churuStack = value; }
    }

    private Stack<GameObject> boxStack = new Stack<GameObject>();
    public Stack<GameObject> BoxStack
    {
        get { return boxStack; }
        set { boxStack = value; }
    }

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        baseCost = DataManager.Instance.baseCost;
        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        Vibration.Init();
        DataManager.Instance.AddObjStackCountList(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(pT == PlayerType.Joystick)
            JoystickMove();

        OnCart();
    }

    private void JoystickMove()
    {
        if (joystick == null)
        {
            joystick = FindObjectOfType<Joystick>();
            return;
        }

        float joyX = joystick.Horizontal;
        float joyZ = joystick.Vertical;

        Vector3 moveDirection = new Vector3(joyX, 0, joyZ);

        if (moveDirection != Vector3.zero)
        {
            Vector3 cameraForward = mainCamera.transform.forward;
            cameraForward.y = 0;
            cameraForward.Normalize();

            Vector3 cameraRight = mainCamera.transform.right;
            cameraRight.y = 0;
            cameraRight.Normalize();

            Vector3 adjustedDirection = (moveDirection.z * cameraForward + moveDirection.x * cameraRight).normalized;

            float currentSpeed = animator.GetFloat("Blend") == 1? CartSpeed : BaseSpeed;
            animator.SetBool("isMove", true);

            cc.Move(adjustedDirection * currentSpeed * Time.deltaTime);

            Quaternion newRotation = Quaternion.LookRotation(adjustedDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * 10f);
        }
        else
        {
            animator.SetBool("isMove", false);
        }

        Vector3 currentPosition = transform.position;
        currentPosition.y = 0;
        transform.position = currentPosition;
    }

    public void OnCart()
    {
        if (ingredientStack.Count <= 0 && boxStack.Count <= 0 && churuStack.Count <= 0)
        {
            cart.transform.DOScale(0, 0.2f);
            animator.SetFloat("Blend", 0);
        }
        else
        {
            cart.transform.DOScale(1, 0.2f);
            animator.SetFloat("Blend", 1);
        }
    }

    public void DoBoxPackagingAnimationPlayer()
    {
        transform.rotation = Quaternion.Euler(0, -90f, 0);
        animator.SetLayerWeight(1, 1);
    }

    public void StopBoxPackagingAnimationPlayer()
    {
        animator.SetLayerWeight(1, 0);
    }

    #region ���� �ְ� �޴� �ڵ��
    // �̰� ��� �޴� �ڵ�
    public void TakeObject(IngredientMaker im)
    {
        if (im.ChuruStack.Count > 0 && MaxObjStackCount > ingredientStack.Count && boxStack.Count <= 0 && churuStack.Count <= 0)
        {
            Utility.ObjectDrop(cartTransform, null, im.ChuruStack, ingredientStack, 1);
            Vibration.VibratePop();
        }
    }

    // �̰� �����̾ �ø��� �ڵ�
    public void GiveObject(ConveyorBelt cb)
    {
        if (ingredientStack.Count > 0)
        {
            Utility.ObjectDrop(cb.IngredientStorage, null, ingredientStack, cb.CbStack, 1);
            Vibration.VibratePop();
        }
    }

    // �̰� ����̳� �ڽ� ���� �� ���� �ڵ� bool ���� ���� ��� �������� �ڽ� �������� �޶���
    public void GiveObject(BoxStorage bs, bool isChuru)
    {
        Stack<GameObject> newStack = isChuru ? churuStack : boxStack;
        Stack<GameObject> checkStack = isChuru ? boxStack : churuStack;

        if (bs.BoxStack.Count > 0 && MaxObjStackCount > newStack.Count && ingredientStack.Count <= 0 && checkStack.Count <= 0)
        {
            Utility.ObjectDrop(cartTransform, null, bs.BoxStack, newStack, 1);
            Vibration.VibratePop();
        }
    }

    // �̰� ����� �ڽ� �����ϴ� ������ �ű� �� ���� �ڵ�
    public void GiveObject(BoxPackaging bp)
    {
        if (churuStack.Count > 0)
        {
            Utility.ObjectDrop(bp.churuStorageParent, null, churuStack, bp.ChuruStorage, 4);
            Vibration.VibratePop();
        }
    }

    // �̰� Ʈ���� �ڽ� �ű�� �ڵ�
    public void GiveObject(Truck tr)
    {
        if (boxStack.Count > 0 && ingredientStack.Count <= 0)
        {
            Utility.ObjectDrop(tr.BoxLoadingTransform, null, boxStack, tr.BoxStack, 3);
            tr.BoxCountTextUpdate();
            Vibration.VibratePop();
        }
    }
    #endregion

    // �̰� ������� �������� ������ �÷��̾ ������ �־
    // ���� ������ �� �÷��̾�� ������ �Ŵ����� ó���ϴ� �ڵ�
    public void ObjectDataSave()
    {
        foreach (var item in employee)
        {
            if(!baseCost.employeeList.Contains(item.name))
                baseCost.employeeList.Add(item.name);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ingredient"))
        {
            Debug.Log("����");
            Rigidbody rd = collision.transform.GetComponent<Rigidbody>();
            if (rd != null)
                Destroy(collision.transform.GetComponent<Rigidbody>());
            Utility.ObjectDrop(cartTransform, collision.gameObject, null, ingredientStack, 0);
            Vibration.VibratePop();
        }
    }
}
