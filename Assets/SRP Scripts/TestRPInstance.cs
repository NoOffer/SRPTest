using UnityEngine;
using UnityEngine.Rendering;

public class TestPipelineInstance : RenderPipeline
{
    public TestPipelineInstance()
    {
    }

    protected override void Render(ScriptableRenderContext renderContext, Camera[] cameras)
    {
        foreach (Camera camera in cameras)
        {
            Render(renderContext, camera);
        }

        // Draw skybox
        renderContext.DrawSkybox(cameras[0]);

        // Submit buffered outputs
        renderContext.Submit();
    }

    private void Render(ScriptableRenderContext renderContext, Camera camera)
    {
        // Apply camera settings to correctly setup the context
        renderContext.SetupCameraProperties(camera);

        // Create a command buffer
        CommandBuffer buffer = new CommandBuffer();

        // Setup commands
        buffer.ClearRenderTarget(
            (camera.clearFlags & CameraClearFlags.Depth) != 0,
            (camera.clearFlags & CameraClearFlags.Color) != 0,
            camera.backgroundColor
        );

        // Execute and release the command buffer
        renderContext.ExecuteCommandBuffer(buffer);
        buffer.Release();
    }
}
