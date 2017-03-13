using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMovement : MonoBehaviour {

	Vector3 pos;                  	
	public float speed = 2.0f;    	
	public float gridSize = 0.0f;

	public KeyCode upKey;
	public KeyCode downKey;
	public KeyCode leftKey;
	public KeyCode rightKey;

	// Use this for initialization
	void Start () {
		pos = transform.position;   // For getting the initial position
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards (transform.position, pos, Time.deltaTime * speed);    // For moving to position

		SetDestination (upKey, 1, 1, 0, gridSize); 
		SetDestination (downKey, -1, -1, 0, gridSize);
		SetDestination (leftKey, -1, -1, gridSize, 0);
		SetDestination (rightKey, 1, 1, gridSize, 0); 


	}

	public void SetDestination(KeyCode key, int i1, int i2, float u1, float u2){
		if (Input.GetKey (key)) {
			if (GameManager.instance.Fuel > 0) {
				if (transform.position == pos) {
					//Using gridsize (positive or negative) to set point based on what key pressed
					pos = pos + new Vector3 (u1 * i1, u2 * i2, 0);
					//Deplete Fuel on every player movement
//					FuelScript.instance.Fuel -= FuelScript.instance.baseFuelCost;
//					Debug.Log ("Fuel: " + FuelScript.instance.Fuel);
				}
			}
		}
	}
		
}
