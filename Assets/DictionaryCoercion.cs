using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/*
Dictionary not compatible with IDictionary ?

Temp/StagingArea/Data/ConvertedDotNetCode/global/DictionaryCoercion.as(17): col: 172 Error: Implicit coercion of a value of type System.Collections.Generic:Dicti
onary$2 to an unrelated type System.Collections:IDictionary.
 
*/
public class DictionaryCoercion {
	public IDictionary DoSomething() {
		return new Dictionary<string, int>();
	}
}
