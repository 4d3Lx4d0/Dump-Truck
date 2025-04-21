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

    int randomIndex = 0;
    private ReputationManager reputationManager;

    void Start()
    {
        dragDrop.onDroppedCorrectly.AddListener(CheckSandwich);
        customerOrder = ReadOrderData.LoadData();
        reputationManager = FindObjectOfType<ReputationManager>();
    }

    void OnEnable()
    {
        int[] allowedIndices = { 2, 5 }; 

        if (sprites.Length > 0 && targetImage != null)
        {
            List<int> validIndices = new List<int>();
            foreach (int index in allowedIndices)
            {
                if (index >= 0 && index < sprites.Length)
                    validIndices.Add(index);
            }

            if (validIndices.Count > 0)
            {
                randomIndex = validIndices[Random.Range(0, validIndices.Count)];
                targetImage.sprite = sprites[randomIndex];
            }
        }
        Order();
    }

    public void Order()
    {
        StartCoroutine(OrderCoroutine());
    }

    private IEnumerator OrderCoroutine()
    {
        // Wait until LevelManager is initialized
        while (LevelManager.Instance == null)
        {
            yield return null;
        }

        int orderTotal = Random.Range(1, LevelManager.Instance.currentLevel + 1);

        yield return new WaitForSeconds(3f);

        // Add null check for customerOrder
        if (customerOrder == null || customerOrder.Level == null || customerOrder.Level.Count == 0)
        {
            Debug.LogError("Customer order data not loaded properly!");
            yield break;
        }

        int levelIndex = Mathf.Clamp(LevelManager.Instance.currentLevel - 1, 0, customerOrder.Level.Count - 1);
        var levelOrders = customerOrder.Level[levelIndex].Order;

        // Add null check for orderDisplay
        if (orderDisplay == null)
        {
            Debug.LogError("Order display reference is missing!");
            yield break;
        }

        for (int i = 0; i < orderTotal; i++)
        {
            if (i >= orderDisplay.transform.childCount)
            {
                Debug.LogWarning("Not enough order display slots available");
                break;
            }

            int orderType = Random.Range(0, levelOrders.Count);
            TMP_Text prompt = orderDisplay.transform.GetChild(i).GetChild(0).GetComponent<TMP_Text>();

            if (prompt != null)
            {
                prompt.text = levelOrders[orderType].prompt;
                order.Add(levelOrders[orderType]);
                orderDisplay.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }

    public void CheckSandwich()
    {
        Dictionary<string, int> ingredientInSandwich = new Dictionary<string, int>();
        GameObject sandwich = transform.Find("Sandwich").gameObject;
        bool falseOrder = false;

        // Count ingredients in sandwich
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

        bool matched = false;

        // Check against orders
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
                targetImage.sprite = sprites[randomIndex - 1];
                matched = true;

                // Add reputation for successful order
                if (reputationManager != null)
                {
                    reputationManager.IncreaseReputation(3); // Reward for correct order
                }
                break;
            }
        }

        if (!matched)
        {
            Debug.Log($"Order index GAGAL dipenuhi.");
            Destroy(sandwich);
            targetImage.sprite = sprites[randomIndex - 2];
            falseOrder = true;

            // Reduce reputation for failed order
            if (reputationManager != null)
            {
                reputationManager.ReduceReputation(5); // Penalty for wrong order
            }
        }

        if (order.Count <= 0 || falseOrder)
        {
            animator.SetBool("Out", true);
            StartCoroutine(ResetCustomer());
            falseOrder = false;
            order.Clear();

            foreach (Transform child in orderDisplay.transform)
            {
                child.gameObject.SetActive(false);
            }
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