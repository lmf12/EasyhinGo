﻿using UnityEngine;
using UnityEngine.EventSystems; 
using System.Collections;

public class StartScript : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {

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

		//读取分数
		string str = PlayerPrefs.GetString("isStoryPlay", "null");
		if (!str.Equals ("null")) {
			Application.LoadLevel (1);
		} else {
			Application.LoadLevel (8);
		}
	} 
}
