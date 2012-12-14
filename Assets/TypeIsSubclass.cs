#define WORKAROUND

using UnityEngine;
using System;
using System.Collections;

#if WORKAROUND
using UnityEngine.Flash;
#endif

public class TypeIsSubclass {
	class A {
	}
	class B : A {
	}
	void Start() {
		B b = new B();
		IsSubclassOf(b.GetType(), typeof(A));
	}
	
	static bool IsSubclassOf(Type a, Type b) {
#if !WORKAROUND
  /*
Temp/StagingArea/Data/ConvertedDotNetCode/global/TypeIsSubclass.as(20): col: 14 Error: Call to a possibly undefined method _Type_IsSubclassOf_Type through a reference with static type System:Type.
  */
		return b.IsSubclassOf(a);
#else
		return ActionScript.Expression<bool>("$b.Type_IsSubclassOf_Type($a)");
#endif
	}
}
