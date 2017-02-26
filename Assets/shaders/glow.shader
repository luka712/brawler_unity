Shader "Custom/glow"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}

		_BloomThreshold("BloomThreshold", float) =  0.3

		_BlurAmount("BlurAmount", float) = 0.003

		_BloomSaturation("BloomSaturation", float) = 4.0
		_BaseSaturation("BaseSaturation", float) = 2.0
		_BloomIntensity("BloomIntensity", float) = 2.0
		_BaseIntensity("BaseIntensity", float) = 2.0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass 
		{
			Name "HorizontalBlur"

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			float _BlurAmount;

			fixed4 frag(v2f input) : SV_Target
			{
				half4 sum = half4(0.0,0.0,0.0,0.0);

				for (int i = -5; i <= 5; i++)
				{
					float t = 0.15;
					if(i != 0)
					{
						t /= abs(i);
					}
					sum += tex2D(_MainTex, float2(input.uv.x + i * _BlurAmount, input.uv.y)) * t;
				}

				return sum;
			}
			ENDCG
		}
		GrabPass {  }
		Pass
		{
			Name "VerticalBlur"

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _GrabTexture : register(s0);
			float _BlurAmount;


			fixed4 frag(v2f input) : SV_Target
			{
				half4 sum = half4(0.0,0.0,0.0,0.0);

				for (int i = -5; i <= 5; i++)
				{
					float t = 0.15;
					if (i != 0)
					{
						t /= abs(i);
					}
					sum += tex2D(_GrabTexture, float2(input.uv.x , input.uv.y + i * _BlurAmount)) * t;
				}

				return sum;
			}
			ENDCG
		}
		GrabPass{}
		Pass
		{
			Name "BloomExtractAndCombine"

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			sampler2D _GrabTexture : register(s0);
			float _BloomThreshold;

			float _BloomSaturation;
			float _BloomIntensity;
			float _BaseSaturation;
			float _BaseIntensity;

			half4 extract(v2f i)
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				// just invert the colors
				return saturate((col - _BloomThreshold) / (1 - _BloomThreshold));
			}

			float4 AdjustSaturation(float4 color, float saturation)
			{
				float grey = dot(color, float3(0.3, 0.59, 0.11));

				return lerp(grey, color, saturation);
			}

			fixed4 frag (v2f i) : SV_Target
			{
				half4 bloom = extract(i);
				half4 base = tex2D(_GrabTexture, i.uv);

				bloom = AdjustSaturation(bloom, _BloomSaturation) * _BloomIntensity;
				base = AdjustSaturation(base, _BaseSaturation) * _BaseIntensity;

				base *= (1 - saturate(bloom));

				return base + bloom;
			}
			ENDCG
		}
	}
}
