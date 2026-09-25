using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float vida;
    public float vidaMax;
    public float pinchoDam = 20;
    public float enemyDam = 10;

    public Slider sliderVida;
    
    void Start()
    {
        vida = 100;
    }
    void Update()
    {
        sliderVida.value = vida;
        
        vidaMax = Mathf.Max(0, 100);

        if(vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pincho"))
        {
            vida = vida - pinchoDam;
            Debug.Log(vida);
        }

        if (collision.CompareTag("Enemy"))
        {
            vida = vida - enemyDam;
            Debug.Log(vida);
        }

    }
}
