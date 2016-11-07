using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StratMainLogic : MonoBehaviour {

	public Image launch;

	private WWWHelper wwwHelper;

	private float duration = 1.0f;  //动画持续
	private float thinSpead;
	private bool isPlaying = false;  //是否播放动画

	// Use this for initialization
	void Start () {

		Invoke("hideLaunch", 2);
		thinSpead = (launch.color.a - 0) / (duration / Time.fixedDeltaTime);


		wwwHelper = GameObject.Find ("HttpHelper").GetComponent<WWWHelper> ();

		wwwHelper.GET ("http://115.29.144.170/risker/", gameObject);
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

	void RequestDone (string result) {

	}

	void hideLaunch() {

		isPlaying = true;
	}
}
