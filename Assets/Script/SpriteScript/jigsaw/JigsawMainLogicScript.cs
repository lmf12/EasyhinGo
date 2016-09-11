using UnityEngine;
using System.Collections;

public class JigsawMainLogicScript : MonoBehaviour {

	private float brickWidth = 2.7f;
	private ArrayList map = new ArrayList(new string[9]);  // 保存地图信息

	// Use this for initialization
	void Start () {
	
		this.initLocation ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

	private void initLocation () {
		
		string rootName = "jigsaw_img_1_0";

		ArrayList randomList = this.getRandomListWithCount (8);

		for (int i = 0; i < 8; ++i) {

			string name = rootName + (i + 1);

			GameObject brick = GameObject.Find(name);
			brick.transform.position = this.getPostionWithIndex((int)randomList[i]);

			map [(int)randomList [i]] = name;
		}

		map [8] = "";
	}

	private Vector2 getPostionWithIndex(int index) {

		float x = ((index % 3) - 1) * brickWidth;
		float y = - ((index / 3) - 1) * brickWidth;

		return new Vector2 (x, y);
	}

	// 获取0～count－1 的随机数组
	private ArrayList getRandomListWithCount (int count) {

		ArrayList list = new ArrayList ();

		for (int i = 0; i < count; ++i) {

			list.Add (i);
		}

		for (int i = count - 1; i >= 1; --i) {

			int index = (int)(Random.value * (i-1));
			int temp = (int)list [index];
			list[index] = list[i];
			list[i] = temp;
		}

		return list;
	}
}

