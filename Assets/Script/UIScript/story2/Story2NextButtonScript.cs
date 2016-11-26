using UnityEngine;
using UnityEngine.EventSystems; 
using System.Collections;

public class Story2NextButtonScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 当按钮被按下后系统自动调用此方法  
	public void OnPointerDown (PointerEventData eventData) {

	}  

	// 当按钮抬起的时候自动调用此方法  
	public void OnPointerUp (PointerEventData eventData) {

		PlayerPrefs.SetString ("openHomeWithoutLaunch", "1");

		Application.LoadLevel (0);

		PlayerPrefs.SetString ("isStoryPlayEnd", "1");

	} 
}
