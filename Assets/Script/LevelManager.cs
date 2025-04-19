using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public int currentLevel = 1;
    public List<string> availableIngredients = new List<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            UpdateAvailableIngredients();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LevelUp()
    {
        if (currentLevel < 3)
        {
            currentLevel++;
            UpdateAvailableIngredients();

            // Reset reputasi setelah naik level (opsional)
            // FindObjectOfType<ReputationManager>().ReduceReputation(GetRequiredReputation());
        }
    }

    private void UpdateAvailableIngredients()
    {
        availableIngredients.Clear();

        // Level 1: 2 bahan dasar
        if (currentLevel == 1)
        {
            availableIngredients.Add("Lettuce");
            availableIngredients.Add("Tomato");
        }
        // Level 2: tambah 2 bahan (total 4)
        else if (currentLevel == 2)
        {
            availableIngredients.Add("Lettuce");
            availableIngredients.Add("Tomato");
            availableIngredients.Add("Bacon");
            availableIngredients.Add("Chicken");
        }
        // Level 3: semua bahan
        else if (currentLevel == 3)
        {
            availableIngredients.Add("Lettuce");
            availableIngredients.Add("Tomato");
            availableIngredients.Add("Bacon");
            availableIngredients.Add("Chicken");
            availableIngredients.Add("Cheese"); // Bahan baru di level 3
        }
    }

    public bool IsIngredientAvailable(string ingredientName)
    {
        return availableIngredients.Contains(ingredientName);
    }
}