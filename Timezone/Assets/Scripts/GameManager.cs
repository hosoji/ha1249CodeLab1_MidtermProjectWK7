using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Net;
using SimpleJSON;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	public GameObject [] cities;

	public GameObject canvas;
	public GameObject canvasFuel;
	public GameObject textUI;
	public GameObject bg;

	Text myText;
	Text fuelText;
	public Text cityName;

	float hrs;
	float mins;

	int num;

	public int days = 0;

	const int HOURS_MAX = 24;
	const int MINS_MAX = 60;


	const int FUEL_MIN = 0;
	public const int FUEL_MAX = 1500;

	public int baseFuelCost;

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

	int timeMod = 5;

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


//		hourVar = System.DateTime.Now.ToString("HH:");
//		string t = System.DateTime.Now.AddHours (2).ToString ("HH:mm");
//		string m = System.DateTime.Now.AddHours (10).ToString ("HH:mm");

	}

	// Update is called once per frame
	void Update () {
		Scene currentScene = SceneManager.GetActiveScene();
		string sceneName = currentScene.name;

//		hrs = (hrs + Time.deltaTime/100);
		if (sceneName == "Main") {
			mins = (mins + Time.deltaTime * (timeMod * 4));
		} else {
			mins = (mins + Time.deltaTime * timeMod);
		}

		if (Fuel == FUEL_MIN) {
			SceneManager.LoadScene (0);
			Destroy (gameObject);
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

		JSONClass json = new JSONClass();
		
		UtilScript.WriteStringToFile (Application.dataPath, "file.json", json.ToString ());


		string result = UtilScript.ReadStringFromFile (Application.dataPath, "file.json");

		JSONNode readJSON = JSON.Parse (result);


		WebClient client = new WebClient ();

		string content = client.DownloadString ("https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20weather.forecast%20where%20woeid%20in%20(select%20woeid%20from%20geo.places(1)%20where%20text%3D%22" 
												+ city +"%22)&format=json&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys");

		JSONNode location = JSON.Parse (content);

		string weather = location ["query"] ["results"] ["channel"] ["item"] ["condition"]["code"];

		return weather;
		
		}

	public void GetCityWeather(GameObject city, string weather ){

		string[] thunder = {"1", "3", "4", "37", "38", "39", "45", "47"};
		string[] cloudy = {"26", "27", "28", "29", "30", "44"}; 
		string[] rainy = { "5", "9", "10", "11", "12", "17", "35", "40", "46", "32", "33"};
//		Debug.Log (city.name + ": " + weather);

		for ( int i =0; i < thunder.Length; i++){
			if (weather == thunder [i]) {
				UtilScript.LoadCityWeather (city, "ThunderClouds", "ThunderCondition", 3);
			}
		}

		for ( int i =0; i < cloudy.Length; i++){
			if (weather == cloudy [i]) {
				UtilScript.LoadCityWeather (city, "CloudSprite", "CloudBase", 1);
			}
		}

		for ( int i =0; i < rainy.Length; i++){
			if (weather == rainy [i]) {
				UtilScript.LoadCityWeather (city, "RainCondition", "CloudBase", 2);
			}
		}
			
	}
}
