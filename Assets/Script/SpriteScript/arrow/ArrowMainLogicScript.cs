using UnityEngine;
using System.Collections;

public class ArrowMainLogicScript : MonoBehaviour {

	public GameObject ropePrefab;

	private GameObject rope1;
	private GameObject balloon1;
	private GameObject object1;

	// Use this for initialization
	void Start () {

		balloon1 = GameObject.Find ("balloon_1");
		object1 = GameObject.Find ("object_1");

		if (object1.GetComponent<DistanceJoint2D> () != null) {
			rope1 = this.drawLine (rope1, this.localToWorld (balloon1, object1.GetComponent<DistanceJoint2D> ().connectedAnchor), this.localToWorld (object1, object1.GetComponent<DistanceJoint2D> ().anchor));
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (object1.GetComponent<DistanceJoint2D> () != null) {
			rope1 = this.drawLine (rope1, this.localToWorld (balloon1, object1.GetComponent<DistanceJoint2D> ().connectedAnchor), this.localToWorld (object1, object1.GetComponent<DistanceJoint2D> ().anchor));
		} else {
			Destroy (rope1);
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
}
