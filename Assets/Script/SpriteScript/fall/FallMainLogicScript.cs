﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FallMainLogicScript : MonoBehaviour {

	public GameObject people;
	public GameObject boom;

	public GameObject ray;
	public GameObject roc;
	public GameObject song;
	public GameObject mono;

	private float speed = 0.06f;

	private float minX = -6;
	private float maxX = 6;

	private bool isMoving;
	private bool isRight;

	private bool isLeftPress;
	private bool isRightPress;


	private GameObject currentObj1;
	private GameObject currentObj2;
	private GameObject currentObj3;
	private GameObject currentObj4;

	private int objType;



	public Image imgRay;
	public Image imgMono;
	public Image imgSong;
	public Image imgRoc;

	public Sprite spRay;
	public Sprite spMono;
	public Sprite spSong;
	public Sprite spRoc;



	// Use this for initialization
	void Start () {
	
		isMoving = false;
		isRight = true;

		isLeftPress = false;
		isRightPress = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float flag = canCreate ();
		if (flag > -8) {

			switch (objType) {
			case 1:
				currentObj1 = createObj (flag);
				break;
			case 2:
				currentObj2 = createObj (flag);
				break;
			case 3:
				currentObj3 = createObj (flag);
				break;
			case 4:
				currentObj4 = createObj (flag);
				break;
			default:
				break;
			}
		}

		if (isMoving) {

			//边界判断
			if (people.transform.position.x < minX) {
				people.transform.position = new Vector2(minX, people.transform.position.y);
				isMoving = false;
				return;
			}
			if (people.transform.position.x > maxX) {
				people.transform.position = new Vector2(maxX, people.transform.position.y);
				isMoving = false;
				return;
			}

			people.transform.Translate (new Vector2 (speed, 0));
		}
	}

	public void quitGame() {

		Application.LoadLevel (1);
	}

	public void moveLeft() {

		isLeftPress = true;
		isMoving = true;
		if (isRight) {
			this.turnFace ();
		}
		isRight = false;
	}

	public void moveRight() {
	
		isRightPress = true;
		isMoving = true;
		if (!isRight) {
			this.turnFace ();
		}
		isRight = true;
	}

	//1 left 2 right
	public void stopMoving(int type) {

		if (type == 1) {
			isLeftPress = false;
		} else if (type == 2) {
			isRightPress = false;
		}

		if (isLeftPress) {
			moveLeft ();
		} else if (isRightPress) {
			moveRight ();
		} else {
			isMoving = false;
		}
	}

	//转向
	private void turnFace() {

		Vector2 scale = people.transform.localScale;
		scale.x *= -1;
		people.transform.localScale = scale;
	}

	private GameObject createBoom(float x) {

		return (GameObject)Instantiate (boom, new Vector2(x, 5), Quaternion.identity);
	}

	private GameObject createHead(float x) {

		int index = (int)(Random.value * 9);

		switch (index) {
		case 0:
		case 1:
			return (GameObject)Instantiate (ray, new Vector2(x, 5), Quaternion.identity);
			break;
		case 2:
			int i = (int)(Random.value * 2);
			return (GameObject)Instantiate (i==0 ?song:mono, new Vector2(x, 5), Quaternion.identity);
			break;
		case 3:
		case 4:
		case 5:
			return (GameObject)Instantiate (mono, new Vector2(x, 5), Quaternion.identity);
			break;
		default:
			return (GameObject)Instantiate (roc, new Vector2(x, 5), Quaternion.identity);
			break;
		}
	}

	private GameObject createObj(float x) {

		int index = (int)(Random.value * 3);

		if (index == 0) {
			return createBoom (x);
		} else {
			return createHead (x);
		}
	}

	private float canCreate () {

		if (currentObj1 == null) {
			objType = 1;
			return (-5.5f + Random.value * 2);
		} else if (currentObj2 == null) {
			objType = 2;
			return (-2.5f + Random.value * 2);
		} else if (currentObj3 == null) {
			objType = 3;
			return (0.5f + Random.value * 2);
		} else if (currentObj4 == null) {
			objType = 4;
			return (3.5f + Random.value * 2);
		} else {
			return -10.0f;
		}
	}

	public void getMono() {

		imgMono.sprite = spMono;
	}

	public void getRoc() {

		imgRoc.sprite = spRoc;
	}

	public void getRay() {

		imgRay.sprite = spRay;
	}

	public void getSong() {

		imgSong.sprite = spSong;
	}
}