using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed;
    private Rigidbody2D rb;
    public int maxHealth =20;
    public int currentHealth;
    private float movementInputDirectionX, movementInputdirectionY;
    public Animator anim;
    public float dashSpeed = 50f;
    public Vector3 moveDir;
    private bool canDash = true;
    private float dashCoolDown = 0.5f;
    private Vector3 lastDir;
    public LayerMask projectileLayer;
    public HealthBar healthBar;
    [SerializeField] private GameObject parryField;
    public int damage = 2;
    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();

        // setting player health
      currentHealth = maxHealth;
      healthBar.SetMaxHealth(maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && canDash)
        {
            StartCoroutine(Dash(dashCoolDown));
        }

        if (currentHealth <= 0)
        {
            Destroy(transform.parent.gameObject);
        }
        parryField.transform.position = transform.position;
    }

    private void FixedUpdate()
    {
        movementInputDirectionX = Input.GetAxisRaw("Horizontal");
        movementInputdirectionY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(movementInputDirectionX, movementInputdirectionY).normalized;

        //keep face direction when idlle
        if (moveDir != new Vector3(0, 0, 0))
            lastDir = moveDir;
        else
            lastDir = new Vector3(transform.localScale.x, 0, 0);
        rb.velocity = moveDir * playerSpeed * Time.deltaTime;

        //facing left right
        if (movementInputDirectionX > 0)
            transform.localScale = new Vector2(1, 1);
        else if (movementInputDirectionX < 0)
            transform.localScale = new Vector2(-1, 1);

        //dash mechanic
        if (Input.GetKeyDown(KeyCode.Mouse1) && canDash)
        {
            StartCoroutine(Dash(dashCoolDown));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("EnemyProjectile"))
        {
            TakeDamage(collision.gameObject.GetComponent<EnemyProjectile>().damage);
        }
    }

    private IEnumerator Dash(float dashCD)
    {
        canDash = false;
        Vector3 dashPosition = transform.position + lastDir * dashSpeed;
        rb.MovePosition(dashPosition);
        yield return new WaitForSeconds(dashCD);
        canDash = true;
    }

    public void TakeDamage(int damage)
    {
      currentHealth -= damage;
      healthBar.SetHealth(currentHealth);
    }
 }