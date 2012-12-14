using UnityEngine;
using System.Collections;

public class ObjectEquals {

	void Start () {
/*
Temp/StagingArea/Data/ConvertedDotNetCode/global/ObjectEquals.as(16): col: 15 Error: Call to a possibly undefined method Object_Equals_Object_Object through a reference with static type Class.
*/ 
		if (Object.Equals ("1", "1")) {
			Debug.Log ("OK");
		}
	}

	/* Workaround missing Flash function...
	public new static bool Equals(object o1, object o2) {
		if (o1 == null) return o2 == null;
		return o1.Equals(o2);
	}
	*/
}
