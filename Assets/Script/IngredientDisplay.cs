using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class IngredientDisplay : MonoBehaviour
{
    public Ingredient ingredient;

    public Image sprite;
    public TMP_Text stock;
    public TMP_Text restock;


    public int curStock;
    public int curRestock;

    public UnityEvent<Ingredient, int> restockEvent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.name = ingredient.name;
        sprite.sprite = ingredient.sprite;
        stock.text = ingredient.maxStock.ToString() +  "/" + ingredient.maxStock.ToString();
        restock.text = ingredient.maxRestock.ToString() +  "/" + ingredient.maxRestock.ToString();
        curRestock = ingredient.maxRestock;
        curStock = ingredient.maxStock;
    }

    public void UseStock()
    {
        if (curStock == 0) return;
        curStock--;
        stock.text = curStock +  "/" + ingredient.maxStock.ToString();
    }

    public void UseRestock()
    {
        if (curRestock == 0) return;
        curRestock--;
        if(curStock>0)restockEvent?.Invoke(ingredient, curStock);
        curStock = ingredient.maxStock;
        restock.text = curRestock +  "/" + ingredient.maxRestock.ToString();
        stock.text = curStock +  "/" + ingredient.maxStock.ToString();
    }

    public int GetCurStock()
    {
        return curStock;
    }

}
