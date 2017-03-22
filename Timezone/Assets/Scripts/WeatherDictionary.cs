using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherDictionary : MonoBehaviour {

	public Dictionary<string,string[]> weather = new Dictionary<string, string[]> ();
	string[] thunderCodes = { "1", "3", "4", "37", "38", "39", "45", "47" };
	string[] cloudyCodes = { "26", "27", "28", "29", "30", "44" };
	string[] rainyCode = { "5", "9", "10", "11", "12", "17", "35", "40", "46", "32", "33" };

	void Start(){

		weather.Add("thunder",  thunderCodes);
		weather.Add("cloudy",  cloudyCodes);
		weather.Add("rainy",  rainyCode);

	}

}

