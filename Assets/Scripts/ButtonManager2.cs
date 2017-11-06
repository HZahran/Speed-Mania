using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager2 : MonoBehaviour {

    public static ButtonManager2 bm;
    public Transform pauseMenu;
    private bool gamePaused = false;

    public void PauseGame(bool pause)
    {
        pauseMenu.gameObject.SetActive(pause);
        Time.timeScale = pause? 0:1;

        if (GameManager.gm.GetComponent<AudioSource>().isActiveAndEnabled) {
            if (pause)
                GameManager.gm.GetComponent<AudioSource>().Pause();
            else
                GameManager.gm.GetComponent<AudioSource>().UnPause();
        }
        gamePaused = pause;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gamePaused = !gamePaused;
            PauseGame(gamePaused);
        }
    }
}
