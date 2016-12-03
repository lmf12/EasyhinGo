using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

	public Image icon;
	public Sprite yellowIcon;

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


		isPlaying = false;

		GameObject.Find ("MainLogic").GetComponent<ArrowMainLogicScript>().addScore(gameObject);

		icon.sprite = yellowIcon;

		playSound (2);
	}

	//旋转 从0～2pi  顺时针
	private void rotation(GameObject obj ,float r) {

		Quaternion rotation = obj.transform.localRotation;

		float angle = r;

		rotation.z = Mathf.Sin(angle / 2);

		rotation.w = Mathf.Cos(angle / 2);

		obj.transform.localRotation = rotation;
	}

	public void playSound(int index) {

		GameObject.Find("Main Camera").GetComponent<CameraScript>().PlaySound (index, false, 1);
	}
}
