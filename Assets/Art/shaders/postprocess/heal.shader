// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "heal"
{
	Properties
	{
		_lifeColorForce("lifeColorForce", Float) = 1
		_heal_texture("heal_texture", 2D) = "black" {}
		_SpeedAndDirection("SpeedAndDirection", Vector) = (0,-0.1,0,0)
		_elipse2Force("elipse2Force", Float) = 0.5
		_elipse1W("elipse1W", Float) = 1.1
		_elipse1H("elipse1H", Float) = 1
		_elipse2H("elipse2H", Float) = 1
		_Color("Color ", Color) = (0.2210751,0.735849,0.2047882,0)
		_elipse2W("elipse2W", Float) = 1.3
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}

	SubShader
	{
		LOD 0

		Cull Off
		ZWrite Off
		ZTest Always
		
		Pass
		{
			CGPROGRAM

			

			#pragma vertex Vert
			#pragma fragment Frag
			#pragma target 3.0

			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"

		
			struct ASEAttributesDefault
			{
				float3 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
				
			};

			struct ASEVaryingsDefault
			{
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
				float2 texcoordStereo : TEXCOORD1;
			#if STEREO_INSTANCING_ENABLED
				uint stereoTargetEyeIndex : SV_RenderTargetArrayIndex;
			#endif
				
			};

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform half4 _MainTex_ST;
			
			uniform sampler2D _heal_texture;
			uniform float2 _SpeedAndDirection;
			uniform float _lifeColorForce;
			uniform float _elipse1W;
			uniform float _elipse1H;
			uniform float _elipse2W;
			uniform float _elipse2H;
			uniform float _elipse2Force;
			uniform float4 _Color;


			
			float2 TransformTriangleVertexToUV (float2 vertex)
			{
				float2 uv = (vertex + 1.0) * 0.5;
				return uv;
			}

			ASEVaryingsDefault Vert( ASEAttributesDefault v  )
			{
				ASEVaryingsDefault o;
				o.vertex = float4(v.vertex.xy, 0.0, 1.0);
				o.texcoord = TransformTriangleVertexToUV (v.vertex.xy);
#if UNITY_UV_STARTS_AT_TOP
				o.texcoord = o.texcoord * float2(1.0, -1.0) + float2(0.0, 1.0);
#endif
				o.texcoordStereo = TransformStereoScreenSpaceTex (o.texcoord, 1.0);

				v.texcoord = o.texcoordStereo;
				float4 ase_ppsScreenPosVertexNorm = float4(o.texcoordStereo,0,1);

				

				return o;
			}

			float4 Frag (ASEVaryingsDefault i  ) : SV_Target
			{
				float4 ase_ppsScreenPosFragNorm = float4(i.texcoordStereo,0,1);

				float2 uv_MainTex = i.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 color2 = IsGammaSpace() ? float4(0.522069,0.990566,0.4625756,0) : float4(0.2350394,0.9786729,0.1810219,0);
				float2 texCoord35 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner34 = ( _Time.y * _SpeedAndDirection + texCoord35);
				float2 texCoord53 = i.texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult11_g7 = (float2(_elipse1W , _elipse1H));
				float temp_output_17_0_g7 = length( ( (texCoord53*2.0 + -1.0) / appendResult11_g7 ) );
				float2 appendResult11_g5 = (float2(_elipse2W , _elipse2H));
				float temp_output_17_0_g5 = length( ( (i.texcoord.xy*2.0 + -1.0) / appendResult11_g5 ) );
				float temp_output_92_0 = ( saturate( ( ( 1.0 - temp_output_17_0_g5 ) / fwidth( temp_output_17_0_g5 ) ) ) * _elipse2Force );
				float2 appendResult11_g6 = (float2(1.5 , 1.0));
				float temp_output_17_0_g6 = length( ( (i.texcoord.xy*2.0 + -1.0) / appendResult11_g6 ) );
				float4 lerpResult39 = lerp( float4( 0,0,0,0 ) , ( tex2D( _heal_texture, panner34 ) * _lifeColorForce ) , ( 1.0 - saturate( ( saturate( ( ( 1.0 - temp_output_17_0_g7 ) / fwidth( temp_output_17_0_g7 ) ) ) + temp_output_92_0 + ( saturate( ( ( 1.0 - temp_output_17_0_g6 ) / fwidth( temp_output_17_0_g6 ) ) ) * 0.1 ) ) ) ));
				float4 lerpResult3 = lerp( tex2D( _MainTex, uv_MainTex ) , color2 , lerpResult39);
				float4 lerpResult97 = lerp( _Color , float4( 0,0,0,0 ) , (1.0 + (( 1.0 - temp_output_92_0 ) - 0.5) * (0.8 - 1.0) / (1.0 - 0.5)));
				

				float4 color = ( lerpResult3 + lerpResult97 );
				
				return color;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18900
1913;18;1920;1001;1620.595;-276.0821;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;110;-236.3853,1686.196;Inherit;False;Constant;_Float12;Float 7;2;0;Create;True;0;0;0;False;0;False;1.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;90;-106.9808,1266.763;Inherit;False;Property;_elipse2W;elipse2W;8;0;Create;True;0;0;0;False;0;False;1.3;1.3;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;89;-112.713,1353.272;Inherit;False;Property;_elipse2H;elipse2H;6;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;111;-238.6853,1603.497;Inherit;False;Constant;_Float13;Float 6;2;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;53;-444.8841,700.9357;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;76;-127.4866,932.456;Inherit;False;Property;_elipse1W;elipse1W;4;0;Create;True;0;0;0;False;0;False;1.1;1.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;112;164.0044,1975.607;Inherit;False;Constant;_Float14;Float 8;1;0;Create;True;0;0;0;False;0;False;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;113;19.44341,1639.787;Inherit;True;Ellipse;-1;;6;3ba94b7b3cfd5f447befde8107c04d52;0;3;2;FLOAT2;0,0;False;7;FLOAT;0.5;False;9;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;75;-98.78657,1094.756;Inherit;False;Property;_elipse1H;elipse1H;5;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;93;293.4089,1556.174;Inherit;False;Property;_elipse2Force;elipse2Force;3;0;Create;True;0;0;0;False;0;False;0.5;0.5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;91;148.8479,1220.354;Inherit;True;Ellipse;-1;;5;3ba94b7b3cfd5f447befde8107c04d52;0;3;2;FLOAT2;0,0;False;7;FLOAT;0.5;False;9;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;114;361.7618,1678.224;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;37;-1060.349,685.715;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;36;-1092.349,523.715;Inherit;False;Property;_SpeedAndDirection;SpeedAndDirection;2;0;Create;True;0;0;0;False;0;False;0,-0.1;0,-0.1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.FunctionNode;83;161.2172,952.5468;Inherit;True;Ellipse;-1;;7;3ba94b7b3cfd5f447befde8107c04d52;0;3;2;FLOAT2;0,0;False;7;FLOAT;0.5;False;9;FLOAT;0.5;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;92;491.1663,1258.791;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;35;-1091.349,317.715;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;94;741.7806,1079.538;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;34;-854.3488,475.715;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SaturateNode;95;854.0834,952.3976;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-235.5209,523.3719;Inherit;False;Property;_lifeColorForce;lifeColorForce;0;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;22;-545.3859,440.2232;Inherit;True;Property;_heal_texture;heal_texture;1;0;Create;True;0;0;0;False;0;False;-1;7c1e0d8e8a2e5d944ae382419a787d4a;7c1e0d8e8a2e5d944ae382419a787d4a;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;109;906.1243,1596.947;Inherit;False;Constant;_Float11;Float 11;1;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;102;769.3072,1294.617;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;4;-808.5827,-137.7379;Inherit;False;0;0;_MainTex;Shader;False;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;108;1153.672,1540.202;Inherit;False;Constant;_Float10;Float 10;1;0;Create;True;0;0;0;False;0;False;0.8;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-81.75363,316.534;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;107;783.4736,1552.694;Inherit;False;Constant;_Float9;Float 9;1;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;82;699.4172,688.0707;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;6;-543.2064,-141.4843;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;39;232.7032,440.0579;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;106;1065.672,1273.202;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;2;-519.197,231.0154;Inherit;False;Constant;_Color0;Color 0;0;0;Create;True;0;0;0;False;0;False;0.522069,0.990566,0.4625756,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;96;1069.102,934.2996;Inherit;False;Property;_Color;Color ;7;0;Create;True;0;0;0;False;0;False;0.2210751,0.735849,0.2047882,0;0.2210751,0.735849,0.2047882,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;97;1406.608,1049.681;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;3;1133.378,118.9773;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;100;1541.985,288.2734;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;72;-753.3838,717.2784;Inherit;False;Constant;_Float3;Float 3;2;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1;1818.349,63.90885;Float;False;True;-1;2;ASEMaterialInspector;0;2;heal;32139be9c1eb75640a847f011acf3bcf;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;True;7;False;-1;False;False;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;113;7;110;0
WireConnection;113;9;111;0
WireConnection;91;7;90;0
WireConnection;91;9;89;0
WireConnection;114;0;113;0
WireConnection;114;1;112;0
WireConnection;83;2;53;0
WireConnection;83;7;76;0
WireConnection;83;9;75;0
WireConnection;92;0;91;0
WireConnection;92;1;93;0
WireConnection;94;0;83;0
WireConnection;94;1;92;0
WireConnection;94;2;114;0
WireConnection;34;0;35;0
WireConnection;34;2;36;0
WireConnection;34;1;37;0
WireConnection;95;0;94;0
WireConnection;22;1;34;0
WireConnection;102;0;92;0
WireConnection;28;0;22;0
WireConnection;28;1;29;0
WireConnection;82;0;95;0
WireConnection;6;0;4;0
WireConnection;39;1;28;0
WireConnection;39;2;82;0
WireConnection;106;0;102;0
WireConnection;106;1;107;0
WireConnection;106;3;109;0
WireConnection;106;4;108;0
WireConnection;97;0;96;0
WireConnection;97;2;106;0
WireConnection;3;0;6;0
WireConnection;3;1;2;0
WireConnection;3;2;39;0
WireConnection;100;0;3;0
WireConnection;100;1;97;0
WireConnection;1;0;100;0
ASEEND*/
//CHKSM=A7A287E80418F5F52F031A5C4C8B9ACDA2C3888A