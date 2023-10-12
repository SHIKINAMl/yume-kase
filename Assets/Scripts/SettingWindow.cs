using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingWindow : MonoBehaviour
{
    private SaveManager saveManager;
    private HomeManager homeManager;

    private TextWindow textWindow;
    private YumeNikki yumeNikki;

    private Image window;

    private bool isActive = false;

    private Button closeButton;
    private Slider mainVolumeSlider;
    private Slider SEVolumeSlider;
    private Slider textSpeedSlider;

    private List<float> currentValues;

    public bool isHome = false;

    private void Start()
    {
        if (isHome)
        {
            homeManager = GameObject.Find("HomeManager").GetComponent<HomeManager>();
        }

        else
        {
            textWindow = GameObject.Find("TextWindow").GetComponent<TextWindow>();
            yumeNikki = GameObject.Find("YumeNikki").GetComponent<YumeNikki>();
        }

        saveManager = GameObject.Find("SaveManager").GetComponent<SaveManager>();

        window = this.GetComponent<Image>();
        window.enabled = false;

        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }

        isActive = false;

        closeButton = this.transform.Find("CloseButton").GetComponent<Button>();
        mainVolumeSlider = this.transform.Find("MainVolumeSlider").GetComponent<Slider>();
        SEVolumeSlider = this.transform.Find("SEVolumeSlider").GetComponent<Slider>();
        textSpeedSlider = this.transform.Find("TextSpeedSlider").GetComponent<Slider>();

        currentValues = saveManager.LoadSave().settingData;

        mainVolumeSlider.value = currentValues[0];
        SEVolumeSlider.value = currentValues[1];
        textSpeedSlider.value = currentValues[2];
    }

    private void Update()
    {
        if (isActive)
        {
            if (currentValues[0] != mainVolumeSlider.value)
            {
                currentValues[0] = mainVolumeSlider.value;
                AudioListener.volume = currentValues[0];
            }
                
            if (currentValues[1] != SEVolumeSlider.value)
            { 
                currentValues[1] = SEVolumeSlider.value;
            
                foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("Item"))
                {
                    AudioSource audioSource = gameObject.GetComponent<AudioSource>();
                    audioSource.volume = currentValues[1];
                }
            }               
        
            if (currentValues[2] != textSpeedSlider.value)
            {
                currentValues[2] = textSpeedSlider.value;

                if (!isHome)
                {
                    textWindow.textSpeed = currentValues[2];
                    yumeNikki.textSpeed = currentValues[2];
                }
            }
        }
    }

    public void OnClickOpen()
    {
        window.enabled = true;

        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(true);
        }

        isActive = true;
    }

    public void OnClickClose()
    {
        window.enabled = false;

        foreach (Transform child in this.transform)
        {
            child.gameObject.SetActive(false);
        }

        isActive = false;
        
        saveManager.OnSaveSetting(currentValues);
    }
}