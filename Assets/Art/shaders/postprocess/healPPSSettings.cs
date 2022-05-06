// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>
#if UNITY_POST_PROCESSING_STACK_V2
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess( typeof( healPPSRenderer ), PostProcessEvent.AfterStack, "heal", true )]
public sealed class healPPSSettings : PostProcessEffectSettings
{
	[Tooltip( "lifeColorForce" )]
	public FloatParameter _lifeColorForce = new FloatParameter { value = 1f };
	[Tooltip( "heal_texture" )]
	public TextureParameter _heal_texture = new TextureParameter {  };
	[Tooltip( "SpeedAndDirection" )]
	public Vector4Parameter _SpeedAndDirection = new Vector4Parameter { value = new Vector4(0f,-0.1f,0f,0f) };
	[Tooltip( "elipse2Force" )]
	public FloatParameter _elipse2Force = new FloatParameter { value = 0.5f };
	[Tooltip( "elipse1W" )]
	public FloatParameter _elipse1W = new FloatParameter { value = 1.1f };
	[Tooltip( "elipse1H" )]
	public FloatParameter _elipse1H = new FloatParameter { value = 1f };
	[Tooltip( "elipse2H" )]
	public FloatParameter _elipse2H = new FloatParameter { value = 1f };
	[Tooltip( "Color " )]
	public ColorParameter _Color = new ColorParameter { value = new Color(0.2210751f,0.735849f,0.2047882f,0f) };
	[Tooltip( "elipse2W" )]
	public FloatParameter _elipse2W = new FloatParameter { value = 1.3f };
}

public sealed class healPPSRenderer : PostProcessEffectRenderer<healPPSSettings>
{
	public override void Render( PostProcessRenderContext context )
	{
		var sheet = context.propertySheets.Get( Shader.Find( "heal" ) );
		sheet.properties.SetFloat( "_lifeColorForce", settings._lifeColorForce );
		if(settings._heal_texture.value != null) sheet.properties.SetTexture( "_heal_texture", settings._heal_texture );
		sheet.properties.SetVector( "_SpeedAndDirection", settings._SpeedAndDirection );
		sheet.properties.SetFloat( "_elipse2Force", settings._elipse2Force );
		sheet.properties.SetFloat( "_elipse1W", settings._elipse1W );
		sheet.properties.SetFloat( "_elipse1H", settings._elipse1H );
		sheet.properties.SetFloat( "_elipse2H", settings._elipse2H );
		sheet.properties.SetColor( "_Color", settings._Color );
		sheet.properties.SetFloat( "_elipse2W", settings._elipse2W );
		context.command.BlitFullscreenTriangle( context.source, context.destination, sheet, 0 );
	}
}
#endif
