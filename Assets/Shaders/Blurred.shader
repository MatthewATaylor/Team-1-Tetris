Shader "Hidden/Custom/Blurred"
{
	HLSLINCLUDE

		#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

		TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);

		int maxOffset;
		float boardLeft;
		float boardRight;
		float boardTop;
		float boardBottom;
		float screenWidth;
		float screenHeight;

		float4 Frag(VaryingsDefault input) : SV_Target
		{
			bool withinBoard =
				input.texcoord.x >= boardLeft &&
				input.texcoord.x <= boardRight &&
				input.texcoord.y <= boardTop &&
				input.texcoord.y >= boardBottom;
			bool maxOffsetIs0 = maxOffset == 0;

			int i;

			// Perform horizontal blur
			float4 horizontalColor = float4(0, 0, 0, 0);
			for (i = -maxOffset; i <= maxOffset; ++i) {
				// Calculate x coordinate of sample
				float2 sampleCoords = input.texcoord;
				float xOffset = i * 1.0f / screenWidth;
				bool xOutOfBounds =
					sampleCoords.x + xOffset > boardRight ||
					sampleCoords.x + xOffset < boardLeft;
				sampleCoords.x =
					xOutOfBounds * sampleCoords.x +
					(1 - xOutOfBounds) * (sampleCoords.x + xOffset);

				// Sample from offset
				float4 sampleColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, sampleCoords);
				horizontalColor += sampleColor;
			}
			// Take average color
			horizontalColor /= (2.0f * maxOffset + maxOffsetIs0);  // Add maxOffsetIs0 to prevent divide by 0

			// Perform vertical blur
			float4 verticalColor = float4(0, 0, 0, 0);
			for (i = -maxOffset; i <= maxOffset; ++i) {
				// Calculate x coordinate of sample
				float2 sampleCoords = input.texcoord;
				float yOffset = i * 1.0f / screenHeight;
				bool yOutOfBounds =
					sampleCoords.y + yOffset > boardTop ||
					sampleCoords.y + yOffset < boardBottom;
				sampleCoords.y =
					yOutOfBounds * sampleCoords.y +
					(1 - yOutOfBounds) * (sampleCoords.y + yOffset);

				// Sample from offset
				float4 sampleColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, sampleCoords);
				verticalColor += sampleColor;
			}
			// Take average color
			verticalColor /= (2.0f * maxOffset + maxOffsetIs0);  // Add maxOffsetIs0 to prevent divide by 0

			float4 blurColor = (horizontalColor + verticalColor) / 2.0f;
			float4 singleSampleColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, input.texcoord);

			return withinBoard * blurColor + (1 - withinBoard) * singleSampleColor;
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
