// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2743,x:33407,y:32722,varname:node_2743,prsc:2|custl-9055-OUT,clip-5127-A;n:type:ShaderForge.SFN_LightAttenuation,id:7539,x:31165,y:33007,varname:node_7539,prsc:2;n:type:ShaderForge.SFN_LightVector,id:4523,x:31166,y:32729,varname:node_4523,prsc:2;n:type:ShaderForge.SFN_Dot,id:3906,x:31360,y:32782,varname:node_3906,prsc:2,dt:1|A-4523-OUT,B-3697-OUT;n:type:ShaderForge.SFN_NormalVector,id:3697,x:31166,y:32852,prsc:2,pt:True;n:type:ShaderForge.SFN_Multiply,id:5530,x:31553,y:32782,varname:node_5530,prsc:2|A-3906-OUT,B-7539-OUT;n:type:ShaderForge.SFN_Step,id:4105,x:31782,y:32782,varname:node_4105,prsc:2|A-8417-OUT,B-5530-OUT;n:type:ShaderForge.SFN_Lerp,id:7176,x:32457,y:32833,varname:node_7176,prsc:2|A-3973-OUT,B-5127-RGB,T-4105-OUT;n:type:ShaderForge.SFN_Multiply,id:5011,x:31981,y:32390,varname:node_5011,prsc:2|A-5127-RGB,B-4191-OUT;n:type:ShaderForge.SFN_Step,id:9859,x:31782,y:32912,varname:node_9859,prsc:2|A-1461-OUT,B-5530-OUT;n:type:ShaderForge.SFN_Multiply,id:6446,x:32019,y:32912,varname:node_6446,prsc:2|A-9859-OUT,B-8933-OUT;n:type:ShaderForge.SFN_Add,id:9055,x:32672,y:32833,varname:node_9055,prsc:2|A-7176-OUT,B-2014-OUT;n:type:ShaderForge.SFN_OneMinus,id:4191,x:31741,y:32339,varname:node_4191,prsc:2|IN-8933-OUT;n:type:ShaderForge.SFN_Lerp,id:3973,x:32281,y:32353,varname:node_3973,prsc:2|A-5127-RGB,B-5011-OUT,T-7743-OUT;n:type:ShaderForge.SFN_Lerp,id:2014,x:32253,y:32912,varname:node_2014,prsc:2|A-9406-OUT,B-6446-OUT,T-8796-OUT;n:type:ShaderForge.SFN_Vector1,id:9406,x:32019,y:33041,varname:node_9406,prsc:2,v1:0;n:type:ShaderForge.SFN_Tex2d,id:5127,x:31741,y:32144,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_5127,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector3,id:8933,x:31502,y:32463,varname:node_8933,prsc:2,v1:1,v2:0.9955375,v3:0.9705882;n:type:ShaderForge.SFN_Vector1,id:7743,x:32028,y:32559,varname:node_7743,prsc:2,v1:0.33;n:type:ShaderForge.SFN_Vector1,id:8796,x:32028,y:32642,varname:node_8796,prsc:2,v1:0.27;n:type:ShaderForge.SFN_Vector1,id:8417,x:31523,y:32943,varname:node_8417,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector1,id:1461,x:31553,y:32999,varname:node_1461,prsc:2,v1:0.8;proporder:5127;pass:END;sub:END;*/

Shader "Custom/SH_Cartoon" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
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
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 d3d11_9x n3ds wiiu 
            #pragma target 2.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                clip(_MainTex_var.a - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 node_8933 = float3(1,0.9955375,0.9705882);
                float node_5530 = (max(0,dot(lightDirection,normalDirection))*attenuation);
                float node_9406 = 0.0;
                float3 finalColor = (lerp(lerp(_MainTex_var.rgb,(_MainTex_var.rgb*(1.0 - node_8933)),0.33),_MainTex_var.rgb,step(0.5,node_5530))+lerp(float3(node_9406,node_9406,node_9406),(step(0.8,node_5530)*node_8933),0.27));
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 d3d11_9x n3ds wiiu 
            #pragma target 2.0
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
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
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                clip(_MainTex_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
