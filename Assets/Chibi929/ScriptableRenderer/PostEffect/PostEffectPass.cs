using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Chibi929
{
  public class PostEffectPass : ScriptableRenderPass
  {
    private const string COMMAND_BUFFER_NAME = nameof(PostEffectPass);

    private Material _mat;

    public PostEffectPass(RenderPassEvent renderPassEvent, Material mat)
    {
      this.renderPassEvent = renderPassEvent;
      _mat = mat;
    }

    /// <summary>
    /// 描画処理
    /// </summary>
    /// <param name="context"></param>
    /// <param name="renderingData"></param>
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
      if (!_mat)
      {
        return;
      }

      var cmd = CommandBufferPool.Get(COMMAND_BUFFER_NAME);
      Blit(cmd, ref renderingData, _mat, 0);
      context.ExecuteCommandBuffer(cmd);
      CommandBufferPool.Release(cmd);
    }
  }
}
