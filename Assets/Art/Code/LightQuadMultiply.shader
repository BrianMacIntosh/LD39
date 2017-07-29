Shader "Beryllium/Light Quad Multiply"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend DstColor Zero

		Pass
		{
		CGPROGRAM
			#pragma vertex SpriteVert
			#pragma fragment LocalSpriteFrag
			#pragma target 2.0
			#pragma multi_compile_instancing
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnitySprites.cginc"

			fixed4 LocalSpriteFrag(v2f IN) : SV_Target
			{
				fixed4 c = SampleSpriteTexture(IN.texcoord);// *IN.color;
				//c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
}
