using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inGameBoombox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("StartSound");
        StartCoroutine(playSoundAfterThreeSeconds());
    }

    IEnumerator playSoundAfterThreeSeconds()
    {
        yield return new WaitForSeconds(3);
        FindObjectOfType<AudioManager>().Play("NotInBattle");
    }

}
