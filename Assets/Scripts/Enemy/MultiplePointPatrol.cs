using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplePointPatrol : MonoBehaviour
{
    private enum FacingDirection{
        back,
        right,
        left,
        forward
    }

    [SerializeField] float speed;
    public Transform[] patrolPoints;
    public float waitTime;
    private int currentPointIndex = 0;
    [SerializeField]private bool once = false;
    private Sensor sensor;
    private Enemy_Action enemy;
    private float diffX;
    private float diffY;
    private float tan;
    private Transform target;
    private FacingDirection lastDirection;
    private Animator anim;
    private bool stop = false;

    // Start is called before the first frame update
    void Start()
    {
        sensor = transform.GetComponentInChildren<Sensor>();
        enemy = transform.GetComponent<Enemy_Action>();
        anim = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        target = patrolPoints[currentPointIndex];
        if (transform.position != patrolPoints[currentPointIndex].position)
        {
            diffX = transform.position.x - target.position.x;
            diffY = transform.position.y - target.position.y;
            tan = diffX / diffY;
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * sensor.inRange * Time.deltaTime);        
        }
        else if(transform.position == patrolPoints[currentPointIndex].position)
        {
            stop = true;
            if (once == false)
            {
                once = true;
                StartCoroutine(Wait());
            }
        }

        // movement
        if (target.position.x < transform.position.x && (Mathf.Atan(tan) * Mathf.Rad2Deg <= -55f || Mathf.Atan(tan) * Mathf.Rad2Deg >= 55f) && Mathf.Abs(target.position.y - transform.position.y) < 0.8f)
        {
            enemy.facingIndex = 1;
            enemy.sr.sprite = enemy.faceDirectionSprites[enemy.facingIndex];
            enemy.anim.SetInteger("faceDir", enemy.facingIndex);
            enemy.sr.flipX = false;
            lastDirection = FacingDirection.left;
        }
        else if (target.position.x > transform.position.x && (Mathf.Atan(tan) * Mathf.Rad2Deg <= -55f || Mathf.Atan(tan) * Mathf.Rad2Deg >= 55f) && Mathf.Abs(target.position.y - transform.position.y) < 0.8f)
        {
            enemy.facingIndex = 1;
            //sr.sprite = faceDirectionSprites[facingIndex];
            enemy.sr.flipX = true;
            enemy.anim.SetInteger("faceDir", enemy.facingIndex);
            lastDirection = FacingDirection.right;
        }
        else if (target.position.y > transform.position.y && (Mathf.Atan(tan) * Mathf.Rad2Deg > -55f || Mathf.Atan(tan) * Mathf.Rad2Deg < 55f))
        {
            enemy.facingIndex = 2;
            // sr.sprite = faceDirectionSprites[facingIndex];
            enemy.anim.SetInteger("faceDir", enemy.facingIndex);
            enemy.sr.flipX = false;
            lastDirection = FacingDirection.back;
        }
        else if(target.position.y < transform.position.y && (Mathf.Atan(tan) * Mathf.Rad2Deg > -55f || Mathf.Atan(tan) * Mathf.Rad2Deg < 55f))
        {
            enemy.facingIndex = 0;
            //sr.sprite = faceDirectionSprites[facingIndex];
            enemy.anim.SetInteger("faceDir", enemy.facingIndex);
            enemy.sr.flipX = false;
            lastDirection = FacingDirection.forward;
        }
    }

    IEnumerator Wait()
    {
        if (lastDirection == FacingDirection.left)
        {
            anim.SetInteger("lastDir", (int)FacingDirection.right);
            Debug.Log((int)FacingDirection.left);
            anim.SetBool("Stop", stop);
        }
        else if (lastDirection == FacingDirection.right)
        {
            anim.SetInteger("lastDir", (int)FacingDirection.right);
            anim.SetBool("Stop", stop);
        }
        else if (lastDirection == FacingDirection.back)
        {
            anim.SetInteger("lastDir", (int)FacingDirection.back);
            anim.SetBool("Stop", stop);
            Debug.Log((int)FacingDirection.back);
        }
        else if (lastDirection == FacingDirection.forward)
        {
            anim.SetInteger("lastDir", (int)FacingDirection.forward - 1);
            anim.SetBool("Stop", stop);
            Debug.Log((int)FacingDirection.forward -1);
        }

        yield return new WaitForSeconds(waitTime);
        stop = false;
        anim.SetBool("Stop", stop);
        anim.SetInteger("lastDir", -1);
        if (currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }
        once = false;
    }
        
}
