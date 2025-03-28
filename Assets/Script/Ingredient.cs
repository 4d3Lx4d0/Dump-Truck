using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable Objects/Ingredient")]
public class Ingredient : ScriptableObject
{
    public Sprite sprite;

    public Type type;

    public int maxStock;
    public int maxRestock;
}

public enum Type
{
    Veggies,
    Meat,
    Cheese
}
