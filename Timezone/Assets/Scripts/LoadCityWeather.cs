using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCityWeather : MonoBehaviour {

	public GameObject [] cities;

	// Use this for initialization
	void Start () {
		
		for (int i = 0; i < cities.Length; i++) {
			string c = cities [i].name.ToString ();
//			Debug.Log (GameManager.instance.CheckCityWeather (c));

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
