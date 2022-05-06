// Amplify Shader Editor - Visual Shader Editing Tool
// Copyright (c) Amplify Creations, Lda <info@amplify.pt>
#if UNITY_POST_PROCESSING_STACK_V2
using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess( typeof( damagePPSRenderer ), PostProcessEvent.AfterStack, "damage", true )]
public sealed class damagePPSSettings : PostProcessEffectSettings
{
	[Tooltip( "CorruptionIntensity" )]
	public FloatParameter _CorruptionIntensity = new FloatParameter { value = 1f };
}

public sealed class damagePPSRenderer : PostProcessEffectRenderer<damagePPSSettings>
{
	public override void Render( PostProcessRenderContext context )
	{
		var sheet = context.propertySheets.Get( Shader.Find( "damage" ) );
		sheet.properties.SetFloat( "_CorruptionIntensity", settings._CorruptionIntensity );
		context.command.BlitFullscreenTriangle( context.source, context.destination, sheet, 0 );
	}
}
#endif
