using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CustomerBehavior customerBehavior;
    public Image background;
    public ReputationManager reputationManager; // Reference to ReputationManager

    public int sceneNumber;
    public int maxCustomer;
    public int successfulOrder = 0;


    void Start()
    {
        customerBehavior.successfulOrder.AddListener(OrderSuccess);

        // Get reference to ReputationManager if not set in inspector
        if (reputationManager == null)
        {
            reputationManager = FindObjectOfType<ReputationManager>();
        }

        string sceneName = SceneManager.GetActiveScene().name;
        sceneNumber = int.Parse(sceneName) - 1;

        switch (sceneNumber)
        {
            case 0:
                maxCustomer = 5;
                break;
            case 1:
                maxCustomer = 10;
                break;
            case 2:
                maxCustomer = 15;
                break;
        }
    }

    public void OrderSuccess()
    {
        successfulOrder++;
        UpdateBackgroundAlpha();
        CheckReputation(); // Check after each order

        if (successfulOrder >= maxCustomer)
        {
            if (sceneNumber == 3) // Last level
            {
                SceneManager.LoadScene("Main Menyu");
            }
            else
            {
                SceneManager.LoadScene(sceneNumber + 1);
            }
        }
    }

    private void UpdateBackgroundAlpha()
    {
        float alpha = 1f - ((float)successfulOrder / maxCustomer);
        alpha = Mathf.Clamp01(alpha);

        if (background != null)
        {
            Color newColor = background.color;
            newColor.a = alpha;
            background.color = newColor;
        }
    }

    private void CheckReputation()
    {
        if (reputationManager.GetCurrentReputation() < 70)
        {
            SceneManager.LoadScene("Loss");
        }
    }
}