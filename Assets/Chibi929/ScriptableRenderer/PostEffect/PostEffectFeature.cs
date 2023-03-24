using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Chibi929
{
  public class PostEffectFeature : ScriptableRendererFeature
  {
    [SerializeField]
    private Material _mat;

    PostEffectPass _pass;

    public override void Create()
    {
      _pass = new PostEffectPass(RenderPassEvent.AfterRenderingTransparents, _mat);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
      renderer.EnqueuePass(_pass);
    }
  }
}
