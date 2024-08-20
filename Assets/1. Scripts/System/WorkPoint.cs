using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public enum WorkPointType
{
    Ingredient,
    ConveyorBelt_ingredient,
    BoxPackaging_churu,
    ChuruStorage,
    BoxStorage,
    TestCar,
    Packaging,
    Office
}
public class WorkPoint : MonoBehaviour
{
    [SerializeField] private IngredientMaker _ingredientMaker;
    [SerializeField] private ConveyorBelt _conveyorBelt;
    [SerializeField] private BoxStorage _boxStorage;
    [SerializeField] private BoxPackaging _boxPackaging;
    [SerializeField] private TestCar testCar;

    [Title("WorkPointType")]
    [EnumToggleButtons, SerializeField] private WorkPointType wpType;

    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        Player p = other.GetComponent<Player>();
        Employee e = other.GetComponent<Employee>();
        if (p != null || e != null)
        {
            switch(wpType)
            {
                case WorkPointType.Ingredient:
                    if (p != null) p.TakeObject(_ingredientMaker);
                    if (e != null) e.TakeObject(_ingredientMaker);
                    break;
                case WorkPointType.ConveyorBelt_ingredient :
                    if (p != null) p.GiveObject(_conveyorBelt);
                    //if (e != null) e.GiveObject(_conveyorBelt, false);
                    break;
                case WorkPointType.ChuruStorage:
                    p.GiveObject(_boxStorage, true);
                    break;
                case WorkPointType.BoxStorage:
                    p.GiveObject(_boxStorage, false);
                    break;
                case WorkPointType.BoxPackaging_churu:
                    if (p != null) p.GiveObject(_boxPackaging);
                    //if (e != null) e.GiveObject(_conveyorBelt, true);
                    break;
                case WorkPointType.TestCar:
                    p.GiveObject(testCar);
                    break;
                case WorkPointType.Packaging:
                    _boxPackaging.Packaging();
                    break;
                case WorkPointType.Office:
                    UIManager.Instance.ShowUpgradeUI();
                    break;
            }
        }
    }
}
