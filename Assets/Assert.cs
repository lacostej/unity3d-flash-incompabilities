using System;

public class Assert
{
	public static Void AssertTrue(bool exp, string msg="") {
		if (!exp)
			throw new Exception("FAILED: " + msg);
	}
	public static Void AssertEquals(object exp, object exp2, string msg="") {
		AssertTrue (ObjectEquals.MyEquals(exp, exp2), msg);
	}
}

