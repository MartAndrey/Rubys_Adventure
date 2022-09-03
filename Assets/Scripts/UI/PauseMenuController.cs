using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public static PauseMenuController Instance;

    // ====================ANIMATOR=====================
    Animator animator;
    Animator transitionScene;

    // ====================CANVAS=====================
    [SerializeField] Canvas pauseMenu;

    // ====================IMAGE=====================
    [SerializeField] Image pauseImageEnable;
    [SerializeField] Image pauseImageDiseable;

    // ====================SOUND=====================
    [SerializeField] AudioSource audioSource;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        transitionScene = GameObject.Find("Transition").GetComponentInChildren<Animator>();
        pauseMenu.enabled = false;
        animator = GetComponent<Animator>();

        UpdateSound();
    }

    public void Continue()
    {
        GameManager.Instance.PauseScene();
    }

    public void MainMenu()
    {
        transitionScene.Play("FadeOut");
        Time.timeScale = 1;
        GameManager.Instance.MenuScene();
    }

    public void Return()
    {
        transitionScene.Play("FadeOut");
        Time.timeScale = 1;
        GameManager.Instance.GameScene();
    }

    // Transition when you press the ESC key
    public void PauseMenu()
    {
        if (!pauseMenu.enabled)
        {
            animator.Play("FadeUp");
            pauseMenu.enabled = true;
            Time.timeScale = 0;
            audioSource.volume = 0.2f;
            animator.enabled = true;
        }
        else
        {
            animator.SetTrigger("Transition");
            Time.timeScale = 1;
            StartCoroutine(DisablePauseMenuRoutiner());
        }
    }

    IEnumerator DisablePauseMenuRoutiner()
    {
        yield return new WaitForSeconds(1);
        pauseMenu.enabled = false;
        audioSource.volume = 0.5f;
        animator.enabled = false;
    }

    void UpdateSound()
    {
        if (AudioListener.volume == 1)
        {
            pauseImageEnable.enabled = true;
        }
        else if (AudioListener.volume == 0)
        {
            pauseImageDiseable.enabled = true;
        }
    }
}
