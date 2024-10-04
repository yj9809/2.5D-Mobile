using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GameManager.Instance.P;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Stack<GameObject> stack
                = player.IngredientStack.Count > 0 ? player.IngredientStack
                : player.ChuruStack.Count > 0 ? player.ChuruStack
                : player.BoxStack.Count > 0 ? player.BoxStack
                : null;
            if (stack != null)
            {
                ClearStack(stack);
            }
        }
    }

    private void ClearStack(Stack<GameObject> stack)
    {
        foreach (GameObject item in stack)
        {
            PoolingManager.Instance.ReturnObjecte(item);
        }
        stack.Clear();
    }
}
