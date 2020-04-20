using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandController : MonoBehaviour
{
    public SpriteRenderer _leftOpenHand;
    public SpriteRenderer _leftClosedHand;
    public SpriteRenderer _rightOpenHand;
    public SpriteRenderer _rightClosedHand;

    private void Start()
    {
        Release();
    }

    public void Grab()
    {
        _leftOpenHand.enabled = false;
        _leftClosedHand.enabled = true;
        _rightOpenHand.enabled = false;
        _rightClosedHand.enabled = true;
    }

    public void Release()
    {
        _leftOpenHand.enabled = true;
        _leftClosedHand.enabled = false;
        _rightOpenHand.enabled = true;
        _rightClosedHand.enabled = false;
    }
}
