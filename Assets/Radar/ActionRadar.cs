using UnityEngine;
using System.Collections.Generic;

public class ActionRadar : ActionBase {
	
	Material m_MaterialMask;
	
	public Texture2D m_TexRadar;
	public bool m_ShowTexRadar = false;
	float m_DrawHeight,m_DrawWidth;
	float m_DistFact;
	
	Vector2 m_Center;
	bool	m_IsOnLeftSide;
	bool	m_OnlyOneSide;
	float	m_AlphaStart = 0.0f;
	float	m_AlphaSpeed = 180.0f;
	float	m_Alpha1;
	
	float m_TimeStart;
	float m_TimeLength = 0.0f;
	Color m_CurrentColor;
	bool m_ColorIsWhite;
	
	private List<AngleBlock> m_LAngleBlock = new List<AngleBlock>();

	private class AngleBlock {
		private float m_Alpha1;
		private float m_Alpha2;
		public float m_Dist;
		
		public void SetFirstAngle(float _a,float _d) {
			m_Alpha1 = m_Alpha2 = _a;
			m_Dist = _d;
		}
		
		public void AddAngle(float _a, float _d) {
			if(_a<m_Alpha1)
				m_Alpha1 = _a;
			if(_a>m_Alpha2)
				m_Alpha2 = _a;
			if(m_Dist>_d)
				m_Dist = _d;
		}
		
		
		public void EndAngle() {
			//On gère le cas où les angles sont proches l'un de l'autre 
			//mais leurs valeurs sont éloignées de 2*PI
			float Diff1 = Mathf.Abs(m_Alpha1 - m_Alpha2);
			float Diff2 = Mathf.Abs(m_Alpha1 + 2*Mathf.PI - m_Alpha2);
			if(Diff2<Diff1) {
				float tmp = m_Alpha2;
				m_Alpha2 = m_Alpha1 + 2*Mathf.PI;
				m_Alpha1 = tmp;
			}
		}
		
		public bool Contains(float _A) {
			if(_A>m_Alpha1 && _A<m_Alpha2)
				return true;
			if((_A+2*Mathf.PI)>m_Alpha1 && (_A+2*Mathf.PI)<m_Alpha2)
				return true;
			
			return false;
		}
	}
	
	private float GetAngle(float X,float Y) {
		return Mathf.Atan2(Y - m_Center.y, X - m_Center.x);
	}

	private float GetDist(float X,float Y) {
		return (new Vector2(Y - m_Center.y, X - m_Center.x)).magnitude;
	}
/*
	public Texture2D CreateTextureByHandAndSaveIt() {
		m_TexRadar = CreateTextureByHand();		
		FileUtil.SaveBytes("Radar.png", m_TexRadar.EncodeToPNG());
		return m_TexRadar;
	}
*/		
	private Texture2D CreateTextureByHand() {
		int i,j, jmax;
		
		Texture2D TexRadar = new Texture2D(150,75,TextureFormat.ARGB32,false);
		Color[] AColor = new Color[TexRadar.width*TexRadar.height];
		Color col = new Color(1,1,1,0);
		Color colW = new Color(1,1,1,1);
		for( i=0;i<TexRadar.width;i++) {
			jmax = TexRadar.height * i / TexRadar.width;
			for( j=0;j<jmax;j++) {
				colW.a = (float) (jmax-j)/jmax;
				AColor[i+j*TexRadar.width] = colW;
			}
			for( j=jmax;j<TexRadar.height;j++) {
				AColor[i+j*TexRadar.width] = col;
			}
		}
		
		TexRadar.SetPixels(AColor);
		TexRadar.Apply(false,false);	
		TexRadar.wrapMode = TextureWrapMode.Clamp;
		TexRadar.filterMode = FilterMode.Bilinear;
		return TexRadar;
	}
	
    public ActionRadar() {
		m_TexRadar = (Texture2D) Resources.Load("FxRadar/Radar");
		m_MaterialMask = (Material) Resources.Load("FxRadar/MatRadar");
		m_TimeLength = 360.0f/m_AlphaSpeed + 0.5f;	
		m_DrawHeight = m_TexRadar.height*3;
		m_DrawWidth = m_TexRadar.width*3;
		m_DistFact = 0.0f;
	}
	
	public void SetCenter(Vector2 _Center) {
		m_Center = _Center;
		m_IsOnLeftSide = true; /*MthBoard.m_Instance.IsInBoardLeft(_Center);*/
		m_OnlyOneSide = true; /* MthBoard.m_Instance.m_DisplayLeftSideOnly;*/
		m_LAngleBlock.Clear();
	}
	

