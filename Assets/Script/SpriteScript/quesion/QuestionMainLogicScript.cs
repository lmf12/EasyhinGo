﻿ using UnityEngine;
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
	public Sprite textureSureDisable;

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
	private int currentChooseRole;
	private bool isSelectAnswer;


	public Text textQuestion;
	public Text textItem1;
	public Text textItem2;
	public Text textItem3;
	public Text textItem4;

	public Image guize;
	private bool isBegin;

	private string[] currentQuestionList;
	private string[] currentAList;
	private string[] currentBList;
	private string[] currentCList;
	private string[] currentDList;
	private string[] currentRList;

	public Image word1;
	public Image word2;
	public Image word3;
	public Image word4;

	public Texture2D whiteBg;

	public GameObject yesPrefab;
	public GameObject noPrefab;

	private int currentTouchPos;

	public Texture2D cardOpen; 

	//随机问题列表
	private int[] randomQuestionList;

	private GameObject[] resultList;

	private int currentScore;

	public Image score1;
	public Image score2;
	public Image score3;
	public Image score4;

	public Sprite starYellow;

	private bool isGameEnd;
	private int totalTime;
	public Text textTime;

	public Text winText;
	public Image win;

	private bool isGameStop;


	public Button music;
	public Button music1;
	public Sprite musicOn;
	public Sprite musicOff;


	//研发
	private string[] question1 = {
		"研发中心的男女比例是多少？",
		"研发中心伙伴们最多是哪个省份？",
		"研发中心伙伴们最多是哪个星座？",
		"研发中心最早进入公司的谁？",
		"妈咪知道最早版本发布时间？",
		"公司成立日期是几月几号？"
	};
	private string[] question1_A = {
		"6:1",
		"广东省",
		"白羊座",
		"肖君",
		"2014年11月7日",
		"2014年2月14日"
	};
	private string[] question1_B = {
		"5:1",
		"江西省",
		"双子座",
		"挡平",
		"2014年11月8日",
		"2014年3月14日"
	};
	private string[] question1_C = {
		"7:2",
		"湖南省",
		"天蝎座",
		"刘阳",
		"2014年10月11日",
		"2014年4月14日"
	};
	private string[] question1_D = {
		"8:3",
		"湖北省",
		"天秤座",
		"江涛",
		"2014年10月15日",
		"2014年5月14日"
	};
	private string[] question1_R = {
		"5:1",
		"广东省",
		"双子座",
		"江涛",
		"2014年10月11日",
		"2014年2月14日"
	};




	//产品
	private string[] question2 = {
		"产品中心的男女比例是多少？",
		"产品中心伙伴们最多是哪个省份？",
		"产品中心伙伴们最多是哪个星座？",
		"产品中心最早进入公司的是谁？",
		"公司的logo有几种颜色？",
		"公司年龄30岁以上的有几位？"
	};
	private string[] question2_A = {
		"2:11",
		"广东省",
		"天秤座",
		"何明超",
		"2",
		"27"
	};
	private string[] question2_B = {
		"3:11",
		"河南省",
		"巨蟹座",
		"刘喜",
		"3",
		"28"
	};
	private string[] question2_C = {
		"4:15",
		"湖南省",
		"天蝎座",
		"关智鹏",
		"4",
		"29"
	};
	private string[] question2_D = {
		"4:17",
		"湖北省",
		"处女座",
		"罗应秋",
		"5",
		"30"
	};
	private string[] question2_R = {
		"3:11",
		"广东省",
		"天蝎座",
		"何明超",
		"3",
		"28"
	};






	//总办
	private string[] question3 = {
		"总经办的男女比例是多少？",
		"总经办伙伴们最多是哪个省份？",
		"总经办伙伴们最多是哪个星座？",
		"总经办最早进入公司的谁？",
		"超过两年的员工有几位？",
		"公司去年年会主题是什么？"
	};
	private string[] question3_A = {
		"3:11",
		"广东省",
		"射手座",
		"梁总",
		"14",
		"专注超越"
	};
	private string[] question3_B = {
		"4:11",
		"甘肃省",
		"白羊座",
		"张雪冰",
		"15",
		"超越成长"
	};
	private string[] question3_C = {
		"3:14",
		"湖南省",
		"天蝎座",
		"潘兰兰",
		"16",
		"专注创造"
	};
	private string[] question3_D = {
		"4:13",
		"湖北省",
		"天秤座",
		"唐海英",
		"17",
		"发展成长"
	};
	private string[] question3_R = {
		"3:14",
		"广东省",
		"射手座",
		"梁总",
		"15",
		"专注超越"
	};





	//医生
	private string[] question4 = {
		"医院投资管理事业部的男女比例是多少？",
		"医院投资管理事业部伙伴们最多是哪个省份？",
		"医院投资管理事业部伙伴们最多是哪个星座？",
		"医院投资管理事业部最早进入公司的谁？",
		"公司今年开了几家诊所？",
		"北京联合诊所在几月几号开业？"
	};
	private string[] question4_A = {
		"6:13",
		"广东省",
		"双子座",
		"魏婷婷",
		"3",
		"2016年10月20日"
	};
	private string[] question4_B = {
		"7:15",
		"江西省",
		"双鱼座",
		"谭李莉",
		"4",
		"2016年10月21日"
	};
	private string[] question4_C = {
		"7:13",
		"湖南省",
		"狮子座",
		"滕赢赢",
		"5",
		"2016年10月22日"
	};
	private string[] question4_D = {
		"7:12",
		"四川省",
		"天秤座",
		"刘胜",
		"6",
		"2016年10月23日"
	};
	private string[] question4_R = {
		"7:12",
		"湖南省",
		"天秤座",
		"魏婷婷",
		"3",
		"2016年10月21日"
	};






	// Use this for initialization
	void Start () {

		isGameStop = false;
		totalTime = 0;
		isGameEnd = false;
		currentScore = 0;
		card = new GameObject[9];
		isChoose = false;
		isSelectAnswer = false;
		isBegin = false;

		randomQuestionList = new int[9];

		resultList = new GameObject[9];
	
		for (int i = 0; i < 9; ++i) {
			card[i] = createCard (new Vector2(group1originX + cardWidth * (i % 3), group1originY - cardHeight * (i / 3)));
		}

		hideAllPeople ();

		initMusicBtn ();
	}
	
	// Update is called once per frame
	void Update () {
	
		if (!isBegin || isGameEnd || isGameStop) {
			return;
		}
			

		if (choosePanel.transform.localScale.x == 0 && (Input.touchCount > 0 || Input.GetMouseButtonDown (0))) {                  

			if (Input.touchCount > 0 && Input.GetTouch (0).phase != TouchPhase.Began) {
				return;
			}

			if (questionPanel.transform.localScale.x == 0) {

				int touchIndex = getTouchedCard (getTouchPos ());

				if (touchIndex >= 0) {

					showQuestionPanel (touchIndex);

					currentTouchPos = touchIndex;
				}
			} 
		}
	}

	public void quitGame() {

		Application.LoadLevel (1);

		PlayerPrefs.SetString ("switch_postion", "" + 1);
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

	private void showQuestionPanel(int index) {

		//选对，不用再打开
		if (resultList [index] != null && resultList [index].name.Split('(')[0].Equals("QuestionYesPrefab")) {

			return;
		}



		int num = randomQuestionList [index];

		if (num != -1) {
			textQuestion.text = num == -1 ? "" : currentQuestionList [num];
			textItem1.text = num == -1 ? "" : currentAList [num];
			textItem2.text = num == -1 ? "" : currentBList [num];
			textItem3.text = num == -1 ? "" : currentCList [num];
			textItem4.text = num == -1 ? "" : currentDList [num];

			questionPanel.transform.localScale = new Vector2 (1, 1);
		} else {
			changeTexture (card[index], whiteBg);
		}
	}

	private void hideQuestionPanel() {

		toggle1.isOn = false;
		toggle2.isOn = false;
		toggle3.isOn = false;
		toggle4.isOn = false;
		questionPanel.transform.localScale = new Vector2 (0, 0);

		isSelectAnswer = false;
		btnQuestionSure.GetComponent<Image> ().sprite = textureSureDisable;
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

		currentChooseRole = 3;
		hasChoose ();
		hideAllPeople ();
		textureRocy.transform.localScale = new Vector2 (1, 1);
		btnRoc.GetComponent<Image> ().sprite = btnRoc1;

		showWord (1);
	}

	public void chooseMono() {

		currentChooseRole = 2;
		hasChoose ();
		hideAllPeople ();
		textureMono.transform.localScale = new Vector2 (1, 1);
		btnMono.GetComponent<Image> ().sprite = btnMono1;

		showWord (2);
	}

	public void chooseRay() {

		currentChooseRole = 1;
		hasChoose ();
		hideAllPeople ();
		textureRay.transform.localScale = new Vector2 (1, 1);
		btnRay.GetComponent<Image> ().sprite = btnRay1;

		showWord (3);
	}

	public void chooseSong() {

		currentChooseRole = 4;
		hasChoose ();
		hideAllPeople ();
		textureSong.transform.localScale = new Vector2 (1, 1);
		btnSong.GetComponent<Image> ().sprite = btnSong1;

		showWord (4);
	}

	public void btnSureClick() {

		if (isChoose) {

			isBegin = true;
			choosePanel.transform.localScale = new Vector2 (0, 0);
			initRandomList ();

			playAudio ();

			InvokeRepeating("timeCount", 1, 1);

			switch (currentChooseRole) {
			case 1:
				currentQuestionList = question1;
				currentAList = question1_A;
				currentBList = question1_B;
				currentCList = question1_C;
				currentDList = question1_D;
				currentRList = question1_R;
				break;
			case 2:
				currentQuestionList = question2;
				currentAList = question2_A;
				currentBList = question2_B;
				currentCList = question2_C;
				currentDList = question2_D;
				currentRList = question2_R;
				break;
			case 3:
				currentQuestionList = question3;
				currentAList = question3_A;
				currentBList = question3_B;
				currentCList = question3_C;
				currentDList = question3_D;
				currentRList = question3_R;
				break;
			case 4:
				currentQuestionList = question4;
				currentAList = question4_A;
				currentBList = question4_B;
				currentCList = question4_C;
				currentDList = question4_D;
				currentRList = question4_R;
				break;
			default:
				break;
			}
		}
	}
		
	//问题面板
	public void toggleSelect() {

		if (getCurrentSelectItem() >= 0) {

			isSelectAnswer = true;
			btnQuestionSure.GetComponent<Image> ().sprite = textureSureNormal;
		} else {

			isSelectAnswer = false;
			btnQuestionSure.GetComponent<Image> ().sprite = textureSureDisable;
		}
	}


	public void btnQuestionSureClick() {

		if (isSelectAnswer) {

			createResult (isAnswerTrue(getCurrentSelectString()), currentTouchPos);

			hideQuestionPanel ();

		}
	}

	private void initRandomList () {

		string[] originList = null;
		switch (currentChooseRole) {
		case 1:
			originList = question1;
			break;
		case 2:
			originList = question2;
			break;
		case 3:
			originList = question3;
			break;
		case 4:
			originList = question4;
			break;
		default:
			break;
		}

		int count = originList.Length;

		int[] list = new int[9]; 

		for (int i = 0; i < count; ++i) {
			list [i] = i;
		}
		for (int i = count; i < 9; ++i) {
			list [i] = -1;
		}


		int index = 8;

		while (index >= 1) {

			int targetIndex = (int)(Random.value * index);
			targetIndex = targetIndex == index ? targetIndex-1 : targetIndex;

			int temp = list [targetIndex];
			list [targetIndex] = list [index];
			list [index] = temp;
	
			index--;
		}

		for (int i = 0; i < 9; ++i) {

			randomQuestionList [i] = list [i];
		}

	}

	public void closeGuize() {

		guize.transform.localScale = new Vector2 (0, 0);

		choosePanel.transform.localScale = new Vector2 (1, 1);

	}

	private void hideAllWord() {

		word1.transform.localScale = new Vector2 (0, 0);
		word2.transform.localScale = new Vector2 (0, 0);
		word3.transform.localScale = new Vector2 (0, 0);
		word4.transform.localScale = new Vector2 (0, 0);
	}

	private void showWord(int i) {

		hideAllWord ();

		switch (i) {
		case 1:
			word1.transform.localScale = new Vector2 (1, 1);
			break;
		case 2:
			word2.transform.localScale = new Vector2 (1, 1);
			break;
		case 3:
			word3.transform.localScale = new Vector2 (1, 1);
			break;
		case 4:
			word4.transform.localScale = new Vector2 (1, 1);
			break;
		}
	}

	private int getCurrentSelectItem() {

		if (toggle1.isOn) {
			return 0;
		} else if (toggle2.isOn) {
			return 1;
		} else if (toggle3.isOn) {
			return 2;
		} else if (toggle4.isOn) {
			return 3;
		} else {
			return -1;
		}
	}

	private void createResult(bool isTrue, int index) {

		playSound (isTrue ? 0 : 1);

		if (resultList [index] != null) {

			Destroy (resultList [index]);
		}

		Vector2 pos = new Vector2 (group1originX + cardWidth * (index % 3), group1originY - cardHeight * (index / 3));

		resultList [index] = (GameObject)Instantiate (isTrue ? yesPrefab : noPrefab, pos, Quaternion.identity);

		changeTexture (card[index], cardOpen);

		if (isTrue) {

			currentScore++;
			updateScoreView ();

			if (currentScore >= 4) {

				initMusicBtn1 ();

				isGameEnd = true;
				win.transform.localScale = new Vector2 (1,1);
				winText.text = textTime.text;

				stopAudio ();

				playSound (2);

				saveScore ();
			}
		}
	}

	private bool isAnswerTrue(string answer) {

		return currentRList [randomQuestionList [currentTouchPos]].Equals (answer);
	}

	private string getCurrentSelectString() {

		if (toggle1.isOn) {
			return currentAList [randomQuestionList [currentTouchPos]];
		} else if (toggle2.isOn) {
			return currentBList [randomQuestionList [currentTouchPos]];
		} else if (toggle3.isOn) {
			return currentCList [randomQuestionList [currentTouchPos]];
		} else if (toggle4.isOn) {
			return currentDList [randomQuestionList [currentTouchPos]];
		} else {
			return "";
		}
	}


	private void updateScoreView() {

		if (currentScore > 0) {

			score1.sprite = starYellow;

			if (currentScore > 1) {

				score2.sprite = starYellow;

				if (currentScore > 2) {

					score3.sprite = starYellow;

					if (currentScore > 3) {

						score4.sprite = starYellow;

					}
				}
			}
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

	public void openBackPanel() {

		GameObject backBg = GameObject.Find ("back_bg");
		backBg.transform.localScale = new Vector2 (1, 1);

		Time.timeScale = 0;
		isGameStop = true;
	}

	public void closeBackPanel() {

		GameObject backBg = GameObject.Find ("back_bg");
		backBg.transform.localScale = new Vector2 (0, 0);

		Time.timeScale = 1;
		isGameStop = false;
	}

	public void backYClick() {

		quitGame ();
		Time.timeScale = 1;
	}

	public void backNClick() {

		closeBackPanel ();
	}
		

	public void rePlayGame () {

		Application.LoadLevel (7);
	}

	private void saveScore() {

		string str = PlayerPrefs.GetString("score_1", "null");
		if (str.Equals ("null")) {
			PlayerPrefs.SetString ("score_1", "" + totalTime);
		} else {
			if (int.Parse (str) > totalTime) {
				PlayerPrefs.SetString ("score_1", "" + totalTime);
			}
		}
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

	public void playSound(int index) {

		GameObject.Find("Main Camera").GetComponent<CameraScript>().PlaySound (index, false, 1);
	}

	public void openMusic() {

		PlayerPrefs.SetString ("closeMusic", "" + 0);
		playAudio ();
		music.image.sprite = musicOn;
		music1.image.sprite = musicOn;
	}

	public void closeMusic() {

		PlayerPrefs.SetString ("closeMusic", "" + 1);
		stopAudio ();
		music.image.sprite = musicOff;
		music1.image.sprite = musicOff;
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

	public void initMusicBtn1() {

		string str1 = PlayerPrefs.GetString("closeMusic", "null");
		if (!str1.Equals ("null") && int.Parse (str1) == 1) {
			music1.image.sprite = musicOff;
		} else {
			music1.image.sprite = musicOn;
		}

	}
}
