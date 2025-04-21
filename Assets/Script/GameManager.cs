using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text levelText;
    public TMP_Text reputationRequirementText;
    public Button nextLevelButton;
    public GameObject levelUpPanel;

    private LevelManager levelManager;
    private ReputationManager reputationManager;

    void Start()
    {
        levelManager = LevelManager.Instance;
        reputationManager = FindObjectOfType<ReputationManager>();
        UpdateUI();

        nextLevelButton.onClick.AddListener(TryLevelUp);
    }

    void Update()
    {
        // Update UI secara real-time ketika reputasi berubah
        UpdateLevelUpAvailability();
    }

    void UpdateUI()
    {
        levelText.text = "Level: " + levelManager.currentLevel;
        UpdateLevelUpAvailability();
    }

    void UpdateLevelUpAvailability()
    {
        bool isMaxLevel = levelManager.currentLevel >= 3;
        bool hasEnoughRep = reputationManager.GetCurrentReputation() >= GetRequiredReputation();

        nextLevelButton.interactable = !isMaxLevel && hasEnoughRep;
        levelUpPanel.SetActive(!isMaxLevel && hasEnoughRep);

        if (!isMaxLevel)
        {
            reputationRequirementText.text = $"{levelManager.currentLevel + 1}\nRequires {GetRequiredReputation()} Reputation";
        }
        else
        {
            reputationRequirementText.text = "Level Maksimal!";
        }
    }

    int GetRequiredReputation()
    {
        return levelManager.currentLevel * 20;
    }

    public void TryLevelUp()
    {
        if (reputationManager.GetCurrentReputation() >= GetRequiredReputation())
        {
            levelManager.LevelUp();
            UpdateUI();

            // Refresh ingredient displays
            IngredientDisplay[] displays = FindObjectsOfType<IngredientDisplay>();
            foreach (var display in displays)
            {
                display.gameObject.SetActive(levelManager.IsIngredientAvailable(display.ingredient.name));
            }
        }
    }
}