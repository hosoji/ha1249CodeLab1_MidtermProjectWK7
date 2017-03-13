using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CityCheckScript : MonoBehaviour {

	public GameObject textUI;
	Text cityName;

	[SerializeField] float timeOverCity = 0f; // Time hovering over a city
	[SerializeField] Image progressImage; // assign in the inspector

	RaycastHit2D rayHit;

	bool overCity = false;

	// Use this for initialization
	void Start () {
		cityName = textUI.GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {

//		Ray2D ray = new Ray2D(transform.position, transform.forward);

		rayHit = Physics2D.Raycast (transform.position, transform.up, 8f);

			 
		if (rayHit.collider != null && rayHit.collider.tag == "City") {
//			Debug.Log ("City in range: " + rayHit.transform.name);
			IdentifyCity ();

				
		} else {
			cityName.text = null;
		}

		if (overCity == true) {
//			Debug.Log ("Plane over City");
			timeOverCity = Mathf.Clamp01 (timeOverCity + Time.deltaTime); //After 1sec this variable will be one

			if (timeOverCity == 1f) {
				SceneManager.LoadScene (1);
				timeOverCity = 0;
			}

		} else{
			timeOverCity = Mathf.Clamp01 (timeOverCity - Time.deltaTime);
		}

		progressImage.fillAmount = timeOverCity; // Update UI image
	}

	public void OnTriggerEnter2D (Collider2D other){

		if (other.tag == "City") {
			Debug.Log ("Land at " + other.name);

			overCity = true;
		}
	}

	public void OnTriggerExit2D (Collider2D other){

		if (other.tag == "City") {
			overCity = false;
		}
	}
	public void IdentifyCity(){

//			Debug.Log ("Close");
			cityName.text = rayHit.transform.name.ToString ();
	}
		
}
