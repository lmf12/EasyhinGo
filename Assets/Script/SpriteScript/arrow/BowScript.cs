using UnityEngine;
using System.Collections;

public class BowScript : MonoBehaviour {

	public GameObject arrowPrefab;

	private GameObject currentArrow;

	private bool isTouchBegin;

	private bool canShoot;

	public GameObject bow2;

	// Use this for initialization
	void Start () {
	
		isTouchBegin = false;
		canShoot = true;
	}

	// Update is called once per frame
	void FixedUpdate () {

		if (currentArrow != null && currentArrow.transform.position.y <= -10) {
			canShoot = true;
			currentArrow = null;
		}

		if (!canShoot) {
			return;
		}

		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {

				Vector2 touchPos = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);

				if (this.isPosInObj (this.gameObject, touchPos)) {
					isTouchBegin = true;

					transform.localScale = new Vector2 (0, 0);
					bow2.transform.localScale = new Vector2 (1, 1);

					if (currentArrow != null) {
						Destroy (currentArrow);
					}
					currentArrow = (GameObject)Instantiate (arrowPrefab, transform.position, Quaternion.identity);
				}
			} else if (Input.GetTouch (0).phase == TouchPhase.Moved) {
				
			} else if (Input.GetTouch (0).phase == TouchPhase.Ended) {

				isTouchBegin = false;
				currentArrow.GetComponent<ArrowScript> ().beginShoot (this.getVelocity(currentArrow, 15));
				canShoot = false;

				transform.localScale = new Vector2 (1, 1);
				bow2.transform.localScale = new Vector2 (0, 0);
			}
		}

		if (isTouchBegin) {
			this.rotationBow ();
		}
	}

	//旋转弓
	private void rotationBow() {

		Vector2 touchPos = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
		Vector2 objPos = transform.position;

		if (touchPos.x < objPos.x) {
			this.rotation (this.gameObject, -this.getSin (touchPos.x - objPos.x, touchPos.y - objPos.y));

			if (currentArrow != null) {
				this.rotation (currentArrow, -this.getSin (touchPos.x - objPos.x, touchPos.y - objPos.y) - Mathf.PI / 2);
			}
		}

		updateBow2 ();
	}

	//获取速度
	private Vector2 getVelocity(GameObject obj, float speed) {

		Quaternion rotation = obj.transform.localRotation;

		float angle = Mathf.Asin (rotation.z) * 2;

		return new Vector2 (-speed * Mathf.Sin(angle), speed * Mathf.Cos(angle));
	}

	//物体是否被点中
	private bool isPosInObj(GameObject obj, Vector2 pos) {

		float objWidth = obj.GetComponent<Renderer> ().bounds.size.x;
		float objHeight = obj.GetComponent<Renderer> ().bounds.size.y;
		float objX = obj.transform.position.x;
		float objY = obj.transform.position.y;

		if ((pos.x >= objX - objWidth / 2) && (pos.x <= objX + objWidth / 2)
		    && (pos.y >= objY - objHeight / 2) && (pos.y <= objY + objHeight / 2)) {

			return true;
		} else {
			return false;
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

	//获取正弦值
	private float getSin(float x, float y) {

		return y / Mathf.Sqrt (x * x + y * y);
	}

	private void updateBow2() {

		bow2.transform.localRotation = transform.localRotation;
	}
}
