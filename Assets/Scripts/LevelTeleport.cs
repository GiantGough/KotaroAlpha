using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTeleport : MonoBehaviour {

	public string levelToLoad;

	public bool unlocked;

	//deals with sprites for hub 'doors'
	public Sprite doorLeftOpen;
	public Sprite doorRightOpen;
	public Sprite doorLeftClosed;
	public Sprite doorRightClosed;

	public SpriteRenderer doorLeft;
	public SpriteRenderer doorRight;



	// Use this for initialization
	void Start () {

		//Sets level1 to have the value of 1 so that it's always unlocked.
		PlayerPrefs.SetInt("Level1", 1);


		//gets the int value of whatever is stored as levlToLoad
		if (PlayerPrefs.GetInt (levelToLoad) == 1) {
			unlocked = true;
		} else {
			unlocked = false;
		}


		//checks to see if unlocked, if unlocked changes sprite accordingly
		if (unlocked) {
			doorLeft.sprite = doorLeftOpen;
			doorRight.sprite = doorRightOpen;
		} else {
			doorLeft.sprite = doorLeftClosed;
			doorRight.sprite = doorRightClosed;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			//if (Input.GetButtonDown ("Jump") && unlocked) 
			if(Input.GetKey(KeyCode.E))
			{
				SceneManager.LoadScene (levelToLoad);
			}
		}
	}

}
