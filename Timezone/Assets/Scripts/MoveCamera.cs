using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

	public Transform player;

	public float maxUp, maxDown, maxLeft, maxRight;

	public float speed;

	public float zoneUp, zoneDown, zoneLeft, zoneRight;

	// Use this for initialization
	void Start () {

		Camera.main.transform.position = new Vector3 (player.position.x, player.position.y, Camera.main.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector2 playerScreenPos = Camera.main.WorldToViewportPoint (player.position);

		if (playerScreenPos.x <= zoneLeft) {
			Camera.main.transform.Translate (Vector2.left * speed * Time.deltaTime);
		}
		if (playerScreenPos.x >= zoneRight) {
			Camera.main.transform.Translate (Vector2.right * speed * Time.deltaTime);
		}
		if (playerScreenPos.y <= zoneDown) {
			Camera.main.transform.Translate (Vector2.down * speed * Time.deltaTime);
		}
		if (playerScreenPos.y >= zoneUp) {
			Camera.main.transform.Translate (Vector2.up * speed * Time.deltaTime);
		}




//		Debug.Log("Player is " + playerScreenPos.x + " from the left");
//		Debug.Log("Player is " + playerScreenPos.y + " from the bottom");	

		
	}
}
