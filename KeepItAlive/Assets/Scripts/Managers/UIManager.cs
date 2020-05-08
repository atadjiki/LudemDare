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

    public GameObject ControlsPanel;

    public Image FadePanel;

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

        FadePanel.gameObject.SetActive(true);
        FadeCamera(false, 5);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void OnGUI()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

    public void FadeCamera(bool on, float duration)
    {

        StopAllCoroutines();

        if(on)
        {
            StartCoroutine(FadeCameraTo(255, duration));
        }
        else
        {
            StartCoroutine(FadeCameraTo(0, duration));
        }
    }

    IEnumerator FadeCameraTo(float targetAlpha, float duration)
    {
        Color initialColor = FadePanel.color;

        float currentTime = 0;

        while(currentTime < duration)
        {
            FadePanel.color = Color.Lerp(initialColor, new Color(initialColor.r, initialColor.g, initialColor.b, targetAlpha), currentTime / duration);
            currentTime += Time.smoothDeltaTime;

            yield return null;
        }    
    }

    public void ToggleControlsPanel()
    {
        if(ControlsPanel.activeSelf)
        {
            ControlsPanel.SetActive(false);
        }
        else
        {
            ControlsPanel.SetActive(true);
        }
    }
}
