using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RunMainLogicScript : MonoBehaviour {

	public Text textTime;

	private int totalTime;

	private bool isGameEnd;

	// Use this for initialization
	void Start () {
	
		totalTime = 0;

		InvokeRepeating("timeCount", 1, 1);

		isGameEnd = false;
	}
	
	// Update is called once per frame
	void Update () {
	
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

		isGameEnd = true;
	}
}
