using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkPoint : MonoBehaviour
{
    [SerializeField] private ChuruManager cm;
    [SerializeField] private ConveyorBelt cb;
    [SerializeField] private BoxStorage bs;
    // Start is called before the first frame update
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
                p.GiveObject(cb);
            }

            if(bs != null)
            {
                p.GiveObject(bs);
            }
        }
    }
}
