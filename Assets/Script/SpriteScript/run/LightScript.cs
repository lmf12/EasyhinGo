using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LightScript : MonoBehaviour {

	public Button setButton;
	public GameObject floorPrefab;

	private float distance = 5.0f;
	private float moveSpeed = 0.1f;

	private bool startMoving = false;
	private bool following = false;

	private bool canShowSetButton = false;

	private float spaceY = 0.5f;

	private Vector2 startLoc;

	private GameObject marry;

	// Use this for initialization
	void Start () {
	
		startLoc = transform.position;
		this.reset ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float newLightX = transform.transform.position.x;
		float newLightY = transform.transform.position.y;

		if (startMoving && marry != null) {
			newLightX += moveSpeed;
			newLightY = marry.transform.position.y + spaceY;
			if (newLightX - marry.transform.position.x >= distance) {
				newLightX = marry.transform.position.x + distance;
				startMoving = false;
				following = true;
			}
		}

		if (following) {

			newLightX = marry.transform.position.x + distance;
			newLightY = marry.transform.position.y  + spaceY;
		}

		transform.position = new Vector2 (newLightX, newLightY);

		if (canShowSetButton) {
			setButton.transform.localScale = new Vector2 (1, 1);
		} else {
			setButton.transform.localScale = new Vector2 (0, 0);
		}
	}

	public void setFloor() {

		Instantiate (floorPrefab, new Vector2(transform.position.x, transform.position.y - 2*spaceY), Quaternion.identity);
	}

	public void reset () {

		startMoving = false;
		following = false;
		canShowSetButton = false;
		marry = null;
		setButton.transform.localScale = new Vector2 (0, 0); 

		transform.position = startLoc;
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (marry == null) {
			startMoving = true;
			marry = other.gameObject;
		}

		if (marry != null) {

			canShowSetButton = false;
		}
	}

	void OnTriggerStay2D(Collider2D other) {

		if (marry != null) {

			canShowSetButton = false;
		}
	}

	void OnTriggerExit2D(Collider2D other) {

		if (marry != null) {

			canShowSetButton = true;
		}
	}
}
