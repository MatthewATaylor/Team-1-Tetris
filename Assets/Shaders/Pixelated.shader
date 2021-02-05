Shader "Hidden/Custom/Pixelated"
{
	HLSLINCLUDE

		#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

		TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
		float numPixels;

		float4 Frag(VaryingsDefault i) : SV_Target
		{
			float2 textureCoordinates = i.texcoord;
			textureCoordinates.x = (int)(textureCoordinates.x * numPixels) / numPixels;
			textureCoordinates.y = (int)(textureCoordinates.y * numPixels) / numPixels;
			float4 color = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, textureCoordinates);
			return color;
		}

	ENDHLSL

	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			HLSLPROGRAM

				#pragma vertex VertDefault
				#pragma fragment Frag

			ENDHLSL
		}
	}
}
