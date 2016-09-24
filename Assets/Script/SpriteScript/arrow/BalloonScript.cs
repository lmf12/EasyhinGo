using UnityEngine;
using System.Collections;

public class BalloonScript : MonoBehaviour {

	private float g = 9.8f; //重力

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void FixedUpdate () {
	
		GetComponent<Rigidbody2D> ().AddForce (new Vector2(0, 2*g));
	}
}
