// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2199,x:33163,y:32701,varname:node_2199,prsc:2|emission-9598-OUT,olwid-3167-OUT,olcol-6206-RGB;n:type:ShaderForge.SFN_Tex2d,id:3476,x:32195,y:32756,ptovrint:False,ptlb:TurbulencesTex,ptin:_TurbulencesTex,varname:node_3476,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-5982-UVOUT;n:type:ShaderForge.SFN_Multiply,id:5406,x:32414,y:32773,varname:node_5406,prsc:2|A-3476-RGB,B-3476-A;n:type:ShaderForge.SFN_FragmentPosition,id:1021,x:31395,y:32716,varname:node_1021,prsc:2;n:type:ShaderForge.SFN_Append,id:1639,x:31604,y:32729,varname:node_1639,prsc:2|A-1021-X,B-1021-Y;n:type:ShaderForge.SFN_Slider,id:3167,x:32246,y:33219,ptovrint:False,ptlb:OutlineWidth,ptin:_OutlineWidth,varname:node_3167,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Color,id:6206,x:32390,y:33046,ptovrint:False,ptlb:OutlineColor,ptin:_OutlineColor,varname:node_6206,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:8695,x:31824,y:32729,varname:node_8695,prsc:2|A-1639-OUT,B-4857-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4857,x:31593,y:32939,ptovrint:False,ptlb:Size,ptin:_Size,varname:node_4857,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Rotator,id:5982,x:32014,y:32729,varname:node_5982,prsc:2|UVIN-8695-OUT,ANG-2206-OUT;n:type:ShaderForge.SFN_Time,id:1172,x:31593,y:33053,varname:node_1172,prsc:2;n:type:ShaderForge.SFN_Multiply,id:2206,x:31782,y:33077,varname:node_2206,prsc:2|A-1172-T,B-2299-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2299,x:31593,y:33210,ptovrint:False,ptlb:RotationSpeed,ptin:_RotationSpeed,varname:node_2299,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.01;n:type:ShaderForge.SFN_Tex2d,id:9587,x:32657,y:32632,ptovrint:False,ptlb:Ronces,ptin:_Ronces,varname:node_9587,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8695-OUT;n:type:ShaderForge.SFN_Lerp,id:9598,x:32898,y:32820,varname:node_9598,prsc:2|A-5406-OUT,B-9587-RGB,T-9587-A;proporder:3476-3167-6206-4857-2299-9587;pass:END;sub:END;*/

Shader "Custom/SHD_DangerousZone" {
    Properties {
        _TurbulencesTex ("TurbulencesTex", 2D) = "white" {}
        _OutlineWidth ("OutlineWidth", Range(0, 1)) = 0
        _OutlineColor ("OutlineColor", Color) = (1,0,0,1)
        _Size ("Size", Float ) = 0
        _RotationSpeed ("RotationSpeed", Float ) = 0.01
        _Ronces ("Ronces", 2D) = "white" {}
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        LOD 200
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 d3d11_9x 
            #pragma target 2.0
            uniform float _OutlineWidth;
            uniform float4 _OutlineColor;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = mul(UNITY_MATRIX_MVP, float4(v.vertex.xyz + v.normal*_OutlineWidth,1) );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                return fixed4(_OutlineColor.rgb,0);
            }
            ENDCG
        }
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
            uniform sampler2D _TurbulencesTex; uniform float4 _TurbulencesTex_ST;
            uniform float _Size;
            uniform float _RotationSpeed;
            uniform sampler2D _Ronces; uniform float4 _Ronces_ST;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_1172 = _Time + _TimeEditor;
                float node_5982_ang = (node_1172.g*_RotationSpeed);
                float node_5982_spd = 1.0;
                float node_5982_cos = cos(node_5982_spd*node_5982_ang);
                float node_5982_sin = sin(node_5982_spd*node_5982_ang);
                float2 node_5982_piv = float2(0.5,0.5);
                float2 node_8695 = (float2(i.posWorld.r,i.posWorld.g)*_Size);
                float2 node_5982 = (mul(node_8695-node_5982_piv,float2x2( node_5982_cos, -node_5982_sin, node_5982_sin, node_5982_cos))+node_5982_piv);
                float4 _TurbulencesTex_var = tex2D(_TurbulencesTex,TRANSFORM_TEX(node_5982, _TurbulencesTex));
                float4 _Ronces_var = tex2D(_Ronces,TRANSFORM_TEX(node_8695, _Ronces));
                float3 emissive = lerp((_TurbulencesTex_var.rgb*_TurbulencesTex_var.a),_Ronces_var.rgb,_Ronces_var.a);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
