using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public RawImage crosshair;
    public GameObject pre_interact_text;
    public GameObject during_interact_text;

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
}
