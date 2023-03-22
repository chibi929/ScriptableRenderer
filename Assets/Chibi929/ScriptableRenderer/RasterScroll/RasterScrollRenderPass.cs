using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Chibi929
{
  public class RasterScrollRenderPass : ScriptableRenderPass
  {
    private const string COMMAND_BUFFER_NAME = nameof(RasterScrollRenderPass);
    private const int RENDER_TEXTURE_ID = 1;

    private readonly Material _mat;
    private RenderTargetIdentifier _currentTarget;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="renderPassEvent"></param>
    /// <param name="mat"></param>
    public RasterScrollRenderPass(RenderPassEvent renderPassEvent, Material mat)
    {
      this.renderPassEvent = renderPassEvent;
      _mat = mat;
    }

    /// <summary>
    /// 描画に必要なデータを設定する
    /// </summary>
    /// <param name="renderTarget"></param>
    /// <param name="speed"></param>
    /// <param name="amplitude"></param>
    /// <param name="frequency"></param>
    /// <param name="fillBlackOutSide"></param>
    public void SetParams(RenderTargetIdentifier renderTarget, float speed, float amplitude, float frequency, bool fillBlackOutSide)
    {
      _currentTarget = renderTarget;
      _mat.SetFloat("_Speed", speed);
      _mat.SetFloat("_Amplitude", amplitude);
      _mat.SetFloat("_Frequency", frequency);
      _mat.SetFloat("_FillBlackOutSide", Convert.ToSingle(fillBlackOutSide));
    }

    /// <summary>
    /// 描画処理
    /// </summary>
    /// <param name="context"></param>
    /// <param name="renderingData"></param>
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
      var cmdBuffer = CommandBufferPool.Get(COMMAND_BUFFER_NAME);

      var descriptor = renderingData.cameraData.cameraTargetDescriptor;
      descriptor.depthBufferBits = 0;

      cmdBuffer.GetTemporaryRT(RENDER_TEXTURE_ID, descriptor);
      cmdBuffer.Blit(_currentTarget, RENDER_TEXTURE_ID, _mat);
      cmdBuffer.Blit(RENDER_TEXTURE_ID, _currentTarget);
      cmdBuffer.ReleaseTemporaryRT(RENDER_TEXTURE_ID);

      context.ExecuteCommandBuffer(cmdBuffer);
      context.Submit();
      CommandBufferPool.Release(cmdBuffer);
    }
  }
}
