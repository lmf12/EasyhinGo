using UnityEngine;
using System.Collections;

public class MoveFloorScript : MonoBehaviour {

	private float leftLoc = 8f;
	private float rightLoc = 13f;
	private bool isRight = true;
	private float speed = 0.05f;

	private GameObject marry; //站着的人物

	// Use this for initialization
	void Start () {
	
		marry = null;
	}

	// Update is called once per frame
	void FixedUpdate () {
	
		if (transform.position.x >= this.rightLoc) {
			transform.position = new Vector2(this.rightLoc, transform.position.y);
			isRight = false;
		} else if (transform.position.x <= this.leftLoc) {
			transform.position = new Vector2(this.leftLoc, transform.position.y);
			isRight = true;
		}

		if (isRight && transform.position.x < this.rightLoc) {
			transform.Translate (new Vector3 (speed, 0, 0));
			if (marry != null) {
				marry.transform.Translate (new Vector3 (speed, 0, 0));
			} 
		} else if (!isRight && transform.position.x > this.leftLoc) {
			transform.Translate (new Vector3 (-speed, 0, 0));
			if (marry != null) {
				marry.transform.Translate (new Vector3 (-speed, 0, 0));
			} 
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (!coll.gameObject.name.Equals ("marry")) {
			return;
		}

		marry = coll.gameObject;
	}

	void OnCollisionStay2D(Collision2D coll) {

		if (!coll.gameObject.name.Equals ("marry")) {
			return;
		}

		marry = coll.gameObject;
	}

	void OnCollisionExit2D(Collision2D coll) {

		if (!coll.gameObject.name.Equals ("marry")) {
			return;
		}

		marry = null;
	}
}
