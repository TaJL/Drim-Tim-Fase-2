using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NonPersistantSingleton<GameManager>
{
  public static int score = 0;
  public static Recipe[] all_recipes;

  public static Camera camera_reference;
    private void Start()
    {
        all_recipes = Resources.LoadAll<Recipe>("");
        camera_reference = Camera.main;
        // print("Recipes loaded:");
        // foreach (var recipe in all_recipes)
        // {
        //     print(recipe.NameOfDrink);
        // }
    }

    public static Recipe GetRandomDrink()
    {
        return all_recipes[Random.Range(0, all_recipes.Length)];
    }
    // Update is called once per frame
    void Update()
    {

    }
}
