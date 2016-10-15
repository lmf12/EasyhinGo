using UnityEngine;
using System.Collections;

public class BalloonObjectScript : MonoBehaviour {

	private float duration = 2.0f;  //动画持续
	private float maxScale = 9.0f;
	private float minThin = 0.6f;

	private bool isExpanding = true;  //正在变大
	private bool isThining = true;    //正在变淡
	private bool isPlaying = false;  //是否播放动画

	private float moveSpeedY;
	private float scaleSpeed;
	private float thinSpead;

	Vector2 targetLoc = new Vector2(0, 0);

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (transform.position.y <= -12 && isPlaying == false) {

			rotation (gameObject, 0);
			Destroy (gameObject.GetComponent<Rigidbody2D>());
			Destroy (gameObject.GetComponent<PolygonCollider2D>());
			Vector2 pos = transform.position;
			pos.x = 0;
			transform.position = pos;
			isPlaying = true;

			moveSpeedY = (targetLoc.y - transform.position.y) / (duration / Time.fixedDeltaTime);
			scaleSpeed = ((maxScale - 0.8f) + (maxScale - 2))  / (duration / Time.fixedDeltaTime);
			thinSpead = (this.GetComponent<SpriteRenderer> ().color.a - minThin) / (duration / 2 / Time.fixedDeltaTime);
		}

		if (!isPlaying) {
			return;
		}

		//移动Y
		if (Mathf.Abs (targetLoc.y - transform.position.y) <= moveSpeedY) {
			Vector2 pos = transform.position;
			pos.y = targetLoc.y;
			transform.position = pos;
		} else {
			transform.Translate (new Vector2(0, moveSpeedY));
		}

		if (isExpanding) {  //扩大
			if (Mathf.Abs (maxScale - transform.localScale.x) <= scaleSpeed) {

				transform.localScale = new Vector2 (maxScale, maxScale);
				isExpanding = false;
			} else {

				transform.localScale = new Vector2 (transform.localScale.x + scaleSpeed, transform.localScale.y + scaleSpeed);
			}
		} else {  //缩小

			if (Mathf.Abs (2 - transform.localScale.x) <= scaleSpeed) {

				transform.localScale = new Vector2 (2, 2);
			} else {

				transform.localScale = new Vector2 (transform.localScale.x - scaleSpeed, transform.localScale.y - scaleSpeed);
			}
		}

		if (isThining) {  //变淡
			if (Mathf.Abs (minThin - this.GetComponent<SpriteRenderer> ().color.a) <= thinSpead) {

				Color c = this.GetComponent<SpriteRenderer> ().color; 
				c.a = minThin; 
				this.GetComponent<SpriteRenderer> ().color = c;

				isThining = false;
			} else {

				Color c = this.GetComponent<SpriteRenderer> ().color; 
				c.a -= thinSpead; 
				this.GetComponent<SpriteRenderer> ().color = c;
			}
		} else {   //变深
			if (Mathf.Abs (1 - this.GetComponent<SpriteRenderer> ().color.a) <= thinSpead) {

				Color c = this.GetComponent<SpriteRenderer> ().color; 
				c.a = 1; 
				this.GetComponent<SpriteRenderer> ().color = c;
			} else {

				Color c = this.GetComponent<SpriteRenderer> ().color; 
				c.a += thinSpead; 
				this.GetComponent<SpriteRenderer> ().color = c;
			}
		}

		//完成动画
		if (transform.position.y == targetLoc.y && transform.localScale.x == 2 && this.GetComponent<SpriteRenderer> ().color.a == 1) {
			Application.LoadLevel (1);
		}


	}

	//旋转 从0～2pi  顺时针
	private void rotation(GameObject obj ,float r) {

		Quaternion rotation = obj.transform.localRotation;

		float angle = r;

		rotation.z = Mathf.Sin(angle / 2);

		rotation.w = Mathf.Cos(angle / 2);

		obj.transform.localRotation = rotation;
	}
}
