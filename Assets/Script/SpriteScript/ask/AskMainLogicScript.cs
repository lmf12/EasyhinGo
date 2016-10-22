using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AskMainLogicScript : MonoBehaviour {

	//文本内容
	private string[] questions = {
		"问题1",
		"问题2",
		"问题3",
		"问题4",
		"问题5",
		"问题6",
		"问题7",
		"问题8",
		"问题9",
		"问题10",
		"问题11",
		"问题12",
		"问题13",
		"问题14",
		"问题15",
		"问题16",
		"问题17",
		"问题18",
		"问题19",
		"问题20"
	};

	private string[] answers = {
		"答案1",
		"答案2",
		"答案3",
		"答案4",
		"答案5",
		"答案6",
		"答案7",
		"答案8",
		"答案9",
		"答案10",
		"答案11",
		"答案12",
		"答案13",
		"答案14",
		"答案15",
		"答案16",
		"答案17",
		"答案18",
		"答案19",
		"答案20"
	};

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

	private string[] questionList;
	private string[] answerList;

	private int[] questionMappingList;
	private int[] answerMappingList;

	void Start () {

		isStartOpenAnim = false;
		isStartCloseAnim = false;
		currentIndex = -1;

		card = new GameObject[18];
		textList = new Text[18];
		questionMappingList = new int[18];
		answerMappingList = new int[18];

		resortQuestionAndAnswerList ();

		for (int i = 0; i < 9; ++i) {
			card[i] = createCard (new Vector2(group1originX + cardWidth * (i % 3), group1originY - cardHeight * (i / 3)));
			addTextToCard (getRandomQuestion(i), card[i], i);
		}

		for (int i = 0; i < 9; ++i) {
			card[i+9] = createCard (new Vector2(group2originX + cardWidth * (i % 3), group2originY - cardHeight * (i / 3)));
			addTextToCard (getRandomAnswer(i), card[i+9], i+9);
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

	//重排问题
	private void resortQuestionAndAnswerList() {

		int index = questions.Length - 1;

		while (index >= 1) {

			int targetIndex = (int)(Random.value * index);
			targetIndex = targetIndex == index ? targetIndex-1 : targetIndex;

			string temp = questions [targetIndex];
			questions [targetIndex] = questions [index];
			questions [index] = temp;

			temp = answers [targetIndex];
			answers [targetIndex] = answers [index];
			answers [index] = temp;

			index--;
		}

		questionList = new string[questions.Length];
		questions.CopyTo (questionList, 0);

		answerList = new string[answers.Length];
		answers.CopyTo (answerList, 0);
	}

	//获取问题
	private string getRandomQuestion(int index) {

		int count = card.Length - index;
		int targetIndex = (int)(Random.value * count);
		targetIndex = targetIndex == count ? targetIndex - 1 : targetIndex;

		string value = questionList[index + targetIndex];
		questionList [targetIndex] = questionList [index];

		int realIndex = 0;
		for (int i = 0; i < questions.Length; ++i) {
			if (value == questions [i]) {
				realIndex = i;
				break;
			}
		}
		questionMappingList [index] = realIndex;

		return value;
	}

	//获取答案
	private string getRandomAnswer(int index) {

		int count = card.Length - index;
		int targetIndex = (int)(Random.value * count);
		targetIndex = targetIndex == count ? targetIndex - 1 : targetIndex;

		string value = answerList[index + targetIndex];
		answerList [targetIndex] = answerList [index];

		int realIndex = 0;
		for (int i = 0; i < answers.Length; ++i) {
			if (value == answers [i]) {
				realIndex = i;
				break;
			}
		}
		answerMappingList [index] = realIndex;

		return value;
	}
		
}
