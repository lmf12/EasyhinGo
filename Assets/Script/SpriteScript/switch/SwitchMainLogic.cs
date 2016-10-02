using UnityEngine;
using System.Collections;

public class SwitchMainLogic : MonoBehaviour {

	public GameObject background1;
	public GameObject background2;

	private Vector2 touchLastPos;
	private float moveDistance;

	bool isScrolling = false;

	private float baseVelocity = 12.0f;
	private float baseForce = 0.3f;
	private float minDis = 0.4f; 
	private float beginX1 = 25.0f;
	private float endX1 = -25.0f;
	private float beginX2 = 11.82f;
	private float endX2 = -11.82f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (isScrolling) {
			if (background1.transform.position.x <= endX1 + minDis) {

				background1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				background1.transform.position = new Vector2 (endX1, background1.transform.position.y);
				isScrolling = false;
			} else if (background1.transform.position.x >= beginX1 - minDis) {

				background1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				background1.transform.position = new Vector2 (beginX1, background1.transform.position.y);
				isScrolling = false;
			}

			if (background1.GetComponent<Rigidbody2D> ().velocity.x > 0) {
				if (background1.GetComponent<Rigidbody2D> ().velocity.x - baseForce > 0) {
					background1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (background1.GetComponent<Rigidbody2D> ().velocity.x - baseForce, 0);
				}
				else {
					background1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
					isScrolling = false;
				}
			} else if (background1.GetComponent<Rigidbody2D> ().velocity.x < 0) {
				if (background1.GetComponent<Rigidbody2D> ().velocity.x + baseForce < 0) {
					background1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (background1.GetComponent<Rigidbody2D> ().velocity.x + baseForce, 0);
				}
				else {
					background1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
					isScrolling = false;
				}
			}
		}

		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				
				touchLastPos = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
				background1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				isScrolling = false;
			} else if (Input.GetTouch (0).phase == TouchPhase.Moved) {

				Vector2 touchCurrentPos = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
				moveDistance = touchCurrentPos.x - touchLastPos.x;

				float afterPosX = background1.transform.position.x + moveDistance;
				if (afterPosX <= endX1) {
					background1.transform.position = new Vector2 (endX1, background1.transform.position.y);
				} else if (afterPosX >= beginX1) {
					background1.transform.position = new Vector2 (beginX1, background1.transform.position.y);
				} else {
					background1.transform.Translate (new Vector3(moveDistance, 0, 0));
				}
				touchLastPos = touchCurrentPos;
			} else if (Input.GetTouch (0).phase == TouchPhase.Ended) {


				if (moveDistance != 0 && background1.transform.position.x - endX1 >= minDis && beginX1 - background1.transform.position.x >= minDis) {
					background1.GetComponent<Rigidbody2D> ().velocity = new Vector2(baseVelocity * moveDistance, 0);
					isScrolling = true;
				}
			}
		}

		updateBackground2Pos ();
	}

	private void updateBackground2Pos() {

		Vector2 pos = background1.transform.position;
		float background2X = pos.x * (beginX2 / beginX1);
		background2.transform.position = new Vector2 (background2X, background2.transform.position.y);
	}
}