	public void SetLBlock(List<Rect> _LRect) {
		AngleBlock AB;
		foreach(Rect r in _LRect) {
			AB = new AngleBlock();
			AB.SetFirstAngle(GetAngle(r.x,r.y),				GetDist(r.x,r.y));
			AB.AddAngle(GetAngle(r.x+r.width,r.y),			GetDist(r.x+r.width,r.y));
			AB.AddAngle(GetAngle(r.x,r.y+r.height),			GetDist(r.x,r.y+r.height));
			AB.AddAngle(GetAngle(r.x+r.width,r.y+r.height),	GetDist(r.x+r.width,r.y+r.height));
			AB.EndAngle();
			m_LAngleBlock.Add(AB);
		}
	}
	
	public override void StartAction() {
		m_TimeStart = Time.time;
		m_ShowTexRadar = true;		
		m_Alpha1 = m_AlphaStart;
		m_MaterialMask.SetColor("_Color",new Color(1,1,1,1));
		m_ColorIsWhite = true;
		if(m_OnlyOneSide) {
			m_MaterialMask.SetTexture("_MaskTex",(Texture2D) ResourcesLoad("Images/fond/guideLandR4"));
		} else {
		if(m_IsOnLeftSide) {
			m_MaterialMask.SetTexture("_MaskTex",(Texture2D) ResourcesLoad("Images/fond/guideL4"));
		} else {
			m_MaterialMask.SetTexture("_MaskTex",(Texture2D) ResourcesLoad("Images/fond/guideR4"));
		}}
		//GOMusicMgr.PlayClip2D("Sons/SndEff_ParticleWizz",1.0f);
   }

   static object ResourcesLoad(string _R) {
   		object R = Resources.Load(_R);
   		if (R == null)
   			throw new System.Exception("Couldn't load " + _R);
   		return R;
	}
	

	
	public override bool DoAction() {
		if(Time.time>m_TimeStart + m_TimeLength) {
			EndAction();
			return false;
		}
		return true;
	}
	
//	static int firstPass = -1;
	public override bool OnGUI() {
//		if((firstPass--) >0)
//			return false;
		
		if(!m_ShowTexRadar)
		{
			return false;
		}

		if (!Event.current.type.Equals(EventType.Repaint)) {
			return false;
		}
		float dt = Time.time - m_TimeStart;

		m_Alpha1 = m_AlphaStart + m_AlphaSpeed*dt;
		
		bool colShouldBeWhite = true;
		float aRadian = m_Alpha1*Mathf.Deg2Rad;
		float Dist = m_DrawHeight;
		if(aRadian>Mathf.PI)
			aRadian -= 2*Mathf.PI;
		foreach(AngleBlock AB in m_LAngleBlock) {
			if(AB.Contains(aRadian)) {
				colShouldBeWhite = false;
				if(AB.m_Dist<Dist)
					Dist = AB.m_Dist;
//				break;
			}
		}
		if(m_ColorIsWhite != colShouldBeWhite) {
			if(colShouldBeWhite)
				m_MaterialMask.SetColor("_Color",new Color(1,1,1,1));
			else {
//				ScBase.PlayClickButtonSound();
				m_MaterialMask.SetColor("_Color",new Color(1,0.2f,0.2f,1));
			}
			m_ColorIsWhite = colShouldBeWhite;
		}
		
//		if(dt<0.2f) {
//			col.a = dt/0.2f;
//		} else {
//		if(dt>m_TimeLength-0.2f) {
//			col.a = (m_TimeLength - dt)/0.2f;
//		} else {
////			m_MaterialMask.SetColor("_Color",new Color(1,1,1,1));
//		}}
//		m_MaterialMask.SetColor("_Color",col);
		
		float f0 = Dist/(m_DrawWidth);
		float df = Mathf.Min(Time.deltaTime*30,0.5f);
		m_DistFact = f0*df + m_DistFact*(1.0f-df);
		float Larg = m_DrawHeight*m_DistFact;
		float Length = m_DrawWidth*m_DistFact;
		Rect RRadar = new Rect(m_Center.x, m_Center.y-Larg, Length, Larg);
		Matrix4x4 matrixBackup = GUI.matrix;
		GUIUtility.RotateAroundPivot(m_Alpha1, GUIUtil.AdaptToScreen(m_Center));
		Graphics.DrawTexture(GUIUtil.AdaptToScreenR(RRadar),m_TexRadar,m_MaterialMask);
		GUI.matrix = matrixBackup;

		return true;
	}
	
	public override void EndAction() {
		if (!m_ShowTexRadar) return;
		m_MaterialMask.SetTexture("_MaskTex",(Texture2D) Resources.Load("Images/fond/guideR4"));
		m_ShowTexRadar = false;
		m_MaterialMask = null;
		Resources.UnloadAsset(m_TexRadar);
		m_TexRadar = null;
	}

	
}









