using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] Canvas pauseMenu;

    void Start()
    {
        pauseMenu.enabled = false;
    }

    public void Return()
    {
        StartCoroutine(ChangeTransitionScene.Instance.LoadSceneRutiner("GameScene"));
        PauseMenu();
        //TODO
    }

    public void PauseMenu()
    {
        if (!pauseMenu.enabled)
        {
            pauseMenu.enabled = true;
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.enabled = false;
             Time.timeScale = 1;
        }
    }
}
