using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
  [SerializeField] private float playerSpeed;
  [SerializeField] public float dashSpeed = 50f;
  private float dashCoolDown = 0.5f;
  private float movementInputDirectionX, movementInputdirectionY;

  private bool canDash = true;
  public bool shoot = false;
  private bool isDashing = false;

  public int damage = 2;
  public int maxHealth = 20;
  public int currentHealth;
  public int maxStamina;
  public int currStamina;

  public Animator anim;

  public Vector3 moveDir;
  private Vector3 lastDir;

  private Rigidbody2D rb;

  public LayerMask projectileLayer;

  public HealthBar healthBar;

  public event EventHandler OnPickUpPowerUps;
  public event EventHandler OnLeftMouseClick;
  public event EventHandler OnRightMouseClick;

  [SerializeField] private GameObject parryField;



  // Start is called before the first frame update
  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();

    // setting player health
    currentHealth = maxHealth;
    currStamina = maxStamina;
    healthBar.SetMaxHealth(maxHealth);
    OnRightMouseClick += Player_OnRightMouseClick;
  }



  private void Update()
  {

    if (EventSystem.current.IsPointerOverGameObject() == false)
    {
      if (Input.GetMouseButton(1) && currStamina >= 5)
      {
        currStamina -= 5;
        isDashing = true;
      }

      if (currentHealth <= 0)
      {
        Destroy(transform.parent.gameObject);
      }
      parryField.transform.position = transform.position;

      if (Input.GetMouseButtonDown(0))
      {
        OnLeftMouseClick?.Invoke(this, EventArgs.Empty);
      }
    }

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
    rb.velocity = moveDir * playerSpeed * Time.fixedDeltaTime;

    //facing left right
    if (movementInputDirectionX > 0)
      transform.localScale = new Vector2(1, 1);
    else if (movementInputDirectionX < 0)
      transform.localScale = new Vector2(-1, 1);

    if (isDashing)
    {
      OnRightMouseClick?.Invoke(this, EventArgs.Empty);
    }
  }

  private IEnumerator Dash(float dashCD)
  {
    canDash = false;
    rb.velocity = new Vector2(lastDir.x, lastDir.y) * dashSpeed;
    Debug.Log(rb.velocity);
    //Vector3 dashPosition = transform.position + lastDir * dashSpeed;
    //rb.MovePosition(dashPosition);
    yield return new WaitForSeconds(dashCD);
    canDash = true;
    Debug.Log("can dash");
  }

  private void Player_OnRightMouseClick(object sender, EventArgs e)
  {
    if (canDash)
      StartCoroutine(Dash(dashCoolDown));
    isDashing = false;
  }

  public void TakeDamage(int damage)
  {
    currentHealth -= damage;
    healthBar.SetHealth(currentHealth);
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("PowerUp"))
    {
      damage = 10;
      OnPickUpPowerUps?.Invoke(this, EventArgs.Empty);
      Destroy(collision.gameObject);
    }
  }
}