using UnityEngine;
using UnityEngine.Rendering;
using Conditional = System.Diagnostics.ConditionalAttribute;

public class TestPipelineInstance : RenderPipeline
{
    private Material errorMaterial;

    public TestPipelineInstance()
    {
        Shader errorShader = Shader.Find("Hidden/InternalErrorShader");
        errorMaterial = new Material(errorShader)
        {
            hideFlags = HideFlags.HideAndDontSave
        };
    }

    protected override void Render(ScriptableRenderContext renderContext, Camera[] cameras)
    {
        foreach (Camera camera in cameras)
        {
            Render(renderContext, camera);
        }
    }

    private void Render(ScriptableRenderContext renderContext, Camera camera)
    {
        // Apply camera settings to correctly setup the context
        renderContext.SetupCameraProperties(camera);

        // Create command buffer
        CommandBuffer buffer = new CommandBuffer
        {
            name = "Command Buffer"
        };

        // Setup commands
        buffer.ClearRenderTarget(
            (camera.clearFlags & CameraClearFlags.Depth) != 0,
            (camera.clearFlags & CameraClearFlags.Color) != 0,
            camera.backgroundColor
        );

        // Execute and release the command buffer
        renderContext.ExecuteCommandBuffer(buffer);
        buffer.Release();

        // Fix scene view UI
#if UNITY_EDITOR
        if (camera.cameraType == CameraType.SceneView)
        {
            ScriptableRenderContext.EmitWorldGeometryForSceneView(camera);
        }
#endif

        // Culling process
        ScriptableCullingParameters cullingParameters;
        if (!camera.TryGetCullingParameters(out cullingParameters))
        {
            return;
        }
        CullingResults cullingResults = renderContext.Cull(ref cullingParameters);

        // Drawing phase
        // Setup render settings
        DrawingSettings drawSettings = new DrawingSettings(new ShaderTagId("ForwardBase"), new SortingSettings());

        FilteringSettings filterSettings = FilteringSettings.defaultValue;

        // Draw error materials
        drawSettings.overrideMaterial = errorMaterial;
        renderContext.DrawRenderers(
            cullingResults, ref drawSettings, ref filterSettings
        );

        drawSettings = new DrawingSettings(new ShaderTagId("SRPDefaultUnlit"), new SortingSettings());
        // Draw Opaque
        filterSettings.renderQueueRange = RenderQueueRange.opaque;
        renderContext.DrawRenderers(
            cullingResults, ref drawSettings, ref filterSettings
        );

        // Draw skybox
        renderContext.DrawSkybox(camera);

        // Draw transparent
        filterSettings.renderQueueRange = RenderQueueRange.transparent;
        renderContext.DrawRenderers(
            cullingResults, ref drawSettings, ref filterSettings
        );

        // Submit buffered outputs
        renderContext.Submit();
    }
}
