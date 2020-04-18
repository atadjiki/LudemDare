using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CharacterBillboardController : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }
}
