Shader "Cutom/TestPatterns"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
            
            float random (in float x) {
                return frac(sin(x)*1e4);
            }

            float random (in half2 st) {
                return frac(sin(dot(st.xy, half2(12.9898,78.233)))* 43758.5453123);
            }

            float randomSerie(float x, float freq, float t) {
                return step(.8,random( floor(x*freq)-floor(t) ));
            }

            fixed4 frag (v2f i) : SV_Target
            {
                half2 st = i.uv*_ScreenParams.xy/_ScreenParams.xy;
                st.x *= _ScreenParams.x/_ScreenParams.y;

                half3 color = half3(0.0, 0.0, 0.0);

                float cols = 1.;
                float freq = random((half2)floor(_Time))+abs(atan(_Time)*0.1);
                float t = 60.+_Time*(1.0-freq)*30.;

                //if (frac(st.y*cols* 0.5) < 0.5){
                //    t *= -1.0;
                //}

                freq += random(floor(st.y));

                float offset = 0.025;
                color = half3(randomSerie(st.x, freq*100., t+offset),
                             randomSerie(st.x, freq*100., t),
                             randomSerie(st.x, freq*100., t-offset));

                half4 col = half4(1.0-color,1.0);
                return col;
            }
            ENDCG
        }
    }
}
