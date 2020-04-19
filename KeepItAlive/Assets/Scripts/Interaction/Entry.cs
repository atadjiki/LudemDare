using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entry : Interactable
{
    public GameObject Hinge;
    public Transform LerpTo;
    public Transform LerpFrom;
    private bool Lerping;
    private bool InteractedWith;

    public float openAngle = -120f;

    public float doorTime = 5;
    public float playerTime = 2;
    public float positioningTime = 1.0f;


    public float playerLerpDelay = 1;

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public override void Interact()
    {
        if (InteractedWith)
        {
            return;
        }

        base.Interact();

        InteractedWith = true;

        MovePlayerIntoParty();

        audioSource.PlayOneShot(AudioManager.Instance.entry);

        GameState.Instance.CurrentPhase = GameState.Phase.Game;
    }

    private void MovePlayerIntoParty()
    {
       PlayerController.Instance.ToggleControls(false);

        StartCoroutine(DoEntrySequence());
    }

    IEnumerator DoEntrySequence()
    {
        StartCoroutine(LerpPlayerToStartingPoint());
        yield return new WaitForSeconds(positioningTime);
        StartCoroutine(DoDoorAnim());
        StartCoroutine(DoPlayerAnim());
        StartCoroutine(LockWaitControls());
        StartCoroutine(DoDoorCloseAnim());

    }

    IEnumerator LerpPlayerToStartingPoint()
    {
        float currentTime = 0;
        Vector3 initialPosition = PlayerController.Instance.transform.position;
        Quaternion initialRotation = PlayerController.Instance.transform.rotation;

        while (currentTime < positioningTime)
        {
            PlayerController.Instance.transform.position
                = Vector3.Slerp(initialPosition, LerpFrom.position, (currentTime / positioningTime));

            PlayerController.Instance.transform.rotation
                = Quaternion.Slerp(initialRotation, LerpFrom.rotation, currentTime / positioningTime);

            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator DoDoorAnim()
    {
        float currentTime = 0;
        float initialAngle = Hinge.transform.localEulerAngles.y;

        while (currentTime < doorTime)
        {

            Hinge.transform.localEulerAngles = new Vector3(0, Mathf.LerpAngle(initialAngle, openAngle, currentTime/doorTime), 0);

            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator DoDoorCloseAnim()
    {
        yield return new WaitForSeconds(doorTime);
        float currentTime = 0;
        float initialAngle = Hinge.transform.localEulerAngles.y;

        while (currentTime < doorTime)
        {

            Hinge.transform.localEulerAngles = new Vector3(0, Mathf.LerpAngle(initialAngle, 0, currentTime / doorTime), 0);

            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator DoPlayerAnim()
    {
        float currentTime = 0;
        Vector3 initialPosition = PlayerController.Instance.transform.position;

        while (currentTime < playerTime)
        {
            PlayerController.Instance.transform.position
                = Vector3.Slerp(initialPosition, LerpTo.position, (currentTime/playerTime));

            currentTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator LockWaitControls()
    {
        yield return new WaitForSeconds(playerLerpDelay);

        PlayerController.Instance.ToggleControls(true);

    }
}
