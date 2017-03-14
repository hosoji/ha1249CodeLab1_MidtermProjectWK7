using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingScript : MonoBehaviour {

	Rigidbody2D rb;

	public Vector2 forceAmount;
	public Vector2 liftAmount;

	float brakeFactor = 0f;

	private float brakeAmount;
	public float BrakeAmount {
		get{
			return brakeAmount;
		}
		set{
			brakeAmount = value;
		}
	}

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		rb.isKinematic = true;

		Invoke ("DescendPlane", 2f);

		brakeAmount = 0.1f;
	}

	// Update is called once per frame
	void Update () {

		//To Center Camera
		Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, -1);

//		if (Input.GetKeyDown (KeyCode.B)) {
//			brakeAmount = brakeAmount * brakeFactor;
//			ApplyBrakes (brakeAmount);
//		}

		if (Input.GetKey (KeyCode.Space)) {
			rb.constraints = RigidbodyConstraints2D.FreezePositionY;
			GameManager.instance.Fuel -= GameManager.instance.baseFuelCost;
		} else {
			rb.constraints = RigidbodyConstraints2D.None;
		}

		if (Input.GetKey (KeyCode.Z)) {
			rb.AddForce (liftAmount, ForceMode2D.Impulse);
			GameManager.instance.Fuel -= GameManager.instance.baseFuelCost;
		}
		
	}

	void DescendPlane(){
		rb.isKinematic = false;
		rb.AddForce (forceAmount, ForceMode2D.Impulse);
	}

	void ApplyBrakes(float b){
		rb.drag = b;
		
	}

	public void OnTriggerEnter2D (Collider2D other){

//
//		if (other.tag == "Correct") {
//			brakeFactor = 2.5f;
//		}
//		else if (other.tag == "Incorrect"){
//			brakeFactor = 0f;
//		}
	}
		

//	public void OnTriggerExit2D (Collider2D other){
//
//
//		if (other.tag == "Landing Cue") {
//			brakeFactor = 0f;
//		}
//	}

}
