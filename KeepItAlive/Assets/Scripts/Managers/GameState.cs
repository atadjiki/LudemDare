using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private static GameState _instance;

    public static GameState Instance { get { return _instance; } }

    private bool PlayingMusic = false;
    private bool PlayingCorrectMusic = false;

    private int HandedOutBeers = 0;
    private int GuestCount;

    private bool GameComplete;

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
    }

    private void Update()
    {
        if(GameComplete == false)
        {
            CheckState();
        }
    }

    internal void Build()
    {
        GuestCount = FindObjectsOfType<CharacterInteraction>().Length;
    }

    public void MusicOn()
    {
        PlayingMusic = true;
    }

    public void MusicOff()
    {
        PlayingMusic = false;
    }

    public void PlayingCorrectSong()
    {
        PlayingCorrectMusic = true;
    }

    public void PlayingIncorrectSong()
    {
        PlayingCorrectMusic = false;
    }

    public void IncrementBeers()
    {
        HandedOutBeers++;
    }

    public bool IsGameComplete()
    {
        return GameComplete;
    }

    public bool IsMusicPlaying()
    {
        return PlayingMusic;
    }

    public bool IsSongCorrect()
    {
        return PlayingCorrectMusic;
    }

    public void CheckState()
    {
        if(PlayingCorrectMusic && PlayingMusic && HandedOutBeers >= GuestCount)
        {
            GameComplete = true;
        }
    }
}