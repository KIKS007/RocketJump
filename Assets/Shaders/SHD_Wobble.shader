// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2160,x:33370,y:32690,varname:node_2160,prsc:2|alpha-6570-OUT,refract-2115-OUT;n:type:ShaderForge.SFN_Vector1,id:6570,x:32334,y:32896,varname:node_6570,prsc:2,v1:0;n:type:ShaderForge.SFN_TexCoord,id:5690,x:31261,y:32781,varname:node_5690,prsc:2,uv:0;n:type:ShaderForge.SFN_Append,id:6390,x:32855,y:33027,varname:node_6390,prsc:2|A-2419-R,B-2419-G;n:type:ShaderForge.SFN_Time,id:2868,x:31319,y:33145,varname:node_2868,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4634,x:31524,y:33205,varname:node_4634,prsc:2|A-2868-T,B-8508-OUT;n:type:ShaderForge.SFN_Tex2d,id:2419,x:31884,y:32771,ptovrint:False,ptlb:node_2419,ptin:_node_2419,varname:node_2419,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-1025-UVOUT;n:type:ShaderForge.SFN_Panner,id:1025,x:31681,y:32771,varname:node_1025,prsc:2,spu:0,spv:1|UVIN-5690-UVOUT,DIST-4634-OUT;n:type:ShaderForge.SFN_Lerp,id:2115,x:33087,y:33038,varname:node_2115,prsc:2|A-3823-OUT,B-6390-OUT,T-7494-OUT;n:type:ShaderForge.SFN_Vector1,id:3823,x:32855,y:33153,varname:node_3823,prsc:2,v1:0;n:type:ShaderForge.SFN_Slider,id:230,x:32629,y:33341,ptovrint:False,ptlb:EffectStrength,ptin:_EffectStrength,varname:node_230,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_RemapRange,id:7494,x:33098,y:33244,varname:node_7494,prsc:2,frmn:0,frmx:1,tomn:0,tomx:0.005|IN-230-OUT;n:type:ShaderForge.SFN_Vector1,id:8508,x:31319,y:33273,varname:node_8508,prsc:2,v1:0.1;proporder:2419-230;pass:END;sub:END;*/

Shader "Custom/SHD_Wobble" {
    Properties {
        _node_2419 ("node_2419", 2D) = "white" {}
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
        GrabPass{ "Refraction" }
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
            uniform sampler2D Refraction;
            uniform float4 _TimeEditor;
            uniform sampler2D _node_2419; uniform float4 _node_2419_ST;
            uniform float _EffectStrength;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 screenPos : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
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
                float node_3823 = 0.0;
                float4 node_2868 = _Time + _TimeEditor;
                float2 node_1025 = (i.uv0+(node_2868.g*0.1)*float2(0,1));
                float4 _node_2419_var = tex2D(_node_2419,TRANSFORM_TEX(node_1025, _node_2419));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + lerp(float2(node_3823,node_3823),float2(_node_2419_var.r,_node_2419_var.g),(_EffectStrength*0.005+0.0));
                float4 sceneColor = tex2D(Refraction, sceneUVs);
////// Lighting:
                float3 finalColor = 0;
                return fixed4(lerp(sceneColor.rgb, finalColor,0.0),1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
