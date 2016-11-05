using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class JigsawBrickScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.touchCount > 0) {

			GameObject lastBrick = GameObject.Find("jigsaw_img_1_09");
			if (lastBrick.GetComponent<JigsawLastBrick> ().isFinish ()) {

				return;
			}

			if (Input.GetTouch (0).phase == TouchPhase.Began) {

				GameObject mainLogic = GameObject.Find("MainLogic");

				Vector2 touchPos = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
				Vector2 objPos = this.gameObject.transform.position;

				if ((objPos - touchPos).magnitude < mainLogic.GetComponent<JigsawMainLogicScript> ().getBrickWidth() / 2) {

					int index = mainLogic.GetComponent<JigsawMainLogicScript> ().getNextPostion (gameObject.name);

					if (index >= 0) {
						transform.position = mainLogic.GetComponent<JigsawMainLogicScript> ().getPostion (index);
						mainLogic.GetComponent<JigsawMainLogicScript> ().moveBrick (gameObject.name, index);
					}
				}

			} else if (Input.GetTouch (0).phase == TouchPhase.Moved) {
				
			} else if (Input.GetTouch (0).phase == TouchPhase.Ended) {
				
			}
		}
	}
}
