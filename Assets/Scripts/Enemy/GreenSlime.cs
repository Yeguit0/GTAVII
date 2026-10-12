using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GreenSlime : MonoBehaviour
{
    public Transform player;
    public TextMeshProUGUI texto;
    public string nombre;
    public Animator animator;
    public float moveSpeed = 1f;
    public LayerMask layer;
    public PlayerController playerController;
    private Rigidbody2D rb;
    private SpriteRenderer sr;


    public int damage = 2;
    public int vida = 10;
    public bool canMove = true;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        texto.text = nombre;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            FollowPlayer();
        }

        if(vida <= 0)
        {
            canMove = false;
            animator.SetBool("isDead", true);
        }
    }

    public void MuerteDefinitiva()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((layer & (1 << collision.gameObject.layer)) != 0)
        {
            StartCoroutine(Hurt(0.1f));
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();

            playerController.StartCoroutine(playerController.Hurt(damage));
            Debug.Log("Vida del jugador: " + playerController.vida);
        }
    }

    void FollowPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

        if (direction.x > 0.1f)
        {
            sr.flipX = false;
        }
        else if (direction.x < -0.1f)
        {
            sr.flipX = true;
        }
    }

    IEnumerator Hurt(float tiempo)
    {
        animator.SetBool("isHurt", true);
        vida = vida - 2;
        yield return new WaitForSeconds(tiempo);
        animator.SetBool("isHurt", false);
    }
}
