using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : Interactable
{
    public override void Interact()
    {
        base.Interact();

        Debug.Log(gameObject.name + " is grabbed");
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

}
