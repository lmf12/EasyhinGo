using UnityEngine;
using System.Collections;

public class HandleScript : MonoBehaviour {

	public GameObject brick;

	private float scaleSpeed = 0.005f;
	private float maxScale = 2.0f;
	private bool canSwitch = false;
	private bool canScale = false;
	private bool isTurnOn = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (canScale) {
			if (brick.transform.localScale.x - scaleSpeed <= 0) {
				brick.transform.localScale = new Vector2 (0, brick.transform.localScale.y);
				canScale = false;
				isTurnOn = false;

				Vector2 scale = transform.localScale;
				scale.x *= -1;
				transform.localScale = scale;

			} else {
				brick.transform.localScale = new Vector2 (brick.transform.localScale.x - scaleSpeed, brick.transform.localScale.y);
			}
		}
	}

	public void switchHandle () {

		if (canSwitch && !isTurnOn) {

			Vector2 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;

			brick.transform.localScale = new Vector2 (maxScale, brick.transform.localScale.y);
			canScale = true;
			isTurnOn = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other) {

		canSwitch = true;
	}

	void OnTriggerExit2D(Collider2D other) {

		canSwitch = false;
	}
}
