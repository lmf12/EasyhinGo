using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		Vector2 velocity = GetComponent<Rigidbody2D> ().velocity;
		velocity.y = 10.0f;
		velocity.x = 10.0f;
		GetComponent<Rigidbody2D> ().velocity = velocity;

		this.rotation (this.getSin(velocity.x, velocity.y));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		Vector2 velocity = GetComponent<Rigidbody2D> ().velocity;

		this.rotation (this.getSin(velocity.x, velocity.y));
	}

	//旋转 从0～2pi  顺时针
	private void rotation(float r) {

		Quaternion rotation = transform.localRotation;

		float angle = r - Mathf.PI / 2;

		rotation.z = Mathf.Sin(angle / 2);

		rotation.w = Mathf.Cos(angle / 2);

		transform.localRotation = rotation;
	}

	//获取正弦值
	private float getSin(float x, float y) {

		return y / Mathf.Sqrt (x * x + y * y);
	}
}
