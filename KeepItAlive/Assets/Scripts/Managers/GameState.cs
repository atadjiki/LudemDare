using System;
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

    private HashSet<Constants.Dialogue.Character> Characters;


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

        Characters = new HashSet<Constants.Dialogue.Character>();
    }

    IEnumerator StartBlackThenFade()
    {
        
        yield return new WaitForSeconds(3);
        UIManager.Instance.FadeCamera(false, 3);
    }

    public Constants.Dialogue.Character RegisterCharacter()
    {
        Array values = Enum.GetValues(typeof(Constants.Dialogue.Character));

        if (Characters.Count >= values.Length)
        {
            return 0;
        }

        Constants.Dialogue.Character character;

        while (true)
        {
            character = (Constants.Dialogue.Character)UnityEngine.Random.Range(0, values.Length);

            if (Characters.Contains(character) == false)
            {
                break;
            }
        }

        Characters.Add(character);

        return character;
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
        UIManager.Instance.PresentSubtitles("music on! vibe increased!", 2);
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
        GuestCount = FindObjectsOfType<CharacterInteraction>().Length;

        if(HandedOutBeers == GuestCount)
        {
            UIManager.Instance.PresentSubtitles("everybody has a beer! good job!", 2);
        }
        else
        {
            UIManager.Instance.PresentSubtitles("beers handed out: " + HandedOutBeers + "/" + GuestCount, 2);
        }
        
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
        if(CurrentPhase == Phase.Game)
        {
            GuestCount = FindObjectsOfType<CharacterInteraction>().Length;

            if (PlayingCorrectMusic && PlayingMusic && HandedOutBeers >= GuestCount)
            {
                UIManager.Instance.PresentSubtitles("vibe achieved! you're a party animal!", 5);
            }

            CurrentPhase = Phase.End;
        }
        
    }
}