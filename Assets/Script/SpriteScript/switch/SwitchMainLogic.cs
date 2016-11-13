using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SwitchMainLogic : MonoBehaviour {

	public Button button1;
	public Button button2;
	public Button button3;
	public Button button4;
	public Button button5;

	public GameObject loc1;
	public GameObject loc2;
	public GameObject loc3;
	public GameObject loc4;
	public GameObject loc5;

	public GameObject background1;
	public GameObject background2;
	public GameObject background3;
	public GameObject background4;

	private Vector2 touchLastPos;
	private float moveDistance;

	public Image mainLoc;

	bool isScrolling = false;

	private float baseVelocity = 12.0f;
	private float baseForce = 0.3f;
	private float minDis = 0.4f; 
	private float beginX1 = 0.01f;
	private float endX1 = -115.0f;
	private float beginX2 = 0.01f;
	private float endX2 = -100.0f;
	private float beginX3 = 0.01f;
	private float endX3 = -85.0f;
	private float beginX4 = 0.01f;
	private float endX4 = -70.0f;


	private float originLocX;
	private float originMainX;

	// Use this for initialization
	void Start () {

		originLocX = background1.transform.position.x;
		originMainX = Camera.main.ScreenToWorldPoint (mainLoc.transform.position).x;
	}
	
	// Update is called once per frame
	void Update() {

		updateBackground1Pos ();

		updateBackground2Pos ();
		updateBackground3Pos ();
		updateBackground4Pos ();

		updateStone ();
		updateBoat ();

		updateButtonLoc ();
	}

	private void updateBackground1Pos() {

		float posX = Camera.main.ScreenToWorldPoint (mainLoc.transform.position).x;
		float background1X = originLocX + posX - originMainX;
		background1.transform.position = new Vector2 (background1X, background1.transform.position.y);
	}

	private void updateBackground2Pos() {

		Vector2 pos = background1.transform.position;
		float background2X = beginX2 + (endX2 - beginX2) * ((pos.x - beginX1) / (endX1 - beginX1)); 
		background2.transform.position = new Vector2 (background2X, background2.transform.position.y);
	}

	private void updateBackground3Pos() {

		Vector2 pos = background1.transform.position;
		float background3X = beginX3 + (endX3 - beginX3) * ((pos.x - beginX1) / (endX1 - beginX1)); 
		background3.transform.position = new Vector2 (background3X, background3.transform.position.y);
	}

	private void updateBackground4Pos() {

		Vector2 pos = background1.transform.position;
		float background4X = beginX4 + (endX4 - beginX4) * ((pos.x - beginX1) / (endX1 - beginX1)); 
		background4.transform.position = new Vector2 (background4X, background4.transform.position.y);
	}

	private void updateStone() {

		float beginx = -55.0f;
		float endx = -65.0f;
		float beginA = 0;
		float endA = 1;


		Vector2 pos = background4.transform.position;
		GameObject stone = GameObject.Find ("switch_stone_4");

		if (pos.x <= beginx && pos.x >= endx) {

			Color c = stone.GetComponent<SpriteRenderer> ().color; 
			c.a = beginA + (endA - beginA) * ((pos.x - beginx) / (endx - beginx)); 
			stone.GetComponent<SpriteRenderer> ().color = c;
		} else if (pos.x > beginx) {
			Color c = stone.GetComponent<SpriteRenderer> ().color; 
			c.a = beginA; 
			stone.GetComponent<SpriteRenderer> ().color = c;
		} else {
			Color c = stone.GetComponent<SpriteRenderer> ().color; 
			c.a = endA; 
			stone.GetComponent<SpriteRenderer> ().color = c;
		}
	}

	private void updateBoat() {

		float beginx = -60.0f;
		float endx = -68.0f;
		float beginA = 0;
		float endA = 1;

		float beginY = 5.65f;
		float endY = 0;


		Vector2 pos = background4.transform.position;
		GameObject boat = GameObject.Find ("switch_boat");

		Vector2 posBoat = boat.transform.position;

		if (pos.x <= beginx && pos.x >= endx) {

			Color c = boat.GetComponent<SpriteRenderer> ().color; 
			c.a = beginA + (endA - beginA) * ((pos.x - beginx) / (endx - beginx)); 
			boat.GetComponent<SpriteRenderer> ().color = c;

			posBoat.y = beginY + (endY - beginY) * ((pos.x - beginx) / (endx - beginx));
			boat.transform.position = posBoat;
		} else if (pos.x > beginx) {
			Color c = boat.GetComponent<SpriteRenderer> ().color; 
			c.a = beginA; 
			boat.GetComponent<SpriteRenderer> ().color = c;

			posBoat.y = beginY;
			boat.transform.position = posBoat;
		} else {
			Color c = boat.GetComponent<SpriteRenderer> ().color; 
			c.a = endA; 
			boat.GetComponent<SpriteRenderer> ().color = c;

			posBoat.y = endY;
			boat.transform.position = posBoat;
		}
	}

	private void updateButtonLoc() {

		button1.transform.position = Camera.main.WorldToScreenPoint (loc1.transform.position);
		button2.transform.position = Camera.main.WorldToScreenPoint (loc2.transform.position);
		button3.transform.position = Camera.main.WorldToScreenPoint (loc3.transform.position);
		button4.transform.position = Camera.main.WorldToScreenPoint (loc4.transform.position);
		button5.transform.position = Camera.main.WorldToScreenPoint (loc5.transform.position);
	}
}
