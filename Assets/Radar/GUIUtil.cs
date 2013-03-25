using UnityEngine;


public class SpriteFont {
	public  Vector2 MeasureString(string _S) {return GUIUtil.MeasureString(_S); }
}

public class GUIUtil
{
	static public Vector2 IpadScreen = new Vector2(1024,768);
	private static readonly float IpadScreenRatio =  IpadScreen.x * 1.0f / IpadScreen.y;
	
	//private static Rect VisibleScreen = new Rect(33, 1, 608, 380); // hardcoded for other tests
	private static Rect VisibleScreenFullScreen = new Rect(0, 0, Screen.width, Screen.height);
	private static Rect VisibleScreenForRatioKept = CenterToScreenKeepRatio(VisibleScreenFullScreen, IpadScreenRatio);
	
	static public Vector2 MeasureString (string _src)
	{
		return GUI.skin.label.CalcSize (new GUIContent (_src));
	}
	
	static public Rect ExpandRectAtBottom(Rect R) {
		R.y = Screen.height - R.y - R.height;
		R.height += R.height/2;
		return R;
	}

	static public Vector2 DrawTextCentered(string _str) {
		Vector2 strSize;
		strSize = GUIUtil.MeasureString(_str);
		
		Rect R = new Rect();
		R.x = Screen.width/2 - strSize.x/2;
		R.y = Screen.height/2 - strSize.y/2;
		R.width = strSize.x;
		R.height = strSize.y;
		
		GUI.Label(R, _str);
		return strSize;
	}
	static public Vector2 DrawTextXCentered(float y, string _str) {
		Vector2 strSize;
		strSize = GUIUtil.MeasureString(_str);
		
		Rect R = new Rect();
		R.x = Screen.width/2 - strSize.x/2;
		R.y = y;
		// Workaround: GUIUtil.MeasureString gives a size slightly too small and text can disappear. Added some more space in a GUI.Label
		R.width = strSize.x+20;
		R.height = strSize.y+10;
		
		GUI.Label(R, _str);
		return strSize;
	}
	static public Vector2 DrawTextBottomRight(string _str) {
		Vector2 strSize;
		strSize = GUIUtil.MeasureString(_str);
		
		Rect R = new Rect();
		R.x = Screen.width - strSize.x - 20;
		R.y = Screen.height - strSize.y - 20;
		R.width = strSize.x;
		R.height = strSize.y;
		
		GUI.Label(R, _str);
		return strSize;
	}	
	
	
	static public float AdaptScreenWidthFactor() {
		return ((float)Screen.width)/IpadScreen.x;
	}
	static public float AdaptScreenHeightFactor() {
		return ((float)Screen.height)/IpadScreen.y;
	}
	
	static public Vector2 AdaptToScreen(Vector2 _src) {
		Vector2 dst = new Vector2();
		dst.x = _src.x*Screen.width/IpadScreen.x;
		dst.y = _src.y*Screen.height/IpadScreen.y;
		return dst;
	}

	// center the specified rectangle on the screen to keep the screen ratio
	private static Rect CenterToScreenKeepRatio(Rect R, float _RatioToKeep) {
		Rect R2 = new Rect();
		float RectRatio = R.width / R.height;
		//Debug.Log("Before " + R + " for Screen: " + Screen.width + " " + Screen.height + " screenRatio:" + _RatioToKeep + " RectRatio: " + RectRatio);
		if (_RatioToKeep < RectRatio) {
			R2.height = R.height;
			R2.width = R.width * _RatioToKeep / RectRatio;
			R2.x = (R.width - R2.width) / 2;
			R2.y = 0;
		} else {
			R2.height = R.height * _RatioToKeep / RectRatio;
			R2.width = R.width;
			R2.x = 0;
			R2.y = (R.height - R2.height) / 2;
		}
		//Debug.Log("After " + R2 + " for Screen: " + Screen.width + " " + Screen.height);
		return R2;
	}	

	static public Rect AdaptToScreenR(Rect _src) {
		Rect VisibleScreen;
		if (true /*PrefMgr.m_ConserveGUIRatio*/) {
			// keep ipad screen ratio
			VisibleScreen = VisibleScreenForRatioKept;
		} else {
			// maximize use of screen real estate, taking into consideration screen ratio differences.
			VisibleScreen = VisibleScreenFullScreen;
		}
//		float RRatio = _src.width / _src.height;
		//Debug.Log("Before " + _src + " for Screen: " + Screen.width + " " + Screen.height + " screenRatio:" + IpadScreenRatio + " picRatio: " + RRatio);
		Rect dst = new Rect();
		dst.x = VisibleScreen.x + _src.x*VisibleScreen.width/IpadScreen.x;
		dst.y = VisibleScreen.y + _src.y*VisibleScreen.height/IpadScreen.y;
		dst.width = _src.width*VisibleScreen.width/IpadScreen.x;
		dst.height = _src.height*VisibleScreen.height/IpadScreen.y;
		//Debug.Log("After " + dst + " for Screen: " + Screen.width + " " + Screen.height);
		return dst;
	}
	static public Vector2 GetZoomScreen() {
		return new Vector2(Screen.width/IpadScreen.x,Screen.height/IpadScreen.y);
	}
	
