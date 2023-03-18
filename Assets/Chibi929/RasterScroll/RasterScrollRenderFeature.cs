using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Chibi929
{
  public class RasterScrollRenderFeature : ScriptableRendererFeature
  {
    [SerializeField]
    private Shader _shader;

    [Header("周期")]
    [SerializeField]
    private float _speed = 1.5f;
    public float Speed { set { _speed = value; } }

    [Header("振幅")]
    [SerializeField]
    private float _amplitude = 0.4f;
    public float Amplitude { set { _amplitude = value; } }

    [Header("周波数")]
    [SerializeField]
    private float _frequency = 2.5f;
    public float Frequency { set { _frequency = value; } }

    [Header("はみ出た部分を黒く塗るかどうか")]
    [SerializeField]
    private bool _fillBlackOutSide = true;
    public bool FillBlackOutSide { set { _fillBlackOutSide = value; } }

    /// <summary>
    ///
    /// </summary>
    private RasterScrollRenderPass _pass;

    public override void Create()
    {
      var mat = CoreUtils.CreateEngineMaterial(_shader);
      _pass = new RasterScrollRenderPass(RenderPassEvent.AfterRenderingTransparents, mat);
    }


    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
      _pass.SetParams(renderer.cameraColorTarget, _speed, _amplitude, _frequency, _fillBlackOutSide);
      renderer.EnqueuePass(_pass);
    }
  }
}
