using UnityEngine;
using System.Collections;

public class GrayBrickScript : MonoBehaviour {

	private bool isTouch = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (!coll.gameObject.name.Equals ("marry")) {
			return;
		}

		isTouch = true;
	}

	void OnCollisionExit2D(Collision2D coll) {

		if (!coll.gameObject.name.Equals ("marry")) {
			return;
		}

		if (isTouch) {
			Destroy (gameObject);
		}
	}
}
