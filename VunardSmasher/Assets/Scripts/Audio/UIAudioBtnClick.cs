using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIAudioBtnClick : MonoBehaviour
{
    [SerializeField] private AudioClip clip;

    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(() =>
        {
            if (clip) ServiceLocator.GetService<AudioManager>().PlaySound(clip);
        });
    }
}
