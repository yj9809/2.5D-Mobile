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
        #region 플레이어 중심 화살표 (주석처리)
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

        #region 타겟 위치 화살표
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
        guideText.text = $"재료 보관소로 이동 하자 !";

        if (player.IngredientStack.Count > 0)
        {
            ToNextStep();
        }
    }

    private void _Step1()
    {
        SetActiveTarget(1);
        guideText.text = $"재료를 컨베이어 벨트로 옮기자 !";

        if (player.IngredientStack.Count <= 0)
        {
            ToNextStep();
        }
    }

    private void _Step2()
    {
        SetActiveTarget(2);
        guideText.text = $"완성된 츄룹을 포장작업대 창고로 옮기자 !";

        if (player.ChuruStack.Count > 0)
        {
            ToNextStep();
        }
    }

    private void _Step3()
    {
        SetActiveTarget(3);
        guideText.text = $"츄룹 창고 이동 작업\n{boxPackaging.ChuruStorage.Count} / 5";

        if (player.ChuruStack.Count <= 0 && boxPackaging.ChuruStorage.Count >= 5)
        {
            ToNextStep();
        }
    }

    private void _Step4()
    {
        SetActiveTarget(4);
        guideText.text = $"포장작업대에서\n박스포장을 진행하자 !";

        if (boxStorage.bsType == BoxStorageType.BoxStorage && boxStorage.BoxStack.Count >= 1)
        {
            ToNextStep();
        }
    }

    private void _Step5()
    {
        truck.gameObject.SetActive(true);
        SetActiveTarget(5);
        guideText.text = $"완성한 박스를\n트럭에 싣자 !";

        if (player.BoxStack.Count > 0)
        {
            ToNextStep();
        }
    }

    private void _Step6()
    {
        SetActiveTarget(6);
        guideText.text = $"박스 트럭 상차 작업\n{truck.BoxStack.Count} / 5";

        if (player.BoxStack.Count <= 0 && truck.BoxStack.Count >= 5)
        {
            ToNextStep();
        }
    }

    private void _Step7()
    {
        SetActiveTarget(7);
        guideText.text = $"돈을 모아 직원을 고용하자 !\n{baseCost.playerGold} / 50";

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
