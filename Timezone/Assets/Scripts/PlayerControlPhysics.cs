using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlPhysics : MonoBehaviour {

	Rigidbody2D rb;

	[SerializeField] ParticleSystem myPSystem;

	public float speed;
	public float rotationSpeed;


	public KeyCode upKey;
	public KeyCode downKey;
	public KeyCode rightKey;
	public KeyCode leftKey;

	const float BRAKE_FACTOR = 0.15f;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
//		myPSystem = gameObject.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {



		if (GameManager.instance.Fuel > 0) {
			if (Input.GetKey (upKey)) {
				MovePlayer (Vector2.up);
			} else if (Input.GetKey (downKey)) {
				MovePlayer (Vector2.down);
			} else if (Input.GetKey (leftKey)) {
				MovePlayer (Vector2.left);
			} else if (Input.GetKey (rightKey)) {
				MovePlayer (Vector2.right);

			} 

		// Brings player to a complete stop when speed drop below a certain level
		float rv;
			rv = rb.velocity.magnitude;
		if(rv < BRAKE_FACTOR) {
				rb.velocity = new Vector2(0, 0);
			}
			
		}
	}

	void MovePlayer(Vector3 dir){

		myPSystem.Play ();

		rb.AddForce (dir * Time.deltaTime * speed, ForceMode2D.Force);
		
		// Rotates plane to towards input direction 
		float zAngle = Mathf.Atan2(dir.x,dir.y) * Mathf.Rad2Deg * -1;
		transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, zAngle),Time.deltaTime * rotationSpeed);

		// Depletes Fuel
		GameManager.instance.Fuel -= GameManager.instance.baseFuelCost;
			
	}
}
