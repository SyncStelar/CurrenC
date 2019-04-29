using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void startGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void gameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

    public void youWin()
    {
        SceneManager.LoadScene("You Win");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
