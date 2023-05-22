Shader "Test RP/Unlit" {

	Properties
	{
		_Color("Color", Color) = (1, 1, 1, 1)
	}

	SubShader
	{
		Pass
		{
			HLSLPROGRAM
				#pragma target 5.5
				//#pragma multi_compile_instancing
				#pragma vertex vert
				#pragma fragment frag

				#include "../ShaderLibrary/Unlit.hlsl"

				struct a2v
				{
					float4 posOS : POSITION;
				};

				struct v2f
				{
					float4 posCS : SV_POSITION;
				};

				float4 _Color;

				v2f vert(a2v i)
				{
					v2f o;
					o.posCS = mul(unity_MatrixVP, mul(unity_ObjectToWorld, float4(i.posOS.xyz, 1.0)));
					return o;
				}

				float4 frag(v2f i) : SV_TARGET
				{
					return _Color;
				}
			ENDHLSL
		}
	}
}