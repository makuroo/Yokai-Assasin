using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    public GameObject pauseMenu;
    public Player player;
    public GameObject DefeatPanel;
    public GameObject VictoryPanel;
    public bool isPaused = false;
    private GameObject enemies;
  // Start is called before the first frame update
  void Start()
  {
        player = GameObject.FindObjectOfType<Player>();
    if (Time.timeScale == 0)
    {
      Time.timeScale = 1;
    }
  }

  // Update is called once per frame
  private void Update()
  {
        enemies = GameObject.FindGameObjectWithTag("Enemy");
        if(enemies == null)
        {
            Time.timeScale = 0;
            VictoryPanel.SetActive(true);
        }
    if (Input.GetKeyDown(KeyCode.Escape) && player.currentHealth > 0)
    {
      CheckIsPaused();
    }
        
    if (player.currentHealth <= 0)
    {
          
      DefeatPanel.SetActive(true);
    }

  }

  public void PauseGame()
  {
    pauseMenu.SetActive(true);
    Time.timeScale = 0f;
    isPaused = true;
  }

  public void ResumeGame()
  {
    pauseMenu.SetActive(false);
    Time.timeScale = 1f;
    isPaused = false;
  }

  private void CheckIsPaused()
  {
    if (isPaused == false)
      PauseGame();
    else
      ResumeGame();
  }

    public void Restart()
    {
        SceneManager.LoadScene("Level1");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("ManMenu");
    }

}


