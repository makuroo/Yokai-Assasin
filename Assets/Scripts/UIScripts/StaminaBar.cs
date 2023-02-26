using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
  public Slider staminaBar;
  private float maxStamina;
  private float currentStamina;
  public static StaminaBar instance;
  public Player player;
  public Gradient gradient;
  public Image fill;



  private float lastStaminaUse;

      private void Awake()
      {
        player = GameObject.Find("chara1").GetComponent<Player>();
        fill.color = gradient.Evaluate(1f);
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

      private void Update()
      {
        if (lastStaminaUse == 0 || lastStaminaUse < 3)
          lastStaminaUse += Time.deltaTime;

        if (lastStaminaUse >= 3f && player.currStamina < maxStamina)
        {
          StartCoroutine(RegenStamina());
          lastStaminaUse = 0;
        }
      }

    public void UseStamina(float staminaNeeded)
    {
      if (currentStamina >= staminaNeeded)
      {
        Debug.Log(player.currStamina);
        player.currStamina -= staminaNeeded;
        SetStamina();
        lastStaminaUse = 0;
      }
      else
      {
        Debug.Log("Stamina not enough");
      }
    }

      private IEnumerator RegenStamina()
      {
         
        while (player.currStamina < player.maxStamina )
        {
          yield return new WaitForSeconds(.1f);
          player.currStamina += 50 / player.maxStamina;
            
          SetStamina();
        }
      }

      private void SetStamina()
      {
        maxStamina = player.maxStamina;
        currentStamina = player.currStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = currentStamina;
        fill.color = gradient.Evaluate(staminaBar.normalizedValue);
      }
}
