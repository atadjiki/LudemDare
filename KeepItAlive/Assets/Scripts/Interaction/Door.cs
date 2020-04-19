using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    private bool opened = false;
    private bool moving = false;
    public float lerpTime = 5f;

    private Transform DoorTransform;

    public float closedAngle = 0.0f;
    public float openAngle = -90.0f;

    AudioSource audioSource;

    private void Awake()
    {
        DoorTransform = this.transform.parent;
        audioSource = GetComponent<AudioSource>();
      
    }

    public override void Interact()
    {
        base.Interact();

        if (moving == false)
        {
            Toggle();
            audioSource.PlayOneShot(AudioManager.Instance.fridge);
           // AudioManager.Instance.PlaySoundEffect(this.gameObject, AudioManager.SoundEffect.Door);
        }
    }

    void Toggle()
    {
        if (opened)
        {
            StartCoroutine(RotateDoor(closedAngle));
        }
        else
        {
            StartCoroutine(RotateDoor(openAngle));
        }
    }

    IEnumerator RotateDoor(float targetAngle)
    {
        moving = true;
        float currentTime = 0;

        while (currentTime < lerpTime)
        {
            DoorTransform.localEulerAngles = new Vector3(0, Mathf.LerpAngle(DoorTransform.localEulerAngles.y, targetAngle, currentTime/lerpTime), 0);
            currentTime += Time.smoothDeltaTime;
            yield return new WaitForEndOfFrame();
        }

        DoorTransform.localEulerAngles = new Vector3(0, targetAngle, 0);

        if (opened)
        {
            opened = false;
        }
        else
        {
            opened = true;
        }

        moving = false;
    }

    public bool IsOpen()
    {
        return opened;
    }
}

