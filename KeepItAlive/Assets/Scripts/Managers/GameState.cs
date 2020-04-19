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
        UIManager.Instance.PresentSubtitles("Music On! Vibe Increased!", 2);
        PlayingMusic = true;
    }

    public void MusicOff()
    {
        UIManager.Instance.PresentSubtitles("Vibe Down!", 2);
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
        GuestCount = FindObjectsOfType<CharacterInteraction>().Length;
        UIManager.Instance.PresentSubtitles("Beers Handed Out: " + HandedOutBeers + "/" + GuestCount, 2);
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
        GuestCount = FindObjectsOfType<CharacterInteraction>().Length;

        if (PlayingCorrectMusic && PlayingMusic && HandedOutBeers >= GuestCount)
        {
            UIManager.Instance.PresentSubtitles("Vibe Achieved! You're a Party Animal!", 10);
        }
    }
}