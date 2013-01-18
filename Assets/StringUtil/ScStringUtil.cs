using UnityEngine;
using System.Collections.Generic;
using MiniJSON;

public class ScStringUtil : MonoBehaviour {
	string s1 = " a \n b \n c";
	string s2 = "div(mult(KeyT;C4T);mult(C4T;C2T))";
	string p1;
    string before ="";
    string inside ="";
    string after ="";
	bool b;
	List<string> insideParams;

	public void Start() {
		p1 = StringUtil.RemoveAllWhite(s1);
		
		b = StringUtil.FindBracketBloc(ref before, ref inside, ref after, s2);
		insideParams = StringUtil.FindParam(inside);
	}

	public void OnGUI() {
		GUI.Label(new Rect(20, 20, 400, 100), "Whited...:");
		GUI.Label(new Rect(150, 20, 400, 100), "<" + p1 + ">");
		GUI.Label(new Rect(150, 40, 400, 100), "<" + b + "," + before + "_:_" + inside +"_:_" + after + ">");
		for(int i = 0; i < insideParams.Count; i++) {
			GUI.Label(new Rect(150, 40 + (i+1)*20, 400, 100), "PARAM[" + i + "]" + insideParams[i] + ">");	
		}
	}
}

