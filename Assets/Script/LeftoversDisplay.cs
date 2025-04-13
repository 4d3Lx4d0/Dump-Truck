using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeftoversDisplay : MonoBehaviour
{
    public Ingredient ingredient;
    public Image container;
    public Image sprite;
    public TMP_Text stock;
    public int curStock;

    // Reference to the reputation manager
    private ReputationManager reputationManager;

    void Start()
    {
        // Find the ReputationManager in the scene
        reputationManager = FindObjectOfType<ReputationManager>();
        if (reputationManager == null)
        {
            Debug.LogError("No ReputationManager found in the scene!");
        }
    }

    public void SetLeftovers(Ingredient ingredient, int leftoverStock)
    {
        // Check if this is a duplicate leftover
        if (curStock > 0 && reputationManager != null)
        {
            reputationManager.ReduceReputation(2); // Reduce by 2 points for duplicate
            Debug.Log($"Do not Waste Food!");
        }

        // Set the new leftover values
        transform.name = ingredient.name;
        sprite.sprite = ingredient.sprite;
        stock.text = leftoverStock.ToString();
        curStock = leftoverStock;
        container.gameObject.SetActive(true);
        sprite.gameObject.SetActive(true);
        stock.gameObject.SetActive(true);
    }
    public void UseStock()
    {
        if (curStock == 0) return;
        curStock--;
        stock.text = curStock.ToString();

        if (curStock == 0)
        {
            container.gameObject.SetActive(false);
            sprite.gameObject.SetActive(false);
            stock.gameObject.SetActive(false);
        }
    }

    public int GetCurStock()
    {
        return curStock;
    }


}