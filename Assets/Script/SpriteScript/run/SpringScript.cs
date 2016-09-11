using UnityEngine;
using System.Collections;

public class SpringScript : MonoBehaviour {

	private float jumpVelocity = 12.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {

		if (!coll.gameObject.name.Equals ("marry")) {
			return;
		}

		ContactPoint2D contactBegin = coll.contacts[0];
		ContactPoint2D contactEnd = coll.contacts[coll.contacts.Length-1];

		Vector2 posBegin = contactBegin.point; 
		Vector2 posEnd = contactEnd.point; 

		if (posBegin.y > transform.position.y && posEnd.y > transform.position.y) {

			Vector2 velocity = coll.gameObject.GetComponent<Rigidbody2D> ().velocity;
			velocity.y = jumpVelocity;
			coll.gameObject.GetComponent<Rigidbody2D> ().velocity = velocity;
		}
	}

	void OnCollisionStay2D(Collision2D coll) {

	}

	void OnCollisionExit2D(Collision2D coll) {

	}
}
