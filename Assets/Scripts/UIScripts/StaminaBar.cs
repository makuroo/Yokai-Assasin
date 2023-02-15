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
     }

    private void Start()
    {
        SetStamina();
        player.OnStaminaUse += Player_OnStaminaUse;
    }

    private void Player_OnStaminaUse(object sender, Player.OnStaminaUseEventArgs e)
    {
        if (player.isDashing == true)
        {
            UseStamina(e.dashStamina);
            
        }
        if (player.parried == true)
        {
            UseStamina(e.parryStamina);
        }
    }

      public void UseStamina(int staminaNeeded)
      {
        if (currentStamina >= staminaNeeded)
        {

            player.currStamina -= staminaNeeded;
            SetStamina();
      
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
        while (player.currStamina < player.maxStamina)
        {
            player.currStamina += player.maxStamina / player.maxStamina;
            Debug.Log(player.maxStamina);
            SetStamina();
            yield return new WaitForSeconds(50f * Time.deltaTime);
        }
      }

    private void SetStamina()
    {
        maxStamina = player.maxStamina;
        currentStamina = player.currStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = currentStamina;
        
    }
}
