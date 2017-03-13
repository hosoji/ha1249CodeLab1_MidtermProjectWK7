using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructuresExampleScript : MonoBehaviour {

	List<string> phrases = new List<string>();

	public List<string> names; // Can be made public and used in the inspector like an array 

	public Queue<string> waitInLine = new Queue<string>(); // Does not show up in inspector

	public Stack<string> stack = new Stack<string> (); // Does not show up in inspector

	public Dictionary<string,string> websters = new Dictionary<string, string> ();

	// Use this for initialization
	void Start () {

		//list
		names.Add ("at");
		names.Add ("teaching"); // add to end of list


		names.Insert (2, "not");

		names.Remove ("Not");
		names.RemoveAt (0);

		string[] nameArray = names.ToArray ();

		for (int i = 0; i < names.Count; i++){
			
		}

		foreach(string name in names){
			Debug.Log ("name: " + name);
		}

		//queue

		waitInLine.Enqueue ("red door");
		waitInLine.Enqueue ("blue");
		waitInLine.Enqueue ("green");

		string firstInLine = waitInLine.Dequeue ();
		Debug.Log (firstInLine);

		foreach (string lego in waitInLine) {
			Debug.Log (waitInLine.Dequeue ());
		}

		Debug.Log (waitInLine.Peek ()); // Shows you whats next in a queue without removing

		//stack
		stack.Push("Ace");
		stack.Push("King");
		stack.Push("Queen");

		Debug.Log (stack.Pop ()); // last thing i put in stack (LIFO)

		//Dictionary

		websters.Add ("car", "4 wheel");
		websters.Add ("camel", "horse by commitee");
		websters.Add ("dog", "better than cat");

		Debug.Log (websters ["dog"]);

		foreach (string key in websters.Keys) {
			Debug.Log ("key: " + key + "value: " + websters[key]);
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
