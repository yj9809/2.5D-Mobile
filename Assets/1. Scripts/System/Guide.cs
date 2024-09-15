using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class Guide : MonoBehaviour
{
    [SerializeField] private RectTransform guideLine;
    [SerializeField] private TextMeshProUGUI guideText;

    [SerializeField] private GameObject[] targets;

    private BaseCost baseCost;
    private BoxPackaging boxPackaging;
    private BoxStorage boxStorage;
    private Truck truck;
    private Player player;

    void Start()
    {
        baseCost = DataManager.Instance.baseCost;
        player = GameManager.Instance.P;

        boxPackaging = FindObjectOfType<BoxPackaging>();
        boxStorage = GameObject.Find("BoxStorage").GetComponent<BoxStorage>();
        truck = GameObject.Find("Truck").GetComponent<Truck>();

        truck.gameObject.SetActive(false);
        SetTargetsActive(false);
    }

    void Update()
    {
        GuideLine();
        GuideStep();
    }

    private void GuideLine()
    {
        #region �÷��̾� �߽� ȭ��ǥ (�ּ�ó��)
        /*if (step < targets.Length)
        {
            Transform target = targets[step].transform;
            Vector3 direction = target.position - player.position;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            arrow.rotation = lookRotation * Quaternion.Euler(90, 0, 0);

            float distance = direction.magnitude;
            float guideScaleFactor = Mathf.Clamp(distance / 10f, 0.3f, 1f);
            canvas.localScale = new Vector3(guideScaleFactor, guideScaleFactor, guideScaleFactor);

            arrow.localPosition = Vector3.zero;
        }*/
        #endregion

        #region Ÿ�� ��ġ ȭ��ǥ
        if (DataManager.Instance.baseCost.guideStep < targets.Length)
        {
            Transform target = targets[DataManager.Instance.baseCost.guideStep].transform;
            Vector3 targetPosition = new Vector3(target.position.x, guideLine.position.y, target.position.z);

            guideLine.DOMove(targetPosition, 1f).SetEase(Ease.OutSine);
        }
        #endregion
    }

    private void GuideStep()
    {
        switch (DataManager.Instance.baseCost.guideStep)
        {
            case 0: _Step0(); break;
            case 1: _Step1(); break;
            case 2: _Step2(); break;
            case 3: _Step3(); break;
            case 4: _Step4(); break;
            case 5: _Step5(); break;
            case 6: _Step6(); break;
            case 7: _Step7(); break;
        }
    }

    private void _Step0()
    {
        SetActiveTarget(0);
        guideText.text = $"��� �����ҷ� �̵� ���� !";

        if (player.IngredientStack.Count > 0)
        {
            ToNextStep();
        }
    }

    private void _Step1()
    {
        SetActiveTarget(1);
        guideText.text = $"��Ḧ �����̾� ��Ʈ�� �ű��� !";

        if (player.IngredientStack.Count <= 0)
        {
            ToNextStep();
        }
    }

    private void _Step2()
    {
        SetActiveTarget(2);
        guideText.text = $"�ϼ��� ����� �����۾��� â��� �ű��� !";

        if (player.ChuruStack.Count > 0)
        {
            ToNextStep();
        }
    }

    private void _Step3()
    {
        SetActiveTarget(3);
        guideText.text = $"��� â�� �̵� �۾�\n{boxPackaging.ChuruStorage.Count} / 5";

        if (player.ChuruStack.Count <= 0 && boxPackaging.ChuruStorage.Count >= 5)
        {
            ToNextStep();
        }
    }

    private void _Step4()
    {
        SetActiveTarget(4);
        guideText.text = $"�����۾��뿡��\n�ڽ������� �������� !";

        if (boxStorage.bsType == BoxStorageType.BoxStorage && boxStorage.BoxStack.Count >= 1)
        {
            ToNextStep();
        }
    }

    private void _Step5()
    {
        truck.gameObject.SetActive(true);
        SetActiveTarget(5);
        guideText.text = $"�ϼ��� �ڽ���\nƮ���� ���� !";

        if (player.BoxStack.Count > 0)
        {
            ToNextStep();
        }
    }

    private void _Step6()
    {
        SetActiveTarget(6);
        guideText.text = $"�ڽ� Ʈ�� ���� �۾�\n{truck.BoxStack.Count} / 5";

        if (player.BoxStack.Count <= 0 && truck.BoxStack.Count >= 5)
        {
            ToNextStep();
        }
    }

    private void _Step7()
    {
        SetActiveTarget(7);
        guideText.text = $"���� ��� ������ ������� !\n{baseCost.playerGold} / 50";

        if (baseCost.baseEmployeeAddCount > 0)
        {
            guideText.text = $"-";
        }
    }

    private void ToNextStep()
    {
        DataManager.Instance.baseCost.guideStep++;
    }

    private void SetTargetsActive(bool isActive)
    {
        foreach (var target in targets)
        {
            target.SetActive(isActive);
        }
    }

    private void SetActiveTarget(int index)
    {
        if (index >= 0 && index < targets.Length)
        {
            targets[index].SetActive(true);
        }
    }
}
