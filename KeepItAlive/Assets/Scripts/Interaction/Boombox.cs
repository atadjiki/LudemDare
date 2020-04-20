using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MusicTrack
{
    public enum Track { Cassette, Cat, Lamp, Beer };
    public Track track;
    public AudioClip clip;
}

[RequireComponent(typeof(AudioSource))]
public class Boombox : Interactable
{
    private bool Enabled = false;

    public List<MusicTrack> Music;

    private int CurrentIndex;

    private Vector3 Deck_Pos = new Vector3();
    private Vector3 Deck_Scale = new Vector3();
    private Quaternion Deck_Rot = new Quaternion();

    public GameObject cassettePrefab;

    public BumpinStereoSpeakers left_speaker;
    public BumpinStereoSpeakers right_speaker;

    public override void Interact()
    {
        base.Interact();

        if (Enabled)
        {

            if(GetComponent<AudioSource>().mute)
            {
                GetComponent<AudioSource>().mute = false;
                GameState.Instance.MusicOn();
                left_speaker.On();
                right_speaker.On();
                
            }
            else
            {
                GetComponent<AudioSource>().mute = true;
                GameState.Instance.MusicOff();
                left_speaker.Off();
                right_speaker.Off();
            }
        }

        AudioManager.Instance.audioSource.PlayOneShot(AudioManager.Instance.button);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Cassette>() != null)
        {
            PlayMusic(MusicTrack.Track.Cassette);
            GameState.Instance.PlayingCorrectSong();
            PlayerController.Instance.Release();
            Enabled = true;
            TakeObject(other.gameObject);

            Instantiate<GameObject>(cassettePrefab);

        }
        else if(other.gameObject.GetComponent<Beer>() != null)
        {
            PlayMusic(MusicTrack.Track.Beer);
            GameState.Instance.PlayingIncorrectSong();
            PlayerController.Instance.Release();
            TakeObject(other.gameObject);
        }
    }

    public void PlayMusic(MusicTrack.Track Track)
    {
        StartCoroutine(DelayThenMusic(Track));
    }

    IEnumerator DelayThenMusic(MusicTrack.Track Track)
    {
        AudioManager.Instance.audioSource.PlayOneShot(AudioManager.Instance.cassetteDeck);

        yield return new WaitForSeconds(AudioManager.Instance.cassetteDeck.length);

        GameState.Instance.MusicOn();
        left_speaker.On();
        right_speaker.On();

        foreach (MusicTrack track in Music)
        {
            if (track.track == Track)
            {
                this.GetComponent<AudioSource>().clip = track.clip;
                this.GetComponent<AudioSource>().Play();
                this.GetComponent<AudioSource>().loop = true;
                break;
            }
        }

        foreach (CharacterInteraction c in FindObjectsOfType<CharacterInteraction>())
        {
            c.CurrentCategory = Constants.Dialogue.Category.Music;
        }
    }

    public void TakeObject(GameObject obj)
    {
        obj.GetComponent<Interactable>().enabled = false;
        obj.GetComponent<Rigidbody>().isKinematic = true;
        obj.GetComponent<Collider>().enabled = false;
        obj.transform.parent = this.transform;

        StartCoroutine(LerpObjectToDeck(obj, 2));
    }

    IEnumerator LerpObjectToDeck(GameObject obj, float time)
    {
        float currentTime = 0;

        Vector3 initial_pos = obj.transform.localPosition;
        Vector3 initial_scale = obj.transform.localScale;
        Quaternion initial_rot = obj.transform.localRotation;

        while (currentTime < time)
        {
            obj.transform.localPosition = Vector3.Slerp(initial_pos, Deck_Pos, currentTime / time);
            obj.transform.localScale = Vector3.Slerp(initial_scale, Deck_Scale, currentTime / time);
            obj.transform.localRotation = Quaternion.Slerp(initial_rot, Deck_Rot, currentTime / time);

            currentTime += Time.smoothDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        GameObject.Destroy(obj);

    }
}
