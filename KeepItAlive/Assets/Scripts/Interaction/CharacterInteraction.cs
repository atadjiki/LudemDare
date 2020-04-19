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

    public List<DialogueLine> Dialogue;
    private int currentIndex;
    private int maxIndex;

    private void Awake()
    {
        currentIndex = 0;
        maxIndex = Dialogue.Count;
    }

    public override void Interact()
    {
        base.Interact();

        if(currentIndex >= 0 && currentIndex < maxIndex && Dialogue.Count > 0)
        {
            UIManager.Instance.PresentSubtitles(Dialogue[currentIndex].text, Dialogue[currentIndex].time);
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
        if(other.gameObject.GetComponent<Beer>() != null)
        {
            PlayerController.Instance.Release();
        }
    }
}
