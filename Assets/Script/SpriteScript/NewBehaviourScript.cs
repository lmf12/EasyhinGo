using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour {

	public GameObject logLogic;
	public GameObject screenLogic;

	private Vector2 loc;
	private Vector2 size;

	// Use this for initialization
	void Start () {

		size = screenLogic.GetComponent<ScreenScript> ().getScreenWorldSize();
		loc = screenLogic.GetComponent<ScreenScript> ().getScreenWorldLoc();

		logLogic.GetComponent<LogScript> ().Log ("" + size.x + " " + size.y);

		logLogic.GetComponent<LogScript> ().Log ("" + loc.x + " " + loc.y);

	}
	
	// Update is called once per frame
	void Update () {
	}
}
