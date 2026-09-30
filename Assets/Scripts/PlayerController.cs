using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Collections;
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
    public bool canMove = true;
    public bool canJump = true;
    public bool isDead = false;
    public Transform transform;

    public Animator animator;

    private Vector2 moveInput;

    public Slider sliderVida;
    public TextMeshProUGUI coinsText;

    void Start()
    {
        canMove = true;
        canJump = true;
        isDead = false;
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        vida = 100;
    }
    void Update()
    {
        sliderVida.value = vida;
        coinsText.text = coins.ToString();

        animator.SetFloat("Speed", moveInput.magnitude);

        vidaMax = Mathf.Max(0, 100);


        if(isDead)
        {
            canMove = false;
            canJump = false;
            animator.SetBool("isDead", true);
        }

        if(vida <= 0)
        {
            isDead = true;
        }
    }

    private void FixedUpdate()
    {
        Vector2 movement = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
        rb.linearVelocity = movement;

        if (rb.linearVelocity.x > 0.1f)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        
        else if (rb.linearVelocity.x < -0.1f)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(canMove)
        {
            moveInput = context.ReadValue<Vector2>();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pincho"))
        {
            StartCoroutine(Hurt());
            vida = vida - pinchoDam;
            Debug.Log(vida);
        }

        if (collision.CompareTag("Enemy"))
        {
            StartCoroutine(Hurt());
            vida = vida - enemyDam;
            Debug.Log(vida);
        }
    }

    IEnumerator Hurt()
    {
        animator.SetBool("isHurt", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("isHurt", false);
    }
}
