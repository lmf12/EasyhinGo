using UnityEngine;
using UnityEngine.EventSystems; 
using System.Collections;

public class RunSetScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {

	public GameObject light;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
	}

	// 当按钮被按下后系统自动调用此方法  
	public void OnPointerDown (PointerEventData eventData) {

	}  

	// 当按钮抬起的时候自动调用此方法  
	public void OnPointerUp (PointerEventData eventData) {

		light.GetComponent<LightScript> ().setFloor ();
		transform.localScale = new Vector2 (0, 0);
		light.GetComponent<LightScript> ().reset ();
	} 
}
