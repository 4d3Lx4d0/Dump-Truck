using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class IngredientDisplay : MonoBehaviour, IPointerClickHandler 
{
    public Ingredient ingredient;

    public Image sprite;
    public TMP_Text stock;
    public Slider restockSlider;


    public int curStock;
    public int curRestock;

    public UnityEvent<Ingredient, int> restockEvent;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.name = ingredient.name;
        sprite.sprite = ingredient.sprite;
        stock.text = ingredient.maxStock.ToString() +  "/" + ingredient.maxStock.ToString();
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
        restockSlider.value = curRestock; 
        stock.text = curStock +  "/" + ingredient.maxStock.ToString();
    }

    public int GetCurStock()
    {
        return curStock;
    }

    public void ResetStock()
    {
        curStock = ingredient.maxStock;
        curRestock = ingredient.maxRestock;
        stock.text = curStock + "/" + ingredient.maxStock.ToString();
        restockSlider.value = curRestock;
    }

    public void AddStock(int amount)
    {
        curStock += amount;
        if (curStock > ingredient.maxStock)
            curStock = ingredient.maxStock;

        stock.text = curStock + "/" + ingredient.maxStock.ToString();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)UseRestock();
    }

}
