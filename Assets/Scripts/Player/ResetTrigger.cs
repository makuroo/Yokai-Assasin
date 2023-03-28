using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestTrigger : MonoBehaviour
{
    public void ResetTrigger()
    {
        GetComponent<Animator>().ResetTrigger("Parry");
    }
}
