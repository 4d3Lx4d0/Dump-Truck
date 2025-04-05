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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetLeftovers(Ingredient ingredient, int leftoverStock)
    {
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

        if(curStock == 0)
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
