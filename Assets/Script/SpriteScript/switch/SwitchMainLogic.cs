using UnityEngine;
using System.Collections;

public class SwitchMainLogic : MonoBehaviour {

	public GameObject background;

	private Vector2 touchLastPos;
	private float moveDistance;

	bool isScrolling = false;

	private float baseVelocity = 12.0f;
	private float baseForce = 0.3f;
	private float beginX = 11.82f;
	private float endX = -11.82f;
	private float minDis = 0.4f; 

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (isScrolling) {
			if (background.transform.position.x <= endX + minDis) {

				background.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				background.transform.position = new Vector2 (endX, background.transform.position.y);
				isScrolling = false;
			} else if (background.transform.position.x >= beginX - minDis) {

				background.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				background.transform.position = new Vector2 (beginX, background.transform.position.y);
				isScrolling = false;
			}

			if (background.GetComponent<Rigidbody2D> ().velocity.x > 0) {
				if (background.GetComponent<Rigidbody2D> ().velocity.x - baseForce > 0) {
					background.GetComponent<Rigidbody2D> ().velocity = new Vector2 (background.GetComponent<Rigidbody2D> ().velocity.x - baseForce, 0);
				}
				else {
					background.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
					isScrolling = false;
				}
			} else if (background.GetComponent<Rigidbody2D> ().velocity.x < 0) {
				if (background.GetComponent<Rigidbody2D> ().velocity.x + baseForce < 0) {
					background.GetComponent<Rigidbody2D> ().velocity = new Vector2 (background.GetComponent<Rigidbody2D> ().velocity.x + baseForce, 0);
				}
				else {
					background.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
					isScrolling = false;
				}
			}
		}

		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				
				touchLastPos = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
				background.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				isScrolling = false;
			} else if (Input.GetTouch (0).phase == TouchPhase.Moved) {

				Vector2 touchCurrentPos = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
				moveDistance = touchCurrentPos.x - touchLastPos.x;

				float afterPosX = background.transform.position.x + moveDistance;
				if (afterPosX <= endX) {
					background.transform.position = new Vector2 (endX, background.transform.position.y);
				} else if (afterPosX >= beginX) {
					background.transform.position = new Vector2 (beginX, background.transform.position.y);
				} else {
					background.transform.Translate (new Vector3(moveDistance, 0, 0));
				}
				touchLastPos = touchCurrentPos;
			} else if (Input.GetTouch (0).phase == TouchPhase.Ended) {


				if (moveDistance != 0 && background.transform.position.x - endX >= minDis && beginX - background.transform.position.x >= minDis) {
					background.GetComponent<Rigidbody2D> ().velocity = new Vector2(baseVelocity * moveDistance, 0);
					isScrolling = true;
				}
			}
		}
	}
}
