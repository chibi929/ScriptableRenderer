using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chibi929
{
  [Serializable]
  public struct SinParameter
  {
    public float speed;
    public float amplitude;
    public float frequency;
    public bool fillBlackOutSide;
  }

  public class RasterScrollTrigger : MonoBehaviour
  {
    [SerializeField]
    RasterScrollRenderFeature _feature;

    [Header("正弦波用のパラメーター")]
    [SerializeField]
    SinParameter _sinParameter = new SinParameter
    {
      speed = 1.5f,
      amplitude = 0.4f,
      frequency = 2.5f,
      fillBlackOutSide = true
    };

    [Header("アニメーション時間")]
    [SerializeField]
    private float _duration = 5.0f;

    /// <summary>
    ///
    /// </summary>
    private float _elapsedTime = 0;
    private bool _isAnimation = false;

    private void Update()
    {
      if (Input.GetMouseButtonDown(0))
      {
        _isAnimation = true;
        _elapsedTime = 0;
      }
      UpdateRasterScrollAnimation();
    }

    private void UpdateRasterScrollAnimation()
    {
      if (!_isAnimation)
      {
        _feature.Amplitude = 0;
        return;
      }

      // 経過時間を加算
      _elapsedTime += Time.deltaTime;

      // _duration を正規化する
      var normalizedTime = _elapsedTime / _duration;

      // 時間経過と共に振り幅を大きくする
      var amplitude = Mathf.Lerp(0.0f, _sinParameter.amplitude, normalizedTime);

      // エフェクトパラメータ設定
      _feature.Speed = _sinParameter.speed;
      _feature.Amplitude = amplitude;
      _feature.Frequency = _sinParameter.frequency;
      _feature.FillBlackOutSide = _sinParameter.fillBlackOutSide;

      if (normalizedTime > 1.0)
      {
        _isAnimation = false;
      }
    }
  }
}
