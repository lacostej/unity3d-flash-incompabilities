using UnityEngine;
using System.Collections;

public class Navback : MonoBehaviour {
	
	void OnGUI() {
		if(GUI.Button(new Rect(0,0,20,20), "<" )) {
			Application.LoadLevel(0);
		}
	}
}
