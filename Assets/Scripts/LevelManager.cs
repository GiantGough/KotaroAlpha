using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

	public GameObject SlowPlane;

	public float waitToRespawn;
	public PlayerController thePlayer;

	//particle effects
	public GameObject deathSplosion;


	//Coin/leaf related
	public int coinCount;
	public Text coinText;
	public AudioSource coinSound;

	public Image life1;
	public Image life2;
	public Image life3;

	public Sprite lifeFull;
	public Sprite lifeHalf;
	public Sprite lifeEmpty;

	public int maxLife;
	public int lifeCount;

	private bool respawning;

	//array to hold objects to reset
	public ResetOnRespawn[] objectsToReset;

	public bool invincible; 

	//lives controller
	public Text livesText;
	public int startingLives;
	public int currentLives;

	public GameObject gameOverScreen;

	//music stuff?
	public AudioSource levelMusic;
	public AudioSource gameOverMusic;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerController> ();



		//set starting health
		lifeCount = maxLife;

		objectsToReset = FindObjectsOfType<ResetOnRespawn> ();

		//This will check to see if player has coins and will assign that number of coins to the counter upon loading level
		if (PlayerPrefs.HasKey ("CoinCount")) 
		{
			coinCount = PlayerPrefs.GetInt ("CoinCount");
		}

		//sets starting number of coins
		coinText.text = " " + coinCount;

		//cheks to see number of player lives, saves and loads that number upon loading level
		if (PlayerPrefs.HasKey ("PlayerLives")) {
			currentLives = PlayerPrefs.GetInt ("PlayerLives");
		} else {
			currentLives = startingLives;
		}

		//sets starting lives
		livesText.text = "lives: " + currentLives;
	}
	
	// Update is called once per frame
	void Update () {
		if (lifeCount <= 0 && !respawning) 
		{
			Respawn ();
			respawning = true;
		}
	}

	//Checks for remaining lives. Game over or respawns PC accordingly, starts game over music

	public void Respawn()
	{
		currentLives -= 1;
		livesText.text = "lives: " + currentLives;

		if (currentLives > 0) {
			StartCoroutine ("RespawnCo");
		} else {
			thePlayer.gameObject.SetActive (false);
			gameOverScreen.SetActive (true);
			levelMusic.Stop ();
			gameOverMusic.Play ();
		}



	}

	public IEnumerator RespawnCo()
	{
		thePlayer.gameObject.SetActive (false);

		Instantiate (deathSplosion, thePlayer.transform.position, thePlayer.transform.rotation);

		yield return new WaitForSeconds (waitToRespawn);

		lifeCount = maxLife;
		respawning = false;
		UpdateLifeMeter ();

		//resets coin count upon death
		coinCount = 0;
		coinText.text = " " + coinCount;

		thePlayer.transform.position = thePlayer.respawnPosition;
		thePlayer.gameObject.SetActive (true);

		//goes through every item in objectsToRespawn array 
		for (int i = 0; i < objectsToReset.Length; i++) 
		{
			objectsToReset [i].gameObject.SetActive (true);
			objectsToReset [i].ResetObject();
		}

		SlowPlane.SetActive(true);
	}

	//Adds coins on pickup
	public void AddCoins(int coinsToAdd)
	{
		coinCount += coinsToAdd;

		coinText.text = " " + coinCount;
		coinSound.Play ();
	}

	//subtracts health, updates health UI, applies knockback function
	public void HurtPlayer(int damageToTake)
	{
		if (!invincible) 
		{
			lifeCount -= damageToTake;
			UpdateLifeMeter ();

			thePlayer.Knockback ();

			thePlayer.hurtSound.Play ();
		}
	}


	//adds health on pick up of health item, but not over the max. Then it updates health meter
	public void GiveHealth(int healthToGive)
	{
		lifeCount += healthToGive;

		if (lifeCount > maxLife) 
		{
			lifeCount = maxLife;
		}

		UpdateLifeMeter ();
	}

	public void UpdateLifeMeter()
	{
		switch (lifeCount) 
		{
		case 3:
			life1.sprite = lifeFull;
			life2.sprite = lifeFull;
			life3.sprite = lifeFull;
			return;

		case 2:
			life1.sprite = lifeFull;
			life2.sprite = lifeFull;
			life3.sprite = lifeEmpty;
			return;

		case 1:
			life1.sprite = lifeFull;
			life2.sprite = lifeEmpty;
			life3.sprite = lifeEmpty;
			return;

		case 0:
			life1.sprite = lifeEmpty;
			life2.sprite = lifeEmpty;
			life3.sprite = lifeEmpty;
			return;

		default: 
			life1.sprite = lifeEmpty;
			life2.sprite = lifeEmpty;
			life3.sprite = lifeEmpty;
			return;
		}
	}

	//adds lives when picking up coffee cup item
	public void AddLives(int livesToAdd)
	{
		currentLives += livesToAdd;
		livesText.text = "lives: " + currentLives;
	}

}
