using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	public Rigidbody2D myRigidbody;

	public bool canMove;

	public float jumpSpeed;

	// Checks to see if player is on the ground
	public Transform groundCheck;
	public float groundCheckradius;
	public LayerMask whatIsGround;

	public bool isGrounded;

	//
	private Animator myAnim;

	public Vector3 respawnPosition;

	public LevelManager theLevelManager;

	public GameObject stompBox;

	//variables for knockback
	public float knockbackForce;
	public float knockbackLength;
	private float knockbackCounter;

	public float invincibilityLength;
	private float invincibilityCounter;

	//sound effects variables
	public AudioSource jumpSound;
	public AudioSource hurtSound;

	//On ladder check
	public bool onLadder;
	public float climbSpeed;
	private float climbVelocity;
	private float gravityStore;

	//animation paramater test
	public bool isClimbing;

	// Use this for initialization
	void Start () {
		
		myRigidbody = GetComponent<Rigidbody2D> ();
		myAnim = GetComponent<Animator> ();

		// when die before reaching checkpoint respawn at initial spawn point
		respawnPosition = transform.position;

		theLevelManager = FindObjectOfType<LevelManager> ();

		canMove = true;

		//ladder gravity set up
		gravityStore = myRigidbody.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {

		// Creates overlap circle to given size and checks to see if in contact with ground. Then sets true or false accordingly
		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckradius, whatIsGround);

		if (knockbackCounter <= 0 && canMove) 
		{
			

			// Checks for a button press and moves player accordingly
			if (Input.GetAxisRaw ("Horizontal") > 0f) 
			{
				myRigidbody.velocity = new Vector3 (moveSpeed, myRigidbody.velocity.y, 0f);
				transform.localScale = new Vector3 (1f, 1f, 1f);
			} else if (Input.GetAxisRaw ("Horizontal") < 0f) 
			{
				myRigidbody.velocity = new Vector3 (-moveSpeed, myRigidbody.velocity.y, 0f);
				transform.localScale = new Vector3 (-1f, 1f, 1f);
			} else 
			{
				myRigidbody.velocity = new Vector3 (0f, myRigidbody.velocity.y, 0f);
			}

			// checks for jump button push and then jumps
			if (Input.GetButtonDown ("Jump") && isGrounded) 
			{
				myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, jumpSpeed, 0f);
				jumpSound.Play ();
			}



			//sets invincibilty counter after being knockedback
			if (invincibilityCounter > 0) 
			{
				invincibilityCounter -= Time.deltaTime;
			}

			if (invincibilityCounter <= 0) 
			{
				theLevelManager.invincible = false;
			}
		}

		if (knockbackCounter > 0) 
		{
			knockbackCounter -= Time.deltaTime;

			if (transform.localScale.x > 0) {
				myRigidbody.velocity = new Vector3 (-knockbackForce, knockbackForce, 0f);
			} else 
			{
				myRigidbody.velocity = new Vector3 (knockbackForce, knockbackForce, 0f);
			}
		}
			
		myAnim.SetFloat ("Speed", Mathf.Abs (myRigidbody.velocity.x));
		myAnim.SetBool ("Grounded", isGrounded);


		//turns on stompbox only when moving down
		if (myRigidbody.velocity.y < 0) {
			stompBox.SetActive (true);
		} else {
			stompBox.SetActive (false);
		}

		//ladder code
		if (onLadder) 
		{
			myRigidbody.gravityScale = 0f;

			climbVelocity = climbSpeed * Input.GetAxisRaw ("Vertical");

			myRigidbody.velocity = new Vector2 (myRigidbody.velocity.x, climbVelocity);
		}

		if (!onLadder) 
		{
			myRigidbody.gravityScale = gravityStore;
		}

		//climbing animation paramater test
		if (climbVelocity > 0) {
			isClimbing = true;
		} else if (climbVelocity < 0) {
			isClimbing = true;
		}
		else {
			isClimbing = false;
		}


		//myAnim.SetFloat ("Climb Speed", Mathf.Abs (myRigidbody.velocity.x));
		myAnim.SetBool ("On Ladder", onLadder);

		myAnim.SetBool ("isClimbing", isClimbing);

	}

	//knockback function
	public void Knockback()
	{
		knockbackCounter = knockbackLength;
		invincibilityCounter = invincibilityLength;
		theLevelManager.invincible = true;
	}


	void OnTriggerEnter2D(Collider2D other)
	{
		// kills player if falls
		if (other.tag == "KillPlane") 
		{
			//gameObject.SetActive (false);

			//transform.position = respawnPosition;

			// calls the respawn function located within the Level Manager
			theLevelManager.Respawn ();
		}

		// if die after reaching checkpoint respawn at last checkpoint
		if (other.tag == "Checkpoint") 
		{
			respawnPosition = other.transform.position;
		}
	}

}
