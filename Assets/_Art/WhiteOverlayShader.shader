Shader "Unlit/WhiteOverlayShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color",Color) = (1,1,1,1) //Default to white
        _Transparency ("Transparency", Range(0, 1)) = 1.0 // New transparency property
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha //Enable blending
        
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
            float4 _MainTex_ST;
            float4 _Color;
            float _Transparency; // New transparency variable

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col=tex2D(_MainTex,i.uv);
                if(col.a > 0)// Only change color if the alpha is greater than 0
                {
                    return fixed4(_Color.rgb,col.a * _Transparency);// Keep the alpha value
                }
                return fixed4(0,0,0,0); // Return transparent for fully transparent areas
            }
            ENDCG
        }
    }
}
