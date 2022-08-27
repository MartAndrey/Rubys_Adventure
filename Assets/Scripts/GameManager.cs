using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes { Logo, Menu, Game, GameOver, Pause }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Scenes currentScene = Scenes.Logo;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

       DontDestroyOnLoad(this.gameObject);
    }

    public void LogoScene()
    {
        SetSceneState(Scenes.Logo);
    }

    public void MenuScene()
    {
        SetSceneState(Scenes.Menu);
    }

    public void GameScene()
    {
        SetSceneState(Scenes.Game);
    }

    public void PauseScene()
    {
        SetSceneState(Scenes.Pause);
    }

    public void GameOverScene()
    {
        SetSceneState(Scenes.GameOver);
    }

    void SetSceneState(Scenes newScene)
    {
        if (newScene == Scenes.Logo)
        {
            StartCoroutine(ChangeTransitionScene.Instance.LoadSceneRutiner("MenuScene"));
            newScene = Scenes.Menu;
        }
        else if (newScene == Scenes.Menu)
        {
            
        }
        else if (newScene == Scenes.Game)
        {

        }
        else if (newScene == Scenes.Pause)
        {

        }
        else if (newScene == Scenes.GameOver)
        {

        }

        this.currentScene = newScene;
    }
}
