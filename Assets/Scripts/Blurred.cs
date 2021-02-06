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
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
