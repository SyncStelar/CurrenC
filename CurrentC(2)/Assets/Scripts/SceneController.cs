using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {
    public GameObject Menu;

    private void Update() {
        if (Menu != null && (Input.GetKeyDown(GameController.gc.pause) || Input.GetKeyDown(KeyCode.Escape))) {
            PauseGame();
        }
    }

    public void StartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Level 1");
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void GameOver() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Game Over");
    }

    public void YouWin() {
        Time.timeScale = 1;
        SceneManager.LoadScene("You Win");
    }

    public void MainMenu() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void PauseGame() {
        Time.timeScale = 0;
        Menu.SetActive(true);
    }
    
    public void UnpauseGame() {
        Time.timeScale = 1;
        Menu.SetActive(false);
    }
}
