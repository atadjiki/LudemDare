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

}
