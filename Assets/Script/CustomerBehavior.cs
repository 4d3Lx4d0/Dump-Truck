using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CustomerBehavior : MonoBehaviour
{

    public Image targetImage;
    public Sprite[] sprites; 

    public List<string> order;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnEnable()
    {
        if (sprites.Length > 0 && targetImage != null)
        {
            int randomIndex = Random.Range(0, sprites.Length);
            targetImage.sprite = sprites[randomIndex];
        }
    }
}
