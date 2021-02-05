using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(PixelatedRenderer), PostProcessEvent.AfterStack, "Custom/Pixelated")]
public sealed class Pixelated : PostProcessEffectSettings
{
    [Range(0.0f, 200.0f)]
    public FloatParameter numPixels = new FloatParameter { value = 75.0f };
}

public sealed class PixelatedRenderer : PostProcessEffectRenderer<Pixelated>
{
    public override void Render(PostProcessRenderContext context)
    {
        PropertySheet sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/Pixelated"));
        sheet.properties.SetFloat("numPixels", settings.numPixels);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
