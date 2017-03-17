using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LandingScript : MonoBehaviour {

	Rigidbody2D rb;

	TakeoffScript takeOff;

	public Vector2 forceAmount;
	public Vector2 liftAmount;

	float rotationAmount = 0.5f;
	float rotationLimit1 = 45;
	float rotationLimit2 = 350;


	float contactCounter = 0;

	float yLimit = 10;

	const float BRAKE_FACTOR = 0.1f;

	bool hasBegun = false;

	// Use this for initialization
	void Start () {


		takeOff = GetComponent<TakeoffScript> ();
		rb = GetComponent<Rigidbody2D> ();
		rb.isKinematic = true;

		Invoke ("StartLevel", 2f);

	}

	// Update is called once per frame
	void Update () {

		//To Center Camera
		Camera.main.transform.position = new Vector3 (transform.position.x, transform.position.y, -1);
		rb.constraints = RigidbodyConstraints2D.None;

//		if (Input.GetKey (KeyCode.Z)) {
//			rb.constraints = RigidbodyConstraints2D.FreezePositionY;
//			GameManager.instance.Fuel -= GameManager.instance.baseFuelCost;
//		} else {
//			rb.constraints = RigidbodyConstraints2D.None;
//		}
//		print(transform.rotation.z);
//		print(transform.rotation.eulerAngles.z);
		if (transform.rotation.eulerAngles.z > rotationLimit1 && transform.rotation.eulerAngles.z < rotationLimit2) {
			Debug.Log ("Rotation limit reached");
			GameManager.instance.StartCoroutine("ReloadOnDeath", "Lost Control");
		}
//		if (transform.rotation.z > rotationLimit) {
//			Debug.Log ("Rotation limit reached");
//			GameManager.instance.StartCoroutine("ReloadOnDeath", "Lost Control");
//		}

		if (transform.position.y > yLimit) {
			GameManager.instance.StartCoroutine("ReloadOnDeath", "Landing Aborted");
		}

		if (Input.GetKey (KeyCode.Space)) {
			rb.AddForce (liftAmount, ForceMode2D.Impulse);
			GameManager.instance.Fuel -= GameManager.instance.baseFuelCost;
		}

		// This code is for checking player velocity to end landing scene when velocity is 0

		float rv;
		rv = rb.velocity.magnitude;
		if (rv > 0) {
			hasBegun = true;	
		}

		if(rv < BRAKE_FACTOR) {
			rb.velocity = new Vector2(0, 0);
		}

//		Debug.Log ("Velocity: " + rv);
		if (Mathf.Approximately (rv, 0)) {
			if (hasBegun) {
				Invoke("EndLandingSequence", 5);
			}
		}

	}
		

	void StartLevel(){
		rb.isKinematic = false;
		rb.AddForce (forceAmount, ForceMode2D.Impulse);
	}
		

	public void OnTriggerStay2D (Collider2D other){


		if (other.tag == "Landing Cue") {
			contactCounter++;
			transform.Rotate (0, 0, rotationAmount);
//			Debug.Log ("Contact counter: " + contactCounter );
//			Debug.Log("Rotation: " + transform.rotation);
		}

	}

	void EndLandingSequence (){

		// This code gets called when velocity is 0 (after scene has begun)
		takeOff.isReady = true;
		Destroy (this);
	}

}
