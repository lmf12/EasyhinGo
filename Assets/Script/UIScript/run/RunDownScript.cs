using UnityEngine;
using UnityEngine.EventSystems; 
using System.Collections;

public class RunDownScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {

	public GameObject ladder;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

	// 当按钮被按下后系统自动调用此方法  
	public void OnPointerDown (PointerEventData eventData) {

		ladder.GetComponent<LadderScript> ().climbDown ();
	}  

	// 当按钮抬起的时候自动调用此方法  
	public void OnPointerUp (PointerEventData eventData) {

		ladder.GetComponent<LadderScript> ().stopClimbing ();
	} 
}
