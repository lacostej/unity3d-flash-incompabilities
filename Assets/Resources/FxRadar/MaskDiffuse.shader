Shader "MaskDiffuse" {
Properties {
	_Color ("Main Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_MaskTex ("Base (RGB) Trans (A)", 2D) = "white" {}
}

SubShader {
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
	LOD 200
	Blend SrcAlpha OneMinusSrcAlpha

CGPROGRAM
#pragma surface surf Lambert alpha finalcolor:mycolor
sampler2D _MainTex;
sampler2D _MaskTex;
fixed4 _Color;

struct Input {
	float2 uv_MainTex;
    float4 screenPos;
};

void mycolor (Input IN, SurfaceOutput o, inout fixed4 _color) {
  _color = _Color;
  _color.a = o.Alpha*_Color.a;
}

void surf (Input IN, inout SurfaceOutput o) {
	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
	o.Albedo = _Color.rgb*2.0f;
	float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
	o.Alpha = c.a*tex2D (_MaskTex, screenUV).a;
}
ENDCG
}

Fallback Off
}
