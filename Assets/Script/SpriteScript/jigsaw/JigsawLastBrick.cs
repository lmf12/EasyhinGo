using UnityEngine;
using System.Collections;

public class JigsawLastBrick : MonoBehaviour {
	private float duration = 2.0f;  //动画持续

	private float maxScale = 9.0f;
	private float minThin = 0.6f;

	private float moveSpeedX;
	private float moveSpeedY;
	private float scaleSpeed;
	private float thinSpead;

	private float originScale;

	private bool isExpanding = true;  //正在变大
	private bool isThining = true;    //正在变淡
	private bool isPlaying = false;  //是否播放动画

	Vector2 targetLoc = new Vector2(2.6f, -3.6f);

	// Use this for initialization
	void Start () {
	
		originScale = transform.localScale.x;

		moveSpeedX = (targetLoc.x - transform.position.x) / (duration / Time.fixedDeltaTime);
		moveSpeedY = (targetLoc.y - transform.position.y) / (duration / Time.fixedDeltaTime);
		scaleSpeed = (maxScale - originScale) / (duration / 2 / Time.fixedDeltaTime);
		thinSpead = (this.GetComponent<SpriteRenderer> ().color.a - minThin) / (duration / 2 / Time.fixedDeltaTime);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

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

		//移动X
		if (Mathf.Abs (targetLoc.x - transform.position.x) <= moveSpeedX) {
			Vector2 pos = transform.position;
			pos.x = targetLoc.x;
			transform.position = pos;
		} else {
			transform.Translate (new Vector2(moveSpeedX, 0));
		}
			
		if (isExpanding) {  //扩大
			if (Mathf.Abs (maxScale - transform.localScale.x) <= scaleSpeed) {

				transform.localScale = new Vector2 (maxScale, maxScale);
				isExpanding = false;
			} else {

				transform.localScale = new Vector2 (transform.localScale.x + scaleSpeed, transform.localScale.y + scaleSpeed);
			}
		} else {  //缩小

			if (Mathf.Abs (originScale - transform.localScale.x) <= scaleSpeed) {

				transform.localScale = new Vector2 (originScale, originScale);
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
		if (transform.position.x == targetLoc.x && transform.position.y == targetLoc.y && transform.localScale.x == originScale && this.GetComponent<SpriteRenderer> ().color.a == 1) {
			Application.LoadLevel (1);
		}
			
	}

	public void startAnimation() {

		this.isPlaying = true;
	}

	public bool isFinish() {

		return isPlaying;
	}
}
