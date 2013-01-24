using UnityEngine;

public class ScCustomLog : MonoBehaviour {
	
	string msg;
	string stacktrace;

	public void Start() {
		Reset ();
	}

	public void OnDestroy() {
	}
	
	public void Reset() {
		msg = "";
		stacktrace = "";		
	}

	void OnEnable()
	{
		Application.RegisterLogCallback(logHandler);
	}
 
	void OnDisable()
	{
		Application.RegisterLogCallback(null);
	}

	private void logHandler(string logString, string stackTrace, LogType logType)
	{
		this.msg = logType.ToString() + "-" + logString;
		this.stacktrace = stackTrace;
	}

	public void OnGUI() {
		GUI.Label(new Rect(20, 20, 100, 30), "Log...: ");
		if (GUI.Button(new Rect(150, 20, 100, 20), "CLICK")) {
			Reset();
			Debug.Log("Message...");
		}
		GUI.Label(new Rect(20, 40, 100, 30), "Error...: ");
		if (GUI.Button(new Rect(150, 40, 100, 20), "CLICK")) {
			Reset();
			Debug.LogError("ErrorMessage...");
		}
		GUI.Label(new Rect(20, 60, 100, 30), "Exception...: ");
		if (GUI.Button(new Rect(150, 60, 100, 20), "CLICK")) {
			Reset();
			throw new System.Exception("Simulated Exception in OnGUI()");
		}
		GUI.Label(new Rect(20, 80, 100, 30), "Direct...: ");
		if (GUI.Button(new Rect(150, 80, 100, 20), "CLICK")) {
			Reset();
			logHandler("Hei", "MyStack()\nTheOtherStack()", LogType.Error);
		}
		GUI.Label(new Rect(20, 100, 100, 30), "JSON...: ");
		GUI.Label(new Rect(150, 100, 500, 30), msg);
		GUI.Label(new Rect(20, 120, 100, 30), "Stack...: ");
		GUI.Label(new Rect(150, 120, 500, 500), stacktrace);
	}
}
