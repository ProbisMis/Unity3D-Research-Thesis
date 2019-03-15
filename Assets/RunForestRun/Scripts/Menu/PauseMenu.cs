using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour {

	public static bool GameIsPaused = false;

	public GameObject pauseMenuUI;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameIsPaused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
		
	}

	public void Resume()
	{
		pauseMenuUI.SetActive(false);
		GameIsPaused = false;
	}

	public void Pause()
	{
		
		pauseMenuUI.SetActive(true);
		GameIsPaused = true;
	}

	public void LoadMenu()
	{
		GameIsPaused = false;
		pauseMenuUI.SetActive(false);
		GroundDestroyerQueue.resetQueue();
		SceneManager.LoadScene(0);
	}

	public void QuitGame()
	{
		
		Debug.Log("Quitting Game");
		//GameIsPaused = false;
		Application.Quit();
	}
}
