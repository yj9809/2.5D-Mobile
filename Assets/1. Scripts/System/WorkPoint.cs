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
                Debug.Log("���� : ���");
                if (p != null) p.TakeObject(_ingredientMaker);
                if (e != null) e.TakeObject(_ingredientMaker);
            }

            if(_conveyorBelt != null)
            {
                Debug.Log("���� : �����̾� ��Ʈ");
                if (p != null) p.GiveObject(_conveyorBelt);
                if (e != null) e.GiveObject(_conveyorBelt);
            }

            if(_boxStorage != null)
            {
                Debug.Log("���� : �ڽ�");
                p.GiveObject(_boxStorage);
            }

            if (_delivery != null)
            {
                p.GiveObject(_delivery);
            }
            if(testCar != null)
            {
                Debug.Log("���� : Ʈ��");
                p.GiveObject(testCar);
            }
        }
    }
}
