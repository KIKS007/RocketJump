// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.882353,fgcg:0.3764706,fgcb:0.06666667,fgca:1,fgde:0.06,fgrn:21.2,fgrf:23.8,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4240,x:32719,y:32712,varname:node_4240,prsc:2|custl-827-RGB,clip-827-A;n:type:ShaderForge.SFN_Tex2d,id:827,x:32343,y:32776,ptovrint:False,ptlb:Texture,ptin:_Texture,varname:node_827,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:8ee02b2458edee244bcc4c39a6cc73df,ntxv:0,isnm:False|UVIN-5485-OUT;n:type:ShaderForge.SFN_Tex2d,id:4044,x:31597,y:32841,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_4044,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:ad8883a438a220240a3d203c70bd9445,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:2532,x:31054,y:32891,varname:node_2532,prsc:2,uv:0;n:type:ShaderForge.SFN_Lerp,id:5140,x:31967,y:32793,varname:node_5140,prsc:2|A-2532-U,B-5218-OUT,T-4044-R;n:type:ShaderForge.SFN_Append,id:5485,x:32188,y:32793,varname:node_5485,prsc:2|A-5140-OUT,B-2532-V;n:type:ShaderForge.SFN_Tex2d,id:2428,x:31597,y:33020,ptovrint:False,ptlb:Panner,ptin:_Panner,varname:node_2428,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:d3bb1f7fab43fac44b09c19a1c0f546f,ntxv:0,isnm:False|UVIN-547-UVOUT;n:type:ShaderForge.SFN_Panner,id:547,x:31416,y:33020,varname:node_547,prsc:2,spu:0,spv:1|UVIN-2532-UVOUT,DIST-5597-OUT;n:type:ShaderForge.SFN_Time,id:6874,x:31054,y:33098,varname:node_6874,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5597,x:31250,y:33184,varname:node_5597,prsc:2|A-6874-T,B-5443-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5443,x:31054,y:33247,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_5443,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Lerp,id:5218,x:31919,y:33112,varname:node_5218,prsc:2|A-2532-U,B-2428-G,T-7236-OUT;n:type:ShaderForge.SFN_Slider,id:7236,x:31528,y:33305,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_7236,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;proporder:827-4044-2428-5443-7236;pass:END;sub:END;*/

Shader "Custom/SHD_Clothes" {
    Properties {
        _Texture ("Texture", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _Panner ("Panner", 2D) = "white" {}
        _Speed ("Speed", Float ) = 0
        _Intensity ("Intensity", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
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
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 d3d11_9x 
            #pragma target 2.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform sampler2D _Panner; uniform float4 _Panner_ST;
            uniform float _Speed;
            uniform float _Intensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 node_6874 = _Time + _TimeEditor;
                float2 node_547 = (i.uv0+(node_6874.g*_Speed)*float2(0,1));
                float4 _Panner_var = tex2D(_Panner,TRANSFORM_TEX(node_547, _Panner));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float2 node_5485 = float2(lerp(i.uv0.r,lerp(i.uv0.r,_Panner_var.g,_Intensity),_Mask_var.r),i.uv0.g);
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_5485, _Texture));
                clip(_Texture_var.a - 0.5);
////// Lighting:
                float3 finalColor = _Texture_var.rgb;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 d3d11_9x 
            #pragma target 2.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Texture; uniform float4 _Texture_ST;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform sampler2D _Panner; uniform float4 _Panner_ST;
            uniform float _Speed;
            uniform float _Intensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 node_6874 = _Time + _TimeEditor;
                float2 node_547 = (i.uv0+(node_6874.g*_Speed)*float2(0,1));
                float4 _Panner_var = tex2D(_Panner,TRANSFORM_TEX(node_547, _Panner));
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float2 node_5485 = float2(lerp(i.uv0.r,lerp(i.uv0.r,_Panner_var.g,_Intensity),_Mask_var.r),i.uv0.g);
                float4 _Texture_var = tex2D(_Texture,TRANSFORM_TEX(node_5485, _Texture));
                clip(_Texture_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
