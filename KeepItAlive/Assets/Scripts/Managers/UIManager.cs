using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

    public RawImage crosshair;
    public GameObject pre_interact_text;
    public GameObject during_interact_text;

    public GameObject subtitles_panel;
    public TextMeshProUGUI subtitle_text;
    private bool showing_subtitles;

    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

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

    public bool SubtitlesShowing()
    {
        return showing_subtitles;
    }

    private void ToggleCrosshair(bool flag)
    {
        crosshair.enabled = flag;
    }

    public void SetDefault()
    {
        crosshair.color = Color.white;
        pre_interact_text.SetActive(false);
        during_interact_text.SetActive(false);
    }

    public void SetObjectInRange()
    {
        crosshair.color = Color.green;
        pre_interact_text.SetActive(true);
        during_interact_text.SetActive(false);
    }

    public void SetObjectGrabbed()
    {
        ToggleCrosshair(false);
        pre_interact_text.SetActive(false);
        during_interact_text.SetActive(true);
    }

    public void SetObjectReleased()
    {
        ToggleCrosshair(true);
    }

    public void PresentSubtitles(string text, float time)
    {
        if(showing_subtitles == false)
        {
            StartCoroutine(ShowSubtitlesForTime(text, time));
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(ShowSubtitlesForTime(text, time));
        }
        
    }

    private IEnumerator ShowSubtitlesForTime(string text, float time)
    {
        showing_subtitles = true;
        subtitle_text.text = text;
        subtitles_panel.SetActive(true);
        yield return new WaitForSeconds(time);
        subtitles_panel.SetActive(false);
        subtitle_text.text = "";
        showing_subtitles = false;
    }
}
