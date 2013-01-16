using UnityEngine;
using System.Collections;

public class StringConcatenation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		GUI.Label(new Rect(20, 20, 100, 100), "StringConcat...: ");		
		GUI.Label(new Rect(150, 20, 100, 100), "a" + 'T' + " Equals " + "a" + "T");		
	}
}
