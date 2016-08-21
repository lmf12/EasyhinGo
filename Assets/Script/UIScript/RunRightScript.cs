using UnityEngine;
using UnityEngine.EventSystems; 
using System.Collections;

public class RunRightScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {

	public GameObject marry;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 当按钮被按下后系统自动调用此方法  
	public void OnPointerDown (PointerEventData eventData) {

		marry.GetComponent<MarryScript> ().moveRight (0.1f);
	}  

	// 当按钮抬起的时候自动调用此方法  
	public void OnPointerUp (PointerEventData eventData) {

		marry.GetComponent<MarryScript> ().stopMoving();
	} 
}
