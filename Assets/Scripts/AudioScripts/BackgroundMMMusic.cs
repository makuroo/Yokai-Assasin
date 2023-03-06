using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMMMusic : MonoBehaviour
{
  // Start is called before the first frame update
  private static BackgroundMMMusic bm;
  private void Awake()
  {
    if (bm == null)
    {
      bm = this;
      DontDestroyOnLoad(bm);
    }
    else
    {
      Destroy(gameObject);
    }
  }
}
