using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraScript : MonoBehaviour {

	// 默认屏幕大小
	float width = 1136.0f;
	float height = 640.0f;

	void Awake() {

		// 屏幕适配
		float orthographicSize = Camera.main.orthographicSize;
		orthographicSize *= (Screen.height / (float)Screen.width) / (height / width);
		Camera.main.orthographicSize = orthographicSize;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
