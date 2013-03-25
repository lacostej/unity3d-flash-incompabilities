using UnityEngine;

public class ScStart : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI() {
		for (int i = 1; i <= 6; i++) {
			if(GUI.Button(new Rect(20,20*i,80,20), "" + i)) {
				Application.LoadLevel(i);
			}
		}
	}
}
