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
    public float shield;
    public float shieldMax;
    public int coins = 0;
    public float pinchoDam = 20f;
    public float enemyDam = 10f;
    public float speed = 5f;
    public float jumpForce = 10f;
    public bool isGrounded = false;
    public bool canMove = true;
    public bool canJump = true;
    public bool isDead = false;

    public SpriteRenderer sr;
    public Transform transform;
    public LayerMask targetLayer;

    public Animator animator;

    private Vector2 moveInput;

    public Slider sliderVida;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI shieldText;

    void Start()
    {
        canMove = true;
        canJump = true;
        isDead = false;
        sr = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        vida = 100;
        shield = 20;

    }
    void Update()
    {
        sliderVida.value = vida;
        shieldText.text = shield.ToString();
        coinsText.text = coins.ToString();

        animator.SetFloat("Speed", moveInput.magnitude);

        vidaMax = Mathf.Max(0, 100);

        shieldMax = Mathf.Max(0, 20);



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
            sr.flipX = false;
        }
        
        else if (rb.linearVelocity.x < -0.1f)
        {
            sr.flipX = true;
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
            animator.SetBool("isJumping", true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((targetLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            canJump = true;
            animator.SetBool("isJumping", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if ((targetLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((targetLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            StartCoroutine(CoyoteThingy());
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Pincho") && shield == 0)
        {
            StartCoroutine(Hurt());
            vida = vida - pinchoDam;
            Debug.Log(vida);
        }

        if (collision.CompareTag("Pincho") && shield > 0)
        {
            StartCoroutine(Hurt());
            shield = shield - pinchoDam;
            Debug.Log(vida);
        }

        if (collision.CompareTag("Enemy") && shield == 0)
        {
            StartCoroutine(Hurt());
            vida = vida - enemyDam;
            Debug.Log(vida);
        }

        if (collision.CompareTag("Enemy") && shield > 0)
        {
            StartCoroutine(Hurt());
            shield = shield - pinchoDam;
            Debug.Log(vida);
        }
    }

    IEnumerator Hurt()
    {
        animator.SetBool("isHurt", true);
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("isHurt", false);
    }

    IEnumerator CoyoteThingy()
    {
        yield return new WaitForSeconds(0.2f);
        canJump = false;
    }
}
