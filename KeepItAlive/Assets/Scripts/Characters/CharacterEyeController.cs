using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEyeController : MonoBehaviour
{
    public SpriteRenderer _eyesOpen;
    public SpriteRenderer _eyesClosed;
    public float _minWaitForBlink;
    public float _maxWaitForBlink;
    public float _blinkTime;

    private void Start()
    {
        _eyesOpen.enabled = true;
        _eyesClosed.enabled = false;
        StartCoroutine(CountdownToBlink());
    }

    private IEnumerator CountdownToBlink()
    {
        float timer = Random.Range(_minWaitForBlink, _maxWaitForBlink);
        yield return new WaitForSeconds(timer);
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        _eyesOpen.enabled = false;
        _eyesClosed.enabled = true;
        yield return new WaitForSeconds(_blinkTime);
        _eyesOpen.enabled = true;
        _eyesClosed.enabled = false;

        StartCoroutine(CountdownToBlink());
    }
}
