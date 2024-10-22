using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckDoor : MonoBehaviour
{
    [SerializeField] private Truck truck;

    public void DoorOpen()
    {
        truck.DoorOpen();
    }
}
