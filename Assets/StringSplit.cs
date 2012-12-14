#define WORKAROUND
using UnityEngine;
using System.Collections;

public class StringSplit {
	void Start() {
		char[] Sep = {'a', 'c'};
#if !WORKAROUND
		/**
Temp/StagingArea/Data/ConvertedDotNetCode/global/StringSplit.as(18): col: 50 Error: Call to a possibly undefined method String_Split_CharArray_Int32 through a re
ference with static type Class.
*/
		string[] res = "abcdef".Split(Sep, 2);
#else
		// REWRITE YOUR CODE!
#endif
	}
}
