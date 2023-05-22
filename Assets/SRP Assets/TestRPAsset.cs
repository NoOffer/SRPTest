using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Rendering/TestRP/TestRenderPipelineAsset")]
public class TestRPAsset : RenderPipelineAsset
{
    [SerializeField] private bool dynamicBatching;
    [SerializeField] private bool instancing;

    protected override RenderPipeline CreatePipeline()
    {
        return new TestPipelineInstance(dynamicBatching, instancing);
    }
}
