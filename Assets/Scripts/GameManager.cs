using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Scenes { Logo, Menu, Game, GameOver, Pause }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Scenes currentScene = Scenes.Logo;

    [SerializeField] GameObject transition;
    [SerializeField] GameObject playerObject;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && (currentScene == Scenes.Game || currentScene == Scenes.Pause))
        {
            PauseScene();
        }
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
            ShowCanvasTransition();
            StartCoroutine(ChangeTransitionScene.Instance.LoadSceneRutiner("MenuScene"));
        }
        else if (newScene == Scenes.Game)
        {
            ShowCanvasTransition();
            StartCoroutine(ChangeTransitionScene.Instance.LoadSceneRutiner("GameScene"));
        }
        else if (newScene == Scenes.Pause)
        {
            PauseMenuController pauseMenu = FindObjectOfType<PauseMenuController>();
            pauseMenu.PauseMenu();

            if (currentScene == Scenes.Pause)
            {
                newScene = Scenes.Game;
            }
        }
        else if (newScene == Scenes.GameOver)
        {
            ShowCanvasTransition();
            StartCoroutine(ChangeTransitionScene.Instance.LoadSceneRutiner("GameOverScene", 1.35f));
        }

        this.currentScene = newScene;
        HideCanvasTransition();
    }

    void ShowCanvasTransition()
    {
        ChangeTransitionScene transition = FindObjectOfType<ChangeTransitionScene>();

        if (transition != null)
        {
            Canvas canvas = transition.GetComponentInChildren<Canvas>();
            canvas.enabled = true;
        }
    }

    void HideCanvasTransition()
    {
        StartCoroutine(HideCanvasTransitionRutiner());
    }

    IEnumerator HideCanvasTransitionRutiner()
    {
        yield return new WaitForSeconds(3);

        ChangeTransitionScene transition = FindObjectOfType<ChangeTransitionScene>();

        if (transition != null)
        {
            Canvas canvas = transition.GetComponentInChildren<Canvas>();
            canvas.enabled = false;
        }
    }
}
