using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpinStereoSpeakers : MonoBehaviour
{
    public float _animationTime = 0.3f;
    public AnimationCurve _animationCurve;
    public float _bumpinSize = 1.5f;
    public ParticleSystem _soundBolts;

    private void Start()
    {
        StartCoroutine(Bump());
    }

    private IEnumerator Bump()
    {
        float timer = 0f;
        _soundBolts.Play();
        while (timer < _animationTime)
        {
            timer += Time.deltaTime;
            float normalizedTime = timer / _animationTime;
            float scale = (_animationCurve.Evaluate(normalizedTime) * _bumpinSize) + 1f;
            transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        StartCoroutine(Bump());
    }
}
