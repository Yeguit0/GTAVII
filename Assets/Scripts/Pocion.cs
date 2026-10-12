using UnityEngine;

public class Pocion : MonoBehaviour
{
    public float cura = 100;
    private PlayerController playerController;
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
            playerController.sliderVida.value = playerController.vida;
            Debug.Log(playerController.vida);
            Destroy(gameObject);
        }
    }
}
