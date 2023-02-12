using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

  public GameObject pauseMenu;
    public bool isPaused = false;
  // Start is called before the first frame update
  void Start()
  {

  }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckIsPaused();
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


