using UnityEngine;
using System.Collections;

public class BalloonScript : MonoBehaviour {

	public GameObject obj;

	private float g = 9.8f; //重力

	public float maxY;
	public float minY;
	public bool isUp;

	public float upForce;
	public float downForce;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void FixedUpdate () {

		Vector2 pos = transform.position;

		if (isUp) {

			if (pos.y <= maxY) {
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, upForce * g));
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				isUp = false;
			}

		} else {
			
			if (pos.y >= minY) {
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, downForce * g));
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				isUp = true;
			}

		}

	}

	void OnTriggerStay2D(Collider2D other) {

		if (other.gameObject.name.Substring (0, 5).Equals ("Arrow")) {

			Destroy (obj.GetComponent<DistanceJoint2D>());
			Destroy (gameObject);

			playSound (1);
		}

	}

	void OnTriggerExit2D(Collider2D other) {

	}

	public void playSound(int index) {

		GameObject.Find("Main Camera").GetComponent<CameraScript>().PlaySound (index, false, 1);
	}
}
