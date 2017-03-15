using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;

public class UtilScript : MonoBehaviour {

	public static void WriteJSONtoFile(string path, string fileName, JSONClass json){
		WriteStringToFile (path, fileName, json.ToString ());
	}

	public static void WriteStringToFile ( string path, string fileName, string content){
		StreamWriter sw = new StreamWriter (path + "/" + fileName);

		sw.Write (content);

		sw.Close ();
	}

	public static JSONNode ReadJSOnfromFile(string path, string fileName){
		return JSON.Parse(ReadStringFromFile (path, fileName));
	}

	public static string ReadStringFromFile(string path, string fileName){
		StreamReader sr = new StreamReader (path + "/" + fileName);

		string result = sr.ReadToEnd ();

		sr.Close ();

		return result;
	}

	public static Vector3 CloneVector3(Vector3 vec){
		return new Vector3 (vec.x, vec.y, vec.z);
	}

	public static void LoadCityWeather(GameObject city, string typeName, string conditionName, int amount ){

		GameObject conditionObject = Instantiate (Resources.Load (conditionName) as GameObject);
		conditionObject.transform.position = city.transform.position;

		for (int i = 0; i < amount; i++) {
			GameObject type = Instantiate (Resources.Load (typeName) as GameObject);
			type.transform.parent = conditionObject.transform;
			Vector2 original = conditionObject.transform.position;
			type.transform.position = original + Random.insideUnitCircle * 4;

		}
			
	}

	public static void SaveTransformPosition(Transform t, string path, string name) {

		const char DELIMITER = '|';

		Vector3 transformPos = t.position;
		string content = "" + transformPos.x + DELIMITER + transformPos.y + DELIMITER + transformPos.z; 

		WriteStringToFile (path, name, content);
	}


}
