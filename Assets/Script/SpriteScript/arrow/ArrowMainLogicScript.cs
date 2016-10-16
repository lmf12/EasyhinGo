﻿using UnityEngine;
using System.Collections;

public class ArrowMainLogicScript : MonoBehaviour {

	public GameObject ropePrefab;

	private GameObject rope1;
	private GameObject rope2;
	private GameObject rope3;
	private GameObject balloon1;
	private GameObject object1;
	private GameObject balloon2;
	private GameObject object2;
	private GameObject balloon3;
	private GameObject object3;

	private int score;

	// Use this for initialization
	void Start () {

		score = 0;

		balloon1 = GameObject.Find ("balloon_1");
		object1 = GameObject.Find ("object_1");
		balloon2 = GameObject.Find ("balloon_2");
		object2 = GameObject.Find ("object_2");
		balloon3 = GameObject.Find ("balloon_3");
		object3 = GameObject.Find ("object_3");

		if (object1.GetComponent<DistanceJoint2D> () != null) {
			rope1 = this.drawLine (rope1, this.localToWorld (balloon1, object1.GetComponent<DistanceJoint2D> ().connectedAnchor), this.localToWorld (object1, object1.GetComponent<DistanceJoint2D> ().anchor));
		}

		if (object2.GetComponent<DistanceJoint2D> () != null) {
			rope2 = this.drawLine (rope2, this.localToWorld (balloon2, object2.GetComponent<DistanceJoint2D> ().connectedAnchor), this.localToWorld (object2, object2.GetComponent<DistanceJoint2D> ().anchor));
		}

		if (object3.GetComponent<DistanceJoint2D> () != null) {
			rope3 = this.drawLine (rope3, this.localToWorld (balloon3, object3.GetComponent<DistanceJoint2D> ().connectedAnchor), this.localToWorld (object3, object3.GetComponent<DistanceJoint2D> ().anchor));
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (object1 != null && object1.GetComponent<DistanceJoint2D> () != null) {
			rope1 = this.drawLine (rope1, this.localToWorld (balloon1, object1.GetComponent<DistanceJoint2D> ().connectedAnchor), this.localToWorld (object1, object1.GetComponent<DistanceJoint2D> ().anchor));
		} else {
			if (rope1 != null) {
				Destroy (rope1);
				rope1 = null;
			}
		}

		if (object2 != null && object2.GetComponent<DistanceJoint2D> () != null) {
			rope2 = this.drawLine (rope2, this.localToWorld (balloon2, object2.GetComponent<DistanceJoint2D> ().connectedAnchor), this.localToWorld (object2, object2.GetComponent<DistanceJoint2D> ().anchor));
		} else {
			if (rope2 != null) {
				Destroy (rope2);
				rope2 = null;
			}
		}

		if (object3 != null && object3.GetComponent<DistanceJoint2D> () != null) {
			rope3 = this.drawLine (rope3, this.localToWorld (balloon3, object3.GetComponent<DistanceJoint2D> ().connectedAnchor), this.localToWorld (object3, object3.GetComponent<DistanceJoint2D> ().anchor));
		} else {
			if (rope3 != null) {
				Destroy (rope3);
				rope3 = null;
			}
		}

		if (score >= 3) {

			Application.LoadLevel (1);
		}
	}

	GameObject drawLine(GameObject obj ,Vector2 start, Vector2 end) {

		if (obj == null) {
			
			obj = (GameObject)Instantiate (ropePrefab, new Vector2 ((start.x + end.x) / 2, (start.y + end.y) / 2), Quaternion.identity);
			Vector2 size = obj.GetComponent<Renderer>().bounds.size;

			float width = 0.02f;
			float height = (start - end).magnitude;

			obj.transform.localScale = new Vector2 (width/size.x, height/size.y);
		} else {
			
			obj.transform.position = new Vector2 ((start.x + end.x) / 2, (start.y + end.y) / 2);
		}

		this.rotation (obj, this.getSin(end.y - start.y, end.x - start.x));

		return obj;
	}

	//旋转 从0～2pi  顺时针
	private void rotation(GameObject obj ,float r) {

		Quaternion rotation = obj.transform.localRotation;

		float angle = r;

		rotation.z = Mathf.Sin(angle / 2);

		rotation.w = Mathf.Cos(angle / 2);

		obj.transform.localRotation = rotation;
	}

	//获取正弦值
	private float getSin(float x, float y) {

		return y / Mathf.Sqrt (x * x + y * y);
	}

	private Vector2 localToWorld(GameObject obj, Vector2 pos) {

		return obj.transform.localToWorldMatrix.MultiplyPoint (pos);
	}

	public void addScore(GameObject obj) {

		this.score += 1;

		Destroy (obj);
	}
}