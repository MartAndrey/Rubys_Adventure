using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Image imageSound;

    [SerializeField] Sprite muteEnable;
    [SerializeField] Sprite muteDiseable;
    
    public void Play()
    {
        GameManager.Instance.GameScene();
    }

    public void Exit()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }

    public void MuteAudio()
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
