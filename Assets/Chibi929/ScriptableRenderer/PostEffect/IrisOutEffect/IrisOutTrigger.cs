using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chibi929
{
  public class IrisOutTrigger : PostEffectTrigger
  {
    [Header("シェーダー適用済みのマテリアル")]
    [SerializeField]
    private Material _mat;

    protected override void OnUpdateAnimation(float value)
    {
      _mat.SetFloat("_Threshold", value);
    }
  }
}
