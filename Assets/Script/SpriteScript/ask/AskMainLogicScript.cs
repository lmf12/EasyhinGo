using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	public Text textPerfab;

	//控制
	private int currentIndex;
	private bool isStartOpenAnim;
	private bool isStartCloseAnim;

	private float originScaleX;
	private float originScaleY;
	private float originPosX;
	private float originPosY;

	//数据
	private GameObject[] card;
	private Text[] textList;

	void Start () {

		isStartOpenAnim = false;
		isStartCloseAnim = false;
		currentIndex = -1;

		card = new GameObject[18];
		textList = new Text[18];

		for (int i = 0; i < 9; ++i) {
			card[i] = createCard (new Vector2(group1originX + cardWidth * (i % 3), group1originY - cardHeight * (i / 3)));
			addTextToCard ("werwerewrewrewrewrwerewrewrwerwewerwerewrewrewrewrwerewrewrwerwe"+i, card[i], i);
		}

		for (int i = 0; i < 9; ++i) {
			card[i+9] = createCard (new Vector2(group2originX + cardWidth * (i % 3), group2originY - cardHeight * (i / 3)));
			addTextToCard ("werwerewrewrewrewrwerewrewrwerwewerwerewrewrewrewrwerewrewrwerwe"+(i+9), card[i+9], i+9);
		}

	}
		
	void FixedUpdate () {

		if (isStartOpenAnim) {
			
			changeTexture (card[currentIndex], card[currentIndex].transform.transform.localScale.x < 0 ? textureBack : textureFore);

			if (card[currentIndex].transform.localScale.x + scaleSpeedX >= lastScaleX) {
				card[currentIndex].transform.localScale = new Vector2 (lastScaleX, lastScaleY);
				card[currentIndex].transform.position = new Vector2 (lastPosX, lastPosY);
				isStartOpenAnim = false;
			} else {
				card[currentIndex].transform.localScale = new Vector2 (card[currentIndex].transform.localScale.x + scaleSpeedX, card[currentIndex].transform.localScale.y + scaleSpeedY);
				card[currentIndex].transform.position = new Vector2 (card[currentIndex].transform.position.x + moveSpeedX, card[currentIndex].transform.position.y + moveSpeedY);
			}

		} else if (isStartCloseAnim) {

			changeTexture (card[currentIndex], card[currentIndex].transform.transform.localScale.x < 0 ? textureBack : textureFore);

			if (card[currentIndex].transform.localScale.x - scaleSpeedX <= originScaleX) {
				card[currentIndex].transform.localScale = new Vector2 (originScaleX, originScaleY);
				card[currentIndex].transform.position = new Vector2 (originPosX, originPosY);
				isStartCloseAnim = false;

				card[currentIndex].GetComponent<SpriteRenderer> ().sortingOrder = 0;
				currentIndex = -1;
			} else {
				card[currentIndex].transform.localScale = new Vector2 (card[currentIndex].transform.localScale.x - scaleSpeedX, card[currentIndex].transform.localScale.y - scaleSpeedY);
				card[currentIndex].transform.position = new Vector2 (card[currentIndex].transform.position.x - moveSpeedX, card[currentIndex].transform.position.y - moveSpeedY);
			}
		}

		updateCurrentText ();
	}

	void Update() {

		if (Input.touchCount > 0 || Input.GetMouseButtonDown(0)) {                  

			if (currentIndex == -1) {
				int touchIndex = getTouchedCard (getTouchPos());
				if (touchIndex >= 0) {
					
					scaleSpeedX = (lastScaleX - card[touchIndex].transform.localScale.x) / (duringTime / Time.fixedDeltaTime);
					scaleSpeedY = (lastScaleY - card[touchIndex].transform.localScale.y) / (duringTime / Time.fixedDeltaTime);
					moveSpeedX = (lastPosX - card[touchIndex].transform.position.x) / (duringTime / Time.fixedDeltaTime);
					moveSpeedY = (lastPosY - card[touchIndex].transform.position.y) / (duringTime / Time.fixedDeltaTime);

					card [touchIndex].GetComponent<SpriteRenderer> ().sortingOrder = 1;

					startOpenAnim (touchIndex);
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
	private void startOpenAnim(int index) {

		currentIndex = index;
		originScaleX = card[currentIndex].transform.localScale.x;
		originScaleY = card[currentIndex].transform.localScale.y;
		originPosX = card[currentIndex].transform.position.x;
		originPosY = card[currentIndex].transform.position.y;

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
	private void addTextToCard(string text, GameObject obj, int index) {

		textList [index] = Instantiate (textPerfab);
		textList [index].text = text;
		textList [index].color = Color.red;
		textList [index].transform.position = Camera.main.WorldToScreenPoint (obj.transform.position);
		textList [index].transform.SetParent (GameObject.Find("Canvas").transform);
		textList [index].enabled = false;
	}

	private void updateCurrentText() {
		
		if (currentIndex == -1) {
			return;
		}

		Text text = textList [currentIndex];
		text.enabled = text.transform.localScale.x >= 0;
		text.transform.position = Camera.main.WorldToScreenPoint (card [currentIndex].transform.position);
		text.transform.localScale = card [currentIndex].transform.localScale;
	}
}
