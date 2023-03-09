using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAudio : MonoBehaviour
{
  // Start is called before the first frame update
  public void clickSound()
  {
    FindObjectOfType<AudioManager>().Play("ClickSound");
  }
}
