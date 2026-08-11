Shader "Custom/Crosshair"
{
    Properties
    {
        [MainTexture] _BaseMap("Base Map", 2D) = "transparent" {}
        _Cutoff("Alpha Cutoff", Range(0.0, 1.0)) = 0.1
    }
 
    SubShader
    {
        Tags 
        { 
            // lasth render (after rendering other stuff)
            "Queue" = "Overlay"
            "IgnoreProjector" = "True"
            // unity know this is transparent
            "RenderType" = "Transparent"
            "RenderPipeline" = "UniversalPipeline" 
        }

        Pass
        {
            // for inverse transparency
            Blend OneMinusDstColor Zero
            // have no depth (other object cannot overide the crosshair)
            ZWrite Off
            // draw always
            ZTest Always

            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            // initialize the texture
            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            // add to buffer Tiling/Offset about BaseMap
            CBUFFER_START(UnityPerMaterial)
                float4 _BaseMap_ST;
                half _Cutoff;
            CBUFFER_END

            // poligons - uv
            struct appdata
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };
            // transfered data to fragment shader from vertex shader
            struct v2f
            {
                float4 positionCS : SV_POSITION; 
                float2 uv : TEXCOORD0;
            };

            // on vertex shader just move the information
            v2f vert(appdata data)
            {
                v2f o = (v2f)0;
                o.positionCS = TransformObjectToHClip(data.positionOS.xyz);
                o.uv = TRANSFORM_TEX(data.uv, _BaseMap);
                return o;
            }

            // drawo the texture
            float4 frag(v2f data) : SV_TARGET
            {  
                // take the texture
                float4 tex = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, data.uv);
                // if pixel is less that 1 for A. dont draw it
                clip(tex.a - _Cutoff);
                // pixels what not cliped. show it inverse
                return float4(1.0, 1.0, 1.0, 1.0);
            }
            ENDHLSL
        }
    }
}
