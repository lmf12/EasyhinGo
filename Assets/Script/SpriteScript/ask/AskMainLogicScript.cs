using UnityEngine;
using System.Collections;

public class AskMainLogicScript : MonoBehaviour {

	//参数设置
	private float duringTime = 1f;
	private float scaleSpeedX;
	private float scaleSpeedY;
	private float lastScaleX = 2.0f;
	private float lastScaleY = 2.0f;
	private float moveSpeedX;
	private float moveSpeedY;
	private float lastPosX = 0;
	private float lastPosY = 0;

	private float cardWidth = 1.3f;
	private float cardHeight = 1.8f;
	private float group1originX = -6f;
	private float group1originY = 1f;
	private float group2originX = 3f;
	private float group2originY = 1f;

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
	private float originPosX;
	private float originPosY;

	//数据
	private GameObject[] card;

	void Start () {

		isStartOpenAnim = false;
		isStartCloseAnim = false;

		card = new GameObject[18];

		for (int i = 0; i < 9; ++i) {
			card[i] = createCard (new Vector2(group1originX + cardWidth * (i % 3), group1originY - cardHeight * (i / 3)));
		}

		for (int i = 0; i < 9; ++i) {
			card[i+9] = createCard (new Vector2(group2originX + cardWidth * (i % 3), group2originY - cardHeight * (i / 3)));
		}

	}
		
	void FixedUpdate () {

		if (isStartOpenAnim) {
			
			changeTexture (currentObj, currentObj.transform.transform.localScale.x < 0 ? textureBack : textureFore);

			if (currentObj.transform.localScale.x + scaleSpeedX >= lastScaleX) {
				currentObj.transform.localScale = new Vector2 (lastScaleX, lastScaleY);
				currentObj.transform.position = new Vector2 (lastPosX, lastPosY);
				isStartOpenAnim = false;
			} else {
				currentObj.transform.localScale = new Vector2 (currentObj.transform.localScale.x + scaleSpeedX, currentObj.transform.localScale.y + scaleSpeedY);
				currentObj.transform.position = new Vector2 (currentObj.transform.position.x + moveSpeedX, currentObj.transform.position.y + moveSpeedY);
			}

		} else if (isStartCloseAnim) {

			changeTexture (currentObj, currentObj.transform.transform.localScale.x < 0 ? textureBack : textureFore);

			if (currentObj.transform.localScale.x - scaleSpeedX <= originScaleX) {
				currentObj.transform.localScale = new Vector2 (originScaleX, originScaleY);
				currentObj.transform.position = new Vector2 (originPosX, originPosY);
				isStartCloseAnim = false;

				currentObj.GetComponent<SpriteRenderer> ().sortingOrder = 0;
				currentObj = null;
			} else {
				currentObj.transform.localScale = new Vector2 (currentObj.transform.localScale.x - scaleSpeedX, currentObj.transform.localScale.y - scaleSpeedY);
				currentObj.transform.position = new Vector2 (currentObj.transform.position.x - moveSpeedX, currentObj.transform.position.y - moveSpeedY);
			}
		}
	}

	void Update() {

		if (Input.touchCount > 0 || Input.GetMouseButtonDown(0)) {                  

			if (currentObj == null) {
				int touchIndex = getTouchedCard (getTouchPos());
				if (touchIndex >= 0) {
					
					scaleSpeedX = (lastScaleX - card[touchIndex].transform.localScale.x) / (duringTime / Time.fixedDeltaTime);
					scaleSpeedY = (lastScaleY - card[touchIndex].transform.localScale.y) / (duringTime / Time.fixedDeltaTime);
					moveSpeedX = (lastPosX - card[touchIndex].transform.position.x) / (duringTime / Time.fixedDeltaTime);
					moveSpeedY = (lastPosY - card[touchIndex].transform.position.y) / (duringTime / Time.fixedDeltaTime);

					card [touchIndex].GetComponent<SpriteRenderer> ().sortingOrder = 1;

					startOpenAnim (card [touchIndex]);
				}
			} else {
				if (!isStartOpenAnim) {
					startCloseAnim ();
				}
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
		originPosX = currentObj.transform.position.x;
		originPosY = currentObj.transform.position.y;

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

	//获取被点中的卡片
	private int getTouchedCard(Vector2 pos) {

		for (int i=0; i<card.Length; ++i) {

			Vector2 cardPos = card [i].transform.position;

			if (Mathf.Abs (pos.x - cardPos.x) <= cardWidth / 2 && Mathf.Abs (pos.y - cardPos.y) <= cardHeight / 2) {
				return i;
			}
		}
		return -1;
	}
  
	//添加文本到卡片
	private void addTextToCard(string text, GameObject obj) {


	}
}
