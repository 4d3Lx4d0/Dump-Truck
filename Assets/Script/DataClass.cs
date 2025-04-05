using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class CustomerOrder
{
     public List<Level> Level;
}

[System.Serializable]
public class Level
{
    public List<Order> Order;
}

[System.Serializable]
public class Order
{
    public string prompt;
    public List<Ingredients> ingredients;
    public string type;
}

[System.Serializable]
public class Ingredients
{
    public string name;
    public int count;
    public bool fit;
}




