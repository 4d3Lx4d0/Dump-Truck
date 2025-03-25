using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public SandwichMechanic sandwichMechanic;

    public void ResetSandwichWrapper()
    {
        sandwichMechanic.ResetSandwich();
    }
}