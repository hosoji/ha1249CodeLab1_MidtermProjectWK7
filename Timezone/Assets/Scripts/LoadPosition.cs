using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadPosition : MonoBehaviour {

	public string fileName;

	string filePath;

	const char DELIMITER = '|';

	// Use this for initialization
	void Start () {

		float offSet = 2f;

		filePath = Application.dataPath + "/" + fileName;

		if (File.Exists(filePath)){
			string line = UtilScript.ReadStringFromFile (Application.dataPath, fileName);

			string[] splitLine = line.Split (DELIMITER);

			transform.position = new Vector3 (
				float.Parse(splitLine [0]) + offSet, 
				float.Parse(splitLine [1])+ offSet, 
				float.Parse(splitLine [2]));
		}
	}


}
