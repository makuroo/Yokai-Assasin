using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float playerSpeed;
    public bool shield = false;
    private Rigidbody2D rb;
    public int hp = 20;
    private float movementInputDirectionX, movementInputdirectionY;
    public Animator anim;
    public float dashSpeed = 50f;
    public Vector3 moveDir;
    private bool canDash = true;
    //private float dashTime;
    //private float dashCoolDown = .1f;
    private bool isDashing;
    public float parryRange;
    private Vector3 lastDir;
    public GameObject ParryPoint;
    public LayerMask projectileLayer;
    private bool canParry = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            shield = false;
            canParry = false;
            anim.SetBool("isShield", shield);
        }

        if (canParry && shield == false)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                canParry = false;
                Debug.Log("Parried");
            }
        }
    }

    private void FixedUpdate()
    {
        movementInputDirectionX = Input.GetAxisRaw("Horizontal");
        movementInputdirectionY = Input.GetAxisRaw("Vertical");
        moveDir = new Vector2(movementInputDirectionX, movementInputdirectionY).normalized;
        if (moveDir != new Vector3(0, 0, 0))
            lastDir = moveDir;
        else
            lastDir =new  Vector3(transform.localScale.x, 0, 0);
        rb.velocity = moveDir * playerSpeed *Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse1) && canDash)
        {
            isDashing = true;
            canDash = false;
            Vector3 dashPosition = transform.position + lastDir * dashSpeed;
            rb.MovePosition(dashPosition);
            canDash = true;
        }

        if (movementInputDirectionX > 0)
            transform.localScale = new Vector2(1, 1);
        else if (movementInputDirectionX < 0)
            transform.localScale = new Vector2(-1, 1);
    }
}
