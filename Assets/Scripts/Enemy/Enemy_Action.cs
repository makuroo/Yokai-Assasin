using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Action : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float speed;

    public int hp = 20;
    public int damage = 2;
    private float diffX;
    private float diffY;
    private float tan;
    
    [SerializeField] Transform target;
    //private Vector3 posDiff;
    //private float fixedZ = -90;
    //private float rotz;
    public int facingIndex;
    public Sprite[] faceDirectionSprites;
    public SpriteRenderer sr;
    public Animator anim;
    public Sensor sensor;
    public Sensor meleeSensor;

    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        sr = transform.GetComponent<SpriteRenderer>();
        anim = transform.GetComponent<Animator>();
        sensor = transform.GetComponentInChildren<Sensor>();
        meleeSensor = transform.GetChild(1).GetComponentInChildren<Sensor>();
        projectile.GetComponent<EnemyProjectile>().damage = damage;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void Update()
    {

        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        if (sensor.inRange == 0)
        {
            diffX = transform.position.x - target.position.x;
            diffY = transform.position.y - target.position.y;
            tan = diffX / diffY;
            anim.SetBool("inRange", true);
            if (target.position.x < transform.position.x && (Mathf.Atan(tan) * Mathf.Rad2Deg <= -55f || Mathf.Atan(tan) * Mathf.Rad2Deg >= 55f) && Mathf.Abs(target.position.y - transform.position.y) < 0.8f)
            {
                facingIndex = 1;
                sr.sprite = faceDirectionSprites[facingIndex];
                anim.SetInteger("attackDir", facingIndex);
                sr.flipX = false;
            }
            else if (target.position.x > transform.position.x && (Mathf.Atan(tan) * Mathf.Rad2Deg <= -55f || Mathf.Atan(tan) * Mathf.Rad2Deg >= 55f) && Mathf.Abs(target.position.y - transform.position.y) < 0.8f)
            {
                facingIndex = 1;
                sr.sprite = faceDirectionSprites[facingIndex];
                anim.SetInteger("attackDir", facingIndex);
                sr.flipX = true;
            }
            else if (target.position.y > transform.position.y && (Mathf.Atan(tan) * Mathf.Rad2Deg > -55f || Mathf.Atan(tan) * Mathf.Rad2Deg < 55f))
            {
                facingIndex = 2;
                // sr.sprite = faceDirectionSprites[facingIndex];
                anim.SetInteger("attackDir", facingIndex);
                sr.flipX = false;
            }
            else if (target.position.y < transform.position.y && (Mathf.Atan(tan) * Mathf.Rad2Deg > -55f || Mathf.Atan(tan) * Mathf.Rad2Deg < 55f))
            {
                facingIndex = 0;
                //sr.sprite = faceDirectionSprites[facingIndex];
                anim.SetInteger("attackDir", facingIndex);
                sr.flipX = false;
            }
        }
        else
        {
            anim.SetBool("inRange", false);
        }

        if (meleeSensor.canMelee)
        {
            damage = 3;
        }
        if(player.hp < damage)
        {
            meleeSensor.canMelee = false;
        }
       
        anim.SetBool("canMelee", meleeSensor.canMelee);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void Shoot()
    {
        
        if (sensor.inRange == 0 && sensor.canMelee == false)
        {
            Instantiate(projectile, transform.position, transform.rotation);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Projectile"))
        {
            player.TakeDamage(player.damage);
            Debug.Log(hp);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Projectile"))
        {
            hp -= player.damage;
            Debug.Log(hp);
            Destroy(collision.gameObject);
        }
    }

    public void MeleeAttack()
    {
        player.TakeDamage(damage);
    }
}