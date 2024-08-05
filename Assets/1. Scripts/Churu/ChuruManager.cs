using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ChuruManager : MonoBehaviour
{
    [Title("Ingredient")] // Àç·á
    [TabGroup("Ingredient", "Ingredient")] [SerializeField] private GameObject ingredientPrefab;
    [TabGroup("Ingredient", "Ingredient")] [SerializeField] private Transform ingredientSpawnPoint;
    [TabGroup("Ingredient", "Ingredient")] [SerializeField] private float ingredientSpawnTime = 2f;
    [TabGroup("Ingredient", "Ingredient")] [SerializeField] private int maxIngredient = 10;

    private float spawnTimer = 0f;
    private Stack<GameObject> ingredients = new Stack<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SpawnIngredient();
    }

    private void SpawnIngredient()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= ingredientSpawnTime)
        {
            if (ingredients.Count < maxIngredient)
            {
                GameObject ingredient = Instantiate(ingredientPrefab);
                Vector3 pos = new Vector3(ingredientSpawnPoint.position.x, ingredientSpawnPoint.position.y + ObjectRendererCheck(ingredient) * ingredients.Count, ingredientSpawnPoint.position.z);
                ingredient.transform.position = pos;
                Debug.Log("Spawn Ingredient !");
                ingredients.Push(ingredient);
                ingredient.transform.SetParent(ingredientSpawnPoint);
            }
            spawnTimer = 0f;
        }
        else if(ingredients.Count >= maxIngredient)
        {
            Debug.Log("Ingredient is Full !");
        }
    }

    private float ObjectRendererCheck(GameObject obj)
    {
        Renderer ren = obj.GetComponent<Renderer>();

        return ren.bounds.size.y;
    }
}
