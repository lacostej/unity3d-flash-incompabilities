using UnityEngine;
 

public class  ActionBase  {
	
	
	public virtual void StartAction() {
    }
	
	public virtual  bool DoAction () {
		return true;
	}

	public virtual bool OnGUI() {
		return true;
    }

	public virtual void EndAction() {
    }
	
	public virtual void ToTheEnd()
	{
	}
}
