using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public string levelSelect;
	public string mainMenu;

	private LevelManager theLevelManager;

	// Use this for initialization
	void Start () {
		theLevelManager = FindObjectOfType<LevelManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Restart()
	{
		//resets coin count and player lives back to original value
		PlayerPrefs.SetInt ("CoinCount", 0);
		PlayerPrefs.SetInt ("PlayerLives", theLevelManager.startingLives);

		//gets and loads the current scene/level
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);

	}

	public void LevelSelect()
	{
		PlayerPrefs.SetInt ("CoinCount", 0);
		PlayerPrefs.SetInt ("PlayerLives", theLevelManager.startingLives);

		SceneManager.LoadScene (levelSelect);
	}

	//loads main menu when exit is selected
	public void QuitToMainMenu()
	{
		SceneManager.LoadScene (mainMenu);
	}

}
