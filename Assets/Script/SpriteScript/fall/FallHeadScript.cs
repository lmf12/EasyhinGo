using UnityEngine;
using System.Collections;

public class FallHeadScript : MonoBehaviour {

	//文本内容
	private string[] rays = {
		"帅气的",
		"精壮的",
		"睿智的",
		"野生的"
	};

	private string[] monos = {
		"美丽可人的",
		"楚楚动人的",
		"端庄优雅的",
		"妩媚的"
	};

	private string[] rocs = {
		"富可敌国的",
		"幽默风趣的",
		"学富五车的"
	};

	private string[] songs = {
		"强壮的",
		"稀有的"
	};

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay2D(Collider2D other) {

		GameObject mainLogic = GameObject.Find ("FallMainLogic");

		if (other.gameObject.name.Equals ("people_1")) {

			string str = "捕获 ";

			if (gameObject.name.Equals ("MonoPrefab(Clone)")) {

				int index = (int)(Random.value * monos.Length);
				str = str + monos[index == monos.Length ? 0 : index] + " mono";

				mainLogic.GetComponent<FallMainLogicScript> ().getMono ();
			} else if (gameObject.name.Equals ("RayPrefab(Clone)")) {

				int index = (int)(Random.value * rays.Length);
				str = str + rays[index == rays.Length ? 0 : index] + " 磊哥";

				mainLogic.GetComponent<FallMainLogicScript> ().getRay ();
			} else if (gameObject.name.Equals ("RocPrefab(Clone)")) {

				int index = (int)(Random.value * rocs.Length);
				str = str + rocs[index == rocs.Length ? 0 : index] + " 财神爷";

				mainLogic.GetComponent<FallMainLogicScript> ().getRoc ();
			} else if (gameObject.name.Equals ("SongPrefab(Clone)")) {

				int index = (int)(Random.value * songs.Length);
				str = str + songs[index == songs.Length ? 0 : index] + " 强哥";

				mainLogic.GetComponent<FallMainLogicScript> ().getSong ();
			}

			mainLogic.GetComponent<FallMainLogicScript> ().createText (str);
		}



		Destroy(gameObject);
	}

	void OnTriggerExit2D(Collider2D other) {

	}
}
