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
    public float poisonDam;
    public float speed = 5f;
    public float speedMax = 5f;
    public float jumpForce = 10f;
    public bool isGrounded = false;
    public bool canMove = true;
    public bool canJump = true;
    public bool isDead = false;
    public bool canAttack = true;

    public GameObject slash;

    public SpriteRenderer sr;
    public Transform transform;
    public LayerMask targetLayer;

    public Animator animator;
    public Animator slashAnim;

    private Vector2 moveInput;

    public Slider sliderVida;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI shieldText;



    //trabajito
    public Color originalColor;
    public Color hieloColor;
    public Color venenoColor;
    public Color fuegoColor;
    public string typeDamage;

    void Start()
    {
        canMove = true;
        canJump = true;
        isDead = false;
        canAttack = true;
        sr = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        vida = 100;
        shield = 20;
        poisonDam = (vidaMax / 2);

    }
    void Update()
    {
        sliderVida.value = vida;
        shieldText.text = shield.ToString();
        coinsText.text = coins.ToString();

        animator.SetFloat("Speed", moveInput.magnitude);

        vidaMax = 100;
        shieldMax = 20;

        if(shield > shieldMax)
        {
            shield = shieldMax;
        }

        if(vida > vidaMax)
        {
            vida = vidaMax;
        }

        if(isDead)
        {
            canJump = false;
            canMove = false;
            speed = 0;
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
            Vector3 flip = transform.localScale;
            flip.x = 1;
            transform.localScale = flip;

        }
        
        else if (rb.linearVelocity.x < -0.1f)
        {
            Vector3 flip = transform.localScale;
            flip.x = -1;
            transform.localScale = flip;
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

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed && canAttack)
        {
            Instantiate(slash);
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
            TypeOfDamage("Fuego");
            //StartCoroutine(Hurt(pinchoDam));
            Debug.Log(vida);
        }

        if (collision.CompareTag("Pincho") && shield > 0)
        {
            TypeOfDamage("Fuego");
            //StartCoroutine(Hurt(pinchoDam));
            Debug.Log(vida);
        }

        if (collision.CompareTag("Enemy") && shield == 0)
        {
            StartCoroutine(Hurt(enemyDam));
            Debug.Log(vida);
        }

        if (collision.CompareTag("Enemy") && shield > 0)
        {
            StartCoroutine(Hurt(enemyDam));
            Debug.Log(vida);
        }
    }

    IEnumerator Hurt(float damage)
    {
        animator.SetBool("isHurt", true);
        
        if(shield <= 0)
        {
            vida = vida - damage;
        }

        if(shield > 0)
        {
            shield = shield - damage;
        }

        yield return new WaitForSeconds(0.2f);
        animator.SetBool("isHurt", false);
    }

    IEnumerator CoyoteThingy()
    {
        yield return new WaitForSeconds(0.2f);
        canJump = false;
    }

    IEnumerator Hielo()
    {
        StartCoroutine(Hurt(enemyDam));
        canMove = false;
        canJump = false;
        speed = 0f;
        sr.color = hieloColor;
        yield return new WaitForSeconds(2);
        sr.color = originalColor;
        canMove = true;
        canJump = true;
        speed = 5f;
    }

    IEnumerator Fuego()
    {
        StartCoroutine(Hurt(pinchoDam));
        sr.color = fuegoColor;
        speed = speed * 2f;
        yield return new WaitForSeconds(3f);
        sr.color = originalColor;
        speed = speed / 2f;
        StartCoroutine(Hurt(pinchoDam));
    }

    IEnumerator Veneno()
    {
        StartCoroutine(Hurt(poisonDam));
        sr.color = venenoColor;
        yield return new WaitForSeconds(1f);
        sr.color = originalColor;
    }

    public void TypeOfDamage(string typeDamage)
    {
        if(typeDamage == "Hielo")
        {
            StartCoroutine(Hielo());
        }

        if(typeDamage == "Veneno")
        {
            StartCoroutine(Veneno());
        }

        if(typeDamage == "Fuego")
        {
            StartCoroutine(Fuego());
        }
    }
}
