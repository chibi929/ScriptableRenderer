using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Chibi929
{
  public class MosaicRenderPass : ScriptableRenderPass
  {
    private const string COMMAND_BUFFER_NAME = nameof(MosaicRenderPass);
    private const int RENDER_TEXTURE_ID = 0;

    private RenderTargetIdentifier _currentTarget;
    private int _downSample = 1;

    /// <summary>
    /// コンストラクタ (描画タイミングを決める)
    /// </summary>
    /// <param name="renderPassEvent"></param>
    public MosaicRenderPass(RenderPassEvent renderPassEvent)
    {
      this.renderPassEvent = renderPassEvent;
    }

    /// <summary>
    /// 描画に必要なデータを設定する
    /// </summary>
    /// <param name="renderTarget"></param>
    /// <param name="downSample"></param>
    public void SetParams(RenderTargetIdentifier renderTarget, int downSample)
    {
      _currentTarget = renderTarget;
      _downSample = Mathf.Abs(downSample);
    }

    /// <summary>
    /// 描画処理
    /// </summary>
    /// <param name="context"></param>
    /// <param name="renderingData"></param>
    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
      // CommandBuffer 作成
      var cmdBuffer = CommandBufferPool.Get(COMMAND_BUFFER_NAME);

      // レンダリングデータからカメラを取得
      var camera = renderingData.cameraData.camera;
      // 現在描画しているカメラの解像度をダウンサンプリングする
      var downSample = _downSample <= 0 ? 1 : _downSample;
      var w = camera.scaledPixelWidth / downSample;
      var h = camera.scaledPixelHeight / downSample;

      // 小さいサイズの RenderTexture を生成
      cmdBuffer.GetTemporaryRT(RENDER_TEXTURE_ID, w, h, 0, FilterMode.Point, RenderTextureFormat.Default);

      // 現在のカメラ画像を RenderTexture にコピー
      cmdBuffer.Blit(_currentTarget, RENDER_TEXTURE_ID);

      // 作った RenderTexture を現在のカメラ画像にコピー
      cmdBuffer.Blit(RENDER_TEXTURE_ID, _currentTarget);

      // 小さいサイズの RenderTexture を解放
      cmdBuffer.ReleaseTemporaryRT(RENDER_TEXTURE_ID);

      // 一連の処理を実行して CommandBuffer 削除
      context.ExecuteCommandBuffer(cmdBuffer);
      context.Submit();
      CommandBufferPool.Release(cmdBuffer);
    }
  }
}
