//using System;
using System.Collections.Generic;
using UnityEngine;

public class StringUtil {
	
	public StringUtil () {
	}
	
	public static string RemoveAllWhite(string _str) {
		string dst = "";
		foreach(char c in _str) {
			if(c!=' ' || c!='\t' || c!='\n')
				dst += c;
		}
		return dst;
	}

		
	public static bool FindBracketBloc(ref string _before, ref string _inside, ref string _after, string _str) {
		int i;
		i = _str.IndexOf('(');
		if(i==-1)
			return false;
		_before = _str.Substring(0,i);
		i++;
		int level = 1;
		_inside = "";
		for(;i<_str.Length;i++) {
			if(_str[i]=='(') 
				level++;
			if(_str[i]==')') {
				level--;
				if(level==0)
				   break;
			}
			_inside += _str[i];
		}
		i++;
		_after = _str.Substring(i);
		return true;
	}
	
	
	public static List<string> FindParam(string _str) {
		List<string> ldst = new List<string>();
		string dst;
		int level = 0;
		int iprec = 0;
		for(int i=0;i<_str.Length;i++) {
			if(_str[i]=='(') 
				level++;
			if(_str[i]==')') {
				level--;
			}
			if(_str[i]==';' && level==0) {
				dst = _str.Substring(iprec,i-iprec);
				ldst.Add(dst);
				iprec = i+1;
			}
		}
		dst = _str.Substring(iprec,_str.Length-iprec);
		ldst.Add(dst);
		
		return ldst;
	}
}

