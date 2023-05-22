#ifndef MYRP_UNLIT_INCLUDED
#define MYRP_UNLIT_INCLUDED

//#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"

//CBUFFER_START(UnityPerFrame)
float4x4 unity_MatrixVP;
//CBUFFER_END

//CBUFFER_START(UnityPerDraw)
float4x4 unity_ObjectToWorld;
//CBUFFER_END

////CBUFFER_START(UnityPerMaterial)
//float4 _Color;
////CBUFFER_END

//struct a2v
//{
//    float4 posOS : POSITION;
//};

//struct v2f
//{
//    float4 posCS : SV_POSITION;
//};

//v2f vert (a2v i)
//{
//    v2f o;
//    o.posCS = mul(unity_MatrixVP, mul(unity_ObjectToWorld, float4(i.posOS.xyz, 1.0)));
//    return o;
//}

//float4 frag(v2f i) : SV_TARGET
//{
//    return _Color;
//}

#endif // MYRP_UNLIT_INCLUDED