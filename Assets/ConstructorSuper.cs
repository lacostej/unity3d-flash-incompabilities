#define WORKAROUND
using UnityEngine;
using System.Collections;

public class ConstructorSuper {
#if !WORKAROUND
  /*
   causes

   Temp/StagingArea/Data/ConvertedDotNetCode/global/ConstructorSuper.as(31): col: 10 Error: Call to a possibly undefined method Object_Constructor through a referen
ce with static type System:CLIObject.
  */
    public float[] m_AnimScale = new float[] {0.0f,0.0125f, 0.025f,0.0125f, 0.0f};
#else
    public float[] m_AnimScale;
    public ConstructorSuper() {
    	m_AnimScale = new float[] {0.0f,0.0125f, 0.025f,0.0125f, 0.0f};
    }
#endif
}
