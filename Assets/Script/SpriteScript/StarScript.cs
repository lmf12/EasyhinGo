using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StarScript : MonoBehaviour {

	public Text score;
	public Text win;
	private bool isGet = false;

	private bool isWin = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {

		if (isGet) {
			transform.Translate (new Vector2(0f, 0.1f));
			if (transform.position.y > 200f) {

				transform.Translate (new Vector2(0f, 10f));
				isGet = false;
			}
		}
			
		if (isWin) {
			win.transform.Translate (new Vector2 (0, -2f));

			if (win.transform.position.y <= Screen.height / 2) {
				Application.LoadLevel (1);
			}
		}
			
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (!other.gameObject.name.Equals ("marry")) {
			return;
		}

		isGet = true;

		score.text = "" + (int.Parse (score.text) + 1);

		if (int.Parse (score.text) == 5) {

			isWin = true;
		}
	}

}
