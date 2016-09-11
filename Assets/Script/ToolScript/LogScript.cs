using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LogScript : MonoBehaviour {

	//log开关
	static public bool isOpen = true; 

	public Text logText;

	private string currentContent;

	void Start () {
	
		logText.enabled = isOpen;
		currentContent = logText.text;
	}

	void FixedUpdate () {
	
		if (!logText.text.Equals (currentContent)) {

			logText.text = currentContent;
		}
	}

	public void Log(string log) {

		currentContent += "\n" +log;
	}
}
