#ifndef CURVEDWORLD_CLASSICRUNNER_Z_NEGATIVE_ID21_CGINC
#define CURVEDWORLD_CLASSICRUNNER_Z_NEGATIVE_ID21_CGINC

uniform float3 CurvedWorld_ClassicRunner_Z_Negative_ID21_PivotPoint;
uniform float2 CurvedWorld_ClassicRunner_Z_Negative_ID21_BendSize;    
uniform float2 CurvedWorld_ClassicRunner_Z_Negative_ID21_BendOffset;
  
                 
#include "../../Core/Core.cginc"                           
             
      
////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                                Main Method                                 //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
void CurvedWorld_ClassicRunner_Z_Negative_ID21(inout float4 vertexOS)
{
    CurvedWorld_ClassicRunner_Z_Negative(vertexOS, 
	                        CurvedWorld_ClassicRunner_Z_Negative_ID21_PivotPoint,
							CurvedWorld_ClassicRunner_Z_Negative_ID21_BendSize,
							CurvedWorld_ClassicRunner_Z_Negative_ID21_BendOffset);
}

void CurvedWorld_ClassicRunner_Z_Negative_ID21(inout float4 vertexOS, inout float3 normalOS, float4 tangent)
{
    CurvedWorld_ClassicRunner_Z_Negative(vertexOS, 
                            normalOS, 
                            tangent,
                            CurvedWorld_ClassicRunner_Z_Negative_ID21_PivotPoint,
                            CurvedWorld_ClassicRunner_Z_Negative_ID21_BendSize,
                            CurvedWorld_ClassicRunner_Z_Negative_ID21_BendOffset);
}    

void CurvedWorld_ClassicRunner_Z_Negative_ID21(inout float3 vertexOS)
{
    float4 vertex = float4(vertexOS, 1);
    CurvedWorld_ClassicRunner_Z_Negative_ID21(vertex);

    vertexOS.xyz = vertex.xyz;
}

void CurvedWorld_ClassicRunner_Z_Negative_ID21(inout float3 vertexOS, inout float3 normalOS, float4 tangent)
{
    float4 vertex = float4(vertexOS, 1);
    CurvedWorld_ClassicRunner_Z_Negative_ID21(vertex, normalOS, tangent);

    vertexOS.xyz = vertex.xyz;
}  
                  
////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                               SubGraph Methods                             //
//                                                                            // 
////////////////////////////////////////////////////////////////////////////////
void CurvedWorld_ClassicRunner_Z_Negative_ID21_float(float3 vertexOS, out float3 retVertex)
{
    CurvedWorld_ClassicRunner_Z_Negative_ID21(vertexOS); 	

    retVertex = vertexOS.xyz;
}

void CurvedWorld_ClassicRunner_Z_Negative_ID21_half(half3 vertexOS, out half3 retVertex)
{
    CurvedWorld_ClassicRunner_Z_Negative_ID21(vertexOS); 	

    retVertex = vertexOS.xyz;
}

void CurvedWorld_ClassicRunner_Z_Negative_ID21_float(float3 vertexOS, float3 normalOS, float4 tangent, out float3 retVertex, out float3 retNormal)
{
	CurvedWorld_ClassicRunner_Z_Negative_ID21(vertexOS, normalOS, tangent); 	

    retVertex = vertexOS.xyz;
    retNormal = normalOS.xyz;
}

void CurvedWorld_ClassicRunner_Z_Negative_ID21_half(half3 vertexOS, half3 normalOS, half4 tangent, out half3 retVertex, out float3 retNormal)
{
	CurvedWorld_ClassicRunner_Z_Negative_ID21(vertexOS, normalOS, tangent); 	

    retVertex = vertexOS.xyz;
    retNormal = normalOS.xyz;	
}     

#endif
