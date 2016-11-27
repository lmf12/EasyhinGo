using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RunMainLogicScript : MonoBehaviour {

	public Text textTime;

	private int totalTime;

	private bool isGameEnd;

	public Image guize;

	// Use this for initialization
	void Start () {
	
		totalTime = 0;

		isGameEnd = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void quitGame() {

		Application.LoadLevel (1);
	}

	public void rePlayGame () {

		Application.LoadLevel (3);
	}

	//游戏计时
	private void timeCount() {

		if (isGameEnd) {

			CancelInvoke ();
			return;
		}

		totalTime += 1;
		textTime.text = getTimeStringFromSecond();
	}

	private string getTimeStringFromSecond() {

		string second = "" + (totalTime % 60);
		string minute = "" + ((totalTime / 60) % 60);

		second = (second.Length == 1 ? "0" : "") + second;
		minute = (minute.Length == 1 ? "0" : "") + minute;

		return minute + " : " + second;
	}

	public string getTimeString() {

		return textTime.text;
	}

	public void setGameEnd() {

		saveScore ();

		isGameEnd = true;
	}

	private void saveScore() {

		string str = PlayerPrefs.GetString("score_3", "null");
		if (str.Equals ("null")) {
			PlayerPrefs.SetString ("score_3", "" + totalTime);
		} else {
			if (int.Parse (str) > totalTime) {
				PlayerPrefs.SetString ("score_3", "" + totalTime);
			}
		}
	}

	public void closeGuize() {

		guize.transform.localScale = new Vector2 (0, 0);

		InvokeRepeating("timeCount", 1, 1);
	}

	public void openBackPanel() {

		GameObject backBg = GameObject.Find ("back_bg");
		backBg.transform.localScale = new Vector2 (1, 1);

		Time.timeScale = 0;
	}

	public void closeBackPanel() {

		GameObject backBg = GameObject.Find ("back_bg");
		backBg.transform.localScale = new Vector2 (0, 0);

		Time.timeScale = 1;
	}

	public void backYClick() {

		quitGame ();
		Time.timeScale = 1;
	}

	public void backNClick() {

		closeBackPanel ();
	}
}
