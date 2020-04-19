﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MusicTrack
{
    public enum Track { Cassette, Cat, Lamp };
    public Track track;
    public AudioClip clip;
}

[RequireComponent(typeof(AudioSource))]
public class Boombox : Interactable
{

    private bool Enabled = false;

    public List<MusicTrack> Music;

    private int CurrentIndex;

    private void Awake()
    {
        
    }

    public override void Interact()
    {
        base.Interact();

        if (Enabled)
        {

            if(GetComponent<AudioSource>().mute)
            {
                GetComponent<AudioSource>().mute = false;
            }
            else
            {
                GetComponent<AudioSource>().mute = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Cassette>() != null)
        {
            PlayMusic(MusicTrack.Track.Cassette);
            PlayerController.Instance.Release();
            StartCoroutine(other.gameObject.GetComponent<Cassette>().DestroyAfterSeconds(0.25f));
            Enabled = true;
        }
    }

    public void PlayMusic(MusicTrack.Track Track)
    {
        foreach(MusicTrack track in Music)
        {
            if(track.track == Track)
            {
                this.GetComponent<AudioSource>().clip = track.clip;
                this.GetComponent<AudioSource>().Play();
                this.GetComponent<AudioSource>().loop = true;
                return;
            }
        }
    }
}