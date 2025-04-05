using UnityEngine;

public static class ReadOrderData
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static CustomerOrder LoadData()
    {
        CustomerOrder customerOrder;

        TextAsset customerOrderText = Resources.Load<TextAsset>("Order");

        customerOrder = JsonUtility.FromJson<CustomerOrder>(customerOrderText.text);
        
        return customerOrder;
    }
}
