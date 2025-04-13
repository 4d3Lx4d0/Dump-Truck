using TMPro;
using UnityEngine;

public class ReputationManager : MonoBehaviour
{
    [SerializeField] private int startingReputation = 100;
    [SerializeField] private TMP_Text reputationText;
    private int currentReputation;

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

    private void UpdateReputationDisplay()
    {
        if (reputationText != null)
        {
            reputationText.text = $"Reputation: {currentReputation}";
        }
    }
}