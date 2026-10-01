using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GreenSlime : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI texto;
    public string nombre;

    public int vida = 10;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        texto.text = nombre;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Sword"));
        {
            vida = vida - 2;
        }
    }
}
