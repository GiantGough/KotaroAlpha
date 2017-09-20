using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject target;
	public float followAhead;

	private Vector3 targetPosition;

	public float smoothing;

	public bool followTarget;

	// Use this for initialization
	void Start () {
		followTarget = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (followTarget) {
			targetPosition = new Vector3 (target.transform.position.x, transform.position.y + 10, transform.position.z);

			// this moves target of the camera ahead of the player
			if (target.transform.localScale.x > 0f) {
				targetPosition = new Vector3 (target.transform.position.x, Mathf.Clamp (target.transform.position.y, -15.6f, 20f), transform.position.z);
			} else {
				targetPosition = new Vector3 (target.transform.position.x, Mathf.Clamp (target.transform.position.y, -15.6f, 20f), transform.position.z);
			}

			//transform.position = targetPosition;

			transform.position = Vector3.Lerp (transform.position, targetPosition, smoothing * Time.deltaTime);
		}
	}
}
