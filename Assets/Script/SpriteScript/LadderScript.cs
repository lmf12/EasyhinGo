using UnityEngine;
using System.Collections;

public class LadderScript : MonoBehaviour {

	private bool canClimb = false;
	private float climbSpeed;
	private bool isClimbing = false;

	private GameObject marry;

	// Use this for initialization
	void Start () {
	
		marry = null;
	}
	
	// Update is called once per frame
	void Update () {
	
		if (isClimbing && marry != null) {
			marry.transform.position = new Vector2 (transform.position.x, marry.transform.position.y + climbSpeed);

			//梯子一半以下，下跳
			if (marry.transform.position.y < transform.position.y && climbSpeed < 0) {

				this.stopClimbing ();
				marry.GetComponent<MarryScript> ().setClimbing (false);
			}
		}
	}

	public void climbUp() {

		if (canClimb && marry != null) {
			
			isClimbing = true;
			climbSpeed = 0.05f;
			marry.GetComponent<MarryScript> ().setClimbing (true);
		}
	}

	public void climbDown() {

		if (canClimb && marry != null) {

			isClimbing = true;
			climbSpeed = -0.05f;
			marry.GetComponent<MarryScript> ().setClimbing (true);
		}
	}

	public void stopClimbing() {

		isClimbing = false;
	}

	void OnTriggerEnter2D(Collider2D other) {

		canClimb = true;
		marry = other.gameObject;
	}

	void OnTriggerExit2D(Collider2D other) {

		canClimb = false;
		marry = null;
	}
}
