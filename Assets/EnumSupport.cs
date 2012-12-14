#define WORKAROUND
using System;
using UnityEngine;
using System.Collections;

public class EnumSupport {
	public enum MyEnum {
		One,Two,Three,
		Max
	}
    public static MyEnum StringToMyEnum(string enumVal, MyEnum defaultValue) {
        MyEnum res = defaultValue;
        if (enumVal != null) {
#if !WORKAROUND
            try {
                if (Enum.IsDefined(typeof(MyEnum), enumVal))
         	       res = (MyEnum) Enum.Parse(typeof(MyEnum), enumVal, true);
            } catch (Exception e) {
                    Debug.LogError("Couldn't convert enum string '" + enumVal + " to MyEnum enum: " + e.Message);
            }
else
            // UNITY_FLASH workaround
            // Enum not well supported in Unity 4.0.0
            for (int i = 0; i < (int) MyEnum.Max; i++) {
                MyEnum L = (MyEnum) i;
                if (L.ToString() == enumVal) {
                    res = L;
                    break;
                }
            }
#endif
        }       
        return res;
    }
	
	public static void Main() {
		Assert.AssertEquals(MyEnum.One, StringToMyEnum("One", MyEnum.Two), "first found");
		Assert.AssertEquals(MyEnum.Two, StringToMyEnum("Two", MyEnum.One), "other one found");
		Assert.AssertEquals(MyEnum.Two, StringToMyEnum("Max", MyEnum.Two), "Max ignored");
	}
}
