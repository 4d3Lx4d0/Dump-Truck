using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CustomerBehavior : MonoBehaviour
{

    public Image targetImage;
    public Sprite[] sprites;

    public List<Order> order;

    public GameObject orderDisplay;

    public CustomerOrder customerOrder;

    public DragDrop dragDrop;

    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dragDrop.onDroppedCorrectly.AddListener(CheckSandwich);
        customerOrder = ReadOrderData.LoadData();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        if (sprites.Length > 0 && targetImage != null)
        {
            int randomIndex = Random.Range(0, sprites.Length);
            targetImage.sprite = sprites[randomIndex];
        }
        Order();
    }

    public void Order()
    {
        StartCoroutine(OrderCoroutine());
    }

    private IEnumerator OrderCoroutine()
    {
        int orderTotal = Random.Range(1, 3);

        yield return new WaitForSeconds(3f);

        for (int i = 0; i < orderTotal; i++)
        {
            int orderType = Random.Range(0, 2);
            TMP_Text prompt = orderDisplay.transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>();
            prompt.text = customerOrder.Level[0].Order[orderType].prompt;
            order.Add(customerOrder.Level[0].Order[orderType]);

            orderDisplay.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void CheckSandwich()
    {
        Dictionary<string, int> ingredientInSandwich = new Dictionary<string, int>();

        GameObject sandwich = transform.Find("Sandwich").gameObject;

        for (int i = 0; i < sandwich.transform.childCount; i++)
        {
            string childName = sandwich.transform.GetChild(i).name;

            if (ingredientInSandwich.ContainsKey(childName))
            {
                ingredientInSandwich[childName]++;
            }
            else
            {
                ingredientInSandwich[childName] = 1;
            }
        }

        for (int i = 0; i < order.Count; i++)
        {
            var orderItem = order[i];

            foreach (var ingredient in orderItem.ingredients)
            {
                ingredient.fit = false;

                if (ingredientInSandwich.ContainsKey(ingredient.name))
                {
                    if (ingredientInSandwich[ingredient.name] == ingredient.count)
                        ingredient.fit = true;
                }
            }

            bool allFit = true;
            foreach (var ingredient in orderItem.ingredients)
            {
                if (!ingredient.fit)
                {
                    allFit = false;
                    break;
                }
            }

            if (allFit)
            {
                Destroy(sandwich);
                orderDisplay.transform.GetChild(i).gameObject.SetActive(false);
                order.RemoveAt(i);
                break; // keluar loop karena list order berubah (hindari error)
            }
            else
            {
                Debug.Log($"Order index {i} GAGAL dipenuhi.");
                Destroy(sandwich);
                // reputasi turun
                break; // keluar juga supaya gak error
            }
        }
        
        if (order.Count <= 0)
        {
            animator.SetBool("Out", true);
            StartCoroutine(ResetCustomer());
        }
    }

    private IEnumerator ResetCustomer()
    {
        int interval = Random.Range(1, 3);

        yield return new WaitForSeconds(interval + 3);
        transform.gameObject.SetActive(false);
        transform.gameObject.SetActive(true);
    }
}
