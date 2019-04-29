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
        SceneManager.LoadScene("Level 1");
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void GameOver() {
        SceneManager.LoadScene("Game Over");
    }

    public void YouWin() {
        SceneManager.LoadScene("You Win");
    }

    public void MainMenu() {
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
