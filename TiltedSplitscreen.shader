Shader "Hidden/Tiltedsplitscreen" {
Properties {
	_MainTex ("Base (RGB)", 2D) = "white" {}
	//_OtherTex ("Base (RGB)", 2D) = "white" {}
	//_Vector ("Direction", Vector) = (1,1,0,0)
	//_YDistance ("Y Distance", Float) = 0
	//_XDistance ("Y Distance", Float) = 0
}

SubShader {
	Pass {
		ZTest Always Cull Off ZWrite Off
		Fog { Mode off }
				
CGPROGRAM
#pragma vertex vert_img
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest 
#include "UnityCG.cginc"

uniform sampler2D _MainTex;
uniform sampler2D _OtherTex;
float4 _Vector;
float _YDistance;
float _XDistance;

fixed4 frag (v2f_img i) : COLOR
{	
	float2 center = float2(0.5, 0.5);
	float2 dir = float2(_Vector.x, _Vector.y);
	
	float m = dir.x / dir.y;
	
	float y = m * i.uv.x - m * center.x + center.y;
	float x = i.uv.y / m - center.y / m + center.x;
	
	fixed4 c;
	
	bool xb = i.uv.x < x;
	bool yb = i.uv.y < y;
	bool yd = _YDistance > 0;
	bool xd = _XDistance > 0;
	
	if( (_YDistance == 0 && ((xd && xb) || (!xd && !xb))) ||  ((yb && yd) || (!yb && !yd))){
	  c = tex2D (_OtherTex, i.uv);
	}else{ 
	  c = tex2D (_MainTex, i.uv);
	}
	
	return c;
}
ENDCG

	}
}

Fallback off

}