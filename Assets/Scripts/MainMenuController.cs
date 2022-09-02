using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] Image imageSound;
    Sprite sImageSound;

    void Start()
    {
        UpdateSound();

        sImageSound = imageSound.GetComponent<Sprite>();
    }

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

     void UpdateSound()
    {
        if (AudioListener.volume == 1)
        {
            sImageSound = SoundManager.Instance.muteEnable;
            
        }
        else if (AudioListener.volume == 0)
        {
            sImageSound = SoundManager.Instance.muteDiseable;
        }

        imageSound.sprite = sImageSound;
    }
}
