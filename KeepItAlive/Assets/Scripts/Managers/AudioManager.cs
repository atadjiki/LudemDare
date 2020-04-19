using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }

    public AudioClip beer;
    public AudioClip objectPickUp;
    public AudioClip objectPutDown;
    public AudioClip entry;
    public AudioClip fridge;
    public AudioClip cassetteDeck;
    public AudioClip button;

    public AudioClip[] Voices;

    public AudioSource audioSource;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }

    public AudioClip GetVoice(Constants.Dialogue.Character character)
    {
        if(character == Constants.Dialogue.Character.One)
        {
            return Voices[0];
        }
        else if(character == Constants.Dialogue.Character.Two)
        {
            return Voices[1];
        }
        else
        {
            return Voices[0];
        }
    }

}
