using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{

    public enum Phase { Start, Game, End };
    public Phase CurrentPhase;

    private static GameState _instance;

    public static GameState Instance { get { return _instance; } }

    private bool PlayingMusic = false;
    private bool PlayingCorrectMusic = false;

    private int HandedOutBeers = 0;
    private int GuestCount;


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

        CurrentPhase = Phase.Start;
    }

    private void Update()
    {
        if(CurrentPhase == Phase.Game)
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
        return (CurrentPhase == Phase.End);
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
            
        }
    }
}