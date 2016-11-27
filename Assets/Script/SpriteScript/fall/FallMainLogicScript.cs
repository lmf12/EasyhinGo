using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FallMainLogicScript : MonoBehaviour {

	public GameObject people;
	public GameObject boom;

	public GameObject ray;
	public GameObject roc;
	public GameObject song;
	public GameObject mono;

	private float speed = 0.06f;

	private float minX = -6;
	private float maxX = 6;

	private bool isMoving;
	private bool isRight;

	private bool isLeftPress;
	private bool isRightPress;


	private GameObject currentObj1;
	private GameObject currentObj2;
	private GameObject currentObj3;
	private GameObject currentObj4;

	private int objType;



	public Image imgRay;
	public Image imgMono;
	public Image imgSong;
	public Image imgRoc;

	public Sprite spRay;
	public Sprite spMono;
	public Sprite spSong;
	public Sprite spRoc;


	public Text textTime;

	private int totalTime;

	private bool isGameEnd;

	public Text winText;

	public Image win;

	public Image guize;


	private bool isMonoGet;
	private bool isRayGet;
	private bool isSongGet;
	private bool isRocGet;

	private bool isBegin;


	public Text textPrefab;


	//最后时间
	private float countTime; 



	// Use this for initialization
	void Start () {

		isMoving = false;
		isRight = true;

		isLeftPress = false;
		isRightPress = false;


		isMonoGet = false;
		isRayGet = false;
		isSongGet = false;
		isRocGet = false;

		isBegin = false;

		countTime = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (!isBegin) {

			return;
		}

		if (isGameEnd) {

			return;
		}

		float flag = canCreate ();
		if (flag > -8) {

			switch (objType) {
			case 1:
				currentObj1 = createObj (flag);
				break;
			case 2:
				currentObj2 = createObj (flag);
				break;
			case 3:
				currentObj3 = createObj (flag);
				break;
			case 4:
				currentObj4 = createObj (flag);
				break;
			default:
				break;
			}
		}

		if (isMoving) {

			//边界判断
			if (people.transform.position.x < minX) {
				people.transform.position = new Vector2(minX, people.transform.position.y);
				isMoving = false;
				return;
			}
			if (people.transform.position.x > maxX) {
				people.transform.position = new Vector2(maxX, people.transform.position.y);
				isMoving = false;
				return;
			}

			people.transform.Translate (new Vector2 (speed, 0));
		}


		if (isMonoGet && isRayGet && isSongGet && isRocGet) {

			countTime += Time.fixedDeltaTime;
		}

		if (countTime >= 1) {

			isGameEnd = true;
			winText.text = textTime.text;
			win.transform.localScale = new Vector2 (1, 1);

			saveScore ();

			destoryObj (currentObj1);
			destoryObj (currentObj2);
			destoryObj (currentObj3);
			destoryObj (currentObj4);
		}
	}

	private void destoryObj(GameObject obj) {
		if (obj) {
			Destroy (obj);
		}
	}

	public void quitGame() {

		Application.LoadLevel (1);
	}

	public void rePlayGame () {

		Application.LoadLevel (2);
	}

	public void moveLeft() {

		isLeftPress = true;
		isMoving = true;
		if (isRight) {
			this.turnFace ();
		}
		isRight = false;
	}

	public void moveRight() {
	
		isRightPress = true;
		isMoving = true;
		if (!isRight) {
			this.turnFace ();
		}
		isRight = true;
	}

	//1 left 2 right
	public void stopMoving(int type) {

		if (type == 1) {
			isLeftPress = false;
		} else if (type == 2) {
			isRightPress = false;
		}

		if (isLeftPress) {
			moveLeft ();
		} else if (isRightPress) {
			moveRight ();
		} else {
			isMoving = false;
		}
	}

	//转向
	private void turnFace() {

		Vector2 scale = people.transform.localScale;
		scale.x *= -1;
		people.transform.localScale = scale;
	}

	private GameObject createBoom(float x) {

		return (GameObject)Instantiate (boom, new Vector2(x, 5 + Random.value), Quaternion.identity);
	}

	private GameObject createHead(float x) {

		int index = (int)(Random.value * 9);

		switch (index) {
		case 0:
		case 1:
			return (GameObject)Instantiate (ray, new Vector2(x, 5.5f + Random.value), Quaternion.identity);
			break;
		case 2:
			int i = (int)(Random.value * 2);
			return (GameObject)Instantiate (i==0 ?song:mono, new Vector2(x, 6 + Random.value), Quaternion.identity);
			break;
		case 3:
		case 4:
		case 5:
			return (GameObject)Instantiate (mono, new Vector2(x, 5.8f + Random.value), Quaternion.identity);
			break;
		default:
			return (GameObject)Instantiate (roc, new Vector2(x, 5 + Random.value), Quaternion.identity);
			break;
		}
	}

	private GameObject createObj(float x) {

		int index = (int)(Random.value * 2);

		if (index == 0) {
			return createBoom (x);
		} else {
			return createHead (x);
		}
	}

	private float canCreate () {

		if (currentObj1 == null) {
			objType = 1;
			return (-5.5f + Random.value * 2);
		} else if (currentObj2 == null) {
			objType = 2;
			return (-2.5f + Random.value * 2);
		} else if (currentObj3 == null) {
			objType = 3;
			return (0.5f + Random.value * 2);
		} else if (currentObj4 == null) {
			objType = 4;
			return (3.5f + Random.value * 2);
		} else {
			return -10.0f;
		}
	}


	//游戏计时
	private void timeCount() {

		if (isGameEnd) {

			CancelInvoke ();
			return;
		}

		totalTime += 1;
		textTime.text = getTimeStringFromSecond();
	}

	private string getTimeStringFromSecond() {

		string second = "" + (totalTime % 60);
		string minute = "" + ((totalTime / 60) % 60);

		second = (second.Length == 1 ? "0" : "") + second;
		minute = (minute.Length == 1 ? "0" : "") + minute;

		return minute + " : " + second;
	}

	public void getMono() {

		isMonoGet = true;
		imgMono.sprite = spMono;
	}

	public void getRoc() {

		isRocGet = true;
		imgRoc.sprite = spRoc;
	}

	public void getRay() {

		isRayGet = true;
		imgRay.sprite = spRay;
	}

	public void getSong() {

		isSongGet = true;
		imgSong.sprite = spSong;
	}

	public void getBoom() {

		totalTime += 20;
		textTime.text = getTimeStringFromSecond();
	}

	public void closeGuize() {

		isBegin = true;
		guize.transform.localScale = new Vector2 (0, 0);

		InvokeRepeating("timeCount", 1, 1);
	}

	public void createText(string str) {

		Vector2 pos = people.transform.position;
		pos.y += 1.2f;
		Text text = (Text)Instantiate (textPrefab, Camera.main.WorldToScreenPoint(pos), Quaternion.identity);
		text.text = str;
		if (str.Equals ("+20s")) {
			text.color = Color.red;
		} else {
			text.color = Color.white;
		}
		text.transform.SetParent (GameObject.Find("Canvas").transform);

		Destroy (text, 1);
	}

	private void saveScore() {
		
		string str = PlayerPrefs.GetString("score_0", "null");
		if (str.Equals ("null")) {
			PlayerPrefs.SetString ("score_0", "" + totalTime);
		} else {
			if (int.Parse (str) > totalTime) {
				PlayerPrefs.SetString ("score_0", "" + totalTime);
			}
		}
	}

	public void openBackPanel() {

		GameObject backBg = GameObject.Find ("back_bg");
		backBg.transform.localScale = new Vector2 (1, 1);

		Time.timeScale = 0;
	}

	public void closeBackPanel() {

		GameObject backBg = GameObject.Find ("back_bg");
		backBg.transform.localScale = new Vector2 (0, 0);

		Time.timeScale = 1;
	}

	public void backYClick() {

		quitGame ();
		Time.timeScale = 1;
	}

	public void backNClick() {

		closeBackPanel ();
	}
}
