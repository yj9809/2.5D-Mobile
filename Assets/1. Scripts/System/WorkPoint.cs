using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkPoint : MonoBehaviour
{
    [SerializeField] private IngredientMaker _ingredientMaker;
    [SerializeField] private ConveyorBelt _conveyorBelt;
    [SerializeField] private BoxStorage _boxStorage;
    [SerializeField] private Delivery _delivery;
    [SerializeField] private TestCar testCar;

    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        Player p = other.GetComponent<Player>();
        Employee e = other.GetComponent<Employee>();
        if (p != null || e != null)
        {
            if(_ingredientMaker != null )
            {
                Debug.Log("실행 : 재료");
                if (p != null) p.TakeObject(_ingredientMaker);
                if (e != null) e.TakeObject(_ingredientMaker);
            }

            if(_conveyorBelt != null)
            {
                Debug.Log("실행 : 컨베이어 벨트");
                if (p != null) p.GiveObject(_conveyorBelt);
                if (e != null) e.GiveObject(_conveyorBelt);
            }

            if(_boxStorage != null)
            {
                Debug.Log("실행 : 박스");
                p.GiveObject(_boxStorage);
            }

            if (_delivery != null)
            {
                p.GiveObject(_delivery);
            }
            if(testCar != null)
            {
                Debug.Log("실행 : 트럭");
                p.GiveObject(testCar);
            }
        }
    }
}
