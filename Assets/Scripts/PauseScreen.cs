﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour {

	public string levelSelect;
	public string mainMenu;

	private LevelManager theLevelManager;

	public GameObject thePauseScreen;

	private PlayerController thePlayer;

	// Use this for initialization
	void Start () {
		theLevelManager = FindObjectOfType<LevelManager> ();
		thePlayer = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Pause")) 
		{
			if (Time.timeScale == 0f) {
				ResumeGame ();
			} else {
				PauseGame ();
			}
		}
	}

	public void PauseGame()
	{
		//freezes game
		Time.timeScale = 0;

		thePauseScreen.SetActive (true);
		thePlayer.canMove = false;
		//theLevelManager.levelMusic.Pause ();
		
	}

	public void ResumeGame()
	{
		thePauseScreen.SetActive (false);

		//unfreezes game
		Time.timeScale = 1f;
		thePlayer.canMove = true;
		//theLevelManager.levelMusic.Play ();
	}

	public void LevelSelect()
	{
		//saves values so when loading hub level loads proper lives and coin counts.
		PlayerPrefs.SetInt ("PlayerLives", theLevelManager.currentLives);
		PlayerPrefs.SetInt ("CointCount", theLevelManager.coinCount);

		Time.timeScale = 1f;

		SceneManager.LoadScene (levelSelect);
	}

	public void QuitToMainMenu()
	{
		Time.timeScale = 1f;
		SceneManager.LoadScene (mainMenu);
	}

}