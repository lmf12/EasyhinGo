using UnityEngine;
using System.Collections;

public class ScreenScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	//获取屏幕的世界尺寸
	public Vector2 getScreenWorldSize() {

		return Camera.main.ScreenToWorldPoint (new Vector2(Screen.width, Screen.height));
	}

	//获取屏幕的世界位置
	public Vector2 getScreenWorldLoc() {

		return Camera.main.ScreenToWorldPoint (new Vector2(0, 0));
	}
}
