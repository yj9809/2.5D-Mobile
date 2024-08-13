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

    private Player player;
    private void Start()
    {
        player = GameManager.Instance.P;
    }

    // Start is called before the first frame update
    private void OnTriggerStay(Collider other)
    {
        Player p = other.GetComponent<Player>();
        Employee e = other.GetComponent<Employee>();
        if (p != null || e != null)
        {
            if(_ingredientMaker != null )
            {
                if (p != null) p.TakeObject(_ingredientMaker);
                if (e != null) e.TakeObject(_ingredientMaker);
            }

            if(_conveyorBelt != null)
            {
                if (p != null) p.GiveObject(_conveyorBelt);
                if (e != null) e.GiveObject(_conveyorBelt);
            }

            if(_boxStorage != null)
            {
                p.GiveObject(_boxStorage);
            }

            if (_delivery != null)
            {
                p.GiveObject(_delivery);
            }
            if(testCar != null)
            {
                p.GiveObject(testCar);
            }
        }
    }
}
