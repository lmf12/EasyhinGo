using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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


	public Text scorePrefab;
	private Text[] scoreList;

	public Image scoreBgPrefab;
	private Image[] scoreBgList;


	// Use this for initialization
	void Start () {

		originLocX = background1.transform.position.x;
		originMainX = Camera.main.ScreenToWorldPoint (mainLoc.transform.position).x;

		createScoreList();
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

		for (int i = 0; i < 5; ++i) {

			scoreList[i] = (Text)Instantiate (scorePrefab, new Vector2(-100, -100), Quaternion.identity);
			scoreList[i].transform.SetParent (GameObject.Find("scoreContent").transform);
			scoreList [i].fontSize = 32;

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
}
