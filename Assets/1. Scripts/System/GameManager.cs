using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Player p;
    public Player P
    {
        get
        {
            if(p == null)
            {
                p = FindObjectOfType<Player>();
            }
            return p;
        }
    }
}
