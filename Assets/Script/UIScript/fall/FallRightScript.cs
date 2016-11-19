using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems; 

public class FallRightScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {

	public GameObject mainLogic;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 当按钮被按下后系统自动调用此方法  
	public void OnPointerDown (PointerEventData eventData) {

		mainLogic.GetComponent<FallMainLogicScript> ().moveRight ();
	}  

	// 当按钮抬起的时候自动调用此方法  
	public void OnPointerUp (PointerEventData eventData) {

		mainLogic.GetComponent<FallMainLogicScript> ().stopMoving(2);
	} 
}
