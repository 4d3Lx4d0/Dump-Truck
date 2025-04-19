// Tempel script ini ke tombol untuk test
using UnityEngine;
using UnityEngine.UI;

public class ButtonDebug : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => {
            Debug.Log("TOMBO DIKLIK!"); // Cek di Console
        });
    }
}