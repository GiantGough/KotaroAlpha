using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {

	public string levelToLoad;
	public string levelToUnlock;


	public GameObject endLevelSplosion;

	private PlayerController thePlayer;
	private CameraController theCamera;
	private LevelManager theLevelManager;

	//allows setting of how long to wait before start moving
	public float waitToMove;
	//how long to wait to load next level
	public float waitToLoad;

	private bool movePlayer;

	public Sprite flagOpen;

	private SpriteRenderer theSpriteRenderer;

	//public GameObject levelEnd;

	// Use this for initialization
	void Start () {
		//levelEnd = FindObjectOfType<LevelEnd>();
		thePlayer = FindObjectOfType<PlayerController>();
		theCamera = FindObjectOfType<CameraController>();
		theLevelManager = FindObjectOfType<LevelManager>();

		theSpriteRenderer = GetComponent<SpriteRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {
		//moves the player once move player in LevelEndCo becomes true. Which should happen when player runs into levelEndObject's collider
		if (movePlayer) 
		{
			thePlayer.myRigidbody.velocity = new Vector3 (thePlayer.moveSpeed, thePlayer.myRigidbody.velocity.y, 0f);
		}
	}

	//loads the new level if Player collides with End Level Object
	void OnTriggerEnter2D(Collider2D other)
	{

		//Instantiate (endLevelSplosion, levelEnd.transform.position, 0f);
		theSpriteRenderer.sprite = flagOpen;
		


		if (other.tag == "Player") 
		{
			//SceneManager.LoadScene (levelToLoad);	

			StartCoroutine ("LevelEndCo");
		}
	}

	public IEnumerator LevelEndCo()
	{
		//This freezes camera, player input, and makes player invincible. Then waits, runs move player and loads up the next level
		thePlayer.canMove = false;
		theCamera.followTarget = false;
		theLevelManager.invincible = true;

		//Changes the level music at the end of the level from overworld to another song
		theLevelManager.levelMusic.Stop ();
		theLevelManager.gameOverMusic.Play ();

		thePlayer.myRigidbody.velocity = Vector3.zero;

		//allows player to carry score/lives over levels (integers)
		PlayerPrefs.SetInt("CoinCount", theLevelManager.coinCount);
		PlayerPrefs.SetInt ("PlayerLives", theLevelManager.currentLives);

		//Will unlock the next level at end of current level by setting value of 'levelToUnlock' to 1
		PlayerPrefs.SetInt (levelToUnlock, 1);

		yield return new WaitForSeconds (waitToMove);

		movePlayer = true;

		yield return new WaitForSeconds (waitToLoad);

		SceneManager.LoadScene (levelToLoad);
	}

}
