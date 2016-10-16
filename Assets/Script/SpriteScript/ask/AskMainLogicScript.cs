using UnityEngine;
using System.Collections;

public class AskMainLogicScript : MonoBehaviour {

	//参数设置
	float duringTime = 2.0f;
	float scaleSpeedX;
	float scaleSpeedY;
	float lastScaleX = 2.0f;
	float lastScaleY = 2.0f;

	public Texture2D textureBack;
	public Texture2D textureFore;

	private GameObject currentObj;

	private bool isStartAnim;

	void Start () {

		isStartAnim = false;

		GameObject card = GameObject.Find ("card_back");
		scaleSpeedX = (lastScaleX - card.transform.localScale.x) / (duringTime / Time.fixedDeltaTime);
		scaleSpeedY = (lastScaleY - card.transform.localScale.y) / (duringTime / Time.fixedDeltaTime);
	}
		
	void FixedUpdate () {

		if (isStartAnim) {
			
			changeTexture (currentObj, currentObj.transform.transform.localScale.x < 0 ? textureBack : textureFore);

			if (currentObj.transform.localScale.x + scaleSpeedX >= lastScaleX) {
				currentObj.transform.localScale = new Vector2 (lastScaleX, lastScaleY);
				finishAnim ();
			} else {
				currentObj.transform.localScale = new Vector2 (currentObj.transform.localScale.x + scaleSpeedX, currentObj.transform.localScale.y + scaleSpeedY);
			}

		}
	}

	void Update() {

		if (Input.touchCount > 0 || Input.GetMouseButtonDown(0)) {                  

			startAnim (GameObject.Find("card_back"));
		} 
	}

	// 替换精灵的图片
	private void changeTexture(GameObject obj, Texture2D newTexture) {

		SpriteRenderer spr = obj.GetComponent<SpriteRenderer>();
		Sprite sp = Sprite.Create(newTexture, spr.sprite.textureRect,new Vector2(0.5f,0.5f));//注意居中显示采用0.5f值  
		spr.sprite = sp;  
	}

	//动画函数
	private void startAnim(GameObject obj) {

		currentObj = obj;
		isStartAnim = true;
	}

	private void finishAnim() {

		currentObj = null;
		isStartAnim = false;
	}

	//获取点击位置
	private Vector2 getTouchPos() {

		Vector2 pos = new Vector2 ();

		if (Input.touchCount > 0) {
			pos = Input.GetTouch (0).position;
		} else if (Input.GetMouseButtonDown (0)) {
			pos = Input.mousePosition;
		} else {
			pos = new Vector2 (0, 0);
		}

		return Camera.main.ScreenToWorldPoint(pos);
	}
 
}
