using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
  public Slider staminaBar;
  private int maxStamina = 100;
  private int currentStamina;
  public static StaminaBar instance;

  private void Awake()
  {
    instance = this;
  }

  // Start is called before the first frame update
  void Start()
  {
    currentStamina = maxStamina;
    staminaBar.maxValue = maxStamina;
    staminaBar.value = maxStamina;
  }

  public void UseStamina(int staminaNeeded)
  {
    if (currentStamina >= staminaNeeded)
    {
      currentStamina -= staminaNeeded;
      staminaBar.value = currentStamina;

      StartCoroutine(RegenStamina());
    }
    else
    {
      Debug.Log("Stamina not enough");
    }
  }

  private IEnumerator RegenStamina()
  {
    yield return new WaitForSeconds(3);
    while (currentStamina < maxStamina)
    {
      currentStamina += maxStamina / 100;
      staminaBar.value = currentStamina;
      yield return new WaitForSeconds(0.001f * Time.deltaTime);

    }
  }
}
