using UnityEngine;
using System.Collections;

public class ScreenScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

	//获取屏幕的世界尺寸
	public Vector2 getScreenWorldSize() {

		Vector2 pos = Camera.main.ScreenToWorldPoint (new Vector2 (Screen.width, Screen.height));
		Vector2 posRightBottom = new Vector2 (pos.x - Camera.main.transform.position.x, pos.y - Camera.main.transform.position.y);

		return new Vector2 (posRightBottom.x * 2, posRightBottom.y * 2);
	}

	//获取屏幕的世界位置
	public Vector3 getScreenWorldLoc() {

		return Camera.main.transform.position;
	}

	//获取相机最小点
	public Vector2 getCameraMinLoc() {

		return Camera.main.ScreenToWorldPoint (new Vector2(0, 0));
	}

	//获取相机最大点
	public Vector2 getCameraMaxLoc() {

		return Camera.main.ScreenToWorldPoint (new Vector2(Screen.width, Screen.height));
	}

	//获取世界最小点
	public Vector2 getMinWorldLoc() {

		return new Vector2 (-8, -11);
	}

	//获取世界最大点
	public Vector2 getMaxWorldLoc() {

		return new Vector2 (19, 5);
	}
}
