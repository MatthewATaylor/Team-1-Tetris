using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(BlurredRenderer), PostProcessEvent.AfterStack, "Custom/Blurred")]
public sealed class Blurred : PostProcessEffectSettings
{
    [Range(0, 150)]
    public IntParameter maxOffset = new IntParameter { value = 0 };

    [Range(0.0f, 1.0f)]
    public FloatParameter boardLeft = new FloatParameter { value = 0.1f };

    [Range(0.0f, 1.0f)]
    public FloatParameter boardRight = new FloatParameter { value = 0.4f };

    [Range(0.0f, 1.0f)]
    public FloatParameter boardTop = new FloatParameter { value = 0.8f };

    [Range(0.0f, 1.0f)]
    public FloatParameter boardBottom = new FloatParameter { value = 0.2f };

    [Range(0.0f, 10000.0f)]
    public FloatParameter screenWidth = new FloatParameter { value = 800.0f };

    [Range(0.0f, 10000.0f)]
    public FloatParameter screenHeight = new FloatParameter { value = 600.0f };
}

public sealed class BlurredRenderer : PostProcessEffectRenderer<Blurred>
{
    public override void Render(PostProcessRenderContext context)
    {
        PropertySheet sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Blurred"));
        sheet.properties.SetInt("maxOffset", settings.maxOffset);
        sheet.properties.SetFloat("boardLeft", settings.boardLeft);
        sheet.properties.SetFloat("boardRight", settings.boardRight);
        sheet.properties.SetFloat("boardTop", settings.boardTop);
        sheet.properties.SetFloat("boardBottom", settings.boardBottom);
        sheet.properties.SetFloat("screenWidth", settings.screenWidth);
        sheet.properties.SetFloat("screenHeight", settings.screenHeight);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
