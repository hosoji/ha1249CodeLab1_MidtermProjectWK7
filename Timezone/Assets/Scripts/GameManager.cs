using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.ImageEffects;
using System.Net;
using System.IO;
using SimpleJSON;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	//For access to the Image Effect
	VignetteAndChromaticAberration vignette;

//	WeatherDictionary weatherType;

	public GameObject [] cities;

	public GameObject canvas, canvasFuel, textUI, bg;

	Text myText, fuelText;

	public Text cityName;

	float hrs, mins;

	int num;

	public int days = 0;

	const int HOURS_MAX = 24;
	const int MINS_MAX = 60;

	const int FUEL_MIN = 0;
	public const int FUEL_MAX = 2500;

	public int baseFuelCost;
	public int baseRefuel;

	public int fuelStart;

	private int fuel;

	public int Fuel{
		get{
			return fuel;
		}

		set{
			fuel = value;

			if(fuel > FUEL_MAX){
				fuel = FUEL_MAX;
			}

			if(fuel < FUEL_MIN){
				fuel = FUEL_MIN;
			}
		}
	}

	//For having different time passage in different scenes
	int timeMod = 5;

	// Arrays of weather codes from Yahoo API
	string[] thunderCodes = { "1", "3", "4", "37", "38", "39", "45", "47" };
	string[] cloudyCodes = { "26", "27", "28", "29", "30", "44" };
	string[] rainyCodes = { "5", "9", "10", "11", "12", "17", "35", "40", "46", "32", "33" };


	// Use this for initialization
	void Start () {

		if(instance == null){
			instance = this;
			DontDestroyOnLoad(this);
		} else {
			Destroy(gameObject);
		}



		cityName = textUI.GetComponent<Text> ();
		bg.SetActive(false);
	
		myText = canvas.GetComponent<Text> ();
		fuelText = canvasFuel.GetComponent<Text> ();

		hrs = 0;
		mins = Time.time;

		Fuel = FUEL_MAX;

		for (int i = 0; i < cities.Length; i++) {
			string c = cities [i].name.ToString ();
			string weather = CheckCityWeather (c);
			GetCityWeather (cities [i], weather);

		}


//		weatherType = GetComponent<WeatherDictionary> ();
			

	}

	// Update is called once per frame
	void Update () {
		Scene currentScene = SceneManager.GetActiveScene();
		string sceneName = currentScene.name;


		if (sceneName == "Main") {
			mins = (mins + Time.deltaTime * (timeMod * 4));
		} else {
			mins = (mins + Time.deltaTime * timeMod);
		}

		if (Fuel == FUEL_MIN) {
			StartCoroutine (ReloadOnDeath("Out of Fuel"));
		}
	

		Debug.Log ("days: " + days);

		if (hrs >= HOURS_MAX) {
			Reset (hrs, 1);
			days++;
		}

		if (mins >= MINS_MAX) {
			hrs++;
			Reset (mins, 0);
		}

		// for Testing, remove when Timezones implemented
		if (Input.GetKeyDown(KeyCode.B)){
			hrs++;
		}
//
//		CurrentTime = Time.time;
		myText.text = hrs.ToString("00") + ":" + mins.ToString("00");

		fuelText.text = fuel.ToString () + "F";
	}



	public void Reset(float time, int i){
		if (time >= MINS_MAX && i == 0) {
			mins = 0;
		}
		if (time >= HOURS_MAX && i > 0) {
			hrs = 0;
		}
	}
		

	public string CheckCityWeather(string city){

//		JSONClass json = new JSONClass();
//		
//		UtilScript.WriteStringToFile (Application.dataPath, "file.json", json.ToString ());


//		string result = UtilScript.ReadStringFromFile (Application.dataPath, "file.json");
//
//		JSONNode readJSON = JSON.Parse (result);
//

		WebClient client = new WebClient ();

		string content = client.DownloadString ("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22" 
												+ city +"%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");

		JSONNode location = JSON.Parse (content);

		string weather = location ["query"] ["results"] ["channel"] ["item"] ["condition"]["code"];

		return weather;
		
		}

	public void GetCityWeather(GameObject city, string weather ){


//				Debug.Log (city.name + ": " + weather);

		for ( int i =0; i < thunderCodes.Length; i++){
			if (weather == thunderCodes [i]) {
				LoadCityWeather (city, "ThunderClouds", "ThunderCondition", 3);
			}
		}

		for ( int i =0; i < cloudyCodes.Length; i++){
			if (weather == cloudyCodes [i]) {
				LoadCityWeather (city, "CloudSprite", "CloudBase", 1);
			}
		}

		for ( int i =0; i < rainyCodes.Length; i++){
			if (weather == rainyCodes [i]) {
				LoadCityWeather (city, "RainCondition", "CloudBase", 2);
			}
		}
			
	}

	public void LoadCityWeather(GameObject city, string typeName, string conditionName, int amount ){

		GameObject conditionObject = Instantiate (Resources.Load (conditionName) as GameObject);
		conditionObject.transform.position = city.transform.position;

		for (int i = 0; i < amount; i++) {
			GameObject type = Instantiate (Resources.Load (typeName) as GameObject);
			type.transform.parent = conditionObject.transform;
			Vector2 original = conditionObject.transform.position;
			type.transform.position = original + Random.insideUnitCircle * 4;

		}

	}
		

	public IEnumerator ReloadOnDeath(string reason){

		cityName.text = reason;

		float fadeFactor = 0.01f;

		vignette =  Camera.main.GetComponent<VignetteAndChromaticAberration> ();
		vignette.intensity = Mathf.Clamp01(vignette.intensity += fadeFactor);

		yield return new WaitForSeconds(2f);

		SceneManager.LoadScene (0);
		Destroy (gameObject);
	}
}
