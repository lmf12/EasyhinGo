﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;

public class SwitchMainLogic : MonoBehaviour {

	public Button button1;
	public Button button2;
	public Button button3;
	public Button button4;
	public Button button5;

	public GameObject loc1;
	public GameObject loc2;
	public GameObject loc3;
	public GameObject loc4;
	public GameObject loc5;

	public GameObject background1;
	public GameObject background2;
	public GameObject background3;
	public GameObject background4;

	private Vector2 touchLastPos;
	private float moveDistance;

	public Image mainLoc;

	public ScrollRect scroll;

	bool isScrolling = false;

	private float baseVelocity = 12.0f;
	private float baseForce = 0.3f;
	private float minDis = 0.4f; 
	private float beginX1 = 0.01f;
	private float endX1 = -115.0f;
	private float beginX2 = 0.01f;
	private float endX2 = -100.0f;
	private float beginX3 = 0.01f;
	private float endX3 = -85.0f;
	private float beginX4 = 0.01f;
	private float endX4 = -70.0f;


	private float originLocX;
	private float originMainX;

	public Text score1;
	public Text score2;
	public Text score3;
	public Text score4;
	public Text score5;
	private Text[] scoreList;

	public Image scoreBgPrefab;
	private Image[] scoreBgList;

	public Texture2D lineRed1;
	public Texture2D lineRed2;
	public Texture2D lineRed3;
	public Texture2D lineRed4;

	public GameObject line1;
	public GameObject line2;
	public GameObject line3;
	public GameObject line4;

	public Sprite buttonRed1;
	public Sprite buttonRed2;
	public Sprite buttonRed3;
	public Sprite buttonRed4;
	public Sprite buttonRed5;




	public Text name1;
	public Text name2;
	public Text name3;
	public Text name4;
	public Text name5;
	public Text nameSelf;

	public Text time1;
	public Text time2;
	public Text time3;
	public Text time4;
	public Text time5;
	public Text timeSelf;

	public Text rankSelf;

	public Image rank;

	public Button music;
	public Sprite musicOn;
	public Sprite musicOff;


	private WWWHelper wwwHelper;

	// Use this for initialization
	void Start () {

		initText ();

		wwwHelper = GameObject.Find ("HttpHelper").GetComponent<WWWHelper> ();
		requestList (getUserID());

		originLocX = background1.transform.position.x;
		originMainX = Camera.main.ScreenToWorldPoint (mainLoc.transform.position).x;

		createScoreList();

		string str = PlayerPrefs.GetString("switch_postion", "null");
		if (!str.Equals ("null")) {
			setPosition (int.Parse(str));

			PlayerPrefs.SetString ("switch_postion", "" + 0);
		}


		updateProgress ();

		playAudio ();

		initMusicBtn ();
	}
	
	// Update is called once per frame
	void Update() {

		updateBackground1Pos ();

		updateBackground2Pos ();
		updateBackground3Pos ();
		updateBackground4Pos ();

		updateStone ();
		updateBoat ();

		updateButtonLoc ();

		updateScoreLoc ();
	}

	private void updateBackground1Pos() {

		float posX = Camera.main.ScreenToWorldPoint (mainLoc.transform.position).x;
		float background1X = originLocX + posX - originMainX;
		background1.transform.position = new Vector2 (background1X, background1.transform.position.y);
	}

	private void updateBackground2Pos() {

		Vector2 pos = background1.transform.position;
		float background2X = beginX2 + (endX2 - beginX2) * ((pos.x - beginX1) / (endX1 - beginX1)); 
		background2.transform.position = new Vector2 (background2X, background2.transform.position.y);
	}

	private void updateBackground3Pos() {

		Vector2 pos = background1.transform.position;
		float background3X = beginX3 + (endX3 - beginX3) * ((pos.x - beginX1) / (endX1 - beginX1)); 
		background3.transform.position = new Vector2 (background3X, background3.transform.position.y);
	}

	private void updateBackground4Pos() {

		Vector2 pos = background1.transform.position;
		float background4X = beginX4 + (endX4 - beginX4) * ((pos.x - beginX1) / (endX1 - beginX1)); 
		background4.transform.position = new Vector2 (background4X, background4.transform.position.y);
	}

	private void updateStone() {

		float beginx = -55.0f;
		float endx = -65.0f;
		float beginA = 0;
		float endA = 1;


		Vector2 pos = background4.transform.position;
		GameObject stone = GameObject.Find ("switch_stone_4");

		if (pos.x <= beginx && pos.x >= endx) {

			Color c = stone.GetComponent<SpriteRenderer> ().color; 
			c.a = beginA + (endA - beginA) * ((pos.x - beginx) / (endx - beginx)); 
			stone.GetComponent<SpriteRenderer> ().color = c;
		} else if (pos.x > beginx) {
			Color c = stone.GetComponent<SpriteRenderer> ().color; 
			c.a = beginA; 
			stone.GetComponent<SpriteRenderer> ().color = c;
		} else {
			Color c = stone.GetComponent<SpriteRenderer> ().color; 
			c.a = endA; 
			stone.GetComponent<SpriteRenderer> ().color = c;
		}
	}

