using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPoint : MonoBehaviour
{
    [SerializeField] private ChuruManager cm;
    [SerializeField] private ConveyorBelt cb;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        Player p = other.GetComponent<Player>();
        if (p != null)
        {
            if(cm != null )
            {
                p.TakeObject(cm);
            }

            if(cb != null)
            {
                Debug.Log("½ÇÇà");
                p.GiveObject(cb);
            }
        }
    }
}
