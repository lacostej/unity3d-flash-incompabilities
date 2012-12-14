#define WORKAROUND
using UnityEngine;
using System.Collections;

public class ObjectEquals {

	void Start () {
#if !WORKAROUND
/*
Temp/StagingArea/Data/ConvertedDotNetCode/global/ObjectEquals.as(16): col: 15 Error: Call to a possibly undefined method Object_Equals_Object_Object through a reference with static type Class.
*/ 
		bool b = Object.Equals ("1", "1");
#else
		bool b = MyEquals ("1", "1");		
#endif
	}

	/* Workaround missing Flash function... */
	public static bool MyEquals(object o1, object o2) {
		if (o1 == null) return o2 == null;
		return o1.Equals(o2);
	}
}
