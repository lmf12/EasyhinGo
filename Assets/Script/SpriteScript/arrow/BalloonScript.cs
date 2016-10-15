using UnityEngine;
using System.Collections;

public class BalloonScript : MonoBehaviour {

	public GameObject obj;

	private float g = 9.8f; //重力

	private float maxY = 3.0f;
	private float minY = -1.0f;
	private float moveSpeed = 0.03f;
	private bool isUp = true;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void FixedUpdate () {

		Vector2 pos = transform.position;

		print (pos);

		if (isUp) {

			if (pos.y <= maxY) {
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 2.2f * g));
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				isUp = false;
			}

		} else {
			
			if (pos.y >= minY) {
				GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 1.8f * g));
			} else {
				GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
				isUp = true;
			}

		}

	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (coll.gameObject.name.Substring (0, 5).Equals ("Arrow")) {

			Destroy (obj.GetComponent<DistanceJoint2D>());
			Destroy (gameObject);
		}

	}

	void OnCollisionExit2D(Collision2D coll) {

	}
}
