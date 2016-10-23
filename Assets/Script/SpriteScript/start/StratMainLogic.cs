using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StratMainLogic : MonoBehaviour {

	private WWWHelper wwwHelper;
	public Text title;

	// Use this for initialization
	void Start () {
		
		wwwHelper = GameObject.Find ("HttpHelper").GetComponent<WWWHelper> ();

		wwwHelper.GET ("http://115.29.144.170/risker/", gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void RequestDone (string result) {

		title.text = result;
	}
}
