using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class DictionaryCoercion {
	public IDictionary DoSomething() {
		return new Dictionary<string, int>();
	}
}
