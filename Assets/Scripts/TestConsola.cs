using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TestConsola : MonoBehaviour
{
    public string palabra;
    public AudioSource sonidito;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Hola lu" + palabra);
        StartCoroutine (Peo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Peo()
    {
        yield return new WaitForSeconds(2);
        sonidito.Play();
    }

}