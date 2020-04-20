using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;
using TMPro;

[System.Serializable]
public class DialogueLine
{
    public string text;
    public float time;
}

public class CharacterInteraction : Interactable
{
    public Dialogue.Character Character;
    public Dialogue.Category CurrentCategory;
    
    private Vector3 Beer_Pos = new Vector3(-0.3f, -0.4f, 0);
    private Quaternion Beer_Rot = new Quaternion(2, 45, 0, 1);
    private Vector3 Beer_Scale = new Vector3(0.25f, 0.25f, 0.25f);

    private bool HasBeer;
    private bool Talking;

    public GameObject SpeechBubble;
    public TextMeshProUGUI SpeechText;

    private AudioSource audioSource;

    public float takeBeerTime = 2;

    private void Awake()
    {
        HasBeer = false;

        Beer_Rot.eulerAngles = new Vector3(0,UnityEngine.Random.Range(0, 360),0);

        audioSource = GetComponent<AudioSource>();

        Character = GameState.Instance.RegisterCharacter();
    }

    public override void Interact()
    {
        base.Interact();

        PresentDialogue(Dialogue.GetStringByCharacter(Character, GetTypeFromState()), 2);

        CurrentCategory = Constants.Dialogue.GetNext(CurrentCategory);

        if(CurrentCategory == Dialogue.Category.MusicTaste && GameState.Instance.IsMusicPlaying() == false)
        {
            CurrentCategory = Constants.Dialogue.GetNext(CurrentCategory);
        }

        if(audioSource.isPlaying == false)
        {
            audioSource.PlayOneShot(AudioManager.Instance.GetVoice(Character));
        }
        
    }

    public Dialogue.Type GetTypeFromState()
    {
        if(CurrentCategory == Dialogue.Category.Greeting)
        {
            return Dialogue.Type.Greeting;
        }
        else if(CurrentCategory == Dialogue.Category.Beer)
        {
            if(HasBeer)
            {
                return Dialogue.Type.HasBeer;
            }
            else
            {
                return Dialogue.Type.NeedsBeer;
            }
        }
        else if(CurrentCategory == Dialogue.Category.Music)
        {
            if(GameState.Instance.IsMusicPlaying())
            {
                return Dialogue.Type.MusicPlaying;
            }
            else
            {
                return Dialogue.Type.NoMusicPlaying;
            }
        }
        else if(CurrentCategory == Dialogue.Category.MusicTaste)
        {
            if(GameState.Instance.IsSongCorrect())
            {
                return Dialogue.Type.LikesMusic;
            }
            else
            {
                return Dialogue.Type.DislikesMusic;
            }
        }
        else if(CurrentCategory == Dialogue.Category.Misc)
        {
            return Dialogue.Type.Misc;
        }
        else
        {
            return 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Beer>() != null && HasBeer == false)
        {
            
            PlayerController.Instance.Release();

            TakeBeer(other.gameObject);

            audioSource.PlayOneShot(AudioManager.Instance.beer);

        }
    }

    public void TakeBeer(GameObject beer)
    {
        HasBeer = true;

        CurrentCategory = Dialogue.Category.Beer;

        beer.GetComponent<Beer>().enabled = false;
        beer.GetComponent<Rigidbody>().isKinematic = true;
        beer.GetComponent<Collider>().enabled = false;
        beer.transform.parent = this.transform;

        GameState.Instance.IncrementBeers();

        StartCoroutine(LerpBeerToHand(beer, takeBeerTime));
    }

    IEnumerator LerpBeerToHand(GameObject beer, float time)
    {
        float currentTime = 0;
        Vector3 initial_pos = beer.transform.localPosition;
        Vector3 initial_scale = beer.transform.localScale;
        Quaternion initial_rot = beer.transform.rotation;

        while(currentTime < time)
        {
            beer.transform.localPosition = Vector3.Slerp(initial_pos, Beer_Pos, currentTime / time);
            beer.transform.localScale = Vector3.Slerp(initial_scale, Beer_Scale, currentTime / time);
            beer.transform.localRotation = Quaternion.Slerp(initial_rot, Beer_Rot, currentTime / time);

            currentTime += Time.smoothDeltaTime;
            yield return new WaitForFixedUpdate();
        }
    }

    public void PresentDialogue(string text, float time)
    {
        if (Talking == false)
        {
            StartCoroutine(ShowDialogueForTime(text, time));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(ShowDialogueForTime(text, time));
        }

    }

    private IEnumerator ShowDialogueForTime(string text, float time)
    {
        Talking = true;
        SpeechText.text = text;
        SpeechBubble.SetActive(true);
        yield return new WaitForSeconds(time);
        SpeechBubble.SetActive(false);
        SpeechText.text = "";
        Talking = false;
    }
}