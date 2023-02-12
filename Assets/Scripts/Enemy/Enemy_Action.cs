using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Action : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float speed;

    [SerializeField]private int hp = 20;
    [SerializeField]private int damage = 2;

    private Vector2 faceDir;
    private float angle;
    
    public int facingIndex;
    public Sprite[] faceDirectionSprites;
    public SpriteRenderer sr;
    public Animator anim;
    public Sensor sensor;
    public Sensor meleeSensor;

    private Transform playerTransform;
    private Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
        sr = transform.GetComponent<SpriteRenderer>();
        anim = transform.GetComponent<Animator>();
        sensor = transform.GetComponentInChildren<Sensor>();
        meleeSensor = transform.GetChild(1).GetComponentInChildren<Sensor>();
        projectile.GetComponent<EnemyProjectile>().damage = damage;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        if (sensor.inRange == 0)
        {
            SetFacingDirection();
        }
        else
        {
            anim.SetBool("inRange", false);
        }

        if (meleeSensor.canMelee)
        {
            damage = 3;
        }
        if(playerScript.currentHealth < damage)
        {
            meleeSensor.canMelee = false;
        }
       
        anim.SetBool("canMelee", meleeSensor.canMelee);
    }

    public void Shoot()
    {
        if (sensor.inRange == 0 && sensor.canMelee == false)
        {
            Instantiate(projectile, transform.position, transform.rotation);
            projectile.GetComponent<EnemyProjectile>().damage = damage;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Projectile"))
        {
            playerScript.TakeDamage(playerScript.damage);
            Debug.Log(hp);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Projectile"))
        {
            hp -= playerScript.damage;
            Debug.Log(hp);
            Destroy(collision.gameObject);
        }
    }

    public void MeleeAttack()
    {
        playerScript.TakeDamage(damage);
    }

    private void SetFacingDirection()
    {
        faceDir = (transform.position - playerTransform.position).normalized;
        angle = Mathf.Atan2(faceDir.y, faceDir.x) * Mathf.Rad2Deg;
        anim.SetBool("inRange", true);

        if (angle <= 45 && angle >= -45)
        {
            facingIndex = 1;
            sr.sprite = faceDirectionSprites[facingIndex];
            anim.SetInteger("attackDir", facingIndex);
            sr.flipX = false;
        }
        else if (angle >= 135 || angle <= -135)
        {
            facingIndex = 1;
            sr.sprite = faceDirectionSprites[facingIndex];
            anim.SetInteger("attackDir", facingIndex);
            sr.flipX = true;

        }
        else if (angle < -45 && angle > -135)
        {
            facingIndex = 2;
            // sr.sprite = faceDirectionSprites[facingIndex];
            anim.SetInteger("attackDir", facingIndex);
            sr.flipX = false;
            Debug.Log(3);
        }
        else if (angle > 45 && angle < 135)
        {
            facingIndex = 0;
            //sr.sprite = faceDirectionSprites[facingIndex];
            anim.SetInteger("attackDir", facingIndex);
            sr.flipX = false;
        }
    }
}