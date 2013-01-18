using UnityEngine;
using System.Collections.Generic;
using MiniJSON;

public class ScMiniJSON : MonoBehaviour {
	string json1 = "{ \"a\": 1, \"b\": 1.2 }";
	string json2 = "{ \"a\": [1,2], \"b\": true }";
	//string json2 = "{\"elt\": \"Key\", \"position\": [103,465] }";
	IDictionary<string, object> parsed1;
	IDictionary<string, object> parsed2;
	IDictionary<string, object> parsed3;
	
	

	public void Start() {
		parsed1 = (IDictionary<string, object>) MiniJSON.Json.Deserialize(json1);
		parsed2 = (IDictionary<string, object>) MiniJSON.Json.Deserialize(json2);
		TextAsset TA = (TextAsset) Resources.Load("Chapter01");
		if (TA==null) throw new System.Exception("Resource not found");
		parsed3 = (IDictionary<string, object>) MiniJSON.Json.Deserialize(TA.text);
	}

	public void OnGUI() {
		GUI.Label(new Rect(20, 20, 100, 100), "JSON...:");
		GUI.Label(new Rect(150, 20, 100, 100), "" + parsed1.Count +  " object(s)");
		GUI.Label(new Rect(20, 40, 100, 100), "a, b");
		GUI.Label(new Rect(150, 40, 100, 400), DetailObject(parsed1, "a") + ", " + DetailObject(parsed1, "b"));
		GUI.Label(new Rect(20, 60, 100, 100), "Reserialize");
		GUI.Label(new Rect(150, 60, 100, 400), MiniJSON.Json.Serialize(parsed1));
		GUI.Label(new Rect(20, 80, 100, 100), "a");
		GUI.Label(new Rect(150, 80, 400, 400), DetailObject(parsed2, "a") + ", " + DetailObject(parsed2, "b"));
		GUI.Label(new Rect(20, 100, 100, 100), "Reserialize");
		GUI.Label(new Rect(150, 100, 400, 100), MiniJSON.Json.Serialize(parsed2));
		GUI.Label(new Rect(150, 120, 400, 400), MiniJSON.Json.Serialize(parsed3));
	}
	
	private string DetailObject(IDictionary<string, object> root, string name) {
		if (root == null) return "null root";
		if (!root.ContainsKey(name)) return "null " + name;
		object o = root[name];
		return o.GetType() + " " + o;
	}
}

