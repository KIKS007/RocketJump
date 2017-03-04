// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4083,x:32821,y:32700,varname:node_4083,prsc:2|emission-6668-OUT,alpha-111-OUT;n:type:ShaderForge.SFN_Distance,id:1014,x:32313,y:32872,varname:node_1014,prsc:2|A-953-UVOUT,B-4134-OUT;n:type:ShaderForge.SFN_TexCoord,id:953,x:31918,y:32871,varname:node_953,prsc:2,uv:0;n:type:ShaderForge.SFN_RemapRange,id:4134,x:32106,y:33051,varname:node_4134,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:2|IN-953-UVOUT;n:type:ShaderForge.SFN_OneMinus,id:111,x:32479,y:32872,varname:node_111,prsc:2|IN-1014-OUT;n:type:ShaderForge.SFN_Color,id:7117,x:32313,y:32705,ptovrint:False,ptlb:MainColor,ptin:_MainColor,varname:node_7117,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Slider,id:3431,x:32455,y:33202,ptovrint:False,ptlb:Darkness,ptin:_Darkness,varname:node_3431,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Lerp,id:6668,x:32655,y:32993,varname:node_6668,prsc:2|A-7117-RGB,B-3351-OUT,T-3431-OUT;n:type:ShaderForge.SFN_Vector1,id:3351,x:32455,y:33070,varname:node_3351,prsc:2,v1:0;proporder:7117-3431;pass:END;sub:END;*/

Shader "Custom/SHD_BackLights" {
    Properties {
        _MainColor ("MainColor", Color) = (1,0,0,1)
        _Darkness ("Darkness", Range(0, 1)) = 0
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
            uniform float4 _MainColor;
            uniform float _Darkness;
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
////// Lighting:
////// Emissive:
                float node_3351 = 0.0;
                float3 emissive = lerp(_MainColor.rgb,float3(node_3351,node_3351,node_3351),_Darkness);
                float3 finalColor = emissive;
                return fixed4(finalColor,(1.0 - distance(i.uv0,(i.uv0*3.0+-1.0))));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
