using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : NonPersistantSingleton<GameManager>
{
  public static int score = 0;
  public static int errors_counter = 0;
  public static Recipe[] all_recipes;
  public int recipesUnblocked = 5;
  // cuántos clientes debe atender antes de desbloquear las próximas 5 recetas
  public int[] levelMilestones = new int[] {
    3,
    3,
    2,
    1,
    1
  };
  public int milestoneScopeClientsServed = 0;
  public int currentMilestone = 0;

  public static Camera camera_reference;

  void OnEnable () {
    Client.onAnyClientEnded += HandleClientEnded;
  }

    private void Start()
    {
      Events.OnAddMistake += AddMistake;
      // las recetas no se ordenaban correctamente xD
      // la lista quedaba { Recipe1, Recipe10, Recipe11, ... }
      // ésto ordena la lista de acuerdo al número que llevan al final.
      List<Recipe> sortedRecipes = new List<Recipe>(Resources.LoadAll<Recipe>(""));
      sortedRecipes.Sort((Recipe a, Recipe b) => {

          int x =
          int.Parse(a.name.Substring("Recipe".Length, a.name.Length - "Recipe".Length));

          int y =
          int.Parse(b.name.Substring("Recipe".Length, b.name.Length - "Recipe".Length));
          return x.CompareTo(y);
          });
      all_recipes = sortedRecipes.ToArray();
        camera_reference = Camera.main;
        // print("Recipes loaded:");
        // foreach (var recipe in all_recipes)
        // {
        //     print(recipe.NameOfDrink);
        // }
    }

    public void AddMistake(int amount)
    {
      errors_counter += amount;
      Events.OnUIUpdateRating(1.0f-(errors_counter / 10.0f));
    }
    public static Recipe GetRandomDrink()
    {
      Instance.recipesUnblocked =
        Mathf.Clamp(Instance.recipesUnblocked, 0, all_recipes.Length-1);

      return all_recipes[Random.Range(0, Instance.recipesUnblocked)];
    }

  public void HandleClientEnded (Client client) {
    if (client.wasOk) {
      milestoneScopeClientsServed++;
    }

    if (milestoneScopeClientsServed >= levelMilestones[currentMilestone]) {
      milestoneScopeClientsServed = 0;
      currentMilestone = (currentMilestone + 1) % levelMilestones.Length;
      recipesUnblocked += 5;
      print("DING! DING! level up");
    }
  }
}
