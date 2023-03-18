using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Chibi929
{
  public class MosaicRenderFeature : ScriptableRendererFeature
  {
    [SerializeField]
    private int _downSample = 10;
    public int DownSample { set { _downSample = value; } }

    private MosaicRenderPass _pass;

    public override void Create()
    {
      _pass = new MosaicRenderPass(RenderPassEvent.AfterRenderingTransparents);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
      _pass.SetParams(renderer.cameraColorTarget, _downSample);
      renderer.EnqueuePass(_pass);
    }
  }
}
