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
		"",
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
		""
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
		"2016年10月8日"
	};
	private string[] question4_B = {
		"7:15",
		"江西省",
		"双鱼座",
		"谭李莉",
		"4",
		"2016年10月9日"
	};
	private string[] question4_C = {
		"7:13",
		"湖南省",
		"狮子座",
		"滕赢赢",
		"5",
		"2016年10月10日"
	};
	private string[] question4_D = {
		"7:12",
		"四川省",
		"天秤座",
		"刘胜",
		"6",
		"2016年10月11日"
	};
	private string[] question4_R = {
		"7:12",
		"湖南省",
		"天秤座",
		"魏婷婷",
		"3",
		"2016年10月8日"
	};






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
