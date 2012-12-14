#define WORKAROUND
using UnityEngine;
using System.Collections;

public class StringCompare {
	void Start() {
#if !WORKAROUND		
/**
 * causes
 * Temp/StagingArea/Data/ConvertedDotNetCode/global/StringCompare.as(16): col: 37 Error: Call to a possibly undefined method String_Compare_String_String_Boolean th
rough a reference with static type Class.

                        var $num: int = StringOperations.String_Compare_String_String_Boolean("abc", "b", false);
*/
		bool b = string.Compare("abc", "b", true) == 0;
#else
		bool b = EqualsStringIgnoreCase("abc", "b");
#endif
	}

   static bool EqualsStringIgnoreCase(string s1, string s2) {
           if (s1 == null) return (s2 == null);
           if (s2 == null) return (s1 == null);
           return s1.ToLower().Equals(s2.ToLower());
   }
}
