using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

  public GameObject pauseMenu;
  public HealthBar hb;
  public GameObject DefeatPanel;
  public bool isPaused = false;
  // Start is called before the first frame update
  void Start()
  {
    if (Time.timeScale == 0)
    {
      Time.timeScale = 1;
    }
  }

  public void Restart()
  {
    SceneManager.LoadScene("Level1");

  }

  public void GoToMainMenu()
  {
    SceneManager.LoadScene("ManMenu");
  }

  // Update is called once per frame
  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      CheckIsPaused();
    }
    if (hb.slider.value <= 0)
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

}


