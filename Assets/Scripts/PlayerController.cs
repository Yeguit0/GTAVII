using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float vida;
    public float vidaMax;
    public int coins = 0;
    public float pinchoDam = 20f;
    public float enemyDam = 10f;
    public float speed = 5f;
    public float jumpForce = 10f;
    public bool isGrounded = false;

    private Vector2 moveInput;

    public Slider sliderVida;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
        rb.linearVelocity = movement;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
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
