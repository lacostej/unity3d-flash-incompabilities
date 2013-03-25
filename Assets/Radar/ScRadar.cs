using UnityEngine;
using System.Collections.Generic;

public class ScRadar : MonoBehaviour {
	List<Rect> m_LR;
	ActionRadar m_ActionRadar = null;
	bool m_ActionRadarIsFinished;

	public void Start() {
		m_ActionRadar = new ActionRadar();
		Vector2 centerCard = new Vector2(300, 300);
		m_ActionRadar.SetCenter(centerCard);
        m_LR = new List<Rect>();
        m_LR.Add(new Rect(400, 400, 50, 50));
        m_ActionRadar.SetLBlock(m_LR);
        m_ActionRadar.StartAction();
    }

	public void OnDestroy() {
		if (m_ActionRadar != null) {
			m_ActionRadar.EndAction();
			m_ActionRadar = null;
		}
	}
	
	public void Reset() {
	}

	void OnEnable()
	{
	}
 
	void OnDisable()
	{
	}

	void Update() {
 		if(!m_ActionRadarIsFinished && m_ActionRadar!=null) {
        	m_ActionRadarIsFinished = !m_ActionRadar.DoAction();
        }
	}

	public void OnGUI() {
		if(m_ActionRadar != null  && !m_ActionRadarIsFinished) {
        	m_ActionRadar.OnGUI();
        }
	}
}
