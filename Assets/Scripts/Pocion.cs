using UnityEngine;

public class Pocion : MonoBehaviour
{
    public float cura = 100;
    public PlayerController playerController;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerController.vida = cura;
            Debug.Log(playerController.vida);
            Destroy(gameObject);
        }
    }
}
