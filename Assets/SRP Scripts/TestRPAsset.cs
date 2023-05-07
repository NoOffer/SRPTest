using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Rendering/TestRP/TestRenderPipelineAsset")]
public class TestRPAsset : RenderPipelineAsset
{
    protected override RenderPipeline CreatePipeline()
    {
        return new TestPipelineInstance();
    }
}
