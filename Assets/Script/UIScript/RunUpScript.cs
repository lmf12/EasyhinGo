using UnityEngine;
using UnityEngine.EventSystems; 
using System.Collections;

public class RunUpScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {

	public GameObject handle;
	public GameObject ladder;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	// 当按钮被按下后系统自动调用此方法  
	public void OnPointerDown (PointerEventData eventData) {

		handle.GetComponent<HandleScript> ().switchHandle ();
	}  

	// 当按钮抬起的时候自动调用此方法  
	public void OnPointerUp (PointerEventData eventData) {
	} 
}
