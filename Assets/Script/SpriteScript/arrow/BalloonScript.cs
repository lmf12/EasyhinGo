using UnityEngine;
using System.Collections;

public class BalloonScript : MonoBehaviour {

	public GameObject obj;

	private float g = 9.8f; //重力

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void FixedUpdate () {
	
		GetComponent<Rigidbody2D> ().AddForce (new Vector2(0, 2*g));
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
