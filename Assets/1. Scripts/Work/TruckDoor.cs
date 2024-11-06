using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckDoor : MonoBehaviour
{
    [SerializeField] private Truck truck;
    [SerializeField] private Collider collider;
    [SerializeField] private Animator door;

    public void DoorOpen()
    {
        if(truck != null)
            truck.DoorOpen();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(collider != null && other.GetComponent<Player>())
        {
            door.SetBool("Open", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (collider != null && other.GetComponent<Player>())
        {
            door.SetBool("Open", false);
        }
    }
}
