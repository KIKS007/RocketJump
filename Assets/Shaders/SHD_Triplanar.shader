// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.882353,fgcg:0.3764706,fgcb:0.06666667,fgca:1,fgde:0.06,fgrn:21.2,fgrf:23.8,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:9880,x:36025,y:33182,varname:node_9880,prsc:2|custl-9679-OUT;n:type:ShaderForge.SFN_FragmentPosition,id:7436,x:30736,y:32868,varname:node_7436,prsc:2;n:type:ShaderForge.SFN_Append,id:2731,x:31678,y:32697,varname:node_2731,prsc:2|A-5385-OUT,B-7436-Y;n:type:ShaderForge.SFN_Append,id:4006,x:31678,y:32835,varname:node_4006,prsc:2|A-7436-Z,B-7436-X;n:type:ShaderForge.SFN_Append,id:7839,x:31678,y:32987,varname:node_7839,prsc:2|A-7436-X,B-7436-Y;n:type:ShaderForge.SFN_NormalVector,id:7448,x:31752,y:31647,prsc:2,pt:False;n:type:ShaderForge.SFN_Abs,id:6910,x:31932,y:31647,varname:node_6910,prsc:2|IN-7448-OUT;n:type:ShaderForge.SFN_Multiply,id:5724,x:32159,y:31647,varname:node_5724,prsc:2|A-6910-OUT,B-6910-OUT;n:type:ShaderForge.SFN_ComponentMask,id:6084,x:32342,y:31647,varname:node_6084,prsc:2,cc1:0,cc2:1,cc3:2,cc4:-1|IN-5724-OUT;n:type:ShaderForge.SFN_ChannelBlend,id:6240,x:33305,y:33002,varname:node_6240,prsc:2,chbt:0|M-6084-OUT,R-5565-RGB,G-6266-RGB,B-6701-RGB;n:type:ShaderForge.SFN_Tex2d,id:5565,x:32341,y:32676,ptovrint:False,ptlb:Texture YZ,ptin:_TextureYZ,varname:node_5565,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c6147c46eaa99ae47b1d4ff8be499b59,ntxv:0,isnm:False|UVIN-3901-OUT;n:type:ShaderForge.SFN_Tex2d,id:6266,x:32341,y:32861,ptovrint:False,ptlb:Texture ZX,ptin:_TextureZX,varname:_TextureYZ_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c6147c46eaa99ae47b1d4ff8be499b59,ntxv:0,isnm:False|UVIN-7934-OUT;n:type:ShaderForge.SFN_Tex2d,id:6701,x:32341,y:33060,ptovrint:False,ptlb:Texture XY,ptin:_TextureXY,varname:_TextureZX_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c6147c46eaa99ae47b1d4ff8be499b59,ntxv:0,isnm:False|UVIN-706-OUT;n:type:ShaderForge.SFN_Multiply,id:3901,x:32025,y:32687,varname:node_3901,prsc:2|A-2731-OUT,B-9286-OUT;n:type:ShaderForge.SFN_Multiply,id:7934,x:32025,y:32836,varname:node_7934,prsc:2|A-4006-OUT,B-9286-OUT;n:type:ShaderForge.SFN_Multiply,id:706,x:32025,y:32997,varname:node_706,prsc:2|A-7839-OUT,B-9286-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9286,x:31678,y:33163,ptovrint:False,ptlb:Tiling,ptin:_Tiling,varname:node_9286,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:5385,x:31453,y:32711,varname:node_5385,prsc:2|A-7436-Z,B-5181-OUT;n:type:ShaderForge.SFN_Vector1,id:5181,x:31234,y:32731,varname:node_5181,prsc:2,v1:-1;n:type:ShaderForge.SFN_LightVector,id:3957,x:31761,y:33536,varname:node_3957,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:3110,x:31761,y:33666,prsc:2,pt:False;n:type:ShaderForge.SFN_Dot,id:8758,x:31941,y:33599,varname:node_8758,prsc:2,dt:1|A-3957-OUT,B-3110-OUT;n:type:ShaderForge.SFN_Multiply,id:4231,x:32131,y:33599,varname:node_4231,prsc:2|A-8758-OUT,B-1822-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:1822,x:31761,y:33826,varname:node_1822,prsc:2;n:type:ShaderForge.SFN_Step,id:9903,x:32339,y:33599,varname:node_9903,prsc:2|A-1694-OUT,B-4231-OUT;n:type:ShaderForge.SFN_Step,id:2874,x:32339,y:33732,varname:node_2874,prsc:2|A-5321-OUT,B-4231-OUT;n:type:ShaderForge.SFN_LightColor,id:5609,x:32339,y:33447,varname:node_5609,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5215,x:32567,y:33735,varname:node_5215,prsc:2|A-5609-RGB,B-2874-OUT;n:type:ShaderForge.SFN_Lerp,id:4387,x:32758,y:33701,varname:node_4387,prsc:2|A-3463-OUT,B-5215-OUT,T-847-OUT;n:type:ShaderForge.SFN_Vector1,id:3463,x:32567,y:33674,varname:node_3463,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:847,x:32567,y:33927,cmnt:HighlightOpacity,varname:node_847,prsc:2,v1:0.3;n:type:ShaderForge.SFN_OneMinus,id:3643,x:32579,y:33447,varname:node_3643,prsc:2|IN-5609-RGB;n:type:ShaderForge.SFN_Multiply,id:6181,x:34264,y:33408,varname:node_6181,prsc:2|A-6450-OUT,B-3643-OUT;n:type:ShaderForge.SFN_Lerp,id:5592,x:34477,y:33388,varname:node_5592,prsc:2|A-6450-OUT,B-6181-OUT,T-6788-OUT;n:type:ShaderForge.SFN_Vector1,id:6788,x:34264,y:33541,cmnt:ShadowOpacity,varname:node_6788,prsc:2,v1:0.3;n:type:ShaderForge.SFN_Add,id:8316,x:34883,y:33394,varname:node_8316,prsc:2|A-8872-OUT,B-4387-OUT;n:type:ShaderForge.SFN_Lerp,id:8872,x:34698,y:33394,varname:node_8872,prsc:2|A-5592-OUT,B-6450-OUT,T-9903-OUT;n:type:ShaderForge.SFN_Slider,id:1694,x:31942,y:33959,ptovrint:False,ptlb:ShadowSize,ptin:_ShadowSize,varname:node_1694,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Slider,id:5321,x:31942,y:34063,ptovrint:False,ptlb:HighlightSize,ptin:_HighlightSize,varname:_ShadowSize_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:3305,x:35484,y:33174,varname:node_3305,prsc:2|A-8316-OUT,B-2055-RGB,T-2739-R;n:type:ShaderForge.SFN_Color,id:2055,x:34754,y:33007,ptovrint:False,ptlb:StainColor,ptin:_StainColor,varname:node_2055,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.8758622,c3:1,c4:1;n:type:ShaderForge.SFN_Lerp,id:9679,x:35677,y:33174,varname:node_9679,prsc:2|A-3305-OUT,B-6803-OUT,T-2739-G;n:type:ShaderForge.SFN_Vector1,id:6803,x:35484,y:33118,varname:node_6803,prsc:2,v1:1;n:type:ShaderForge.SFN_Tex2dAsset,id:5237,x:31720,y:32240,ptovrint:False,ptlb:Stains,ptin:_Stains,varname:node_5237,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8fddf8f1048e5d342af2b583502e8de1,ntxv:2,isnm:False;n:type:ShaderForge.SFN_ChannelBlend,id:294,x:33292,y:32394,varname:node_294,prsc:2,chbt:0|M-6084-OUT,R-5460-RGB,G-6416-RGB,B-9569-RGB;n:type:ShaderForge.SFN_ComponentMask,id:2739,x:33497,y:32394,varname:node_2739,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-294-OUT;n:type:ShaderForge.SFN_Tex2d,id:6416,x:32341,y:32313,varname:node_6416,prsc:2,tex:8fddf8f1048e5d342af2b583502e8de1,ntxv:0,isnm:False|UVIN-7934-OUT,TEX-5237-TEX;n:type:ShaderForge.SFN_Tex2d,id:5460,x:32341,y:32181,varname:node_5460,prsc:2,tex:8fddf8f1048e5d342af2b583502e8de1,ntxv:0,isnm:False|UVIN-3901-OUT,TEX-5237-TEX;n:type:ShaderForge.SFN_Tex2d,id:9569,x:32341,y:32445,varname:node_9569,prsc:2,tex:8fddf8f1048e5d342af2b583502e8de1,ntxv:0,isnm:False|UVIN-706-OUT,TEX-5237-TEX;n:type:ShaderForge.SFN_Multiply,id:6450,x:33688,y:33238,varname:node_6450,prsc:2|A-5945-RGB,B-6240-OUT;n:type:ShaderForge.SFN_Color,id:5945,x:33305,y:32830,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_5945,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;proporder:5565-6266-6701-9286-1694-5321-2055-5237-5945;pass:END;sub:END;*/

