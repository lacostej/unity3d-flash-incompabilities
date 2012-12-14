#define WORKAROUND
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// SortedList not supported
public class SortedListExample {
	void Start () {
#if !WORKAROUND
		SortedList<int, string> unsupported = new SortedList<int, string>();
#else
		// REWRITE YOUR CODE, implement a SortedList
#endif
	}

}
