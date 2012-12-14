#define WORKAROUND
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

/*
Dictionary not compatible with IDictionary ?

Temp/StagingArea/Data/ConvertedDotNetCode/global/DictionaryCoercion.as(17): col: 172 Error: Implicit coercion of a value of type System.Collections.Generic:Dicti
onary$2 to an unrelated type System.Collections:IDictionary.
 
*/
public class DictionaryCoercion {
#if !WORKAROUND
	/* Temp/StagingArea/Data/ConvertedDotNetCode/global/DictionaryCoercion.as(17): col: 196 Error: Implicit coercion of a value of type System.Collections.Generic:Dicti
onary$2 to an unrelated type System.Collections:IDictionary.

                        var $dictionary: IDictionary = new Dictionary$2(Dictionary$2.$Type.Type_MakeGenericType_TypeArray(CLIArrayFactory.NewArrayWithElements(Type.$Type, Type.ForClass(String), Type.ForCla
ss(int)))).Dictionary$2_Constructor();
*/
	
	public void DoSomething() {
		IDictionary dict = new Dictionary<string, int>();
	}
#else
	public void DoSomething() {
		IDictionary<string, int> dict = new Dictionary<string, int>();
	}	
#endif
}
