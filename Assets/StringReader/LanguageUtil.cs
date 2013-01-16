using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class LanguageUtil : MonoBehaviour {
	Dictionary<string,string> dict;
	
	public void Start() {
		string text = "a=b\nc=d";
		dict = TranslationParser.parse(text);
	}
	public void OnGUI() {
		GUI.Label(new Rect(20, 20, 100, 100), "LanguageUtil...: ");
		GUI.Label(new Rect(150, 20, 100, 100), dict["a"] + " & " + dict["c"]);
	}
}

/** A simple sort of java properties file parser. We only support
  * single lines, some comments (prefix with # or //)
  **/
public class TranslationParser {

	internal static Dictionary<string, string> parse(string text) {
		Dictionary<string, string> data = new Dictionary<string, string>();
		StringReader strReader = new StringReader(text); // THIS FAILS with
/* TypeError: Error #1009: Cannot access a property or method of a null object reference.
        at global$init()
        at Function/<anonymous>()
        at System.IO::TextReader$cinit()
        at global$init()
        at global$init()
        at global::TranslationParser$/TranslationParser_parse_String()[/Users/lacostej/Code/WWTK/FlashPortProblems/Temp/StagingArea/Data/ConvertedDotNetCode/global/TranslationParser.as:23]
        at global::LanguageUtil/LanguageUtil_Start()[/Users/lacostej/Code/WWTK/FlashPortProblems/Temp/StagingArea/Data/ConvertedDotNetCode/global/LanguageUtil.as:26]
*/
		while(true)
		{
    		string aLine = strReader.ReadLine();
    		if (aLine == null) break;
			if (aLine.StartsWith("#") || aLine.StartsWith("//")) continue;
			if (aLine.Trim().Length == 0) continue;
			string[] r = SplitLine(aLine);
			if (r.Length != 2) {
				Debug.Log ("Failed to parse <" + aLine + ">");
				continue;	
			}
			
			data.Add(r[0], r[1].Replace("\\n", "\n"));
		}
		return data;
	}

	internal static string[] SplitLine(string str) {
       // UNITY_FLASH
		// until String.Split support fixed...
		int idx = str.IndexOf('=');
		if (idx == -1) return new string[] {};

		string[] res = new string[2];
		res[0] = str.Substring(0, idx);
		res[1] = str.Substring(idx+1);
		return res;
        /*
		char[] sep = new char[] {'='};
		return str.Split (sep, 2);	
		*/
	}
}
