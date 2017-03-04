// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3691,x:33341,y:32651,varname:node_3691,prsc:2|emission-3461-OUT,alpha-7300-OUT;n:type:ShaderForge.SFN_TexCoord,id:2576,x:32076,y:32812,varname:node_2576,prsc:2,uv:0;n:type:ShaderForge.SFN_Distance,id:3862,x:32388,y:32868,varname:node_3862,prsc:2|A-2974-UVOUT,B-9216-OUT;n:type:ShaderForge.SFN_Vector2,id:9216,x:32076,y:32965,varname:node_9216,prsc:2,v1:0.5,v2:0.5;n:type:ShaderForge.SFN_ScreenPos,id:2974,x:32084,y:33206,varname:node_2974,prsc:2,sctp:2;n:type:ShaderForge.SFN_Power,id:9365,x:32563,y:32868,varname:node_9365,prsc:2|VAL-3862-OUT,EXP-959-OUT;n:type:ShaderForge.SFN_Multiply,id:3461,x:32825,y:32724,varname:node_3461,prsc:2|A-2034-RGB,B-9365-OUT;n:type:ShaderForge.SFN_Color,id:2034,x:32530,y:32611,ptovrint:False,ptlb:VignetColor,ptin:_VignetColor,varname:node_2034,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Add,id:7300,x:32773,y:32902,varname:node_7300,prsc:2|A-9365-OUT,B-9365-OUT;n:type:ShaderForge.SFN_Slider,id:2675,x:32285,y:33191,ptovrint:False,ptlb:EffectStrength,ptin:_EffectStrength,varname:node_2675,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_RemapRange,id:959,x:32592,y:33113,varname:node_959,prsc:2,frmn:0,frmx:1,tomn:5,tomx:2|IN-2675-OUT;proporder:2034-2675;pass:END;sub:END;*/

Shader "Custom/SHD_Vignetting" {
    Properties {
        _VignetColor ("VignetColor", Color) = (0.5,0.5,0.5,1)
        _EffectStrength ("EffectStrength", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 d3d11_9x 
            #pragma target 2.0
            uniform float4 _VignetColor;
            uniform float _EffectStrength;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
////// Lighting:
////// Emissive:
                float node_9365 = pow(distance(sceneUVs.rg,float2(0.5,0.5)),(_EffectStrength*-3.0+5.0));
                float3 emissive = (_VignetColor.rgb*node_9365);
                float3 finalColor = emissive;
                return fixed4(finalColor,(node_9365+node_9365));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
