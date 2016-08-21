using UnityEngine;
using System.Collections;

public class MarryScript : MonoBehaviour {

	private bool isMoving = false;
	private bool isRight = true;   //当前方向，true为右，false为左
	private float speed = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (isMoving) {
			this.gameObject.transform.Translate (new Vector2 (this.speed, 0));
		}

	}

	public void moveLeft(float speed) {
		
		isMoving = true;
		if (isRight) {
			this.turnFace ();
		}
		isRight = false;
		this.speed = -speed;
	}

	public void moveRight(float speed) {

		isMoving = true;
		if (!isRight) {
			this.turnFace ();
		}
		isRight = true;
		this.speed = speed;
	}

	public void stopMoving() {

		isMoving = false;
	}

	//转向
	private void turnFace() {

		Vector2 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}
}
