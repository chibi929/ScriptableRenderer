using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chibi929
{
  public class NegaPosiTrigger : MonoBehaviour
  {
    enum AnimationType
    {
      NONE,
      ZERO,
      ONE
    }

    [Header("シェーダー適用済みのマテリアル")]
    [SerializeField]
    private Material _mat;

    [Header("アニメーション時間")]
    [SerializeField]
    private float _duration = 5.0f;

    /// <summary>
    ///
    /// </summary>
    private float _elapsedTime = 0;
    private AnimationType _animationType = AnimationType.NONE;

    private void Update()
    {
      UpdateHandlingTrigger();
      UpdateRasterScrollAnimation();
    }

    private void UpdateHandlingTrigger()
    {
      if (Input.GetMouseButtonDown(0))
      {
        _animationType = AnimationType.ONE;
        _elapsedTime = 0;
      }
      else if (Input.GetMouseButtonDown(1))
      {
        _animationType = AnimationType.ZERO;
        _elapsedTime = 0;
      }
    }

    private void UpdateRasterScrollAnimation()
    {
      if (_animationType == AnimationType.NONE)
      {
        return;
      }

      // 経過時間を加算
      _elapsedTime += Time.deltaTime;

      // 経過時間を正規化する
      var normalizedTime = _elapsedTime / _duration;
      normalizedTime = _animationType == AnimationType.ONE ? normalizedTime : 1 - normalizedTime;

      // 時間経過と共に振り幅を大きくする
      var value = Mathf.Lerp(0.0f, 1.0f, normalizedTime);

      // エフェクトパラメータ設定
      _mat.SetFloat("_Strength", value);

      if (_elapsedTime >= _duration)
      {
        _animationType = AnimationType.NONE;
        _elapsedTime = 0;
      }
    }
  }
}
