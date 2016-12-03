using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StarScript : MonoBehaviour {

	private CameraScript soundScript;


	public Sprite money;
	public Image win;
	public Text scoreText;

	public Image score1;
	public Image score2;
	public Image score3;
	public Image score4;

	private bool isGet = false;
	private bool isWin = false;

	// Use this for initialization
	void Start () {

		// 获取摄像机的组件
		soundScript = Camera.main.GetComponent<CameraScript>();
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

			scoreText.text = GameObject.Find ("RunMainLogic").GetComponent<RunMainLogicScript> ().getTimeString ();
			win.transform.localScale = new Vector2 (1, 1);
			GameObject.Find ("RunMainLogic").GetComponent<RunMainLogicScript> ().setGameEnd ();
		}
			
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (!other.gameObject.name.Equals ("marry")) {
			return;
		}

		isGet = true;
		soundScript.PlaySound (0, false, 1);

		GameObject.Find ("marry").GetComponent<MarryScript> ().addScore ();

		updateScoreView ();

		if (GameObject.Find ("marry").GetComponent<MarryScript> ().getScore() == 4) {

			isWin = true;
		}
	}

	public void goBack() {

		Application.LoadLevel (1);

		PlayerPrefs.SetString ("switch_postion", "" + 3);

	}

	private void updateScoreView() {

		int currentScore = GameObject.Find ("marry").GetComponent<MarryScript> ().getScore ();

		if (currentScore > 0) {
			
			score1.sprite = money;

			if (currentScore > 1) {
				
				score2.sprite = money;

				if (currentScore > 2) {

					score3.sprite = money;

					if (currentScore > 3) {

						score4.sprite = money;

					}
				}
			}
		}

	}
}
