using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReputationManager : MonoBehaviour
{
    [SerializeField] private int startingReputation = 100;
    [SerializeField] private TMP_Text reputationText;
    private int currentReputation;

    public Image star;

    void Start()
    {
        currentReputation = startingReputation;
        UpdateReputationDisplay();
    }

    public void ReduceReputation(int amount)
    {
        currentReputation -= amount;
        currentReputation = Mathf.Max(0, currentReputation);
        UpdateReputationDisplay();
    }

    public void IncreaseReputation(int amount)
    {
        currentReputation += amount;
        UpdateReputationDisplay();
    }

    public int GetCurrentReputation()
    {
        return currentReputation;
    }

    // Tambahan untuk sistem level
    public bool HasEnoughReputation(int requiredAmount)
    {
        return currentReputation >= requiredAmount;
    }

    private void UpdateReputationDisplay()
    {
        if (reputationText != null)
        {
            reputationText.text = $"Reputation: {currentReputation}";
            star.fillAmount = currentReputation/100f;
        }
    }
}