using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TakeoffScript : MonoBehaviour {

	public bool isReady = false;

	SpriteRenderer sr;

	Rigidbody2D rb;

	public float thrust;
	public float lift;


	// Use this for initialization
	void Start () {

		sr = gameObject.GetComponent<SpriteRenderer> ();

		rb = gameObject.GetComponent<Rigidbody2D> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 playerScreenPos = Camera.main.WorldToViewportPoint (transform.position);

		if (playerScreenPos.x < -1) {
			SceneManager.LoadScene (0);
		}

		if (isReady) {
			if (!sr.flipX) {
				sr.flipX = true;
			}

			if (Input.GetKey(KeyCode.Space)){
				rb.AddForce(-transform.right * thrust, ForceMode2D.Force);
				rb.AddForce(transform.up * lift, ForceMode2D.Force);

				GameManager.instance.Fuel -= GameManager.instance.baseFuelCost;


			}
		}
		
//			rb.constraints = RigidbodyConstraints2D.FreezePositionY;
	}
}