	private void updateBoat() {

		float beginx = -60.0f;
		float endx = -68.0f;
		float beginA = 0;
		float endA = 1;

		float beginY = 5.65f;
		float endY = 0;


		Vector2 pos = background4.transform.position;
		GameObject boat = GameObject.Find ("switch_boat");

		Vector2 posBoat = boat.transform.position;

		if (pos.x <= beginx && pos.x >= endx) {

			Color c = boat.GetComponent<SpriteRenderer> ().color; 
			c.a = beginA + (endA - beginA) * ((pos.x - beginx) / (endx - beginx)); 
			boat.GetComponent<SpriteRenderer> ().color = c;

			posBoat.y = beginY + (endY - beginY) * ((pos.x - beginx) / (endx - beginx));
			boat.transform.position = posBoat;
		} else if (pos.x > beginx) {
			Color c = boat.GetComponent<SpriteRenderer> ().color; 
			c.a = beginA; 
			boat.GetComponent<SpriteRenderer> ().color = c;

			posBoat.y = beginY;
			boat.transform.position = posBoat;
		} else {
			Color c = boat.GetComponent<SpriteRenderer> ().color; 
			c.a = endA; 
			boat.GetComponent<SpriteRenderer> ().color = c;

			posBoat.y = endY;
			boat.transform.position = posBoat;
		}
	}

	private void updateButtonLoc() {

		button1.transform.position = Camera.main.WorldToScreenPoint (loc1.transform.position);
		button2.transform.position = Camera.main.WorldToScreenPoint (loc2.transform.position);
		button3.transform.position = Camera.main.WorldToScreenPoint (loc3.transform.position);
		button4.transform.position = Camera.main.WorldToScreenPoint (loc4.transform.position);
		button5.transform.position = Camera.main.WorldToScreenPoint (loc5.transform.position);
	}

	private void createScoreList() {

		scoreBgList = new Image[5];

		for (int i = 0; i < 5; ++i) {

			scoreBgList[i] = (Image)Instantiate (scoreBgPrefab, new Vector2(-100, -100), Quaternion.identity);
			scoreBgList[i].transform.SetParent (GameObject.Find("scoreContent").transform);
		}

		scoreList = new Text[5];
		scoreList [0] = score1;
		scoreList [1] = score2;
		scoreList [2] = score3;
		scoreList [3] = score4;
		scoreList [4] = score5;

		for (int i = 0; i < 5; ++i) {

			//读取分数
			string str = PlayerPrefs.GetString("score_" + i, "null");
			if (!str.Equals ("null")) {
				scoreList [i].text = getTimeStringFromSecond(int.Parse(str));
				scoreBgList [i].transform.localScale = new Vector2 (1,1);
			} else {
				scoreList [i].text = "";
				scoreBgList [i].transform.localScale = new Vector2 (0,0);
			}
		}
	}

	private void updateScoreLoc() {

		scoreList [0].transform.position = new Vector2 (button1.transform.position.x, button1.transform.position.y + worldToScreenDistance(2.2f));
		scoreList [1].transform.position = new Vector2 (button2.transform.position.x, button2.transform.position.y + worldToScreenDistance(2.2f));
		scoreList [2].transform.position = new Vector2 (button3.transform.position.x, button3.transform.position.y + worldToScreenDistance(2.2f));
		scoreList [3].transform.position = new Vector2 (button4.transform.position.x, button4.transform.position.y + worldToScreenDistance(2.2f));
		scoreList [4].transform.position = new Vector2 (button5.transform.position.x, button5.transform.position.y + worldToScreenDistance(2.2f));

		for (int i = 0; i < 5; ++i) {
			scoreBgList [i].transform.position = new Vector2(scoreList [i].transform.position.x,scoreList [i].transform.position.y-worldToScreenDistance(0.1f));
		}
	}

	private string getTimeStringFromSecond(int time) {

		string second = "" + (time % 60);
		string minute = "" + ((time / 60) % 60);

		second = (second.Length == 1 ? "0" : "") + second;
		minute = (minute.Length == 1 ? "0" : "") + minute;

		return minute + " : " + second;
	}

	public void goHome() {

		PlayerPrefs.SetString ("openHomeWithoutLaunch", "1");

		Application.LoadLevel (0);
	}

	private float worldToScreenDistance(float distance) {

		return Camera.main.WorldToScreenPoint (new Vector2 (0, distance)).y - Camera.main.WorldToScreenPoint (new Vector2 (0, 0)).y;
	}

	private void setPosition(int index) {

		float[] pos = {0, -960, -2458, -3506, -4929};

		scroll.content.anchoredPosition = new Vector2 (pos[index], 0);
	}

