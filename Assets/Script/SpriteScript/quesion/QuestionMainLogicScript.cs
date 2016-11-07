using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestionMainLogicScript : MonoBehaviour {

	public GameObject cardPerfab;
	public GameObject people;

	public GameObject textureRocy;
	public GameObject textureMono;
	public GameObject textureRay;
	public GameObject textureSong;

	public Button btnSure;
	public Button btnQuestionSure;
	public Image choosePanel;
	public Image questionPanel;
	public Sprite textureSureNormal;

	public Button btnRay;
	public Button btnMono;
	public Button btnSong;
	public Button btnRoc;

	public Sprite btnRay1;
	public Sprite btnMono1;
	public Sprite btnSong1;
	public Sprite btnRoc1;

	public Sprite btnRay2;
	public Sprite btnMono2;
	public Sprite btnSong2;
	public Sprite btnRoc2;

	public Toggle toggle1;
	public Toggle toggle2;
	public Toggle toggle3;
	public Toggle toggle4;

	private float cardWidth = 2.2f;
	private float cardHeight = 2.2f;

	private float group1originX = -2.2f;
	private float group1originY = 1f;

	private GameObject[] card;

	private bool isChoose;
	private bool isSelectAnswer;

	// Use this for initialization
	void Start () {

		card = new GameObject[9];
		isChoose = false;
		isSelectAnswer = false;
	
		for (int i = 0; i < 9; ++i) {
			card[i] = createCard (new Vector2(group1originX + cardWidth * (i % 3), group1originY - cardHeight * (i / 3)));
		}

		hideAllPeople ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (choosePanel.transform.localScale.x == 0 && (Input.touchCount > 0 || Input.GetMouseButtonDown (0))) {                  

			if (Input.touchCount > 0 && Input.GetTouch (0).phase != TouchPhase.Began) {
				return;
			}

			if (questionPanel.transform.localScale.x == 0) {

				int touchIndex = getTouchedCard (getTouchPos ());
				if (touchIndex >= 0) {

					showQuestionPanel ();
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

	private void showQuestionPanel() {

		questionPanel.transform.localScale = new Vector2 (1, 1);
	}

	private void hideQuestionPanel() {

		toggle1.isOn = false;
		toggle2.isOn = false;
		toggle3.isOn = false;
		toggle4.isOn = false;
		questionPanel.transform.localScale = new Vector2 (0, 0);
	}

	//获取被点中的卡片
	private int getTouchedCard(Vector2 pos) {

		for (int i=0; i<card.Length; ++i) {

			if (card [i] == null) {
				continue;
			}

			Vector2 cardPos = card [i].transform.position;

			if (Mathf.Abs (pos.x - cardPos.x) <= cardWidth / 2 && Mathf.Abs (pos.y - cardPos.y) <= cardHeight / 2) {
				return i;
			}
		}
		return -1;
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


	//选人面板
	private void hideAllPeople() {

		textureRocy.transform.localScale = new Vector2 (0, 0);
		textureMono.transform.localScale = new Vector2 (0, 0);
		textureRay.transform.localScale = new Vector2 (0, 0);
		textureSong.transform.localScale = new Vector2 (0, 0);

		btnRay.GetComponent<Image> ().sprite = btnRay2;
		btnMono.GetComponent<Image> ().sprite = btnMono2;
		btnSong.GetComponent<Image> ().sprite = btnSong2;
		btnRoc.GetComponent<Image> ().sprite = btnRoc2;
	}

	private void hasChoose() {

		isChoose = true;
		btnSure.GetComponent<Image> ().sprite = textureSureNormal;
	}

	public void chooseRocy() {

		hasChoose ();
		hideAllPeople ();
		textureRocy.transform.localScale = new Vector2 (1, 1);
		btnRoc.GetComponent<Image> ().sprite = btnRoc1;
	}

	public void chooseMono() {

		hasChoose ();
		hideAllPeople ();
		textureMono.transform.localScale = new Vector2 (1, 1);
		btnMono.GetComponent<Image> ().sprite = btnMono1;
	}

	public void chooseRay() {

		hasChoose ();
		hideAllPeople ();
		textureRay.transform.localScale = new Vector2 (1, 1);
		btnRay.GetComponent<Image> ().sprite = btnRay1;
	}

	public void chooseSong() {

		hasChoose ();
		hideAllPeople ();
		textureSong.transform.localScale = new Vector2 (1, 1);
		btnSong.GetComponent<Image> ().sprite = btnSong1;
	}

	public void btnSureClick() {

		if (isChoose) {

			choosePanel.transform.localScale = new Vector2 (0, 0);
		}
	}
		
	//问题面板
	public void toggleSelect() {

		isSelectAnswer = true;
		btnQuestionSure.GetComponent<Image> ().sprite = textureSureNormal;
	}


	public void btnQuestionSureClick() {

		if (isSelectAnswer) {

			hideQuestionPanel ();
		}
	}
}
