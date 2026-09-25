using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TestConsola : MonoBehaviour
{
    public string palabra;
    public string num1 = "Mi nombre es: ";
    public string num2 = "Maricarmen";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine (Suma());   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Suma()
    {
        yield return new WaitForSeconds(2);
    }

    public void Resultado()
    {
        Debug.Log(ProtoSuma(num2));
    }

    public string ProtoSuma(string bla)
    {
        return bla;
    }

}