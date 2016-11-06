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
	public Image choosePanel;
	public Sprite textureSureNormal;

	private float cardWidth = 2.2f;
	private float cardHeight = 2.2f;

	private float group1originX = -2.2f;
	private float group1originY = 1f;

	private GameObject[] card;

	private bool isChoose;

	// Use this for initialization
	void Start () {

		card = new GameObject[9];
		isChoose = false;
	
		for (int i = 0; i < 9; ++i) {
			card[i] = createCard (new Vector2(group1originX + cardWidth * (i % 3), group1originY - cardHeight * (i / 3)));
		}

		hideAllPeople ();
	}
	
	// Update is called once per frame
	void Update () {
	
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

	private void hideAllPeople() {

		textureRocy.transform.localScale = new Vector2 (0, 0);
		textureMono.transform.localScale = new Vector2 (0, 0);
		textureRay.transform.localScale = new Vector2 (0, 0);
		textureSong.transform.localScale = new Vector2 (0, 0);
	}

	private void hasChoose() {

		isChoose = true;
		btnSure.GetComponent<Image> ().sprite = textureSureNormal;
	}

	public void chooseRocy() {

		hasChoose ();
		hideAllPeople ();
		textureRocy.transform.localScale = new Vector2 (1, 1);
	}

	public void chooseMono() {

		hasChoose ();
		hideAllPeople ();
		textureMono.transform.localScale = new Vector2 (1, 1);
	}

	public void chooseRay() {

		hasChoose ();
		hideAllPeople ();
		textureRay.transform.localScale = new Vector2 (1, 1);
	}

	public void chooseSong() {

		hasChoose ();
		hideAllPeople ();
		textureSong.transform.localScale = new Vector2 (1, 1);
	}

	public void btnSureClick() {

		if (isChoose) {

			choosePanel.transform.localScale = new Vector2 (0, 0);
		}
	}
}
