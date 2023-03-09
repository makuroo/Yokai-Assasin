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
  public bool isDashing = false;
  public bool parried = false;

  public int damage = 2;
  public int maxHealth = 20;
  public int currentHealth;
  public float maxStamina;
  public float currStamina;
  public float dashStamina = 5;
  public float parryStamina = 5;

  public Animator anim;

  public Vector3 moveDir;
  private Vector3 lastDir;

  private Rigidbody2D rb;

  public event EventHandler<OnStaminaUseEventArgs> OnStaminaUse;
  public class OnStaminaUseEventArgs : EventArgs
  {
    public float maxStamina, currStamina, dashStamina, parryStamina;
  }

  public HealthBar healthBar;

  public AudioClip footstep;
  AudioSource audioSource;

  public event EventHandler OnPickUpPowerUps;
  public event EventHandler OnLeftMouseClick;
  public event EventHandler OnRightMouseClick;


  private void Awake()
  {
    rb = GetComponent<Rigidbody2D>();
    anim = GetComponent<Animator>();
    audioSource = GetComponent<AudioSource>();

    // setting player health
    currentHealth = maxHealth;
    currStamina = maxStamina;
    healthBar.SetMaxHealth(maxHealth);
  }

  // Start is called before the first frame update
  void Start()
  {
    OnRightMouseClick += Player_OnRightMouseClick;
  }

  private void Update()
  {
    if (EventSystem.current.IsPointerOverGameObject() == false)
    {
      if (Input.GetMouseButtonDown(1) && currStamina >= dashStamina && isDashing == false)
      {
        isDashing = true;
        FindObjectOfType<AudioManager>().Play("DashSound");
      }

      VerifyParryStaminaUsage();

      if (currentHealth <= 0)
      {
        Destroy(transform.parent.gameObject);
      }

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
    {
      //   audioSource.PlayOneShot(footstep, 0.7f);
      lastDir = moveDir;
    }
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
    VerifyDashStaminaUsage();
    canDash = false;
    rb.velocity = new Vector2(lastDir.x, lastDir.y) * dashSpeed;
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
      FindObjectOfType<AudioManager>().Play("PowerUp");
      damage = 10;
      OnPickUpPowerUps?.Invoke(this, EventArgs.Empty);
      Destroy(collision.gameObject);
    }
  }
  private void VerifyDashStaminaUsage()
  {
    if (isDashing)
    {
      OnStaminaUse?.Invoke(this, new OnStaminaUseEventArgs { maxStamina = maxStamina, currStamina = currStamina, dashStamina = dashStamina, parryStamina = parryStamina });
    }
  }

  public void VerifyParryStaminaUsage()
  {
    if (parried)
    {
      OnStaminaUse?.Invoke(this, new OnStaminaUseEventArgs { maxStamina = maxStamina, currStamina = currStamina, dashStamina = dashStamina, parryStamina = parryStamina });
    }
    parried = false;
  }
}