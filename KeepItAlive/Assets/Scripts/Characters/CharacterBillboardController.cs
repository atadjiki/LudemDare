using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBillboardController : MonoBehaviour
{
    private Vector3 _upVector;

    private void Start()
    {
        _upVector = transform.up;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position, _upVector);
    }
}
