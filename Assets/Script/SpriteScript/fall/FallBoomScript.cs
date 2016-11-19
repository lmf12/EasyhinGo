using UnityEngine;
using System.Collections;

public class FallBoomScript : MonoBehaviour {

	public GameObject firePrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other) {

		GameObject mainLogic = GameObject.Find ("FallMainLogic");

		if (other.gameObject.name.Equals ("people_1")) {
			mainLogic.GetComponent<FallMainLogicScript> ().getBoom ();

			mainLogic.GetComponent<FallMainLogicScript> ().createText ("+20s");
		}

		Destroy(transform.GetComponent<Rigidbody2D> ());
		GameObject fire = (GameObject)Instantiate (firePrefab, new Vector2(transform.position.x, transform.position.y-0.15f), Quaternion.identity);

		Destroy (gameObject, 0.1f);
		Destroy (fire, 0.2f);
	}

	void OnTriggerExit2D(Collider2D other) {

	}
}
