using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CustomerBehavior customerBehavior;
    public Image background;

    public int sceneNumber;

    public int maxCustomer;

    public int successfulOrder = 0;

    void Start()
    {
        customerBehavior.successfulOrder.AddListener(OrderSuccess);

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
                maxCustomer = 20;
                break;
        }


    }



    public void OrderSuccess()
    {
        successfulOrder++;

        if (successfulOrder >= maxCustomer) SceneManager.LoadScene(sceneNumber + 1);

        UpdateBackgroundAlpha();
    }

    private void UpdateBackgroundAlpha()
    {
        float alpha = 1f - ((float)successfulOrder / maxCustomer);
        alpha = Mathf.Clamp01(alpha); // Ensure value stays between 0-1

        // For UI Image
        if (background != null)
        {
            Color newColor = background.color;
            newColor.a = alpha;
            background.color = newColor;
        }
    }
}