using UnityEngine;
using System.Collections;

public class MarryScript : MonoBehaviour {

	public GameObject screenLogic;

	private bool isMoving = false;
	private bool isRight = true;   //当前方向，true为右，false为左
	private float speed = 0;
	private bool isGround = true;
	private float jumpVelocity = 5.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector2 worldMinLoc = screenLogic.GetComponent<ScreenScript> ().getMinWorldLoc ();
		Vector2 worldMaxLoc = screenLogic.GetComponent<ScreenScript> ().getMaxWorldLoc ();

		if (isMoving) {

			//边界判断
			if (transform.position.x < worldMinLoc.x) {
				transform.position = new Vector2(worldMinLoc.x, transform.position.y);
				this.stopMoving ();
				return;
			}
			if (transform.position.x > worldMaxLoc.x) {
				transform.position = new Vector2(worldMaxLoc.x, transform.position.y);
				this.stopMoving ();
				return;
			}

			this.gameObject.transform.Translate (new Vector2 (this.speed, 0));
		}

		//相机跟随
		this.cameraFollow();

		//判断死亡
		if (transform.position.y < worldMinLoc.y) {

			Application.LoadLevel (1);
		}
	}

	public void moveLeft(float speed) {

		this.setClimbing (false);

		isMoving = true;
		if (isRight) {
			this.turnFace ();
		}
		isRight = false;
		this.speed = -speed;
	}

	public void moveRight(float speed) {

		this.setClimbing (false);

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

	public void jump() {

		if (isGround) {
			Vector2 velocity = GetComponent<Rigidbody2D> ().velocity;
			velocity.y = jumpVelocity;
			GetComponent<Rigidbody2D> ().velocity = velocity;
		}
	}

	//设置攀爬模式，此时不受重力影响
	public void setClimbing(bool isClimb) {

		if (isClimb) {
			GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
		} else {
			GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeRotation;
		}
	}

	//转向
	private void turnFace() {

		Vector2 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	//相机跟随
	private void cameraFollow() {

		Vector3 screenWorldLoc = screenLogic.GetComponent<ScreenScript> ().getScreenWorldLoc ();
		Vector2 screenMinLoc = screenLogic.GetComponent<ScreenScript> ().getCameraMinLoc ();
		Vector2 screenMaxLoc = screenLogic.GetComponent<ScreenScript> ().getCameraMaxLoc ();
		Vector2 worldMinLoc = screenLogic.GetComponent<ScreenScript> ().getMinWorldLoc ();
		Vector2 worldMaxLoc = screenLogic.GetComponent<ScreenScript> ().getMaxWorldLoc ();

		float cameraLocX = screenWorldLoc.x;
		float cameraLocY = screenWorldLoc.y;
		float cameraLocZ = screenWorldLoc.z;

		//上移
		if (transform.position.y > (screenWorldLoc.y+screenMinLoc.y)*0.5f && screenMaxLoc.y < worldMaxLoc.y) {	
			cameraLocY = transform.position.y + (screenWorldLoc.y - screenMinLoc.y) * 0.5f;
		}
		//下移
		if (transform.position.y < (screenWorldLoc.y+screenMinLoc.y)*0.5f && screenMinLoc.y > worldMinLoc.y) {	
			cameraLocY = transform.position.y + (screenWorldLoc.y - screenMinLoc.y) * 0.5f;
		}
		//左移
		if (transform.position.x < screenWorldLoc.x && screenMinLoc.x > worldMinLoc.x) {
			cameraLocX = transform.position.x;
		}
		//右移
		if (transform.position.x > screenWorldLoc.x && screenMaxLoc.x < worldMaxLoc.x) {	
			cameraLocX = transform.position.x;
		}

		Camera.main.transform.position = new Vector3 (cameraLocX, cameraLocY, cameraLocZ);

	}

	void OnCollisionEnter2D(Collision2D coll) {

		ContactPoint2D contactBegin = coll.contacts[0];
		ContactPoint2D contactEnd = coll.contacts[coll.contacts.Length-1];

		Vector2 posBegin = contactBegin.point; 
		Vector2 posEnd = contactEnd.point; 

		if (posBegin.y < transform.position.y && posEnd.y < transform.position.y) {
			isGround = true;
		}
	}

	void OnCollisionStay2D(Collision2D coll) {

		ContactPoint2D contactBegin = coll.contacts[0];
		ContactPoint2D contactEnd = coll.contacts[coll.contacts.Length-1];

		Vector2 posBegin = contactBegin.point; 
		Vector2 posEnd = contactEnd.point; 

		if (posBegin.y < transform.position.y && posEnd.y < transform.position.y) {
			isGround = true;
		}
	}

	void OnCollisionExit2D(Collision2D coll) {

		isGround = false;
	}
}
