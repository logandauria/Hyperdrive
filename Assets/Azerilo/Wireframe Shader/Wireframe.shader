Shader "Azerilo/Wireframe"
{
	Properties
	{
	    [HDR]  _WireColor ("Wire color", Color) = (0, 0, 0, 1)
	    _WireSize ("Wire size", float) = 0.3
	    _ZMode("Z Mode", Range(-1,1)) = 1
 	   _Cull("Cull Mode", Float) = 2.0
	}
	
	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			Cull[_Cull]
			ZWrite Off

			CGPROGRAM
			#include "UnityCG.cginc"
			#include "Core.cginc"
			#pragma vertex vert
			#pragma fragment frag
			ENDCG
		}
	}
}
