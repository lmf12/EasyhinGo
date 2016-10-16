using UnityEngine;
using System.Collections;

public class AskMainLogicScript : MonoBehaviour {

	//参数设置
	private float duringTime = 1.5f;
	private float scaleSpeedX;
	private float scaleSpeedY;
	private float lastScaleX = 2.0f;
	private float lastScaleY = 2.0f;

	//外部
	public Texture2D textureBack;
	public Texture2D textureFore;
	public GameObject cardPerfab;

	//控制
	private GameObject currentObj;
	private bool isStartOpenAnim;
	private bool isStartCloseAnim;

	private float originScaleX;
	private float originScaleY;

	//数据
	private GameObject card;

	void Start () {

		isStartOpenAnim = false;
		isStartCloseAnim = false;

		card = createCard (new Vector2(0, 0));

		scaleSpeedX = (lastScaleX - card.transform.localScale.x) / (duringTime / Time.fixedDeltaTime);
		scaleSpeedY = (lastScaleY - card.transform.localScale.y) / (duringTime / Time.fixedDeltaTime);
	}
		
	void FixedUpdate () {

		if (isStartOpenAnim) {
			
			changeTexture (currentObj, currentObj.transform.transform.localScale.x < 0 ? textureBack : textureFore);

			if (currentObj.transform.localScale.x + scaleSpeedX >= lastScaleX) {
				currentObj.transform.localScale = new Vector2 (lastScaleX, lastScaleY);
				isStartOpenAnim = false;
			} else {
				currentObj.transform.localScale = new Vector2 (currentObj.transform.localScale.x + scaleSpeedX, currentObj.transform.localScale.y + scaleSpeedY);
			}

		} else if (isStartCloseAnim) {

			changeTexture (currentObj, currentObj.transform.transform.localScale.x < 0 ? textureBack : textureFore);

			if (currentObj.transform.localScale.x - scaleSpeedX <= originScaleX) {
				currentObj.transform.localScale = new Vector2 (originScaleX, originScaleY);
				isStartCloseAnim = false;
				currentObj = null;
			} else {
				currentObj.transform.localScale = new Vector2 (currentObj.transform.localScale.x - scaleSpeedX, currentObj.transform.localScale.y - scaleSpeedY);
			}
		}
	}

	void Update() {

		if (Input.touchCount > 0 || Input.GetMouseButtonDown(0)) {                  

			if (currentObj == null) {
				startOpenAnim (card);
			} else {
				startCloseAnim ();
			}
		} 
	}

	//创建一个卡片
	private GameObject createCard(Vector2 pos) {

		return (GameObject)Instantiate (cardPerfab, pos, Quaternion.identity);
	}

	// 替换精灵的图片
	private void changeTexture(GameObject obj, Texture2D newTexture) {

		SpriteRenderer spr = obj.GetComponent<SpriteRenderer>();
		Sprite sp = Sprite.Create(newTexture, spr.sprite.textureRect,new Vector2(0.5f,0.5f));//注意居中显示采用0.5f值  
		spr.sprite = sp;  
	}

	//动画函数
	private void startOpenAnim(GameObject obj) {

		currentObj = obj;
		originScaleX = currentObj.transform.localScale.x;
		originScaleY = currentObj.transform.localScale.y;
		isStartOpenAnim = true;
	}

	private void startCloseAnim() {

		isStartCloseAnim = true;
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
  
	//添加文本到卡片
	private void addTextToCard(string text, GameObject obj) {


	}
}
