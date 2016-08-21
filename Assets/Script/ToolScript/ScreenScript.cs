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
	public Vector3 getScreenWorldLoc() {

		return Camera.main.transform.position;
	}

	//获取相机最小点
	public Vector2 getCameraMinLoc() {

		Vector2 screenWorldSize = this.getScreenWorldSize ();
		Vector2 screenWorldLoc = this.getScreenWorldLoc();

		return new Vector2 (screenWorldLoc.x-screenWorldSize.x*0.5f, screenWorldLoc.y-screenWorldSize.y*0.5f);
	}

	//获取相机最大点
	public Vector2 getCameraMaxLoc() {

		Vector2 screenWorldSize = this.getScreenWorldSize ();
		Vector2 screenWorldLoc = this.getScreenWorldLoc();

		return new Vector2 (screenWorldLoc.x+screenWorldSize.x, screenWorldLoc.y+screenWorldSize.y);
	}

	//获取世界最小点
	public Vector2 getMinWorldLoc() {

		return new Vector2 (-4, -11);
	}

	//获取世界最大点
	public Vector2 getMaxWorldLoc() {

		return new Vector2 (30, 5);
	}
}
