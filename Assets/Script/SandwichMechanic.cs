using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SandwichMechanic : MonoBehaviour
{
    public GameObject sandwich, selectedMenu, ingredientPrefab;
    public Button resetButton;
    List<GameObject> ingredients = new List<GameObject>();
    public DragDrop dragDrop;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dragDrop.onDroppedCorrectly.AddListener(ResetSandwichSilently);
        resetButton.onClick.AddListener(ResetSandwich);
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void addIngredients(Button button)
    {
        selectedMenu = Resources.Load<GameObject>(button.name);
        if (button.transform.parent.name == "Ingredients")
        {
            if (button.GetComponent<IngredientDisplay>().GetCurStock() == 0) return;
        }
        else
        {
            if (button.GetComponent<LeftoversDisplay>().GetCurStock() == 0) return;
        }

        if (ingredients == null || ingredients.Count == 0)
        {
            GameObject bottomBun = sandwich.transform.Find("Bread").gameObject;
            float bottomBunPos = bottomBun.GetComponent<RectTransform>().anchoredPosition.y;
            float bottomBunHeight = bottomBun.GetComponent<RectTransform>().rect.height;

            GameObject ingredient = GameObject.Instantiate(selectedMenu);
            ingredient.name = selectedMenu.name;
            ingredient.transform.SetParent(sandwich.transform);
            ingredient.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0);
            ingredient.transform.localPosition = new Vector3(0, bottomBunPos, 0);
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
            ingredient.transform.localPosition = new Vector3(0, ingredientPos + (ingredientHeight / 3), 0);
            ingredients.Add(ingredient);
        }

        RectTransform lastRect = ingredients[ingredients.Count - 1].GetComponent<RectTransform>();

        float lastIngredientPos = lastRect.anchoredPosition.y;
        float lastIngredientHeight = lastRect.rect.height;

        GameObject topBun = sandwich.transform.Find("Bun").gameObject;
        RectTransform topBunRect = topBun.GetComponent<RectTransform>();
        float topBunHeight = topBunRect.rect.height;

        topBunRect.anchoredPosition = new Vector2(0, lastIngredientPos + (lastIngredientHeight + 10f));
        topBun.transform.SetAsLastSibling();
    }

    public void ResetSandwich()
    {
        InternalResetSandwich(true); // tombol reset
    }

    public void ResetSandwichSilently()
    {
        InternalResetSandwich(false); // drag ke customer
    }

    private void InternalResetSandwich(bool restoreStock)
    {
        Dictionary<string, int> ingredientCount = new Dictionary<string, int>();
        foreach (GameObject ingredient in ingredients)
        {
            string name = ingredient.name;
            if (ingredientCount.ContainsKey(name))
                ingredientCount[name]++;
            else
                ingredientCount[name] = 1;

            Destroy(ingredient);
        }

        ingredients.Clear();

        GameObject topBun = sandwich.transform.Find("Bun").gameObject;
        GameObject bottomBun = sandwich.transform.Find("Bread").gameObject;

        float bottomBunPos = bottomBun.GetComponent<RectTransform>().anchoredPosition.y;
        float bottomBunHeight = bottomBun.GetComponent<RectTransform>().rect.height;

        topBun.GetComponent<RectTransform>().anchoredPosition =
            new Vector2(0, bottomBunPos + bottomBunHeight - 8f);
        topBun.transform.SetAsLastSibling();

        if (!restoreStock) return;

        IngredientDisplay[] ingredientDisplays = FindObjectsOfType<IngredientDisplay>();
        foreach (IngredientDisplay display in ingredientDisplays)
        {
            if (ingredientCount.ContainsKey(display.name))
            {
                display.AddStock(ingredientCount[display.name]);
            }
        }
    }

}