Shader "Custom/SHD_Triplanar" {
    Properties {
        _TextureYZ ("Texture YZ", 2D) = "white" {}
        _TextureZX ("Texture ZX", 2D) = "white" {}
        _TextureXY ("Texture XY", 2D) = "white" {}
        _Tiling ("Tiling", Float ) = 1
        _ShadowSize ("ShadowSize", Range(0, 1)) = 0
        _HighlightSize ("HighlightSize", Range(0, 1)) = 0
        _StainColor ("StainColor", Color) = (0,0.8758622,1,1)
        _Stains ("Stains", 2D) = "black" {}
        _Color ("Color", Color) = (1,1,1,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 d3d11_9x 
            #pragma target 2.0
            uniform sampler2D _TextureYZ; uniform float4 _TextureYZ_ST;
            uniform sampler2D _TextureZX; uniform float4 _TextureZX_ST;
            uniform sampler2D _TextureXY; uniform float4 _TextureXY_ST;
            uniform float _Tiling;
            uniform float _ShadowSize;
            uniform float _HighlightSize;
            uniform float4 _StainColor;
            uniform sampler2D _Stains; uniform float4 _Stains_ST;
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 node_6910 = abs(i.normalDir);
                float3 node_6084 = (node_6910*node_6910).rgb;
                float2 node_3901 = (float2((i.posWorld.b*(-1.0)),i.posWorld.g)*_Tiling);
                float4 _TextureYZ_var = tex2D(_TextureYZ,TRANSFORM_TEX(node_3901, _TextureYZ));
                float2 node_7934 = (float2(i.posWorld.b,i.posWorld.r)*_Tiling);
                float4 _TextureZX_var = tex2D(_TextureZX,TRANSFORM_TEX(node_7934, _TextureZX));
                float2 node_706 = (float2(i.posWorld.r,i.posWorld.g)*_Tiling);
                float4 _TextureXY_var = tex2D(_TextureXY,TRANSFORM_TEX(node_706, _TextureXY));
                float3 node_6450 = (_Color.rgb*(node_6084.r*_TextureYZ_var.rgb + node_6084.g*_TextureZX_var.rgb + node_6084.b*_TextureXY_var.rgb));
                float node_4231 = (max(0,dot(lightDirection,i.normalDir))*attenuation);
                float node_3463 = 0.0;
                float4 node_5460 = tex2D(_Stains,TRANSFORM_TEX(node_3901, _Stains));
                float4 node_6416 = tex2D(_Stains,TRANSFORM_TEX(node_7934, _Stains));
                float4 node_9569 = tex2D(_Stains,TRANSFORM_TEX(node_706, _Stains));
                float2 node_2739 = (node_6084.r*node_5460.rgb + node_6084.g*node_6416.rgb + node_6084.b*node_9569.rgb).rg;
                float node_6803 = 1.0;
                float3 finalColor = lerp(lerp((lerp(lerp(node_6450,(node_6450*(1.0 - _LightColor0.rgb)),0.3),node_6450,step(_ShadowSize,node_4231))+lerp(float3(node_3463,node_3463,node_3463),(_LightColor0.rgb*step(_HighlightSize,node_4231)),0.3)),_StainColor.rgb,node_2739.r),float3(node_6803,node_6803,node_6803),node_2739.g);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 d3d11_9x 
            #pragma target 2.0
            uniform sampler2D _TextureYZ; uniform float4 _TextureYZ_ST;
            uniform sampler2D _TextureZX; uniform float4 _TextureZX_ST;
            uniform sampler2D _TextureXY; uniform float4 _TextureXY_ST;
            uniform float _Tiling;
            uniform float _ShadowSize;
            uniform float _HighlightSize;
            uniform float4 _StainColor;
            uniform sampler2D _Stains; uniform float4 _Stains_ST;
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
                LIGHTING_COORDS(2,3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 node_6910 = abs(i.normalDir);
                float3 node_6084 = (node_6910*node_6910).rgb;
                float2 node_3901 = (float2((i.posWorld.b*(-1.0)),i.posWorld.g)*_Tiling);
                float4 _TextureYZ_var = tex2D(_TextureYZ,TRANSFORM_TEX(node_3901, _TextureYZ));
                float2 node_7934 = (float2(i.posWorld.b,i.posWorld.r)*_Tiling);
                float4 _TextureZX_var = tex2D(_TextureZX,TRANSFORM_TEX(node_7934, _TextureZX));
                float2 node_706 = (float2(i.posWorld.r,i.posWorld.g)*_Tiling);
                float4 _TextureXY_var = tex2D(_TextureXY,TRANSFORM_TEX(node_706, _TextureXY));
                float3 node_6450 = (_Color.rgb*(node_6084.r*_TextureYZ_var.rgb + node_6084.g*_TextureZX_var.rgb + node_6084.b*_TextureXY_var.rgb));
                float node_4231 = (max(0,dot(lightDirection,i.normalDir))*attenuation);
                float node_3463 = 0.0;
                float4 node_5460 = tex2D(_Stains,TRANSFORM_TEX(node_3901, _Stains));
                float4 node_6416 = tex2D(_Stains,TRANSFORM_TEX(node_7934, _Stains));
                float4 node_9569 = tex2D(_Stains,TRANSFORM_TEX(node_706, _Stains));
                float2 node_2739 = (node_6084.r*node_5460.rgb + node_6084.g*node_6416.rgb + node_6084.b*node_9569.rgb).rg;
                float node_6803 = 1.0;
                float3 finalColor = lerp(lerp((lerp(lerp(node_6450,(node_6450*(1.0 - _LightColor0.rgb)),0.3),node_6450,step(_ShadowSize,node_4231))+lerp(float3(node_3463,node_3463,node_3463),(_LightColor0.rgb*step(_HighlightSize,node_4231)),0.3)),_StainColor.rgb,node_2739.r),float3(node_6803,node_6803,node_6803),node_2739.g);
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
