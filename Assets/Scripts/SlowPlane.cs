using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SlowPlane : MonoBehaviour {
	public GameObject playerController;
	private Rigidbody2D myRigidbody;

	// Use this for initialization
	void Start () {
		myRigidbody = playerController.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {

	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.CompareTag("Player"))
		{
			myRigidbody.velocity = new Vector3(myRigidbody.velocity.x, 0f, 0f);
			transform.gameObject.SetActive(false);
		}
	}
}