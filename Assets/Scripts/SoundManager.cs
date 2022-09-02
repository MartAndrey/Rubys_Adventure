using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public Image imageSound;
    [SerializeField] Sprite muteEnable;
    [SerializeField] Sprite muteDiseable;

    [SerializeField] Image pauseImageEnable;
    [SerializeField] Image pauseImageDiseable;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void MuteAudio()
    {
        imageSound = GameObject.FindWithTag("Sound").GetComponent<Image>();

        if (GameManager.Instance.currentScene == Scenes.Pause)
        {
            pauseImageEnable = GameObject.FindWithTag("Pause Sound E").GetComponent<Image>();
            pauseImageDiseable = GameObject.FindWithTag("Pause Sound D").GetComponent<Image>();

            if (AudioListener.volume == 1)
            {
                pauseImageEnable.enabled = false;
                pauseImageDiseable.enabled = true;
                AudioListener.volume = 0;
            }
            else if (AudioListener.volume == 0)
            {
                pauseImageEnable.enabled = true;
                pauseImageDiseable.enabled = false;
                AudioListener.volume = 1;
            }
        }
        else
        {
            if (AudioListener.volume == 1)
            {
                imageSound.sprite = muteDiseable;
                AudioListener.volume = 0;
            }
            else if (AudioListener.volume == 0)
            {
                imageSound.sprite = muteEnable;
                AudioListener.volume = 1;
            }
        }
    }
}
