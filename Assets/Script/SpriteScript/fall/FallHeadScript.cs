using UnityEngine;
using System.Collections;

public class FallHeadScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other) {

		GameObject mainLogic = GameObject.Find ("FallMainLogic");

		if (other.gameObject.name.Equals ("people_1")) {

			if (gameObject.name.Equals ("MonoPrefab(Clone)")) {
				mainLogic.GetComponent<FallMainLogicScript> ().getMono ();
			} else if (gameObject.name.Equals ("RayPrefab(Clone)")) {
				mainLogic.GetComponent<FallMainLogicScript> ().getRay ();
			} else if (gameObject.name.Equals ("RocPrefab(Clone)")) {
				mainLogic.GetComponent<FallMainLogicScript> ().getRoc ();
			} else if (gameObject.name.Equals ("SongPrefab(Clone)")) {
				mainLogic.GetComponent<FallMainLogicScript> ().getSong ();
			}
		}

		Destroy(gameObject);
	}

	void OnTriggerExit2D(Collider2D other) {

	}
}
