Shader "Chibi929/ImageEffectShader/RasterScroll"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Speed("speed", float) = 1.5
        _Amplitude("amplitude", float) = 0.4
        _Frequency("frequency", float) = 2.5
        [MaterialToggle]
        _FillBlackOutSide("fill black outside", float) = 1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
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

            sampler2D _MainTex;
            float _Speed;
            float _Amplitude;
            float _Frequency;
            float _FillBlackOutSide;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 時間の取得
                float time = _Time.y * _Speed;

                // Y軸ライン上X座標をずらす
                float dx = _Amplitude * sin(radians((i.uv.y * _Frequency + time) * 360));
                i.uv.x += dx;

                if (_FillBlackOutSide) {
                    // はみ出た部分を黒く塗る
                    if (i.uv.x < 0 || 1 < i.uv.x) {
                        return float4(0, 0, 0, 0);
                    }
                }

                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
