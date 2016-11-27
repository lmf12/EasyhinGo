using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using LitJson;

public class StratMainLogic : MonoBehaviour {

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

	public InputField iFName;

	public Image launch;
	public Image rank;
	public Image input;

	public Image updataTips;

	private WWWHelper wwwHelper;

	private float duration = 1.0f;  //动画持续
	private float thinSpead;
	private bool isPlaying = false;  //是否播放动画


	private int requestType;

	// Use this for initialization
	void Start () {

		creatUserID ();

		initText ();


		//是否需要启动页
		string str = PlayerPrefs.GetString("openHomeWithoutLaunch", "null");
		if (!str.Equals ("null")) {
			launch.transform.localScale = new Vector2 (0, 0);
		} else {
			Invoke("hideLaunch", 2);
		}
		PlayerPrefs.SetString ("openHomeWithoutLaunch", "null");



		thinSpead = (launch.color.a - 0) / (duration / Time.fixedDeltaTime);


		wwwHelper = GameObject.Find ("HttpHelper").GetComponent<WWWHelper> ();


		requestList (getUserID());


	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (isPlaying) {

			if (Mathf.Abs (0 - launch.color.a) <= thinSpead) {

				Color c = launch.color; 
				c.a = 0; 
				launch.color = c;
			} else {

				Color c = launch.color; 
				c.a -= thinSpead; 
				launch.color = c;
			}

			if (launch.color.a == 0) {

				launch.transform.localScale = new Vector2 (0, 0);
				isPlaying = false;
			}
		}

	}

	private void requestList(string userid) {

		requestType = 1;
		wwwHelper.GET ("http://120.76.243.215/pro/index.php/api/risker/getRankingList?f_client_id="+userid, gameObject);
	}

	private void requestWrite(string userid, string name, string time) {
		
		requestType = 2;
		wwwHelper.GET ("http://120.76.243.215/pro/index.php/api/risker/saveInfo?f_client_id="+userid+"&f_nick_name="+name+"&f_score="+time, gameObject);
	}

	void RequestDone (string result) {

	
		if (requestType == 1) {

			JsonData jd = JsonMapper.ToObject (result);

			string rank = (string)jd ["result"] ["ranking"];

			JsonData list = jd ["result"] ["list"]; 

			Text[] nameList = { name1, name2, name3, name4, name5 };
			Text[] scoreList = { time1, time2, time3, time4, time5 };

			for (int i = 0; i < list.Count && i < 5; i++) {
				nameList [i].text = (string)list [i] ["f_nick_name"];
				scoreList [i].text = getTimeStringFromSecond (int.Parse ((string)list [i] ["f_score"]));
			}


			if (int.Parse (rank) >= 0 && int.Parse (rank) < list.Count) {
		
				int index = int.Parse (rank);

				nameSelf.text = (string)list [index - 1] ["f_nick_name"];
				timeSelf.text = getTimeStringFromSecond (int.Parse ((string)list [index - 1] ["f_score"]));

				rankSelf.text = rank;
			}
		} else if (requestType == 2) {

			showTips ();
		}
	}

	void hideLaunch() {

		isPlaying = true;
	}

	public void openRank() {

		rank.transform.localScale = new Vector2 (1, 1);

		requestList (getUserID());
	}

	public void closeRank() {

		rank.transform.localScale = new Vector2 (0, 0);
	}

	private void creatUserID() {

		string str = PlayerPrefs.GetString("userid", "null");
		if (str.Equals ("null")) {

			string userid = "";
			System.DateTime now =  System.DateTime.Now;
			char[] list = {' ', ':', '/'};
			string[] chars = ("" + now).Split(list);
			for (int i = 0; i < chars.Length; ++i) {
				userid += chars [i];
			}
			userid += (int)(Random.value * 9);
			userid += (int)(Random.value * 9);

			PlayerPrefs.SetString ("userid", userid);
		}
	}

	private string getUserID () {

		return PlayerPrefs.GetString("userid", "null");
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

	private string getTimeStringFromSecond(int time) {

		string second = "" + (time % 60);
		string minute = "" + ((time / 60) % 60);

		second = (second.Length == 1 ? "0" : "") + second;
		minute = (minute.Length == 1 ? "0" : "") + minute;

		return minute + " : " + second;
	}

	public void inputCancle() {
	
		input.transform.localScale = new Vector2 (0, 0);
	}

	public void inputSubmit() {

		string score0 = PlayerPrefs.GetString("score_0", "null");
		string score2 = PlayerPrefs.GetString("score_2", "null");
		string score3 = PlayerPrefs.GetString("score_3", "null");
		string score4 = PlayerPrefs.GetString("score_4", "null");

		string name = UTF8String(iFName.text);

		if (name != null && name.Length > 0) {
			requestWrite (getUserID(),name, "" + ( int.Parse(score0) + int.Parse(score2) + int.Parse(score3) + int.Parse(score4)));

			PlayerPrefs.SetString ("userNickName", iFName.text);

			input.transform.localScale = new Vector2 (0, 0);
		}
	}

	public void openInputPanel() {

		string score0 = PlayerPrefs.GetString("score_0", "null");
		string score2 = PlayerPrefs.GetString("score_2", "null");
		string score3 = PlayerPrefs.GetString("score_3", "null");
		string score4 = PlayerPrefs.GetString("score_4", "null");

		if (!score0.Equals ("null") && !score2.Equals ("null") && !score3.Equals ("null") && !score4.Equals ("null")) {

			string str = PlayerPrefs.GetString("userNickName", "null");
			if (!str.Equals ("null")) {
				requestWrite (getUserID (), UTF8String(str), "" + (int.Parse (score0) + int.Parse (score2) + int.Parse (score3) + int.Parse (score4)));
			} else {
				input.transform.localScale = new Vector2 (1, 1);
			}

		}


	}

	public string UTF8String(string input)
	{
		return WWW.EscapeURL (input, System.Text.Encoding.UTF8);
	}


	private void showTips() {

		Image tips = (Image)Instantiate (updataTips, new Vector2 (Screen.width/2, Screen.height * 0.3f), Quaternion.identity);

		tips.transform.SetParent (GameObject.Find("Canvas").transform);

		Destroy (tips, 1.5f);
	}
}
