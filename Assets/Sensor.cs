using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    public int inRange = 1;
    public bool canMelee = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && this.CompareTag("Enemy"))
        {
            inRange = 0;
        }

        if (collision.gameObject.CompareTag("Player") && this.CompareTag("Melee Range"))
        {
            canMelee = true;
            Debug.Log("Enter: "+canMelee);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && this.CompareTag("Enemy"))
            inRange = 1;

        if (collision.gameObject.CompareTag("Player") && this.CompareTag("Melee Range"))
        {
            canMelee = false;
            Debug.Log("Exit: " + canMelee);
        }
            
    }
}
