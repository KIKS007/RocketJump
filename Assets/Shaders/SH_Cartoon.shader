// Shader created with Shader Forge v1.32 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.32;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:True,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:2743,x:33407,y:32722,varname:node_2743,prsc:2|custl-9055-OUT;n:type:ShaderForge.SFN_LightAttenuation,id:7539,x:31165,y:33007,varname:node_7539,prsc:2;n:type:ShaderForge.SFN_LightVector,id:4523,x:31166,y:32729,varname:node_4523,prsc:2;n:type:ShaderForge.SFN_Dot,id:3906,x:31360,y:32782,varname:node_3906,prsc:2,dt:1|A-4523-OUT,B-3697-OUT;n:type:ShaderForge.SFN_NormalVector,id:3697,x:31166,y:32852,prsc:2,pt:True;n:type:ShaderForge.SFN_Multiply,id:5530,x:31553,y:32782,varname:node_5530,prsc:2|A-3906-OUT,B-7539-OUT;n:type:ShaderForge.SFN_Step,id:4105,x:31782,y:32782,varname:node_4105,prsc:2|A-9655-OUT,B-5530-OUT;n:type:ShaderForge.SFN_Lerp,id:7176,x:32457,y:32833,varname:node_7176,prsc:2|A-3973-OUT,B-5127-RGB,T-4105-OUT;n:type:ShaderForge.SFN_Multiply,id:5011,x:31981,y:32390,varname:node_5011,prsc:2|A-5127-RGB,B-4191-OUT;n:type:ShaderForge.SFN_Step,id:9859,x:31782,y:32912,varname:node_9859,prsc:2|A-607-OUT,B-5530-OUT;n:type:ShaderForge.SFN_Multiply,id:6446,x:32019,y:32912,varname:node_6446,prsc:2|A-9859-OUT,B-6567-RGB;n:type:ShaderForge.SFN_Add,id:9055,x:32672,y:32833,varname:node_9055,prsc:2|A-7176-OUT,B-2014-OUT;n:type:ShaderForge.SFN_LightColor,id:6567,x:31502,y:32339,varname:node_6567,prsc:2;n:type:ShaderForge.SFN_OneMinus,id:4191,x:31741,y:32339,varname:node_4191,prsc:2|IN-6567-RGB;n:type:ShaderForge.SFN_Lerp,id:3973,x:32281,y:32353,varname:node_3973,prsc:2|A-5127-RGB,B-5011-OUT,T-4605-OUT;n:type:ShaderForge.SFN_Lerp,id:2014,x:32253,y:32912,varname:node_2014,prsc:2|A-9406-OUT,B-6446-OUT,T-3611-OUT;n:type:ShaderForge.SFN_Vector1,id:9406,x:32019,y:33041,varname:node_9406,prsc:2,v1:0;n:type:ShaderForge.SFN_Tex2d,id:5127,x:31741,y:32144,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_5127,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Vector1,id:4605,x:31981,y:32522,varname:node_4605,prsc:2,v1:0.3;n:type:ShaderForge.SFN_Vector1,id:3611,x:31958,y:32676,varname:node_3611,prsc:2,v1:0.3;n:type:ShaderForge.SFN_Vector1,id:9655,x:31416,y:33150,varname:node_9655,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Vector1,id:607,x:31474,y:33227,varname:node_607,prsc:2,v1:0.9;proporder:5127;pass:END;sub:END;*/

Shader "Custom/SH_Cartoon" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
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
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_5530 = (max(0,dot(lightDirection,normalDirection))*attenuation);
                float node_9406 = 0.0;
                float3 finalColor = (lerp(lerp(_MainTex_var.rgb,(_MainTex_var.rgb*(1.0 - _LightColor0.rgb)),0.3),_MainTex_var.rgb,step(0.5,node_5530))+lerp(float3(node_9406,node_9406,node_9406),(step(0.9,node_5530)*_LightColor0.rgb),0.3));
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
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float node_5530 = (max(0,dot(lightDirection,normalDirection))*attenuation);
                float node_9406 = 0.0;
                float3 finalColor = (lerp(lerp(_MainTex_var.rgb,(_MainTex_var.rgb*(1.0 - _LightColor0.rgb)),0.3),_MainTex_var.rgb,step(0.5,node_5530))+lerp(float3(node_9406,node_9406,node_9406),(step(0.9,node_5530)*_LightColor0.rgb),0.3));
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
