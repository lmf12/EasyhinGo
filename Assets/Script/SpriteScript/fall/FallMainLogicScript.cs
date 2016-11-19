using UnityEngine;
using System.Collections;

public class FallMainLogicScript : MonoBehaviour {

	public GameObject people;
	public GameObject boom;

	private float speed = 0.06f;

	private float minX = -6;
	private float maxX = 6;

	private bool isMoving;
	private bool isRight;

	private bool isLeftPress;
	private bool isRightPress;

	// Use this for initialization
	void Start () {
	
		isMoving = false;
		isRight = true;

		isLeftPress = false;
		isRightPress = false;

		createBoom (Random.Range(minX, maxX));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (GameObject.Find ("BoomPerfab(Clone)") == null) {

			createBoom (Random.Range(minX, maxX));
		}

		if (isMoving) {

			//边界判断
			if (people.transform.position.x < minX) {
				people.transform.position = new Vector2(minX, people.transform.position.y);
				isMoving = false;
				return;
			}
			if (people.transform.position.x > maxX) {
				people.transform.position = new Vector2(maxX, people.transform.position.y);
				isMoving = false;
				return;
			}

			people.transform.Translate (new Vector2 (speed, 0));
		}
	}

	public void quitGame() {

		Application.LoadLevel (1);
	}

	public void moveLeft() {

		isLeftPress = true;
		isMoving = true;
		if (isRight) {
			this.turnFace ();
		}
		isRight = false;
	}

	public void moveRight() {
	
		isRightPress = true;
		isMoving = true;
		if (!isRight) {
			this.turnFace ();
		}
		isRight = true;
	}

	//1 left 2 right
	public void stopMoving(int type) {

		if (type == 1) {
			isLeftPress = false;
		} else if (type == 2) {
			isRightPress = false;
		}

		if (isLeftPress) {
			moveLeft ();
		} else if (isRightPress) {
			moveRight ();
		} else {
			isMoving = false;
		}
	}

	//转向
	private void turnFace() {

		Vector2 scale = people.transform.localScale;
		scale.x *= -1;
		people.transform.localScale = scale;
	}

	private void createBoom(float x) {

		GameObject fire = (GameObject)Instantiate (boom, new Vector2(x, 5), Quaternion.identity);
	}
}
