using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestinationScript : MonoBehaviour {


	public int refuelFactor;

	public GameObject canvas;
	Text cityName;

	// Use this for initialization
	void Start () {
		cityName = canvas.GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {

	}

	public void OnTriggerEnter2D (Collider2D other){

		if (other.tag == "Player") {
			cityName.text = gameObject.name.ToString ();
			GameManager.instance.Fuel += refuelFactor;
			Destroy (gameObject);
		}
	}

}
