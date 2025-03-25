using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class SandwichMechanic : MonoBehaviour
{
    public GameObject sandwich, selectedMenu;
    List<GameObject> ingredients = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addIngredients(Button button)
    {
        selectedMenu = Resources.Load<GameObject>(button.name);

        if (ingredients == null || ingredients.Count == 0)
        {
            GameObject bottomBun = sandwich.transform.Find("Bread").gameObject;
            float bottomBunPos = bottomBun.GetComponent<RectTransform>().anchoredPosition.y;
            float bottomBunHeight = bottomBun.GetComponent<RectTransform>().rect.height;

            GameObject ingredient = GameObject.Instantiate(selectedMenu);
            ingredient.name = selectedMenu.name;
            ingredient.transform.SetParent(sandwich.transform);
            ingredient.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
            ingredient.transform.localPosition = new Vector3(0, bottomBunPos + (bottomBunHeight / 2), 0);
            ingredients.Add(ingredient);
        }
        else
        {
            float ingredientPos = ingredients[ingredients.Count - 1].GetComponent<RectTransform>().anchoredPosition.y;
            float ingredientHeight = ingredients[ingredients.Count - 1].GetComponent<RectTransform>().rect.height;

            GameObject ingredient = GameObject.Instantiate(selectedMenu);
            ingredient.name = selectedMenu.name;
            ingredient.transform.SetParent(sandwich.transform);
            ingredient.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
            ingredient.transform.localPosition = new Vector3(0, ingredientPos + ingredientHeight, 0);
            ingredients.Add(ingredient);
        }

        RectTransform lastRect = ingredients[ingredients.Count - 1].GetComponent<RectTransform>();

        float lastIngredientPos = lastRect.anchoredPosition.y;
        float lastIngredientHeight = lastRect.rect.height;

        GameObject topBun = sandwich.transform.Find("Bun").gameObject;
        RectTransform topBunRect = topBun.GetComponent<RectTransform>();
        float topBunHeight = topBunRect.rect.height;

        topBunRect.anchoredPosition = new Vector2(0, lastIngredientPos + lastIngredientHeight + (topBunHeight / 2));
    }

    public void DebugIngredients()
    {
        foreach (GameObject go in ingredients)
        {
            print(go.name);
        }
    }
}
