using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
  public Slider staminaBar;
  private int maxStamina;
  private int currentStamina;
  public static StaminaBar instance;
  public Player player;

  private void Awake()
  {
    player = GameObject.Find("chara1").GetComponent<Player>();
    player.OnStaminaUse += Player_OnStaminaUse;
    // instance = this;
  }

  private void Player_OnStaminaUse(object sender, Player.OnStaminaUseEventArgs e)
  {
    player = GameObject.Find("chara1").GetComponent<Player>();
    player.OnStaminaUse -= Player_OnStaminaUse;
    if (player.isDashing == true)
    {
      UseStamina(e.dashStamina);
    }
    if (player.parried == true)
    {
      UseStamina(e.parryStamina);
    }

  }

  // private void SetStamina(Player p) {
  //   currentStamina = p.maxStamina;
  //   staminaBar.maxValue = p.maxStamina;
  //   staminaBar.value = p.maxStamina;
  // }

  // Start is called before the first frame update
  // void Start()
  // {
  //   currentStamina = maxStamina;
  //   staminaBar.maxValue = maxStamina;
  //   staminaBar.value = maxStamina;
  // }

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