	static public Vector2 AdaptScreenToIpad(Vector2 _src) {
		Vector2 dst = new Vector2();
		dst.x = _src.x*IpadScreen.x/Screen.width;
		dst.y = _src.y*IpadScreen.y/Screen.height;
		return dst;
	}
	
	static public Material m_GUIMat = null;
	static public bool LoadGUIMat()
	{
		Shader sh = Shader.Find("Anims/Shader/ActionShowTextureBlend");
		if (sh == null)
			return false;
		m_GUIMat = new Material(sh);
		if (m_GUIMat == null)
			return false;
		return true;
	}
	
//	On dessine une texture avec une bordure 
//  la bordure est scalée, mais avec le même facteur en width et en height
//  même si la texture est déformée
	
	static public void DrawTexWithBorder(Rect _Where,Texture _Tex, float _Border) {
		DrawTexWithBorder(_Where, _Tex, _Border, Color.white, false);
	}
	
	static public void DrawTexWithBorder(Rect _Where,Texture _Tex, float _Border, Color _color, bool _useColor = true) {
	
		Rect RDst0 = _Where;
		Rect RDst = new Rect();
		Rect RSrc = new Rect();
		
		
		float ZoomX = RDst0.width/_Tex.width;
		float ZoomY = RDst0.height/_Tex.height;
		float Zoom = Mathf.Min(ZoomX,ZoomY); 
		
		float[] ASrcPosX = new float[4]; 
		float[] ASrcPosY = new float[4]; 
		float[] ADstPosX = new float[4]; 
		float[] ADstPosY = new float[4]; 
		
		ASrcPosX[0] = 0;
		ASrcPosX[1] = _Border/_Tex.width;
		ASrcPosX[2] = (_Tex.width - _Border)/_Tex.width;
		ASrcPosX[3] = 1.0f;
		
		ASrcPosY[0] = 0;
		ASrcPosY[1] = _Border/_Tex.height;
		ASrcPosY[2] = (_Tex.height - _Border)/_Tex.height;
		ASrcPosY[3] = 1.0f;
		
		ADstPosX[0] = RDst0.x + 0;
		ADstPosX[1] = RDst0.x + _Border*Zoom;
		ADstPosX[2] = RDst0.x + _Tex.width*ZoomX - _Border*Zoom;
		ADstPosX[3] = RDst0.x + _Tex.width*ZoomX;
		
		ADstPosY[0] = RDst0.y + 0;
		ADstPosY[1] = RDst0.y + _Border*Zoom;
		ADstPosY[2] = RDst0.y + _Tex.height*ZoomY - _Border*Zoom;
		ADstPosY[3] = RDst0.y + _Tex.height*ZoomY;
		
		bool guiMatOk = true;
		Color colBackup = Color.white;
		if (m_GUIMat == null)
			guiMatOk = _useColor && LoadGUIMat();
		if (guiMatOk)
		{
			colBackup = m_GUIMat.color;
			m_GUIMat.color = _color;
		}
		
		for(int i=0;i<3;i++) {
			for(int j=0;j<3;j++) {
				RSrc.x = ASrcPosX[i];
				RSrc.width = ASrcPosX[i+1] - ASrcPosX[i];
				int j0 = 2-j; //Unity donne les coordonnées de haut en bas ou de bas en haut suivant le petit bonheur...
				RSrc.y = ASrcPosY[j0];
				RSrc.height = ASrcPosY[j0+1] - ASrcPosY[j0];
				
				RDst.x = ADstPosX[i];
				RDst.width = ADstPosX[i+1] - ADstPosX[i];
				RDst.y = ADstPosY[j];
				RDst.height = ADstPosY[j+1] - ADstPosY[j];
				
				// load neutral shader to modify color
				if (guiMatOk)
					Graphics.DrawTexture(RDst,_Tex,RSrc,0,0,0,0, m_GUIMat);
				else
					Graphics.DrawTexture(RDst,_Tex,RSrc,0,0,0,0);
			}
		}
		
		if (guiMatOk)
			m_GUIMat.color = colBackup;
	}
}


