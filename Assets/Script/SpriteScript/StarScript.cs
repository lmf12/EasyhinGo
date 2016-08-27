using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StarScript : MonoBehaviour {

	private bool isGet = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

		if (isGet) {
			transform.Translate (new Vector2(0f, 0.1f));
		}

		if (transform.position.y > 200f) {

			transform.Translate (new Vector2(0f, 10f));
			isGet = false;
		}
			
	}

	void OnTriggerEnter2D(Collider2D other) {

		isGet = true;
	}

}
