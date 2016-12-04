using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RunMainLogicScript : MonoBehaviour {

	public Text textTime;

	private int totalTime;

	private bool isGameEnd;

	public Image guize;

	public Button music;
	public Button music1;
	public Sprite musicOn;
	public Sprite musicOff;

	// Use this for initialization
	void Start () {
	
		totalTime = 0;

		isGameEnd = false;

		initMusicBtn ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void quitGame() {

		Application.LoadLevel (1);

		PlayerPrefs.SetString ("switch_postion", "" + 3);
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

		initMusicBtn1 ();
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

		playAudio ();
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

	public void playAudio() {

		string str1 = PlayerPrefs.GetString("closeMusic", "null");
		if (!str1.Equals ("null") && int.Parse (str1) == 1) {
			return;
		}

		GameObject.Find ("audio").GetComponent<AudioSource>().Play();
	}


	public void stopAudio() {

		GameObject.Find ("audio").GetComponent<AudioSource> ().Stop ();
	}


	public void openMusic() {

		PlayerPrefs.SetString ("closeMusic", "" + 0);
		playAudio ();
		music.image.sprite = musicOn;
		music1.image.sprite = musicOn;
	}

	public void closeMusic() {

		PlayerPrefs.SetString ("closeMusic", "" + 1);
		stopAudio ();
		music.image.sprite = musicOff;
		music1.image.sprite = musicOff;
	}

	public void onMusicClick() {

		string str1 = PlayerPrefs.GetString("closeMusic", "null");
		if (!str1.Equals ("null") && int.Parse (str1) == 1) {
			openMusic ();
		} else {
			closeMusic ();
		}
	}

	public void initMusicBtn() {

		string str1 = PlayerPrefs.GetString("closeMusic", "null");
		if (!str1.Equals ("null") && int.Parse (str1) == 1) {
			music.image.sprite = musicOff;
		} else {
			music.image.sprite = musicOn;
		}

	}

	public void initMusicBtn1() {

		string str1 = PlayerPrefs.GetString("closeMusic", "null");
		if (!str1.Equals ("null") && int.Parse (str1) == 1) {
			music1.image.sprite = musicOff;
		} else {
			music1.image.sprite = musicOn;
		}

	}
}
