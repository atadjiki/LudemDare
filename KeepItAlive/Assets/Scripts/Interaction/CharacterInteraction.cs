using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public string text;
    public float time;
}

public class CharacterInteraction : Interactable
{

    public List<DialogueLine> GeneralDialogue;

    private int currentIndex;
    private int maxIndex;

    private Vector3 Beer_Pos = new Vector3(-0.3f, -0.4f, 0);
    private Quaternion Beer_Rot = new Quaternion(2, 45, 0, 1);
    private Vector3 Beer_Scale = new Vector3(0.25f, 0.25f, 0.25f);

    private bool HasBeer;

    private void Awake()
    {
        HasBeer = false;
        currentIndex = 0;
        maxIndex = GeneralDialogue.Count;

        Beer_Rot = new Quaternion(Beer_Rot.x, UnityEngine.Random.Range(0, 360), Beer_Rot.z, 1);
    }

    public override void Interact()
    {
        base.Interact();

        if(currentIndex >= 0 && currentIndex < maxIndex && GeneralDialogue.Count > 0)
        {
            UIManager.Instance.PresentSubtitles(GeneralDialogue[currentIndex].text, GeneralDialogue[currentIndex].time);
        }

        IncrementIndex();
    }

    private void IncrementIndex()
    {
        currentIndex++;

        if(currentIndex >= maxIndex)
        {
            currentIndex = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Got Beer");

        if (other.gameObject.GetComponent<Beer>() != null && HasBeer == false)
        {
            
            PlayerController.Instance.Release();

            TakeBeer(other.gameObject);
        }
    }

    public void TakeBeer(GameObject beer)
    {
        HasBeer = true;

        beer.GetComponent<Beer>().enabled = false;
        beer.GetComponent<Rigidbody>().isKinematic = true;
        beer.GetComponent<Collider>().enabled = false;
        beer.transform.parent = this.transform;

        GameState.Instance.IncrementBeers();

        StartCoroutine(LerpBeerToHand(beer, 2));
    }

    IEnumerator LerpBeerToHand(GameObject beer, float time)
    {
        float currentTime = 0;

        while(currentTime < time)
        {
            beer.transform.localPosition = Vector3.Slerp(beer.transform.localPosition, Beer_Pos, currentTime / time);
            beer.transform.localScale = Vector3.Slerp(beer.transform.localScale, Beer_Scale, currentTime / time);
            beer.transform.localRotation = Quaternion.Slerp(beer.transform.localRotation, Beer_Rot, currentTime / time);

            currentTime += Time.smoothDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}
