using UnityEngine;
using System.Collections;

public class StringSplit {
	void Start() {
		char[] Sep = {'a', 'c'};
		string[] res = "abcdef".Split(Sep, 2);
	}
}
