using UnityEngine;
using System.Collections;

public class ObjectEquals {

	void Start () {
		if (Object.Equals ("1", "1")) {
			Debug.Log ("OK");
		}
	}

}
