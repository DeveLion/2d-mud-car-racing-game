using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{

    public Image audioBtn;
    public Sprite audioON, audioOFF;


    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
        GetAudio();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void GetAudio()
    {

        if (PlayerPrefs.GetInt("Audio") != 0)
        {
            //audio off
            isActive = false;
            ManageAudio(false);
        }
        else
        {
            //audio on
            isActive = true;
            ManageAudio(true);
        }
    }


    void ManageAudio(bool active)
    {
        if (active)
        {
            PlayerPrefs.SetInt("Audio", 0);
            AudioListener.volume = 1;
        }
        else
        {
            PlayerPrefs.SetInt("Audio", 1);
            AudioListener.volume = 0;
        }
    }


    public void AudioSwitcBtn()
    {
        if (isActive)
        {
            isActive = false;
            ManageAudio(false);
            audioBtn.sprite = audioOFF;
        }
        else
        {
            isActive = true;
            ManageAudio(true);
            audioBtn.sprite = audioON;
        }
    }
}
