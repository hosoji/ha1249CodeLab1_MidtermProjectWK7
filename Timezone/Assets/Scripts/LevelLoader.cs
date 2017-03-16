using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

	public float offsetX = 0;
	public float offsetY = 0;

	public string[] fileNames;
	public static int levelNum;

	// initializatize level
	void Start () {

		levelNum = Random.Range (0, 2);
//		levelNum = 0;
		string fileName = fileNames[levelNum];

		string filePath = Application.dataPath +"/"+ fileName;

//		TextAsset textFile = (TextAsset)Resources.Load(fileName) as TextAsset; 

		StreamReader sr = new StreamReader(filePath);

		GameObject levelHolder = new GameObject("Level Holder");

		int yPos = 0;


		while(!sr.EndOfStream){
			string line = sr.ReadLine();

			for(int xPos = 0; xPos < line.Length; xPos++){

				if(line[xPos] == 'X'){
					GameObject platform = Instantiate(Resources.Load("AirportDay") as GameObject);

					platform.transform.parent = levelHolder.transform;

					platform.transform.position = new Vector3(xPos + offsetX, yPos + offsetY, 0);
				}
				if (line [xPos] == 'C') {
					GameObject cloud = Instantiate(Resources.Load("Cloud") as GameObject);

					cloud.transform.parent = levelHolder.transform;

					cloud.transform.position = new Vector3(xPos + offsetX, yPos + offsetY, 0);
				}

				if (line [xPos] == 'U') {
					GameObject cloud = Instantiate(Resources.Load("Cloud") as GameObject);

					cloud.transform.parent = levelHolder.transform;

					cloud.transform.position = new Vector3(xPos + offsetX, yPos + offsetY, 0);

					cloud.GetComponent<SpriteRenderer> ().flipY = true;


				}

				if (line [xPos] == 'W') {
					GameObject water = Instantiate(Resources.Load("Water") as GameObject);

					water.transform.parent = levelHolder.transform;

					water.transform.position = new Vector3(xPos + offsetX, yPos + offsetY, 0);



				}
			}
			yPos--;
		}

		sr.Close();
	}
		
}