	// 替换精灵的图片
	private void changeTexture(GameObject obj, Texture2D newTexture) {

		SpriteRenderer spr = obj.GetComponent<SpriteRenderer>();
		Sprite sp = Sprite.Create(newTexture, spr.sprite.textureRect,new Vector2(0.5f,0.5f));//注意居中显示采用0.5f值  
		spr.sprite = sp;  
	}

	//跟新进度
	private void updateProgress() {
		
		Button[] btnList = {button1, button2, button3, button4, button5};
		Sprite[] btnRedList = {buttonRed1, buttonRed2, buttonRed3, buttonRed4, buttonRed5};

		GameObject[] lineList = {line1, line2, line3, line4};
		Texture2D[] lineRedList = {lineRed1, lineRed2, lineRed3, lineRed4};

		for (int i = 0; i < 5; ++i) {

			//读取分数
			string str = PlayerPrefs.GetString("score_" + i, "null");
			if (!str.Equals ("null")) {

				btnList [i].image.sprite = btnRedList [i];

				if (i != 4) {

					changeTexture (lineList[i], lineRedList[i]);
				}
			}
		}
	}



	public void go1() {

		Application.LoadLevel (2);
	}

	public void go2() {

		string str = PlayerPrefs.GetString("score_0", "null");
		if (str.Equals ("null")) {

			return;
		}

		Application.LoadLevel (7);
	}

	public void go3() {

		string str = PlayerPrefs.GetString("score_1", "null");
		if (str.Equals ("null")) {

			return;
		}

		Application.LoadLevel (4);
	}

	public void go4() {

		string str = PlayerPrefs.GetString("score_2", "null");
		if (str.Equals ("null")) {

			return;
		}

		Application.LoadLevel (3);
	}

	public void go5() {

		string str = PlayerPrefs.GetString("score_3", "null");
		if (str.Equals ("null")) {

			return;
		}

		Application.LoadLevel (5);
	}


	private void requestList(string userid) {

		wwwHelper.GET ("http://120.76.243.215/pro/index.php/api/risker/getRankingList?f_client_id="+userid, gameObject);
	}

	private string getUserID () {

		return PlayerPrefs.GetString("userid", "null");
	}

	void RequestDone (string result) {

		JsonData jd = JsonMapper.ToObject (result);

		string rank = (string)jd ["result"] ["ranking"];

		JsonData list = jd ["result"] ["list"]; 

		Text[] nameList = { name1, name2, name3, name4, name5 };
		Text[] scoreList = { time1, time2, time3, time4, time5 };

		for (int i = 0; i < list.Count && i < 5; i++) {
			nameList [i].text = (string)list [i] ["f_nick_name"];
			scoreList [i].text = getTimeStringFromSecond (int.Parse ((string)list [i] ["f_score"]));
		}


		if (int.Parse (rank) >= 0 && int.Parse (rank) <= list.Count) {

			int index = int.Parse (rank);

			nameSelf.text = (string)list [index - 1] ["f_nick_name"];
			timeSelf.text = getTimeStringFromSecond (int.Parse ((string)list [index - 1] ["f_score"]));

			rankSelf.text = rank;
		}

	}

	public void openRankPanel() {

		rank.transform.localScale = new Vector2 (1,1);
	}

	public void closeRankPanel() {
		
		rank.transform.localScale = new Vector2 (0,0);
	}

	private void initText() {

		name1.text = "--";
		name2.text = "--";
		name3.text = "--";
		name4.text = "--";
		name5.text = "--";

		time1.text = "--";
		time2.text = "--";
		time3.text = "--";
		time4.text = "--";
		time5.text = "--";

		nameSelf.text = "暂无";
		timeSelf.text = "--";

		rankSelf.text = "-";
	}

	public void playAudio() {

		string str1 = PlayerPrefs.GetString("closeMusic", "null");
		if (!str1.Equals ("null") && int.Parse (str1) == 1) {
			return;
		}

		GameObject.Find ("audio").GetComponent<AudioSource>().Play();
	}

	public void stopAudio() {

		GameObject.Find ("audio").GetComponent<AudioSource> ().Stop ();
	}


	public void openMusic() {

		PlayerPrefs.SetString ("closeMusic", "" + 0);
		playAudio ();
		music.image.sprite = musicOn;
	}

	public void closeMusic() {

		PlayerPrefs.SetString ("closeMusic", "" + 1);
		stopAudio ();
		music.image.sprite = musicOff;
	}

	public void onMusicClick() {

		string str1 = PlayerPrefs.GetString("closeMusic", "null");
		if (!str1.Equals ("null") && int.Parse (str1) == 1) {
			openMusic ();
		} else {
			closeMusic ();
		}
	}

	public void initMusicBtn() {

		string str1 = PlayerPrefs.GetString("closeMusic", "null");
		if (!str1.Equals ("null") && int.Parse (str1) == 1) {
			music.image.sprite = musicOff;
		} else {
			music.image.sprite = musicOn;
		}

	}
}
