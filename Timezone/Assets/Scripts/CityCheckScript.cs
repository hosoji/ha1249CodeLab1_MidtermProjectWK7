using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CityCheckScript : MonoBehaviour {

	public string fileName;

	RaycastHit2D rayHit;

	[SerializeField] float timeOverCity = 0f; // Time hovering over a city
	[SerializeField] Image progressImage; // assign in the inspector

	bool overCity = false;

	private const float DIST_RAY = 8f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {


		rayHit = Physics2D.Raycast (transform.position, transform.up, DIST_RAY);

			 
		if (rayHit.collider != null && rayHit.collider.tag == "City") {
//			Debug.Log ("City in range: " + rayHit.transform.name);
			if (!overCity) {
				IdentifyCity ();
				GameManager.instance.bg.SetActive (true);
			}   //Enables color area for text box background

				
		} else {
			if (!overCity) {
				GameManager.instance.cityName.text = null;
				GameManager.instance.bg.SetActive (false);
			}
		}

		if (overCity == true) {
			timeOverCity = Mathf.Clamp01 (timeOverCity + Time.deltaTime); //After 1sec this variable will be one

			if (timeOverCity == 1f) {
				UtilScript.SaveTransformPosition (this.transform, Application.dataPath, fileName);
				SceneManager.LoadScene (1);
				timeOverCity = 0;
			}

		} else {
			timeOverCity = Mathf.Clamp01 (timeOverCity - Time.deltaTime);
		}

		progressImage.fillAmount = timeOverCity; // Update UI image
	}

	public void OnTriggerEnter2D (Collider2D other){

		if (other.tag == "City") {
			GameManager.instance.cityName.text = other.name.ToString ();
			GameManager.instance.bg.SetActive (true);
			overCity = true;

			Debug.Log ("Land at " + other.name);
		}
	}

	public void OnTriggerExit2D (Collider2D other){

		if (other.tag == "City") {
			overCity = false;
		}
	}
	public void IdentifyCity(){

		GameManager.instance.cityName.text = rayHit.transform.name.ToString ();
			
	}
		
}
