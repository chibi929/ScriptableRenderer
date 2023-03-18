using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chibi929
{
  public class MosaicAnimation : MonoBehaviour
  {
    [SerializeField]
    private float _amplitude = 30.0f;

    [SerializeField]
    private float _speed = 1.0f;

    [SerializeField]
    private MosaicRenderFeature _feature;

    private void Update()
    {
      var val = _amplitude * Mathf.Sin(Time.time * _speed);
      _feature.DownSample = (int)val;
    }
  }
}
