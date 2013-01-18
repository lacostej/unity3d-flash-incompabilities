//using System;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class StringUtil {
	
	public StringUtil () {
	}
	
	public static string RemoveAllWhite(string _str) {
		if (_str == null) return null;
		StringBuilder dst = new StringBuilder(_str.Length);
		foreach(char c in _str) {
			if(c!=' ' && c!='\t' && c!='\n')
				dst.Append(c.ToString());
		}
		return dst.ToString();
	}

		
	public static bool FindBracketBloc(ref string _before, ref string _inside, ref string _after, string _str) {
		int i;
		i = _str.IndexOf('(');
		if(i==-1)
			return false;
		_before = _str.Substring(0,i);
		i++;
		StringBuilder insideBuffer = new StringBuilder();
		int level = 1;
		for(;i<_str.Length;i++) {
			if(_str[i]=='(') 
				level++;
			if(_str[i]==')') {
				level--;
				if(level==0)
				   break;
			}
			insideBuffer.Append(_str[i].ToString());
		}
		i++;
		_inside = insideBuffer.ToString();
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

