//#define SHOW_COMPILATION_FAILURE
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SortedListExample {
	void Start () {
#if SHOW_COMPILATION_FAILURE
		SortedList<int, string> unsupported = new SortedList<int, string>();
#endif
	}

}
