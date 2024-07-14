using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMusic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string onAudioText;
    [SerializeField] private string offAudioText;

    private Button button;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = ServiceLocator.GetService<AudioManager>();

        button = GetComponent<Button>();
        button.onClick.AddListener(OnBtnClick);

        
        if (audioManager.pause)
        {
            print("true music");
            if (text) text.SetText(offAudioText);
            return;
        }
        if (text) text.SetText(onAudioText);
    }

    private void OnBtnClick()
    {
        if (audioManager.pause)
        {
            audioManager.MusicSource.UnPause();
            audioManager.pause = false;
            if (text) text.SetText(onAudioText);
            return;
        }
        audioManager.MusicSource.Pause();
        audioManager.pause = true;
        if (text) text.SetText(offAudioText);
    }
}
