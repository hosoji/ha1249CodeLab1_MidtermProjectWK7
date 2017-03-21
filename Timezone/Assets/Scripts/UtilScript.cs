using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleJSON;
using UnityEngine.SceneManagement;

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
		


	public static void SaveTransformPosition(Transform t, string path, string name) {

		const char DELIMITER = '|';

		Vector3 transformPos = t.position;
		string content = "" + transformPos.x + DELIMITER + transformPos.y + DELIMITER + transformPos.z; 

		WriteStringToFile (path, name, content);
	}
		


}
